using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.SalesDetail.Commands;

public class CreateSaleDetailCommand : IRequest
{
    public int Sale_Id { get; set; }
}

public class CreateSaleDetailCommandHandler : IRequestHandler<CreateSaleDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateSaleDetailCommand request, CancellationToken cancellationToken)
    {
        var newSale = _mapper.Map<SaleDetail>(request);

        _unitOfWork.SaleDetail.Add(newSale);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreateSaleDetailCommandMapper : Profile
{
    public CreateSaleDetailCommandMapper()
    {
        CreateMap<CreateSaleDetailCommand, SaleDetail>();
    }
}

public class CreateSaleDetailValidator : AbstractValidator<CreateSaleDetailCommand>
{
    public CreateSaleDetailValidator()
    {
        RuleFor(r => r.Sale_Id).NotNull();
    }
}
