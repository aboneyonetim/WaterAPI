using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Abstractions.Services.Authentication
{
   public interface IExternalAuthentication
    {
        Task<DTOs.TokenDTO> GoogleLoginAsync(string idToken, int accesTokenLifeTime);
    }
}
