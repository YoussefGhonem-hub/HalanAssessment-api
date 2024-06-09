using Microsoft.EntityFrameworkCore;

namespace Ticket.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Ticket.Domain.Entities.Ticket> Tickets { get; set; }
	}

}
