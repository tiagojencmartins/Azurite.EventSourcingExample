namespace Azurite.EventSourcingExample.Entities
{
    public class User : BaseEntity
    {
        public string? Company { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }
    }
}
