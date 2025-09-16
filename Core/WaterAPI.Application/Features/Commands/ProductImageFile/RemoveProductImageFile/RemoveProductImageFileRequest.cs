using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileRequest : IRequest<RemoveProductImageFileResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
