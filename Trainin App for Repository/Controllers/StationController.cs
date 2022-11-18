using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.CQRS.Request.Command.FavStation;
using Trainin_App_for_Repository.CQRS.Request.Query;
using Trainin_App_for_Repository.CQRS.Request.Query.FavStation;

namespace Trainin_App_for_Repository.Controllers
{
    [Route("station")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getNearStations")]
        public async Task<IActionResult> GetNearStations([FromQuery] GetNearStationsByLatLngQueryRequest request)
        {
            var getAllResult = await _mediator.Send(request);
            return Ok(getAllResult);
        }

        [HttpGet("getFavStations")]
        public async Task<IActionResult> GetAllFavStations([FromQuery] GetAllFavStationsQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpPost("addFavStation")]
        public async Task<IActionResult> AddFavStation([FromBody] CreateFavStationCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("deleteFavStation")]
        public async Task<IActionResult> Delete([FromBody] DeleteFavStationCommand request)
        {
            var deleteQueryResult = await _mediator.Send(request);
            return Ok(deleteQueryResult);
        }
    }
}
