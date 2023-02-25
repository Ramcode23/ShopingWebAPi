using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.PurchasesDetail.Queries;

public class GetPurchaseDetailQuery : IRequest<GetPurchaseDetailQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetPurchaseDetailQueryHandler : IRequestHandler<GetPurchaseDetailQuery, GetPurchaseDetailQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchaseDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetPurchaseDetailQueryResponse> Handle(GetPurchaseDetailQuery request, CancellationToken cancellationToken)
    {
        var purchaseDetail = await _unitOfWork.PurchaseDetail.GetAsync(p => p.Id==request.Id.FromHashId());

        if (purchaseDetail is null)
        {
            throw new NotFoundException(nameof(purchaseDetail), request.Id);
        }

        return _mapper.Map<GetPurchaseDetailQueryResponse>(purchaseDetail);
    }
}

public class GetPurchaseDetailQueryResponse
{
    public string Id { get; set; } = default!;
    public int Purchase_Id { get; set; }
}

public class GetPurchaseDetailQueryProfile : Profile
{
    public GetPurchaseDetailQueryProfile() =>
        CreateMap<PurchaseDetail, GetPurchaseDetailQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}