using PlatformService.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = Convert.ToInt32(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine($"---> Connected to message bus {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---> Could not connect to message bus: {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishDto platformPublishDto)
        {
            var message = JsonSerializer.Serialize(platformPublishDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection open, sending message");
                SendMessage(message);
            }
            else
                Console.WriteLine("--> RabbitMQ Connection is closed, not sending message");
        }
        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange:"trigger",
                routingKey:"",
                basicProperties: null,
                body: body);
            Console.WriteLine($"---> Message Posted: {message}");
        }
        public void Dispose()
        {
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            Console.WriteLine("---> Message Bus Disposed");
        }
        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"---> Connection has been shut down: {DateTime.UtcNow}");
        }
    }
}
