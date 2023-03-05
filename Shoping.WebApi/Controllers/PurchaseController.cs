using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Shoping.Application.Features.Purchases.Queries.GetPurchasesQueryHandler;
using Shoping.Application.Features.Purchases.Queries;
using Shoping.Application.Features.Purchases.Commands;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetPurchasesQueryResponse>> GetPurchases() => _mediator.Send(new GetPurchasesQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePurchase([FromBody] UpdatePurchaseCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{Id}")]
        public Task<GetPurchaseQueryResponse> GetPurchaseById([FromRoute] GetPurchaseQuery query) =>
            _mediator.Send(query);

        [HttpPost("CancelPurchase/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelPurchase(string Id)
        {
            await _mediator.Send(new CancelPurchaseCommand { Id = Id });

            return Ok();
        }
    }
}