using MediatR;
using Ticket.Application.Interfaces;

namespace Ticket.Application.Features.Queries.GetTicketsQuery
{
	public class GetTicketsQuery : IRequest<List<Ticket.Domain.Entities.Ticket>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}

	public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<Domain.Entities.Ticket>>
	{
		private readonly ITicketRepository _ticketRepository;

		public GetTicketsQueryHandler(ITicketRepository ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		public async Task<List<Domain.Entities.Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
		{
			var tickets = await _ticketRepository.GetAllAsync(request.PageNumber, request.PageSize);
			return tickets.ToList();
		}
	}

}
