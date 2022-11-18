using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.Address
{
    public class DeleteAddressCommand: IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
    }
}
