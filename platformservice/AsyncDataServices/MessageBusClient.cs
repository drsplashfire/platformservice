using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration= configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"]),
                UserName = "guest", 
                Password = "guest"
            };

            try
            {
                _connection = factory.CreateConnection(new List<string> { _configuration["RabbitMQHost"] });
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine(" --> RabbitMQ Connected to MessageBus");

            }
            catch ( Exception ex )
            {
                Console.WriteLine($" --> Could not connect to a message bus {ex.Message}");
            }
        }
      

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if ( _connection.IsOpen )
            {
                Console.WriteLine(" --> Connection is open, Sending message");
                SendMessage(message);
            }
            else 
            {
                Console.WriteLine(" --> Connection is Closed, not Sending message");

            }
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger", 
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            Console.WriteLine($" --> We have sent message{message}");
        }

        public void Dispose()
        {
            if ( _channel.IsOpen )
            {
                _channel.Close();
                _connection.Close();
            }
        }
        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ Connection Shutdown");
        }
    }
}
