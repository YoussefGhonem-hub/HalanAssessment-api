using MediatR;
using Ticket.Application.Interfaces;

namespace Ticket.Application.Features.Commands.CreateTicketCommand
{
	public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket.Domain.Entities.Ticket>
	{
		private readonly ITicketRepository _ticketRepository;

		public CreateTicketCommandHandler(ITicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		public async Task<Ticket.Domain.Entities.Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
		{
			var ticket = new Ticket.Domain.Entities.Ticket
			{
				CreationDateTime = DateTime.Now,
				PhoneNumber = request.PhoneNumber,
				Governorate = request.Governorate,
				City = request.City,
				District = request.District,
				IsHandled = false
			};
			await _ticketRepository.AddAsync(ticket);
			return ticket;
		}
	}
}
