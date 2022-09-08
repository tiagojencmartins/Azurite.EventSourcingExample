using Azure;
using Azure.Data.Tables;
using System;

namespace UserEventStorage.Models
{
    public sealed class Entity : ITableEntity
    {
        private Entity()
        {
        }

        public string PartitionKey { get; set; }

        public string Event { get; set; }

        public string Payload { get; set; }

        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public DateTimeOffset? Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public ETag ETag { get; set; } = ETag.All;

        public static Entity Create(string id, string @event, string payload)
        {
            return new Entity
            {
                PartitionKey = id,
                Event = @event,
                Payload = payload
            };
        }
    }
}
