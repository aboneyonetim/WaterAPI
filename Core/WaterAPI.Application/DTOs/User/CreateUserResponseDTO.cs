using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.User
{
    public class CreateUserResponseDTO
    {
        public bool Succeeded { get; set; }
        public string Massage { get; set; }
    }
}
