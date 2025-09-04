using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;
using WaterAPI.Application.ViewModels.Products;
using WaterAPI.Domain.Entities;



namespace WaterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private IOrderReadRepository _orderReadRepository;

        public ProductsController(
            ICustomerReadRepository customerReadRepository,
            ICustomerWriteRepository customerWriteRepository,

            IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,

            IOrderReadRepository orderReadRepository,
            IOrderWriteRepository orderWriteRepository
            )
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;

            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;

            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(_orderReadRepository.GetAll(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            // _productWriteRepository.Update(product); //SaveAsync metodu zaten update işlemini yapıyor.Track edildiği için
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }




        //[HttpGet]
        //public async Task Get()
        //{
        //    // var customerId= Guid.NewGuid();
        //    // await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Muiddin" });

        //    //await _orderWriteRepository.AddAsync(new() {   Description = "Yeni Sipariş",Address = "İstanbul",CustomerId= customerId });
        //    //await _orderWriteRepository.AddAsync(new() {   Description = "Eski Sipariş",Address = "Ankara",  CustomerId = customerId });
        //    // await _orderWriteRepository.SaveAsync();
        //    //Order order = await _orderReadRepository.GetByIdAsync("0199147b-e487-7a3f-bed1-73d04b6d3143");
        //    //order.Address = "İzmir";
        //    //await _orderWriteRepository.SaveAsync();
        //}

        //[HttpGet]
        //public async Task Get()
        //{
        //    await _productWriteRepository.AddRangeAsync(new()
        //      {
        //          new(){ Id=Guid.NewGuid(), Name="sarıkula", Price=40,CreatedDate=DateTime.UtcNow, Stock=123,},
        //          new(){ Id=Guid.NewGuid(), Name="meyvesuyu", Price=50,CreatedDate=DateTime.UtcNow, Stock=40,},
        //          new(){ Id=Guid.NewGuid(), Name="limonata", Price=10,CreatedDate=DateTime.UtcNow, Stock=6,},
        //      });
        //    await _productWriteRepository.SaveAsync();

        //    Product p = await _productReadRepository.GetByIdAsync("205e3e1f-22e6-4cb0-ae0c-e54aa6bad667", false);
        //    p.Name = "Mehmet";// bool tracking değişkeni varsayılan olarak true dur ve false yapmamız durumunda tracking işlemi yapılmaz ve gelen veri veri tabanına yazılır.
        //    await _productWriteRepository.SaveAsync(); //_productReadRepository nesnesi oluşturmamıza rağmen bunu _productWriteRepository üzerinden veri tabanına yazdık.
        //                                               //Bunun böyle çalışmasının sebebi IoC Container da singleton yerine scopped kullanmanmamız.Böylece tüm isteklere aynı nesne gönderilir.
        //}
        //public async Task Get() 
        //{
        //    await _productWriteRepository.AddAsync(new() { Name = "Naneli Ayran", Price = 15.424F, Stock = 20,CreatedDate=DateTime.UtcNow });
        //    await _productWriteRepository.SaveAsync();
        //}
    }
}
