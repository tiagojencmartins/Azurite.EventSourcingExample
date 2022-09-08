using Azurite.EventSourcingExample.Mappers;
using Azurite.EventSourcingExample.Models;
using Azurite.EventSourcingExample.Services.Abstract;
using MediatR;

namespace Azurite.EventSourcingExample.Commands
{
    public class CreateUserCommand : IRequest<Guid?>
    {
        public CreateUserRequest User { get; }

        private CreateUserCommand(CreateUserRequest user)
        {
            User = user;
        }

        public static CreateUserCommand Create(CreateUserRequest user)
        {
            return new CreateUserCommand(user);
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid?>
    {
        private const string CreateUserEvent = nameof(CreateUserEvent);

        private readonly IEventService _eventService;

        public CreateUserCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<Guid?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UserMapper.ToDomain(request.User);

            // Some repository magic here
            user.Id = Guid.NewGuid();

            try
            {
                if (!await _eventService.PersistAsync(user.Id, CreateUserEvent, user))
                {
                    return null;
                }
            }
            catch
            {
                return null;
                // Log service magic
            }

            return user.Id;
        }
    }
}
