using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.SalesDetail.Commands
{
    public class CancelSaleDetailCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class CancelSaleDetailCommandHandler : IRequestHandler<CancelSaleDetailCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSaleDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CancelSaleDetailCommand request, CancellationToken cancellationToken)
        {
            var saleDetailId = request.Id.FromHashId();
            var saleDetail = await _unitOfWork.SaleDetail.GetAsync(P => P.Id == saleDetailId);

            if (saleDetail is null)
            {
                throw new NotFoundException();
            }

            saleDetail.IsCanceled = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class CancelSaleDetailValidator : AbstractValidator<CancelSaleDetailCommand>
    {
        public CancelSaleDetailValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}