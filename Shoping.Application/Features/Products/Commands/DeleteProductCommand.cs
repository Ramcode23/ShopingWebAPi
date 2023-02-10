using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public int Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Category_Id { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var ProductId = request.Id.FromHashId();
            var Product = await _unitOfWork.Product.GetAsync(P => P.Id == ProductId);
            _unitOfWork.Product.Remove(Product);

            if (Product is null)
            {
                throw new NotFoundException();
            }

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}
