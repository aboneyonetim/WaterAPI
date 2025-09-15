using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Storage;
using WaterAPI.Application.Features.Commands.Product.CreateProduct;
using WaterAPI.Application.Features.Commands.Product.RemoveProduct;
using WaterAPI.Application.Features.Commands.Product.UpdateProduct;
using WaterAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using WaterAPI.Application.Features.Queries.Product.GetAllProduct;
using WaterAPI.Application.Features.Queries.Product.GetByIdProduct;
using WaterAPI.Application.Repositories;
using WaterAPI.Application.RequestParameters;
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

        private readonly IWebHostEnvironment _webHostEnvironment;

        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private IOrderReadRepository _orderReadRepository;


        readonly IFileReadRepository _fileReadRepository;
        readonly IFileWriteRepository _fileWriteRepository;

        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        readonly IStorageService _storageService;

        readonly IMediator _mediator;//Aracı(mediator) nesnesini getirecek olan Interface
        public ProductsController(
            ICustomerReadRepository customerReadRepository,
            ICustomerWriteRepository customerWriteRepository,

            IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,

            IOrderReadRepository orderReadRepository,
            IOrderWriteRepository orderWriteRepository,

            IWebHostEnvironment webHostEnvironment,


            IFileReadRepository fileReadRepository,
            IFileWriteRepository fileWriteRepository,

            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,

            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IStorageService storageService, 
            IMediator mediator
            )
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;

            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;

            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;

            this._webHostEnvironment = webHostEnvironment;


            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;

            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;

            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;

            _storageService = storageService;

            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
           GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {

            //if (ModelState.IsValid){}
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest )
        {
            UpdateProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok(new
            {

                massage = "Silme işlemi başarılı! "
            });
        }

        






        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromBody]UploadProductImageFileCommandRequest uploadProductImageFileCommandRequest)
        {
            uploadProductImageFileCommandRequest.Files = Request.Form.Files;
               UploadProductImageFileCommandResponse response = await _mediator.Send(uploadProductImageFileCommandRequest);
            return Ok();
            
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetProductImages(string id)
        {
          Product? product= await  _productReadRepository.Table.Include(p => p.productImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.productImageFiles.Select(p => new 
            {
                p.Path,
                p.FileName

            }));
      

        }

       




        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload()
        //{
        //    var datas = await _storageService.UploadAsync("resources/files", Request.Form.Files);
        //    await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile() 
        //    {
        //    FileName=d.fileName,
        //    Path =d.pathOrContainerName,
        //    Storage = _storageService.StorageName,
        //    }).ToList());

        //    await _productImageFileWriteRepository.SaveAsync();
        //    return Ok();
        //}

        //[HttpPost("[action]")]
        //    public async Task<IActionResult> Upload()
        //{
        //   //var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
        //    //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
        //    //{
        //    //    FileName = d.fileName,
        //    //    Path = d.path,
        //    //}).ToList());
        //    //await _productImageFileWriteRepository.SaveAsync();
        //    //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
        //    //{
        //    //    FileName = d.fileName,
        //    //    Path = d.path,
        //    //    Price = new Random().Next()
        //    //}).ToList());
        //    //await _invoiceFileWriteRepository.SaveAsync();
        //    //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new WaterAPI.Domain.Entities.File()
        //    //{
        //    //    FileName = d.fileName,
        //    //    Path = d.path,
        //    //    IsActive = true,
        //    //    IsDeleted= false

        //    //}).ToList());
        //    //await _fileWriteRepository.SaveAsync();
        //    var d1=  _fileReadRepository.GetAll(false);
        //    var d2=  _invoiceFileReadRepository.GetAll(false);
        //    var d3=  _productImageFileReadRepository.GetAll(false);

        //    return Ok();
        //}


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
