using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;

namespace WaterAPI.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IConfiguration _configuration;

        readonly IProductReadRepository _productReadRepository;
        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetProductImageQueryResponse>> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.productImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return (product.productImageFiles.Select(p => new GetProductImageQueryResponse
            {
                //Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
              Path= p.Path,
              FileName= p.FileName,
               Id= p.Id,
            })).ToList();
        }
    }
}
