
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CommandsService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queuename;

        public MessageBusSubscriber(IEventProcessor eventProcessor, IConfiguration configuration)
        {
            _eventProcessor = eventProcessor;
            _configuration = configuration;
            InitializeRabbitMQ();

        }
        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queuename = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queuename,
                exchange: "trigger",
                routingKey: "");

            Console.WriteLine(" --> Listening on the messageBus... ");
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine(" --> RabbitMQ Connection Shutdown ");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine(" --> Event received");
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queuename,autoAck: true, consumer: consumer);
            return Task.CompletedTask;
           
        }

        public override void Dispose() 
        {
            if ( _channel.IsOpen )
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }
    }
}
