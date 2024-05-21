using ControlSystem.Application.Services;
using ControlSystem.Domain.Entities;
using ControlSystem.Domain.Repositories;
using Moq;
using Assert = Xunit.Assert;

namespace ControlSystem.Tests;

public class TransportOrderServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllTransportOrders()
    {
        // Arrange
        var mockMessageBrokerService = new Mock<IMessageBrokerService>();

        var mockRepository = new Mock<ITransportOrderRepository>();
        var transportOrders = new List<TransportOrder>
        {
            new TransportOrder { Id = 1, TransportOrderStatus = TransportOrderStatus.Delivered },
            new TransportOrder { Id = 2, TransportOrderStatus = TransportOrderStatus.InTransit }
        };
        mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transportOrders);

        var service = new TransportOrderService(mockRepository.Object, mockMessageBrokerService.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(transportOrders, result);
    }

    // Add more tests for other methods of TransportOrderService
}