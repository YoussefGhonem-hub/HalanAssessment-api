using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ticket.Application.Interfaces;

namespace Ticket.Application.Helper
{
	public class BackgroundTaskService : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly IServiceScopeFactory _scopeFactory;

		public BackgroundTaskService(IServiceScopeFactory scopeFactory)
		{
			_scopeFactory = scopeFactory;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new Timer(HandleTickets, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
			return Task.CompletedTask;
		}

		private async void HandleTickets(object state)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var ticketRepository = scope.ServiceProvider.GetRequiredService<ITicketRepository>();

				Func<Domain.Entities.Ticket, bool> condition = ticket => (DateTime.Now - ticket.CreationDateTime).TotalMinutes >= 60;

				var tickets = await ticketRepository.GetAllAsync(condition);
				foreach (var ticket in tickets)
				{
					ticket.IsHandled = true;
					await ticketRepository.UpdateAsync(ticket);
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}


}
