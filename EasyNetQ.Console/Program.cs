using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;

const string EXCHANGE = "curso-rabbitmq";
const string QUEUE = "person-created";
const string ROUTING_KEY = "hr.person-created";

var person = new Person("Lindomar", "123.456.789-10", new DateTime(1992, 12, 21));

var bus = RabbitHutch.CreateBus("host=localhost");

// Publicar para um Exchange
//await bus.PubSub.PublishAsync(person);

//// Cria uma fila
//await bus.PubSub.SubscribeAsync<Person>("marketing", msg =>
//{
//    string json = JsonConvert.SerializeObject(msg);
//    Console.WriteLine(json);
//});

// Publicação com mais controle
IAdvancedBus advanced = bus.Advanced;

Exchange exchange = advanced.ExchangeDeclare(EXCHANGE, "topic");
Queue queue = advanced.QueueDeclare(QUEUE);

advanced.Publish(exchange, ROUTING_KEY, true, new Message<Person>(person));

advanced.Consume<Person>(queue, (msg, info) =>
{
    string json = JsonConvert.SerializeObject(msg.Body);
    Console.WriteLine(json);
});

Console.ReadKey();

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