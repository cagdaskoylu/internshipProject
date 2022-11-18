using MediatR;
using System;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Query.Address
{
    public class GetAddressByIdQueryRequest : IRequest<ResponseBase<AddressDto>>
    {
        public Guid Id { get; set; }
    }
}
