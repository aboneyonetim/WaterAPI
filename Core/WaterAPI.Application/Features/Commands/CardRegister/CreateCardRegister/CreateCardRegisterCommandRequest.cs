using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.CardRegister.CreateCardRegister
{
    public class CreateCardRegisterCommandRequest : IRequest<CreateCardRegisterCommandResponse>
    {
    }
}
