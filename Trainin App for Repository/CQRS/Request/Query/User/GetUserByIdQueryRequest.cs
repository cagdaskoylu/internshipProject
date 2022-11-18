using MediatR;
using System;
using Trainin_App_for_Repository.Data.DTO;


namespace Trainin_App_for_Repository.CQRS.Request.Query
{
    public class GetUserByIdQueryRequest: IRequest<ResponseBase<UserDto>>
    {
        public Guid Id { get; set; }    
    }
}
