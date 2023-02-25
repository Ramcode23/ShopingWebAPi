using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.SalesDetail.Queries;

public class GetSaleDetailQuery : IRequest<GetSaleDetailQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetSaleDetailQueryHandler : IRequestHandler<GetSaleDetailQuery, GetSaleDetailQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetSaleDetailQueryResponse> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
    {
        var saleDetail = await _unitOfWork.SaleDetail.GetAsync(s => s.Id==request.Id.FromHashId());

        if (saleDetail is null)
        {
            throw new NotFoundException(nameof(saleDetail), request.Id);
        }

        return _mapper.Map<GetSaleDetailQueryResponse>(saleDetail);
    }
}

public class GetSaleDetailQueryResponse
{
    public string Id { get; set; } = default!;
    public int Sale_Id { get; set; }
}

public class GetSaleDetailQueryProfile : Profile
{
    public GetSaleDetailQueryProfile() =>
        CreateMap<SaleDetail, GetSaleDetailQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}