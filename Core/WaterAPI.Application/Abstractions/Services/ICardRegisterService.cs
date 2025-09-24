using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.CardRegister;
using WaterAPI.Application.DTOs.User;
using WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister;

namespace WaterAPI.Application.Abstractions.Services
{
    public interface ICardRegisterService
    {
        Task<CreateCardRegisterResponseDTO> CreateAsync(CreateCardRegisterRequestDTO model);




        //Task<CreateUserResponseDTO> CreateAsync(CreateUserRequestDTO model);
    }
}
