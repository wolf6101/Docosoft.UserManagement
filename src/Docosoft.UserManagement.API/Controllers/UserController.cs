using System.Net;

using Docosoft.UserManagement.Application.Users;
using Docosoft.UserManagement.Domain.SeedWork;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Docosoft.UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userDto = await _mediator.Send(new GetUserQuery(id));

            if (userDto == null) return NotFound();

            return Ok(userDto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
        {
            var list = await _mediator.Send(query);

            if (list == null || list.Count == 0) return NotFound();

            return Ok(list);
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

        [HttpPut]
        [Route("", Name = "UpdateUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.ErrorOccured) return Conflict(response.Message);
            if (response.ResourceCreated) return CreatedAtRoute("CreateUser", new { id = response.EntityDto.Id }, response.EntityDto);

            return Ok(response.EntityDto);
        }

        [HttpDelete]
        [Route("", Name = "DeleteUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand command)
        {
            var responseDto = await _mediator.Send(command);
            if (responseDto.ErrorOccured) return BadRequest(responseDto.Error);
            if (responseDto.EntityDto == null) return NoContent();

            return Ok(responseDto.EntityDto);
        }
    }
}