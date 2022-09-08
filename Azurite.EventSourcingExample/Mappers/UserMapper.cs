using Azurite.EventSourcingExample.Entities;
using Azurite.EventSourcingExample.Models;

namespace Azurite.EventSourcingExample.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(BaseUserRequest from)
        {

            return new User
            {
                Company = from.Company,
                Age = from.Age,
                Name = from.Name,
            };
        }
    }
}
