using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticket.Application.Interfaces;
using Ticket.Infrastructure.Persistence;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddScoped<ITicketRepository, TicketRepository>();

			return services;
		}
	}
}
