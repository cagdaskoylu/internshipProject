using MediatR;
using System;
using Trainin_App_for_Repository.Data.DTO;

namespace Trainin_App_for_Repository.CQRS.Request.Command
{
    public class DeleteUserCommand : IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
    }
}
