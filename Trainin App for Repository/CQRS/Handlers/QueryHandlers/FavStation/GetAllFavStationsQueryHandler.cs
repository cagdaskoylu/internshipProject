using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.FavStation;
using Trainin_App_for_Repository.CQRS.Response.Query;
using Trainin_App_for_Repository.CQRS.Response.Query.FavStation;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.FavStation;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.FavStation
{
    public class GetAllFavStationsQueryHandler : IRequestHandler<GetAllFavStationsQueryRequest, ResponseBase<List<FavStationResponse>>>
    {
        private readonly IFavStationsRepository _favStationRepository;
        private readonly IMapper _mapper;
        public GetAllFavStationsQueryHandler(IFavStationsRepository favStationsRepository, IMapper mapper)
        {
            _favStationRepository = favStationsRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase<List<FavStationResponse>>> Handle(GetAllFavStationsQueryRequest request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var info = handler.ReadJwtToken(request.Token);
            var id = info.Claims.First(c => c.Type == "nameid").Value;

            var response = new ResponseBase<List<FavStationResponse>>();
            var stations = await _favStationRepository.GetByUserId(Guid.Parse(id));


            if (stations.Count == 0)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Liste boş";

            }
            else
            {
                var favStationList = _mapper.Map<IEnumerable<FavStationsEntity>, IEnumerable<FavStationResponse>>(stations);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Favori istasyonlar getirildi";
                response.Data = favStationList.ToList();
            }
            return response;
        }
    }
}
