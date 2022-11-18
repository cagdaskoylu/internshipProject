using MediatR;
using System;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Query.Car
{
    public class GetCarsByTokenQueryRequest : IRequest<ResponseBase<List<CarDto>>>
    {
        public string Token { get; set; }   
    }
}
