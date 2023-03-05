using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Sales.Queries;

public class GetSaleQuery : IRequest<GetSaleQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, GetSaleQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSaleQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetSaleQueryResponse> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sales = await _unitOfWork.Sale.GetAllIncludingAsync(s => s.Client);
        var sale = sales.FirstOrDefault(s => s.Id==request.Id.FromHashId());

        if (sale is null)
        {
            throw new NotFoundException(nameof(sale), request.Id);
        }

        return _mapper.Map<GetSaleQueryResponse>(sale);
    }
}

public class GetSaleQueryResponse
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public string ClientName { get; set; } = default!;
}

public class GetSaleQueryProfile : Profile
{
    public GetSaleQueryProfile() =>
        CreateMap<Sale, GetSaleQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}