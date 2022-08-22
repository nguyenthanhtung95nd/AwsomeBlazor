using BlazorApp.Application.Features.Tickets.Commands.CreateTicket;
using BlazorApp.Application.Features.Tickets.Commands.DeleteTicket;
using BlazorApp.Application.Features.Tickets.Commands.UpdateTicket;
using BlazorApp.Application.Features.Tickets.Commands.UpdateTicketStatus;
using BlazorApp.Application.Features.Tickets.Queries.GetTickketById;
using BlazorApp.Application.Features.Tickets.Queries.GetTickketList;
using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Ticket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetTickets")]
        public async Task<IActionResult> GetTickets([FromQuery] SearchTicketViewModel viewModel)
        {
            var result = await _mediator.Send(new GetTickketListQuery(viewModel));
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetTicketById")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var result = await _mediator.Send(new GetTicketByIdQuery(id));
            return Ok(result);
        }

        [HttpPost(Name = "CreateTicket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateTicket([FromBody] CreateTicketViewModel viewModel)
        {
            var result = await _mediator.Send(new CreateTicketCommand(viewModel));
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateTicket")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateTicket(int id, [FromBody] UpdateTicketViewModel viewModel)
        {
            await _mediator.Send(new UpdateStatusTicketCommand(id, viewModel));
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTicket")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var command = new DeleteTicketCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("/api/Ticket/UpdateStatusTicket/{id}")]
        public async Task<ActionResult> UpdateStatusTicket(int id, [FromBody] Status status)
        {
            var result = await _mediator.Send(new UpdateTicketStatusCommand(id, status));
            return Ok(result);
        }
    }
}