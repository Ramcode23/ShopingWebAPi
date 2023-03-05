using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.SalesDetail.Queries.GetSalesDetailQueryHandler;

namespace Shoping.Application.Features.SalesDetail.Queries;

public class GetSalesDetailQuery : IRequest<List<GetSalesDetailQueryResponse>>
{

}

public class GetSalesDetailQueryHandler : IRequestHandler<GetSalesDetailQuery, List<GetSalesDetailQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetSalesDetailQueryResponse>> Handle(GetSalesDetailQuery request, CancellationToken cancellationToken)
    {
        var salesDetail = await _unitOfWork.SaleDetail.GetAllIncludingAsync(sd => sd.Sale);

        return _mapper.Map<List<GetSalesDetailQueryResponse>>(salesDetail);
    }
    
    public class GetSalesDetailQueryResponse
    {
        public string Id { get; set; } = default!;
        public string SaleNumber { get; set; } = default!;
    }

    public class GetSalesDetailQueryProfile : Profile
    {
        public GetSalesDetailQueryProfile() =>
            CreateMap<SaleDetail, GetSalesDetailQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}