﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application.Repositories;

namespace WaterAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileHandler : IRequestHandler<RemoveProductImageFileRequest, RemoveProductImageFileResponse>
    {
            readonly IProductReadRepository _productReadRepository;
            readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageFileHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductImageFileResponse> Handle(RemoveProductImageFileRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.productImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.ProductImageFile? productImageFile = product?.productImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if (productImageFile != null) 
            product?.productImageFiles.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
