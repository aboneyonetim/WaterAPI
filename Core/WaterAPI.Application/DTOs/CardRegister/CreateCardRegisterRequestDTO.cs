using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Domain.Entities.Identity;
using WaterAPI.Domain.Entities;

namespace WaterAPI.Application.DTOs.CardRegister
{
    public class CreateCardRegisterRequestDTO
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }


       // public virtual AppUser AppUser { get; set; }
       // public virtual ICollection<CardPayload> CardPayloads { get; set; }
    }
}
