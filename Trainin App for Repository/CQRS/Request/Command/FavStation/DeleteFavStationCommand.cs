using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.FavStation
{
    public class DeleteFavStationCommand : IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
    }
}
