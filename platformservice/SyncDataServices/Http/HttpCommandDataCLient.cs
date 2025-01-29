using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataCLient : ICommanDataCLient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataCLient(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpClient = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CommandsService"]}", httpClient);
            if ( response.IsSuccessStatusCode )
            {
                Console.WriteLine("sync Post to commanservice was Okay" );
            }
            else
            {
                Console.WriteLine("sync Post to commanservice was NOT Okay");

            }
        }
    }
}
