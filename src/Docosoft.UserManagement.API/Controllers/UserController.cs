using System.Net;

using Docosoft.UserManagement.Application.Users;
using Docosoft.UserManagement.Domain.SeedWork;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Docosoft.UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userDto = await _mediator.Send(new GetUserQuery(id));

            if (userDto == null) return NotFound();

            return Ok(userDto);
        }

        [HttpPost]
        [Route("", Name = "CreateUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.ErrorOccured) return BadRequest(response.Message);

            return CreatedAtRoute("CreateUser", new { id = response.EntityDto.Id }, response.EntityDto);
        }
    }
}