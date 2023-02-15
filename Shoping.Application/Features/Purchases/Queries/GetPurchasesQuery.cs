using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Purchases.Queries.GetPurchasesQueryHandler;

namespace Shoping.Application.Features.Purchases.Queries;

public class GetPurchasesQuery : IRequest<List<GetPurchasesQueryResponse>>
{

}

public class GetPurchasesQueryHandler : IRequestHandler<GetPurchasesQuery, List<GetPurchasesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchasesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetPurchasesQueryResponse>> Handle(GetPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _unitOfWork.Purchase.GetAllAsync(p => p.IsCanceled == false);
        // var purchases = await _unitOfWork.Purchase.GetAllAsync(p => p.IsDeleted == false);

        return _mapper.Map<List<GetPurchasesQueryResponse>>(purchases);
    }

    public class GetPurchasesQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Number { get; set; } = default!;
        public DateTime Date { get; set; }
        public string ProviderName { get; set; } = default!;
    }

    public class GetPurchasesQueryProfile : Profile
    {
        public GetPurchasesQueryProfile() =>
            CreateMap<Purchase, GetPurchasesQueryResponse>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}