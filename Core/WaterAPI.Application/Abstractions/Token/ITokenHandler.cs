using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Application.Abstractions.Token
{
   public interface ITokenHandler
    {
          DTOs.TokenDTO CreateAccessToken(int second, AppUser appUser);
          string CreateRefreshToken();
    }
}
