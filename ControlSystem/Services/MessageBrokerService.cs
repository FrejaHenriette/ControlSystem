using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ControlSystem.Services;

public interface IMessageBrokerService : IDisposable
{
    public void SendMessage(string message);
    public void ReceiveMessage();
}

public class RabbitMqMessageBrokerService : IMessageBrokerService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqMessageBrokerService(IConnection connection, IModel channel)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = connection;
        _channel = channel;
    }
    
    public void SendMessage(string message)
    {
        _channel.QueueDeclare("transportOrder", false, false, false, null);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish("", "transportOrder", null, body);
    }
    
    public void ReceiveMessage()
    {
        _channel.QueueDeclare("transportOrder", false, false, false, null);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };
        _channel.BasicConsume("transportOrder", true, consumer);
    }
    
    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
