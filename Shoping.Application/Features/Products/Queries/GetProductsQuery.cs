using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Products.Queries.GetProductsQueryHandler;

namespace Shoping.Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<List<GetProductsQueryResponse>>
{

}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var Products = await _unitOfWork.Product.GetAllAsync();
        return _mapper.Map<List<GetProductsQueryResponse>>(Products);
    }
    
    public class GetProductsQueryResponse
    {
        public string Id { get; set; } = default!;
        public int Code { get; set; }
        public string Name { get; set; } = default!;
        public int Category_Id { get; set; }
        public decimal Price { get; set; }
    }

    public class GetProductsQueryProfile : Profile
    {
        public GetProductsQueryProfile() =>
            CreateMap<Product, GetProductsQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}