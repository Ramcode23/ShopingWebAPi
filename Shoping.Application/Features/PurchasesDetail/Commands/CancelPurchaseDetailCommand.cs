using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.PurchasesDetail.Commands
{
    public class CancelPurchaseDetailCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class CancelPurchaseDetailCommandHandler : IRequestHandler<CancelPurchaseDetailCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CancelPurchaseDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CancelPurchaseDetailCommand request, CancellationToken cancellationToken)
        {
            var purchaseId = request.Id.FromHashId();
            var purchase = await _unitOfWork.PurchaseDetail.GetAsync(P => P.Id == purchaseId);

            if (purchase is null)
            {
                throw new NotFoundException();
            }

            purchase.IsCanceled = true;
            
            // purchase.IsDeleted = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class CancelPurchaseDetailValidator : AbstractValidator<CancelPurchaseDetailCommand>
    {
        public CancelPurchaseDetailValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}
