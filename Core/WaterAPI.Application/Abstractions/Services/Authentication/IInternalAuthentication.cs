using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<DTOs.TokenDTO> LoginAsync(string userNameOrEmail, string password,int accessTokenLifeTime);
        Task<DTOs.TokenDTO> RefreshTokenLoginAsync(string refreshToken);
    }
}
