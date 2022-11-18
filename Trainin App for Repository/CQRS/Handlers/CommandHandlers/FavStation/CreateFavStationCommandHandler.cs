using AutoMapper;
using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.CQRS.Response.Command;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.FavStation;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.FavStation
{
    public class CreateFavStationCommandHandler : IRequestHandler<CreateFavStationCommand, ResponseBase>
    {

        private readonly IFavStationsRepository _favStationsRepostisory;
        private readonly IMapper _mapper;
        public CreateFavStationCommandHandler(IFavStationsRepository favStationsRepostisory, IMapper mapper)
        {
            _favStationsRepostisory = favStationsRepostisory;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(CreateFavStationCommand request, CancellationToken cancellationToken)
        {

            var handler = new JwtSecurityTokenHandler();
            var info = handler.ReadJwtToken(request.Token);
            var id = info.Claims.First(c => c.Type == "nameid").Value;

            var responseBase = new ResponseBase();
            FavStationCreate favStationCreate = new FavStationCreate();
            favStationCreate.UsersEntityId = Guid.Parse(id);
            favStationCreate.SpecificName = request.SpecificName;
            favStationCreate.Brand = request.Brand;
            favStationCreate.Lat = request.Lat;
            favStationCreate.Lng = request.Lng;
            favStationCreate.LpgPrice = request.LpgPrice;
            favStationCreate.GasolineKatkiliPrice = request.GasolineKatkiliPrice;
            favStationCreate.GasolinePrice = request.GasolinePrice;
            favStationCreate.DieselKatkiliPrice = request.DieselKatkiliPrice;
            favStationCreate.DieselPrice = request.DieselPrice;

            await _favStationsRepostisory.Post(_mapper.Map<FavStationCreate, FavStationsEntity>(favStationCreate));
            responseBase.Success = true;
            responseBase.StatusCode = 200;
            responseBase.Message = "Kayit basarili";
            return responseBase;
        }
    }
}
