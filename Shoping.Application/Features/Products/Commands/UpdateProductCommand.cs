using FluentValidation;
using MediatR;
using Shoping.Application.Common.Attributes;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;


namespace Shoping.Application.Features.Products.Commands;

[AuditLog]
public class UpdateProductCommand : IRequest
{
    public string ProductId { get; set; } = default!;
    public string Description { get; set; } = default!;
    public double Price { get; set; }
}

//public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
//{
//    private readonly MyAppDbContext _context;

//    public UpdateProductCommandHandler(MyAppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
//    {
//        var productId = request.ProductId.FromHashId();
//        var product = await _context.Products.FindAsync(productId);

//        if (product is null)
//        {
//            throw new NotFoundException();
//        }

//        product.Description = request.Description;
//        product.Price = request.Price;

//        await _context.SaveChangesAsync(cancellationToken);

//        return Unit.Value;
//    }
//}

//public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
//{
//    public UpdateProductValidator()
//    {
//        RuleFor(r => r.ProductId).NotNull().NotEmpty();
//        RuleFor(r => r.Description).NotNull().NotEmpty();
//        RuleFor(r => r.Price).GreaterThan(0).NotNull().NotEmpty();
//    }
//}
