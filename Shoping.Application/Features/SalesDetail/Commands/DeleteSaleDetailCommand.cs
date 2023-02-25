//using AutoMapper;
//using FluentValidation;
//using MediatR;
//using Shoping.Application.Common.Exceptions;
//using Shoping.Application.Common.Helpers;
//using Shoping.Application.Common.Interfaces;
//using Shoping.Domain.Entities;

//namespace Shoping.Application.Features.Sales.Commands
//{
//    public class DeleteSaleCommand : IRequest
//    {
//        public string Id { get; set; } = default!;
//    }

//    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
//    {
//        private readonly IMapper _mapper;
//        private readonly IUnitOfWork _unitOfWork;

//        public DeleteSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
//        {
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
//        {
//            var saleId = request.Id.FromHashId();
//            var sale = await _unitOfWork.Sale.GetAsync(P => P.Id == saleId);

//            if (sale is null)
//            {
//                throw new NotFoundException();
//            }

//            sale.IsDeleted = true;

//            await _unitOfWork.CommitAsync();

//            return Unit.Value;
//        }
//    }

//    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
//    {
//        public DeleteSaleValidator()
//        {
//            RuleFor(r => r.Id).NotNull().NotEmpty();
//        }
//    }
//}
