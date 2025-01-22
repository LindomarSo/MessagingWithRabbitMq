
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Marketing.API.Models;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Marketing.API.Subscribers;

public class CustomerCreatedSubscriber : IHostedService
{
    const string EXCHANGE = "curso-rabbitmq";
    const string CUSTOMER_CREATED_QUEUE = "customer-created";
    private readonly IChannel _channel;

    public CustomerCreatedSubscriber()
    {
        ConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        IConnection connection = connectionFactory.CreateConnectionAsync("curso-rabbitmq-client-consumer").Result;
        _channel = connection.CreateChannelAsync().Result;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            byte[] contentArray =  eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);

            CustomerCreated? customer = JsonSerializer.Deserialize<CustomerCreated>(contentString);
            Console.WriteLine($"Message received: {contentString}");

            await _channel.BasicAckAsync(eventArgs.DeliveryTag, false, cancellationToken);
        };

        await _channel.BasicConsumeAsync(CUSTOMER_CREATED_QUEUE, false, consumer, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
