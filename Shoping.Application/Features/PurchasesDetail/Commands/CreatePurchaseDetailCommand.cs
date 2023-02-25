using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;
using Shoping.Application.DTOs;

namespace Shoping.Application.Features.PurchasesDetail.Commands;

public class CreatePurchaseDetailCommand : IRequest
{
    public int Purchase_Id { get; set; }
}

public class CreatePurchaseDetailCommandHandler : IRequestHandler<CreatePurchaseDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreatePurchaseDetailCommand request, CancellationToken cancellationToken)
    {
        var newPurchaseDetail = _mapper.Map<PurchaseDetail>(request);
        
        _unitOfWork.PurchaseDetail.Add(newPurchaseDetail);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreatePurchaseDetailCommandMapper : Profile
{
    public CreatePurchaseDetailCommandMapper()
    {
        CreateMap<CreatePurchaseDetailCommand, PurchaseDetail>();
    }
}

public class CreatePurchaseDetailValidator : AbstractValidator<CreatePurchaseDetailCommand>
{
    public CreatePurchaseDetailValidator()
    {
        RuleFor(r => r.Purchase_Id).NotNull();
    }
}
