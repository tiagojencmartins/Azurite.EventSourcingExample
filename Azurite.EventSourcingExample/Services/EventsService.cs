using Azurite.EventSourcingExample.Services.Abstract;
using Newtonsoft.Json;

namespace Azurite.EventSourcingExample.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> PersistAsync(Guid id, string @event, object data)
        {
            var result = await _httpClient.PostAsJsonAsync(
                $"/api/user/{id}",
                new
                {
                    Event = @event,
                    Payload = JsonConvert.SerializeObject(data)
                });

            return result.IsSuccessStatusCode;
        }
    }
}
