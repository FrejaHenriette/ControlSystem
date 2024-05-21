using ControlSystem.Application.Repositories;
using ControlSystem.Domain.Entities;
using ControlSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace ControlSystem.Tests;

public class TransportOrderRepositoryTests
{
    private readonly ControlSystemDbContext _context;
    private readonly TransportOrderRepository _repository;
    
    public TransportOrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ControlSystemDbContext>()
            .UseInMemoryDatabase("testDb").Options;

        _context = new ControlSystemDbContext(options);
        _repository = new TransportOrderRepository(_context);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsAllTransportOrders()
    {
        // Arrange
        var transportOrders = new List<TransportOrder>
        {
            new TransportOrder { Id = 1, TransportOrderState = TransportOrderState.Delivered },
            new TransportOrder { Id = 2, TransportOrderState = TransportOrderState.InTransit }
        };
        _repository.GetAllAsync().Returns(transportOrders);
        
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(transportOrders, result);
    }
    
    [Fact]
    public async Task GetById_ReturnsTransportOrder()
    {
        // Arrange
        var transportOrders = new List<TransportOrder>
        {
            new TransportOrder { Id = 1, TransportOrderState = TransportOrderState.Delivered },
            new TransportOrder { Id = 2, TransportOrderState = TransportOrderState.InTransit },
            new TransportOrder { Id = 3, TransportOrderState = TransportOrderState.InTransit }
        };
        _context.TransportOrders.AddRange(transportOrders);
        
        var repository = new TransportOrderRepository(_context);
        
        // Act
        var result = await repository.GetByIdAsync(1);

        // Assert
        Assert.Equal(transportOrders[0], result);
    }
}