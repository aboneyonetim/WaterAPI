using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Token;
using WaterAPI.Application.DTOs;
using WaterAPI.Application.Exceptions;

namespace WaterAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;    //Handler da bu servisleri çağırdık kullanıcıyla ilgili temel işlemler
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;// Kullanıcının giriş işlemlerinden sorumlu servis
        readonly ITokenHandler _tokenHandler; //Token oluşturma amacıyla çağırılan yer tutucu

        public LoginUserCommandHandler(
            UserManager<Domain.Entities.Identity.AppUser> userManager,
            SignInManager<Domain.Entities.Identity.AppUser> signInManager, 
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }


        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);//aldığı userNameOrEmail e göre appUser ı getiriyor
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

           SignInResult result = await _signInManager.CheckPasswordSignInAsync(user ,request.Password,false); //appuser türünde bir nesne ve string olark bir şifre alıp
            if (result.Succeeded)                           //Authentication başarılı!                    nesnenin aldığı şifre ile doğrulanıp doğrulanmadığını bool türünde döner.
            {
                //...Yetkileri belirlememiz gerekiyor.
               Token token= _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            //return new LoginUserErrorCommandResponse() 
            //{
            //Massage ="Kullanıcı adı veya şifre hatalıdır."
            //};
            throw new AuthenticationErrorException();
        }
    }
}
