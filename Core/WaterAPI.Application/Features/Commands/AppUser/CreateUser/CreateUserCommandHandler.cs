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

namespace WaterAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            //CreateUserCommandRequest nesnesi olarak gelen verileri CreateUserDTO nesnesine dönüştürüyoruz.
            //Persistance katmanındaki userService deki metodu çağıdık parametre olarak bu metot CreateUserDTO alıyor.
            //Bu yüzden CreateUserCommandRequest i CreateUserDTO ya çeviriyoruz.Bize döndürdüğü nesne CreateUserResponseDTO 
            //bu yüzden CreateUserResponseDTO yu CreateUserResponseCommandResponse a çeviriyoruz. ve bunu response olarak döndürüyoruz.
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                UserName = request.UserName,
                NameSurname=request.NameSurname,
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,

            }
                );
            return new()
            {
                Massage=response.Massage,
                Succeeded=response.Succeeded,
            };
                
            //throw new UserCreateFailedException();

        }
    }
}
