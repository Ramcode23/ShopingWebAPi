using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Providers.Commands
{
    public class DeleteProviderCommand : IRequest
    {
        public string Id { get; set; } = default!;
    }

    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProviderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var providerId = request.Id.FromHashId();
            var provider = await _unitOfWork.Provider.GetAsync(P => P.Id == providerId);

            if (provider is null)
            {
                throw new NotFoundException();
            }

            provider.IsDeleted = true;

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteProviderValidator : AbstractValidator<DeleteProviderCommand>
    {
        public DeleteProviderValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}
