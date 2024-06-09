namespace Ticket.Application.Interfaces
{
	public interface ITicketRepository
	{

		Task<Ticket.Domain.Entities.Ticket> AddAsync(Ticket.Domain.Entities.Ticket ticket);
		Task<IEnumerable<Ticket.Domain.Entities.Ticket>> GetAllAsync(int pageNumber, int pageSize = 5);
		Task<IEnumerable<Domain.Entities.Ticket>> GetAllAsync(Func<Domain.Entities.Ticket, bool> condition);
		Task<Ticket.Domain.Entities.Ticket> UpdateAsync(Ticket.Domain.Entities.Ticket ticket);
		Task<Ticket.Domain.Entities.Ticket> GetByIdAsync(int id);
	}
}
