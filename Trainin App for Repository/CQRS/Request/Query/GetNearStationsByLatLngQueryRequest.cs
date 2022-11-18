using MediatR;
using System.Collections.Generic;
using Trainin_App_for_Repository.CQRS.Response.Query;

namespace Trainin_App_for_Repository.CQRS.Request.Query
{
    public class GetNearStationsByLatLngQueryRequest : IRequest<ResponseBase<List<Station>>>
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
