using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.PurchasesDetail.Commands;

public class UpdatePurchaseDetailCommand : IRequest
{
    public string Id { get; set; } = default!;
    public int Purchase_Id { get; set; }
}

public class UpdatePurchaseDetailCommandHandler : IRequestHandler<UpdatePurchaseDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePurchaseDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdatePurchaseDetailCommand request, CancellationToken cancellationToken)
    {
        var purchaseDetailId = request.Id.FromHashId();
        var purchaseDetail = await _unitOfWork.PurchaseDetail.GetAsync(p => p.Id == purchaseDetailId && p.IsCanceled == false);

        if (purchaseDetail is null)
        {
            throw new NotFoundException();
        }

        purchaseDetail.Purchase_Id = request.Purchase_Id;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdatePurchaseDetailValidator : AbstractValidator<UpdatePurchaseDetailCommand>
{
    public UpdatePurchaseDetailValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Purchase_Id).NotNull().GreaterThan(0);
    }
}