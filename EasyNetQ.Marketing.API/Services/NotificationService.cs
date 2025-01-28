
namespace EasyNetQ.Marketing.API.Services;

public class NotificationService : INotificationService
{
    public Task SendEmail(string email, string template, Dictionary<string, string> parameters)
    {
        Console.WriteLine($"{email}, {template}, {parameters["name"]}");
        return Task.CompletedTask;
    }
}
