using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.DTOs.User;
using WaterAPI.Domain.Entities.Identity;

namespace WaterAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken,AppUser user, DateTime accessTokenDate, int addOnAccesTokenDate);
    }
}
