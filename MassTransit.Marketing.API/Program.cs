using MassTransit;
using MassTransit.Marketing.API.Subscribers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<CustomerCreatedSubscriber>();

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

app.MapGet("/weatherforecast", () =>
{
})
.WithName("GetWeatherForecast");

app.Run();
