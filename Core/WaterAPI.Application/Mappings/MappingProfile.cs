using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.Payment;
using WaterAPI.Application.DTOs.User;
using WaterAPI.Application.Features.Commands.AppUser.CreateUser;
using WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister;
using WaterAPI.Application.Features.Commands.Payment.CreatePayment;

namespace WaterAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CreateCardRegisterCommandRequest, CreateUserRequestDTO>();
            CreateMap<CreateUserCommandRequest, CreateUserRequestDTO>().ReverseMap();
            CreateMap<CreateUserResponseDTO, CreateUserCommandResponse>().ReverseMap();
            CreateMap<CreatePaymentCommandRequest, ProcessPaymentRequestDTO>();
            
        }

    }
}
