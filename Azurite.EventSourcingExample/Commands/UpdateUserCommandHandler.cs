using Azurite.EventSourcingExample.Mappers;
using Azurite.EventSourcingExample.Models;
using Azurite.EventSourcingExample.Services.Abstract;
using MediatR;

namespace Azurite.EventSourcingExample.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UpdateUserRequest User { get; }

        private UpdateUserCommand(UpdateUserRequest user)
        {
            User = user;
        }

        public static UpdateUserCommand Create(UpdateUserRequest user)
        {
            return new UpdateUserCommand(user);
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private const string UpdateUserEvent = nameof(UpdateUserEvent);

        private readonly IEventService _eventService;

        public UpdateUserCommandHandler(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UserMapper.ToDomain(request.User);

            // Some repository magic here
            user.Id = Guid.NewGuid();

            try
            {
                if (!await _eventService.PersistAsync(user.Id, UpdateUserEvent, user))
                {
                    return false;
                }
            }
            catch
            {
                return false;
                // Log service magic
            }

            return true;
        }
    }
}
