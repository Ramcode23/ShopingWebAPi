using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Commands;

public class UpdatePurchaseCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Provider_Id { get; set; }
}

public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchaseId = request.Id.FromHashId();
        var purchase = await _unitOfWork.Purchase.GetAsync(p => p.Id == purchaseId && p.IsCanceled == false);
        // var purchase = await _unitOfWork.Purchase.GetAsync(p => p.Id == purchaseId);

        if (purchase is null)
        {
            throw new NotFoundException();
        }

        purchase.Number = request.Number;
        purchase.Date = request.Date;
        purchase.Provider_Id = request.Provider_Id;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdatePurchaseValidator : AbstractValidator<UpdatePurchaseCommand>
{
    public UpdatePurchaseValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Provider_Id).NotNull().GreaterThan(0);
    }
}