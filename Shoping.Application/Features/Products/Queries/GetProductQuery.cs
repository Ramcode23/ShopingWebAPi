using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Products.Queries;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Product.GetAllIncludingAsync(p => p.Category);
        var product = products.FirstOrDefault(p => p.Id==request.Id.FromHashId() && p.IsDeleted==false);

        if (product is null)
        {
            throw new NotFoundException(nameof(product), request.Id);
        }

        return _mapper.Map<GetProductQueryResponse>(product);
    }
}

public class GetProductQueryResponse
{
    public string Id { get; set; } = default!;
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = default!;
    public decimal Price { get; set; }
}

public class GetProductQueryProfile : Profile
{
    public GetProductQueryProfile() =>
        CreateMap<Product, GetProductQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}