using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Deliveries.Commands;

public class UpdateDeliveryCommand : IRequest
{
    public string Id { get; set; } = default!;
}

public class UpdateDeliveryCommandHandler : IRequestHandler<UpdateDeliveryCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDeliveryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDeliveryCommand request , CancellationToken cancellationToken)

    {
        var DeliveryId = request.Id.FromHashId();
        var Delivery = await _unitOfWork.Delivery.GetAsync(D => D.Id == DeliveryId);
        if (Delivery == null) 
        {
            throw new ArgumentException();
        }

        //Delivery.DeliveryDetailId = Convert.ToInt32(request.Id);

        return Unit.Value;
    }
}

public class UpdateDeliveryValidator : AbstractValidator<UpdateDeliveryCommand>
{
    public UpdateDeliveryValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
    }
}