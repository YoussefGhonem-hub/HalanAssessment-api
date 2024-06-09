using MediatR;
using Ticket.Application.Interfaces;

namespace Ticket.Application.Features.Commands.HandleTicketCommand
{
	public class HandleTicketCommand : IRequest
	{
		public int TicketId { get; set; }
	}

	public class HandleTicketCommandHandler : IRequestHandler<HandleTicketCommand>
	{
		private readonly ITicketRepository _ticketRepository;

		public HandleTicketCommandHandler(ITicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		public async Task<Unit> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
		{
			var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
			if (ticket == null)
			{
				throw new KeyNotFoundException("Ticket not found");
			}

			ticket.IsHandled = true;
			await _ticketRepository.UpdateAsync(ticket);
			return Unit.Value;
		}
	}

}
