using Microsoft.EntityFrameworkCore;
using Ticket.Application;
using Ticket.Infrastructure;
using Ticket.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationExtention();
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(builder =>
	builder.AllowAnyOrigin()
		   .AllowAnyHeader()
		   .AllowAnyMethod());
using (var scope = app.Services.CreateScope())
{
	var identityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	await identityDbContext.Database.MigrateAsync();
}
app.Run();
