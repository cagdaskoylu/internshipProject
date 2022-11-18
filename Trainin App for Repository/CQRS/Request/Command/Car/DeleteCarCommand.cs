using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.Car
{
    public class DeleteCarCommand: IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
    }
}
