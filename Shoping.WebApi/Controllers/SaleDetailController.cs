using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Application.Features.SalesDetail.Commands;
using Shoping.Application.Features.SalesDetail.Queries;
using static Shoping.Application.Features.SalesDetail.Queries.GetSalesDetailQueryHandler;

namespace Shoping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<List<GetSalesDetailQueryResponse>> GetSales() => _mediator.Send(new GetSalesDetailQuery());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleDetailCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleDetailCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("{Id}")]
        public Task<GetSaleDetailQueryResponse> GetSaleById([FromRoute] GetSaleDetailQuery query) =>
            _mediator.Send(query);

        [HttpPost("CancelSale/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelSale(string Id)
        {
            await _mediator.Send(new CancelSaleDetailCommand { Id = Id });

            return Ok();
        }
    }
}