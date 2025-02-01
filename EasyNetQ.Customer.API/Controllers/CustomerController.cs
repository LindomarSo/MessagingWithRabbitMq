using EasyNetQ.Customer.API.Bus;
using MessagingEvents.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EasyNetQ.Customer.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CustomerController(IBusService busService) : ControllerBase
{
    const string ROUTING_KEY = "customer-created";
    private readonly IBusService _busService = busService;

    [HttpPost]
    public IActionResult Post(CustomerCreated customer)
    {
        _busService.Publish(ROUTING_KEY, customer);
        return Ok(null);
    }
}

