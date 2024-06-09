using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ticket.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
	public ApplicationDbContext CreateDbContext(string[] args)
	{
		var configBuilder = new ConfigurationBuilder();
		configBuilder.AddJsonFile("appsettings.json");
		var config = configBuilder.Build();
		var connectionString = config.GetConnectionString("DefaultConnection");
		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
		optionsBuilder.UseSqlServer(connectionString);
		return new ApplicationDbContext(optionsBuilder.Options);
	}
}