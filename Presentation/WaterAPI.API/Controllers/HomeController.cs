using Microsoft.AspNetCore.Mvc;

namespace WaterAPI.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
