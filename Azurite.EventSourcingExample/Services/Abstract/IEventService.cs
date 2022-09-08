namespace Azurite.EventSourcingExample.Services.Abstract
{
    public interface IEventService
    {
        Task<bool> PersistAsync(Guid id, string @event, object data);
    }
}
