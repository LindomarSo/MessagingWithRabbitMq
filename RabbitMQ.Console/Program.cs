using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

const string EXCHANGE = "curso-rabbitmq";
const string QUEUE = "person-created";

var person = new Person("Lindomar", "123.456.789-10", new DateTime(1992, 12, 21));

//ConnectionFactory connectionFactory = new ConnectionFactory
//{
//    HostName = "localhost",
//};

ConnectionFactory connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqp://guest:guest@localhost:5672/");

// Create connection with .NET
IConnection connection = await connectionFactory.CreateConnectionAsync("curso-rabbitmq");
IChannel channel = await connection.CreateChannelAsync();

string json = JsonSerializer.Serialize(person);
byte[] bytes = Encoding.UTF8.GetBytes(json);

await channel.BasicPublishAsync(EXCHANGE, "hr.person-created", bytes);

Console.WriteLine($"Mensagem publicada: {json}");

IChannel consumerChannel = await connection.CreateChannelAsync();
AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(consumerChannel);

consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    byte[] contentArray = eventArgs.Body.ToArray();
    string contentString = Encoding.UTF8.GetString(contentArray);

    Person? message = JsonSerializer.Deserialize<Person>(contentString);

    Console.WriteLine($"Message received: {message?.ToString()}");
    await consumerChannel.BasicAckAsync(eventArgs.DeliveryTag, false);
};

await consumerChannel.BasicConsumeAsync(QUEUE, false, consumer);

Console.ReadLine();

class Person
{
    public Person(string fullName, string document, DateTime birthDate)
    {
        FullName = fullName;
        Document = document;
        BirthDate = birthDate;
    }

    public string FullName { get; }
    public string Document { get; }
    public DateTime BirthDate { get; }

    public override string ToString()
    {
        return $"Name: {FullName}, Document: {Document}, BirthDate: {BirthDate}";
    }
}