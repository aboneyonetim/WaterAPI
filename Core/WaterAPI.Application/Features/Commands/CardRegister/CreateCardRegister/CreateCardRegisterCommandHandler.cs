using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services;
using WaterAPI.Application.DTOs.CardRegister;
using WaterAPI.Application.DTOs.User;
using WaterAPI.Application.Repositories;

namespace WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister
{
    public class CreateCardRegisterCommandHandler : IRequestHandler<CreateCardRegisterCommandRequest, CreateCardRegisterCommandResponse>
    {

        ICardRegisterService _cardRegisterService;

        public CreateCardRegisterCommandHandler(ICardRegisterService cardRegisterService)
        {
            _cardRegisterService = cardRegisterService;
        }

        public async Task<CreateCardRegisterCommandResponse> Handle(CreateCardRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            CreateCardRegisterResponseDTO response = await _cardRegisterService.CreateAsync(new()
            {
                AppUserId = request.AppUserId,
                Name = request.Name,
                Number = request.Number,

            }
                );
            return new()
            {
               Message=response.Message,
               Succeeded=response.Succeeded
               
            };



        }

        
    }
}
