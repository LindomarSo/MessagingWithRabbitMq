using EasyNetQ;
using EasyNetQ.Marketing.API.Services;
using EasyNetQ.Marketing.API.Subscribers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<CustomerCreatedSubscriber>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var bus = RabbitHutch.CreateBus("host=localhost");
builder.Services.AddSingleton(bus);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
