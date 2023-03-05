using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.PurchasesDetail.Queries.GetPurchasesDetailQueryHandler;

namespace Shoping.Application.Features.PurchasesDetail.Queries;

public class GetPurchasesDetailQuery : IRequest<List<GetPurchasesDetailQueryResponse>>
{

}

public class GetPurchasesDetailQueryHandler : IRequestHandler<GetPurchasesDetailQuery, List<GetPurchasesDetailQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchasesDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetPurchasesDetailQueryResponse>> Handle(GetPurchasesDetailQuery request, CancellationToken cancellationToken)
    {
        var purchasesDetail = await _unitOfWork.PurchaseDetail.GetAllIncludingAsync(pd => pd.Purchase);

        return _mapper.Map<List<GetPurchasesDetailQueryResponse>>(purchasesDetail);
    }

    public class GetPurchasesDetailQueryResponse
    {
        public string Id { get; set; } = default!;
        public string PurchaseNumber { get; set; } = default!;
    }

    public class GetPurchasesDetailQueryProfile : Profile
    {
        public GetPurchasesDetailQueryProfile() =>
            CreateMap<PurchaseDetail, GetPurchasesDetailQueryResponse>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}