using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Shoping.Application.Features.PurchasesDetail.Queries.GetPurchasesDetailQueryHandler;
using Shoping.Application.Features.PurchasesDetail.Queries;
using Shoping.Application.Features.PurchasesDetail.Commands;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetPurchasesDetailQueryResponse>> GetPurchases() => _mediator.Send(new GetPurchasesDetailQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePurchase([FromBody] CreatePurchaseDetailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePurchase([FromBody] UpdatePurchaseDetailCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{Id}")]
        public Task<GetPurchaseDetailQueryResponse> GetPurchaseById([FromRoute] GetPurchaseDetailQuery query) =>
            _mediator.Send(query);

        [HttpPost("CancelPurchase/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelPurchase(string Id)
        {
            await _mediator.Send(new CancelPurchaseDetailCommand { Id = Id });

            return Ok();
        }
    }
}