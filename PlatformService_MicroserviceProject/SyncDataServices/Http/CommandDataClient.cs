using PlatformService_MicroserviceProject.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            Console.WriteLine($"Request URI: {_config["CommandService"]}");
            //payload, sending a platformreaddto to the commands service
            //we are sending the read dto because we want to include the id
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json");
            //post request to command service, hard coded for now
            var response = await _httpClient.PostAsync(_config["CommandService"], httpContent);
            if(response.IsSuccessStatusCode)
                Console.WriteLine("Sync POST to Command Service was OK! :D");
            else
                Console.WriteLine("Sync POST to Command Service was BAD! D:");

        }
    }
}
