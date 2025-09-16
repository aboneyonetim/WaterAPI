using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Exceptions;

namespace WaterAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        //IUsermanager servisi Identity mekanizmasında kullanıcı işlemlerini kendi yapan bir servistir o yüzden repository eklemeye gerek yoktur.
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
          IdentityResult result = await  _userManager.CreateAsync(new(){
            NameSurname=request.NameSurname,    
            UserName=request.UserName,
            Email=request.Email,

            },request.Password);
            if (result.Succeeded)
                return new()
                {
                    Succeeded = true,
                    Massage = "Kullanıcı başarıyla oluşturulmuştur."

                };
           
            throw new UserCreateFailedException();

        }
    }
}
