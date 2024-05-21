using ControlSystem.Application.Repositories;
using ControlSystem.Application.Services;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransportOrderRepository, TransportOrderRepository>();
builder.Services.AddScoped<TransportOrderService>();
builder.Services.AddSingleton<RabbitMqMessageBrokerService>();

var app = builder.Build();

app.MapPost("/transportorders", (TransportOrder order, ITransportOrderRepository repository) => repository.CreateAsync(order));

app.MapGet("/transportorders", (ITransportOrderRepository repository) => repository.GetAllAsync());
app.MapGet("/transportorders/{id}", (int id, ITransportOrderRepository repository) => repository.GetByIdAsync(id));

app.MapPut("/transportorders/{id}", (int id, TransportOrder order, ITransportOrderRepository repository) => repository.UpdateAsync(order));

app.MapDelete("/transportorders/{id}", (int id, ITransportOrderRepository repository) => repository.DeleteAsync(id));

app.Run();