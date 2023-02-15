﻿using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
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
        var sale = await _unitOfWork.Sale.GetAsync(s => s.Id==request.Id.FromHashId() && s.IsDeleted == false);

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
    public int Client_Id { get; set; }
}

public class GetSaleQueryProfile : Profile
{
    public GetSaleQueryProfile() =>
        CreateMap<Sale, GetSaleQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}