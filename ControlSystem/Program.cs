using ControlSystem.Models;
using ControlSystem.Repository;
using ControlSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<TransportOrder>, Repository<TransportOrder>>();
builder.Services.AddScoped<TransportOrderService>();
builder.Services.AddSingleton<RabbitMqMessageBrokerService>();

var app = builder.Build();

app.MapPost("/transportorders", (TransportOrder order, IRepository<TransportOrder> repository) => repository.CreateAsync(order));

app.MapGet("/transportorders", (IRepository<TransportOrder> repository) => repository.GetAllAsync());
app.MapGet("/transportorders/{id}", (int id, IRepository<TransportOrder> repository) => repository.GetByIdAsync(id));

app.MapPut("/transportorders/{id}", (int id, TransportOrder order, IRepository<TransportOrder> repository) => repository.UpdateAsync(order));

app.MapDelete("/transportorders/{id}", (int id, IRepository<TransportOrder> repository) => repository.DeleteAsync(id));

app.Run();