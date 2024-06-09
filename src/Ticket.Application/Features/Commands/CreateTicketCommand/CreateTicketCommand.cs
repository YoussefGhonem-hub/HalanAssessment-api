using MediatR;

namespace Ticket.Application.Features.Commands.CreateTicketCommand
{
	public class CreateTicketCommand : IRequest<Ticket.Domain.Entities.Ticket>
	{
		public string PhoneNumber { get; set; }
		public string Governorate { get; set; }
		public string City { get; set; }
		public string District { get; set; }
	}
}
