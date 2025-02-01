using EasyNetQ.Marketing.API.Services;
using MessagingEvents.Shared;
using Newtonsoft.Json;

namespace EasyNetQ.Marketing.API.Subscribers;

public class CustomerCreatedSubscriber : IHostedService
{
    const string CUSTOMER_CREATED_QUEUE = "customer-created";

    public IServiceProvider _serviceProvider { get; }
    public IAdvancedBus _bus { get;  }

    public CustomerCreatedSubscriber(IServiceProvider serviceProvider, IBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus.Advanced;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var queue = _bus.QueueDeclare(CUSTOMER_CREATED_QUEUE);
        _bus.Consume<CustomerCreated>(queue, async (message, info) =>
        {
            var json = JsonConvert.SerializeObject(message.Body);
            Console.WriteLine(json);

            await SendEmail(message.Body);
        });
    }

    private async Task SendEmail(CustomerCreated customer)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<INotificationService>()
                .SendEmail("test@gmail.com", CUSTOMER_CREATED_QUEUE, new Dictionary<string, string> { { "name", customer.FullName } });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
