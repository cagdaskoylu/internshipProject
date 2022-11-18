using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Car;
using Trainin_App_for_Repository.CQRS.Request.Query.Car;

namespace Trainin_App_for_Repository.Controllers
{
    [Route("car")]
    [ApiController]
    public class CarController: ControllerBase
    {
        private readonly IMediator _mediator;
        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllCarsQueryRequest request)
        {
            var getAllResult = await _mediator.Send(request);
            return Ok(getAllResult);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] GetCarByIdQueryRequest request)
        {
            var getByIdResult = await _mediator.Send(request);
            return Ok(getByIdResult);
        }

        [HttpGet("getByToken")]
        public async Task<IActionResult> GetByUserId([FromQuery] GetCarsByTokenQueryRequest request)
        {
            var getByIdResult = await _mediator.Send(request);
            return Ok(getByIdResult);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCarCommand request)
        {
            var deleteQueryResult = await _mediator.Send(request);
            return Ok(deleteQueryResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
