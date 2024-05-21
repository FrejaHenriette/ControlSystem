using ControlSystem.Application.DTOs;
using ControlSystem.Application.Repositories;
using ControlSystem.Application.Services;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using ControlSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.Extensions;
using Assert = Xunit.Assert;

namespace ControlSystem.Tests;

public class TransportOrderServiceTests
{
    private readonly ControlSystemDbContext _context;
    private readonly TransportOrderRepository _repository;
    
    public TransportOrderServiceTests()
    {
        var options = new DbContextOptionsBuilder<ControlSystemDbContext>()
            .UseInMemoryDatabase("testDb").Options;

        _context = new ControlSystemDbContext(options);
        _repository = new TransportOrderRepository(_context);
    }
    
    [Fact]
    public async Task SetStatusTransportOrder_ReturnsTransportOrder()
    {
        // Arrange
        var transportOrders = new List<TransportOrder>
        {
            new TransportOrder { Id = 1, TransportOrderState = TransportOrderState.Delivered },
            new TransportOrder { Id = 2, TransportOrderState = TransportOrderState.InTransit },
            new TransportOrder { Id = 3, TransportOrderState = TransportOrderState.InTransit }
        };
        var service = new TransportOrderService(_repository);

        // Act
        await service.SetState(2, "Delivered");

        // Assert
        Assert.Equal(TransportOrderState.Delivered, transportOrders[1].TransportOrderState);
    }

    // Add more tests for other methods of TransportOrderService
}