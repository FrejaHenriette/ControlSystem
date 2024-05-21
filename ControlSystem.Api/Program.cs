using ControlSystem.Application.DTOs;
using ControlSystem.Application.Repositories;
using ControlSystem.Application.Services;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using ControlSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ControlSystemDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransportOrderRepository, TransportOrderRepository>();
builder.Services.AddScoped<TransportOrderService>();

var app = builder.Build();

app.MapPost("/transportorder", (TransportOrder order, TransportOrderService service) => service.CreateAsync(order));

app.MapGet("/transportorder/{id:int}", (int id, TransportOrderService service) => service.GetByIdAsync(id));

app.MapPut("/transportorders/{id:int}", (int id, TransportOrderDto orderDto, TransportOrderService service) => service.UpdateAsync(id, orderDto));

app.MapPut("/transportorders/{id:int}/status/{state}", (int id, string state, TransportOrderService service) => service.SetState(id, state));

app.MapGet("/transportorders/state/{state}", (string state, TransportOrderService service) => service.GetByStateAsync(state));

app.MapDelete("/transportorders/{id:int}", (int id, TransportOrderService service) => service.DeleteAsync(id));

app.Run();