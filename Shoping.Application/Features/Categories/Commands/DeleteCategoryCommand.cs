using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Categories.Commands
{

    public class DeleteCategoryCommand : IRequest
    {
        public string Id { get; set; } = default!;
        // public string Name { get; set; } = default!;
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryId = request.Id.FromHashId();
            var category = await _unitOfWork.Category.GetAsync(C => C.Id == categoryId);
            _unitOfWork.Category.Remove(category);

            if (category is null)
            {
                throw new NotFoundException();
            }

            await this._unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();

        }
    }
}
