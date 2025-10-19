using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs.Payment.Buyer
{
    public class AddressDTO
    {
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
    }
}
