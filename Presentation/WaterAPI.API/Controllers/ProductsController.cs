using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using WaterAPI.Application.Abstractions.Storage;
using WaterAPI.Application.Features.Commands.Product.CreateProduct;
using WaterAPI.Application.Features.Commands.Product.RemoveProduct;
using WaterAPI.Application.Features.Commands.Product.UpdateProduct;
using WaterAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using WaterAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
using WaterAPI.Application.Features.Queries.Product.GetAllProduct;
using WaterAPI.Application.Features.Queries.Product.GetByIdProduct;
using WaterAPI.Application.Features.Queries.ProductImageFile.GetProductImage;
using WaterAPI.Application.Repositories;
using WaterAPI.Application.RequestParameters;
using WaterAPI.Application.ViewModels.Products;
using WaterAPI.Domain.Entities;


namespace WaterAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class ProductsController : ControllerBase
    {
        

        readonly IStorageService _storageService;
        readonly IMediator _mediator;//Aracı(mediator) nesnesini getirecek olan Interface

        public ProductsController(IStorageService storageService,IMediator mediator)
        {
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
        public async Task<IActionResult> Upload([FromForm] UploadProductImageFileCommandRequest uploadProductImageFileCommandRequest)
        {
            uploadProductImageFileCommandRequest.Files = Request.Form.Files;
            UploadProductImageFileCommandResponse response = await _mediator.Send(uploadProductImageFileCommandRequest);
            return Ok();
            
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImageQueryRequest getProductImageQueryRequest )
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(getProductImageQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveImages([FromRoute] RemoveProductImageFileRequest removeProductImageFileRequest, [FromQuery]string imageId)
        {
            removeProductImageFileRequest.ImageId = imageId;
            RemoveProductImageFileResponse response = await _mediator.Send(removeProductImageFileRequest);
            return Ok();
        }




           
        }
}
