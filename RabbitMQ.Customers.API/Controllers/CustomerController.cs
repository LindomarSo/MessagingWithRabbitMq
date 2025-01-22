using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Customers.API.Bus;

namespace RabbitMQ.Customers.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    const string ROUTING_KEY = "customer-created";
    private readonly IBusService _busService;

    public CustomerController(IBusService busService)
    {
        _busService = busService;
    }

    [HttpPost]
    public IActionResult Post(CustomerCreated customer)
    {
        _busService.Publish(ROUTING_KEY, customer);
        return Ok(null);
    }
}

