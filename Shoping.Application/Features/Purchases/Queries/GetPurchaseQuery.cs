using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Queries;

public class GetPurchaseQuery : IRequest<GetPurchaseQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetPurchaseQueryHandler : IRequestHandler<GetPurchaseQuery, GetPurchaseQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchaseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetPurchaseQueryResponse> Handle(GetPurchaseQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _unitOfWork.Purchase.GetAllIncludingAsync(p => p.Provider);
        var purchase = purchases.FirstOrDefault(p => p.Id == request.Id.FromHashId());

        if (purchase is null)
        {
            throw new NotFoundException(nameof(purchase), request.Id);
        }

        return _mapper.Map<GetPurchaseQueryResponse>(purchase);
    }
}

public class GetPurchaseQueryResponse
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public string ProviderName { get; set; } = default!;
}

public class GetPurchaseQueryProfile : Profile
{
    public GetPurchaseQueryProfile() =>
        CreateMap<Purchase, GetPurchaseQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}