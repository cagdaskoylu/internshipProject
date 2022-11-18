using MediatR;
using System.Collections.Generic;
using Trainin_App_for_Repository.CQRS.Response.Query.FavStation;

namespace Trainin_App_for_Repository.CQRS.Request.Query.FavStation
{
    public class GetAllFavStationsQueryRequest : IRequest<ResponseBase<List<FavStationResponse>>>
    {
        public string Token { get; set; }
    }
}
