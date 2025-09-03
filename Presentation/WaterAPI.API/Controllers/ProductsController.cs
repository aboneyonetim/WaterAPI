using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;


namespace WaterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {
          await  _productWriteRepository.AddRangeAsync(new()
            {
                new(){ Id=Guid.NewGuid(), Name="sarıkula", Price=40,CreatedDate=DateTime.UtcNow, Stock=123,},
                new(){ Id=Guid.NewGuid(), Name="meyvesuyu", Price=50,CreatedDate=DateTime.UtcNow, Stock=40,},
                new(){ Id=Guid.NewGuid(), Name="limonata", Price=10,CreatedDate=DateTime.UtcNow, Stock=6,},
            });
           await _productWriteRepository.SaveAsync();
        }
    }
}
