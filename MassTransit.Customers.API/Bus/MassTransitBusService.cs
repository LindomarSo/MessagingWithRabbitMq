namespace MassTransit.Customers.API.Bus;

public class MassTransitBusService(IBus bus) : IBusService
{
    private readonly IBus _bus = bus;

    public async Task Publish<T>(T message)
    {
        if (message is null)
            return;

        await _bus.Publish(message);
    }
}
