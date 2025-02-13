using PlatformService.Dtos;
using RabbitMQ.Client;
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
            }
            catch ( Exception ex )
            {
                Console.WriteLine($" --> Could not connect to a message bus {ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("RabbitMQ Connection Shutdown");
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            throw new NotImplementedException();
        }
    }
}
