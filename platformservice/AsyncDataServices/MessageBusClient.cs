using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration= configuration;
            var factory = new
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            throw new NotImplementedException();
        }
    }
}
