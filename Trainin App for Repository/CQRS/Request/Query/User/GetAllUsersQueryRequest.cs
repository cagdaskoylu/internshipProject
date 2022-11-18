using MediatR;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.DTO;


namespace Trainin_App_for_Repository.CQRS.Request.Query
{
    public class GetAllUsersQueryRequest : IRequest<ResponseBase<List<UserDto>>>
    {
      
    }
}
