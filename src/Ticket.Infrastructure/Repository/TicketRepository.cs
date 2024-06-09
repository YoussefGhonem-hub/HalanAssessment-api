using Microsoft.EntityFrameworkCore;
using Ticket.Application.Interfaces;
using Ticket.Infrastructure.Persistence;

namespace Ticket.Infrastructure.Repository
{

	public class TicketRepository : ITicketRepository
	{
		private readonly ApplicationDbContext _context;

		public TicketRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Domain.Entities.Ticket> AddAsync(Domain.Entities.Ticket ticket)
		{
			await _context.Tickets.AddAsync(ticket);
			await _context.SaveChangesAsync();
			return ticket;
		}

		public async Task<IEnumerable<Domain.Entities.Ticket>> GetAllAsync(int pageNumber, int pageSize)
		{
			return await _context.Tickets
				.OrderBy(t => t.CreationDateTime)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
		public async Task<IEnumerable<Domain.Entities.Ticket>> GetAllAsync(Func<Domain.Entities.Ticket, bool> condition)
		{
			return await Task.Run(() => _context.Tickets
				.Where(condition)
				.OrderBy(t => t.CreationDateTime)
				.ToList());
		}

		public async Task<Domain.Entities.Ticket> UpdateAsync(Domain.Entities.Ticket ticket)
		{
			_context.Tickets.Update(ticket);
			await _context.SaveChangesAsync();
			return ticket;
		}

		public async Task<Domain.Entities.Ticket> GetByIdAsync(int id)
		{
			return await _context.Tickets.FindAsync(id);
		}
	}
}
