using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Customers.API.Bus;

public class RabbitMQClientService : IBusService
{
    const string EXCHANGE = "curso-rabbitmq";
    private readonly IChannel _channel;

    public RabbitMQClientService()
    {
        ConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        IConnection connection = connectionFactory.CreateConnectionAsync("curso-rabbitmq-client-publisher").Result;
        _channel = connection.CreateChannelAsync().Result;
    }

    public void Publish<T>(string routingKey, T message)
    {
        if(message == null)
            throw new ArgumentNullException("message");

        string json = JsonSerializer.Serialize(message);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublishAsync(EXCHANGE, routingKey, true, bytes);
    }
}
 