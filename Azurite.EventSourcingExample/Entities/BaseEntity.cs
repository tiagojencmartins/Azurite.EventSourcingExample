﻿namespace Azurite.EventSourcingExample.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
