using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Commands
{
    public class CancelPurchaseCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class CancelPurchaseCommandHandler : IRequestHandler<CancelPurchaseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CurrentUser _user;

        public CancelPurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _user = currentUserService.User;
        }

        public async Task<Unit> Handle(CancelPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchaseId = request.Id.FromHashId();
            var purchase = await _unitOfWork.Purchase.GetAsync(P => P.Id == purchaseId);

            if (purchase is null)
            {
                throw new NotFoundException();
            }

            purchase.IsCanceled = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class CancelPurchaseValidator : AbstractValidator<CancelPurchaseCommand>
    {
        public CancelPurchaseValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}
