using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ticket.Application.Helper;

namespace Ticket.Application
{
	public static class ServiceCollectionExtention
	{
		public static IServiceCollection AddApplicationExtention(this IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddHostedService<BackgroundTaskService>();
			return services;
		}
	}
}