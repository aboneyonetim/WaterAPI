using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.DTOs.User;
using WaterAPI.Application.Exceptions;
using WaterAPI.Application.Features.Commands.AppUser.CreateUser;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Persistence.Services
{
    public class UserService : IUserService
    {//IUsermanager servisi Identity mekanizmasında kullanıcı işlemlerini kendi yapan bir servistir o yüzden repository eklemeye gerek yoktur.
        UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserRequestDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.UserName,
                Email = model.Email,

            }, model.Password);

            CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };


            if (result.Succeeded)
                response.Massage = "Kullanıcı başarıyla oluşturulmuştur.";

            else
                foreach (var error in result.Errors)
                    response.Massage += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate,int addOnAccesTokenDate)
        {

            
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccesTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

    }
}
