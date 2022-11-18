using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.CQRS.Request.Query;
using Trainin_App_for_Repository.CQRS.Request.Query.User;

namespace Trainin_App_for_Repository.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQueryRequest request)
        {
            var getAllResult = await _mediator.Send(request);
            return Ok(getAllResult);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery]GetUserByIdQueryRequest request)
        {
            var getByIdResult = await _mediator.Send(request);
            return Ok(getByIdResult);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand request)
        {
            var deleteQueryResult = await _mediator.Send(request);
            return Ok(deleteQueryResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] UserLoginQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] UserForgetPasswordQueryRequest request)
        {
            var getAllResult = await _mediator.Send(request);
            return Ok(getAllResult);
        }
    }
}
