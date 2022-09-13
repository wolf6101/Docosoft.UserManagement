using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Docosoft.UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Ok(true);
        }
    }
}