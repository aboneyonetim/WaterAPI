using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Services.Authentication;

namespace WaterAPI.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication,IInternalAuthentication
    {
       
       
       
       
    }
}
