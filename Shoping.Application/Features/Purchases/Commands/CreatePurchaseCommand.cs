using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Purchases.Commands;

public class CreatePurchaseCommand : IRequest
{
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Provider_Id { get; set; }
    public int Created_By { get; set; }
    public DateTime Created_At { get; set; }
    public int Modified_By { get; set; }
    public DateTime Modified_At { get; set; }
}

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var newPurchase = _mapper.Map<Purchase>(request);

        _unitOfWork.Purchase.Add(newPurchase);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreatePurchaseCommandMapper : Profile
{
    public CreatePurchaseCommandMapper() =>
        CreateMap<CreatePurchaseCommand, Purchase>();
}

public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseValidator()
    {
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Provider_Id).NotNull();
        RuleFor(r => r.Created_By).NotNull();
        RuleFor(r => r.Created_At).NotNull();
        RuleFor(r => r.Modified_By).NotNull();
        RuleFor(r => r.Modified_At).NotNull();
    }
}
