using Xunit;
using Moq;
using ControlSystem.Models;
using ControlSystem.Repository;
using Assert = Xunit.Assert;

namespace ControlSystem.Services;
public class TransportOrderServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllTransportOrders()
    {
        // Arrange
        var mockMessageBrokerService = new Mock<IMessageBrokerService>();

        var mockRepository = new Mock<IRepository<TransportOrder>>();
        var transportOrders = new List<TransportOrder>
        {
            new TransportOrder { Id = 1, Status = "Delivered" },
            new TransportOrder { Id = 2, Status = "In Transit" }
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