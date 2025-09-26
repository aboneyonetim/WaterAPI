using AutoMapper;
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
        readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserRequestDTO requestDTO = _mapper.Map<CreateUserRequestDTO>(request);               //request i requestDTO ya çevirip
            CreateUserResponseDTO responseDTO = await _userService.CreateAsync(requestDTO);             //ilgili fonksiyon bize requestDTO su alıp responseDTO su döndürür.
            CreateUserCommandResponse response = _mapper.Map<CreateUserCommandResponse>(responseDTO);   //Karşıladığımız responseDTO yu da response a çevirip döndürüyoruz.

            return response;


            //CreateUserCommandRequest nesnesi olarak gelen verileri CreateUserDTO nesnesine dönüştürüyoruz.
            //Persistance katmanındaki userService deki metodu çağıdık parametre olarak bu metot CreateUserRequestDTO alıyor.
            //Bu yüzden CreateUserCommandRequest i CreateUserRequestDTO ya çeviriyoruz.Bize döndürdüğü nesne CreateUserResponseDTO 
            //bu yüzden CreateUserResponseDTO yu CreateUserResponseCommandResponse a çeviriyoruz. ve bunu response olarak döndürüyoruz.
            //CreateUserResponseDTO response = await _userService.CreateAsync(new()
            //{
            //    UserName = request.UserName,
            //    NameSurname=request.NameSurname,
            //    Email = request.Email,
            //    Password = request.Password,
            //    PasswordConfirm = request.PasswordConfirm,

            //}
            //    );
            //return new()
            //{
            //    Massage=response.Massage,
            //    Succeeded=response.Succeeded,
            //};
            

            //throw new UserCreateFailedException();

        }
    }
}
