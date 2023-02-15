using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Commands
{
    public class DeletePurchaseCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchaseId = request.Id.FromHashId();
            var purchase = await _unitOfWork.Purchase.GetAsync(P => P.Id == purchaseId);

            if (purchase is null)
            {
                throw new NotFoundException();
            }

            purchase.IsDeleted = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeletePurchaseValidator : AbstractValidator<DeletePurchaseCommand>
    {
        public DeletePurchaseValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}
