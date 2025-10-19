using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.DTOs
{
   public class IyzipaySettings
    {
        public string ApiKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public string BaseUrl { get; set; } = ""; // sandbox: https://sandbox-api.iyzipay.com
    }
}
