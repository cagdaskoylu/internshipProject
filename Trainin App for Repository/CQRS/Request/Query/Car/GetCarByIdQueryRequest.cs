using MediatR;
using System;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Query.Car
{
    public class GetCarByIdQueryRequest : IRequest<ResponseBase<CarDto>>
    {
        public Guid Id { get; set; }
    }
}
