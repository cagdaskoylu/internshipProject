using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Address;
using Trainin_App_for_Repository.CQRS.Request.Query.Address;

namespace Trainin_App_for_Repository.Controllers
{
    [Route("address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAddressesQueryRequest request)
        {
            var getAllResult = await _mediator.Send(request);
            return Ok(getAllResult);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] GetAddressByIdQueryRequest request)
        {
            var getByIdResult = await _mediator.Send(request);
            return Ok(getByIdResult);
        }

        [HttpGet("getByToken")]
        public async Task<IActionResult> GetByUserId([FromQuery] GetAddressesByTokenQueryRequest request)
        {
            var getByIdResult = await _mediator.Send(request);
            return Ok(getByIdResult);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteAddressCommand request)
        {
            var deleteQueryResult = await _mediator.Send(request);
            return Ok(deleteQueryResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
