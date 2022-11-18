using MediatR;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Query.Address
{
    public class GetAllAddressesQueryRequest : IRequest<ResponseBase<List<AddressDto>>>
    {

    }
}
