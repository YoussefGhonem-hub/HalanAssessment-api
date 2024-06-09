using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Features.Commands.CreateTicketCommand;
using Ticket.Application.Features.Commands.HandleTicketCommand;
using Ticket.Application.Features.Queries.GetTicketsQuery;
using Ticket.Application.Interfaces;

namespace Ticket.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TicketsController(IMediator mediator, ITicketRepository ticketRepository)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
		{
			var ticket = await _mediator.Send(command);
			return Ok(ticket);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 5)
		{
			var query = new GetTicketsQuery { PageNumber = pageNumber, PageSize = pageSize };
			var tickets = await _mediator.Send(query);
			return Ok(tickets);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Handle(int id)
		{
			var command = new HandleTicketCommand { TicketId = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}

}
