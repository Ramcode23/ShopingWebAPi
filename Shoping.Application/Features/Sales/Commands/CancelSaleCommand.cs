using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Sales.Commands
{
    public class CancelSaleCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var saleId = request.Id.FromHashId();
            var sale = await _unitOfWork.Sale.GetAsync(P => P.Id == saleId);

            if (sale is null)
            {
                throw new NotFoundException();
            }

            sale.IsCanceled = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}