using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.SalesDetail.Commands;

public class UpdateSaleDetailCommand : IRequest
{
    public string Id { get; set; } = default!;
    public int Sale_Id { get; set; }
}

public class UpdateSaleDetailCommandHandler : IRequestHandler<UpdateSaleDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSaleDetailCommand request, CancellationToken cancellationToken)
    {
        var saleDetailId = request.Id.FromHashId();
        var saleDetail = await _unitOfWork.SaleDetail.GetAsync(s => s.Id==saleDetailId && s.IsCanceled == false);

        if (saleDetail is null)
        {
            throw new NotFoundException();
        }

        saleDetail.Sale_Id = request.Sale_Id;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdateSaleDetailValidator : AbstractValidator<UpdateSaleDetailCommand>
{
    public UpdateSaleDetailValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Sale_Id).NotNull().GreaterThan(0);
    }
}
