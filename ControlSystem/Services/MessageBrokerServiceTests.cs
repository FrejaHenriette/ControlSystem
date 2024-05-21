using Xunit;
using Moq;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace ControlSystem.Services;
public class RabbitMqMessageBrokerServiceTests
{
    private Mock<IConnection> _mockConnection;
    private Mock<IModel> _mockChannel;
    private RabbitMqMessageBrokerService _service;

    public RabbitMqMessageBrokerServiceTests()
    {
        _mockConnection = new Mock<IConnection>();
        _mockChannel = new Mock<IModel>();
        _service = new RabbitMqMessageBrokerService(_mockConnection.Object, _mockChannel.Object);
    }

    [Fact]
    public void SendMessage_SendsMessageToQueue()
    {
        // Arrange
        var message = "Test message";

        // Act
        _service.SendMessage(message);

        // Assert
        _mockChannel.Verify(ch => ch.BasicPublish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IBasicProperties>(), It.Is<byte[]>(b => Encoding.UTF8.GetString(b) == message)));
    }

    [Fact]
    public void ReceiveMessage_ReceivesMessageFromQueue()
    {
        // Arrange
        var message = "Test message";
        var body = Encoding.UTF8.GetBytes(message);
        var basicDeliverEventArgs = new BasicDeliverEventArgs { Body = body };

        // Act
        _service.ReceiveMessage();

        // Assert
        _mockChannel.Verify(ch => ch.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<IBasicConsumer>()));
    }
}