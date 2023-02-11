using AutoMapper;
using MediatR;
using Shoping.Application.Common.Interfaces;
using FluentValidation;
using Shoping.Domain.Entities;


namespace Shoping.Application.Features.Client.Commands;

    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; } = default!;
        public string Ruc { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Direction { get; set; } = default!;
    }

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)

    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
    
        var newClient= _mapper.Map<Client>(request);

        this._unitOfWork.Client.Add(newClient);
        await this._unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class CreateClientCommandMapper : Profile
{
    public CreateClientCommandMapper() =>
        CreateMap<CreateClientCommand, Client>();
}

public class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(r => r.Name).NotNull();

    }
}


