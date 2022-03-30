using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using System.Text.Json;

namespace CommandsService.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DeterminedEvent(message);
            switch(eventType)
            {
                case EventType.PlatformPublished:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }
        private EventType DeterminedEvent(string notifmessage)
        {
            Console.WriteLine("---> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifmessage);
            switch(eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("---> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Unfamilar event type");
                    return EventType.Undeterminded;
            }
        }
        private void addPlatform(string platformPublishedMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if(!repo.ExternalPlatformExists(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine("---> Platform Added!");
                    }
                    else
                        Console.WriteLine("---> Platform already exists");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Could not add Platform to the database {ex.Message}");
                }
            }
        }

    }
    enum EventType
    {
        PlatformPublished,
        Undeterminded
    }
}
