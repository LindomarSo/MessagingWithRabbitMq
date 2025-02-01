
using MessagingEvents.Shared;
using System.Text.Json;

namespace MassTransit.Marketing.API.Subscribers;

public class CustomerCreatedSubscriber : IConsumer<CustomerCreated>
{
    public async Task Consume(ConsumeContext<CustomerCreated> context)
    {
        var @event = context.Message;
        Console.WriteLine(JsonSerializer.Serialize(@event));
    }
}
