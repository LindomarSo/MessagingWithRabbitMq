namespace EasyNetQ.Marketing.API.Services;

public interface INotificationService
{
    Task SendEmail(string email, string template, Dictionary<string, string> parameters);
}
