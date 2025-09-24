using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.Abstractions.Token;
using WaterAPI.Application.DTOs;
using WaterAPI.Application.Exceptions;
using WaterAPI.Application.Features.Commands.AppUser.LoginUser;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        //readonly HttpClient _httpClient; //facebook için 
        readonly UserManager<AppUser> _userManager; //Handler da bu servisleri çağırdık kullanıcıyla ilgili temel işlemler
        readonly SignInManager<AppUser> _signInManager; // Kullanıcının giriş işlemlerinden sorumlu servis
        readonly IConfiguration _configuration;
        readonly ITokenHandler _tokenHandler;           //Token oluşturma amacıyla çağırılan yer tutucu
        readonly IUserService _userService;


        public AuthService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IConfiguration configuration,
                           ITokenHandler tokenHandler,
                           IUserService userService)//,HttpClient httpClient)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _userService = userService;
            // _httpClient = httpClient;
        }

        async Task<TokenDTO> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accesTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;

                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);//AspNetUserLogins

                TokenDTO token = _tokenHandler.CreateAccessToken(accesTokenLifeTime,user);
                await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration,5);
                return token;

            }
            throw new Exception("Invalid external authentication");
        }

        public async Task<TokenDTO> GoogleLoginAsync(string idToken, int accesTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Google:Client-ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accesTokenLifeTime);
        }



        public async Task<TokenDTO> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);//aldığı userNameOrEmail e göre appUser ı getiriyor
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false); //appuser türünde bir nesne ve string olark bir şifre alıp
            if (result.Succeeded)                           //Authentication başarılı!                    nesnenin aldığı şifre ile doğrulanıp doğrulanmadığını bool türünde döner.
            {
                //...Yetkileri belirlememiz gerekiyor.
                TokenDTO token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 25);
                return token;
            }
            throw new AuthenticationErrorException();
            //return new LoginUserErrorCommandResponse() 
            //{
            //Massage ="Kullanıcı adı veya şifre hatalıdır."
            //};        }
        }

        public async Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user!=null && user?.RefreshTokenEndDate> DateTime.UtcNow )
            {
             TokenDTO token = _tokenHandler.CreateAccessToken(15,user);
                await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration,300);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}