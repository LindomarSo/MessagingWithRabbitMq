namespace EasyNetQ.Marketing.API.Models;

public class CustomerCreated
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public DateTime BirthDate { get; set; }
}
