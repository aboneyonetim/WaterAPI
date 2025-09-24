using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.CardRegister;
using WaterAPI.Application.Repositories;

namespace WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister
{
    public class CreateCardRegisterCommandHandler : IRequestHandler<CreateCardRegisterCommandRequest, CreateCardRegisterCommandResponse>
    {
        ICardRegisterWriteRepository _cardRegisterWriteRepository;

        public CreateCardRegisterCommandHandler(ICardRegisterWriteRepository cardRegisterWriteRepository)
        {
            _cardRegisterWriteRepository = cardRegisterWriteRepository;
        }

         async Task<CreateCardRegisterCommandResponse> Handle(CreateCardRegisterCommandRequest request, CancellationToken cancellationToken)
        {

            //CreateCardRegisterResponseDTO response = await _cardRegisterWriteRepository.

            
            throw new NotImplementedException();
            
        }

        Task<CreateCardRegisterCommandResponse> IRequestHandler<CreateCardRegisterCommandRequest, CreateCardRegisterCommandResponse>.Handle(CreateCardRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
