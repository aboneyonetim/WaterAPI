using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs;

namespace WaterAPI.Application.Features.Commands.AppUser.GoogleLogin
{
   public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
