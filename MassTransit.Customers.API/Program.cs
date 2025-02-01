using MassTransit;
using MassTransit.Customers.API;
using MassTransit.Customers.API.Bus;
using MessagingEvents.Shared;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBusService, MassTransitBusService>();
builder.Services.AddMassTransit(c =>
{
    c.UsingRabbitMq((context, config) =>
    {
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/customers", async (CustomerCreated customer, [FromServices] IBusService bus) =>
{
    await bus.Publish(customer);
    Results.Ok(new { message = "Mensagem publicada com sucesso" });
});

app.Run();
