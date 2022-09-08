using Azurite.EventSourcingExample.Commands;
using Azurite.EventSourcingExample.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Azurite.EventSourcingExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken token)
        {
            if (request is null)
            {
                return new BadRequestResult();
            }

            var response = await _mediator.Send(CreateUserCommand.Create(request), token);

            if (response is null)
            {
                return Problem();
            }

            return CreatedAtAction(nameof(Create), new { Id = response });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken token)
        {
            if (request is null)
            {
                return new BadRequestResult();
            }

            var response = await _mediator.Send(UpdateUserCommand.Create(request), token);

            if (!response)
            {
                return Problem();
            }

            return NoContent();
        }
    }
}
