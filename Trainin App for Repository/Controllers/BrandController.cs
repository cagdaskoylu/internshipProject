using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request;

namespace Trainin_App_for_Repository.Controllers
{
    [Route("brandManuelUpdate")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrands([FromQuery] UpdateBrandsManualQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result); 
        }
    }
}
