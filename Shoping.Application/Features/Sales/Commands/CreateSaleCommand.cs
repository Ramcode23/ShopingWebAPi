using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;
using Shoping.Application.DTOs;

namespace Shoping.Application.Features.Sales.Commands;

public class CreateSaleCommand : IRequest
{
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public int Client_Id { get; set; } = default!;
    public List<SaleDetailCreateDTO> SaleDetails { get; set; } = new List<SaleDetailCreateDTO>();
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var newSale = _mapper.Map<Sale>(request);
        newSale.Number = Guid.NewGuid().ToString();

        _unitOfWork.Sale.Add(newSale);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreateSaleCommandMapper : Profile
{
    public CreateSaleCommandMapper()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<PurchaseDetailCreateDTO, PurchaseDetail>();
    }
}

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Client_Id).NotNull();
        RuleFor(r => r.SaleDetails.Count).NotEqual(0);
    }
}
