//using PlatformService.Dtos;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;

//namespace PlatformService.SyncDataServices.Http
//{
//    public class HttpCommandDataCLient : ICommanDataCLient
//    {
//        private readonly HttpClient _httpClient;

//        public HttpCommandDataCLient(HttpClient httpClient)
//        {
//            _httpClient=httpClient;
//        }
//        public Task SendPlatformToCommand(PlatformReadDto platformReadDto)
//        {
//            var httpClient = new StringContent(
//                JsonSerializer.Serialize(platformReadDto),
//                Encoding.UTF8,
//                "application/json");
//            var response = await _httpClient.PostAsync( );
//        }
//    }
//}
