using EasyNetQ;
using EasyNetQ.Customer.API.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IBus bus = RabbitHutch.CreateBus("host=localhost");
builder.Services.AddSingleton<IBusService, EasyNetQService>(_ => new EasyNetQService(bus));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
