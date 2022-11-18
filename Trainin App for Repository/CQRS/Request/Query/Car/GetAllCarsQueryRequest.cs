using MediatR;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Query.Car
{
    public class GetAllCarsQueryRequest : IRequest<ResponseBase<List<CarDto>>>
    {

    }
}
