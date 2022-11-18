using AutoMapper;
using MediatR;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Address;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Address;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Address
{
    public class UpdateAddressCommandHandler: IRequestHandler<UpdateAddressCommand, ResponseBase>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var address = await _addressRepository.FindByAsync(x => x.Id == request.Id); 
            if (address == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Adres bulunamadi";
            }
            else if (address.Tag == request.Tag && address.Detail == request.Detail && address.IsFav == request.WillBeFav)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Herhangi bir değişiklik yapılmadı";
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var info = handler.ReadJwtToken(request.Token);
                var id = info.Claims.First(c => c.Type == "nameid").Value;
                address = _mapper.Map<UpdateAddressCommand, AddressesEntity>(request);
                address.UsersEntityId = Guid.Parse(id);

                //var apiKey = "AIzaSyD7kF_8VUFz9tGnoBJkmedBaTgjQmUnRoQ";
                //var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?address=" + request.City + "," + request.District + "&key=" + apiKey);
                //var requestGeo = new RestRequest();
                //var responseGeo = client.Execute(requestGeo);

                //var options = new JsonSerializerOptions()
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                //};

                //var json = responseGeo.Content;
                //var result = JsonSerializer.Deserialize<PlaceDto>(json, options);

                //double lat = 0;
                //double lng = 0;

                //foreach (var place in result.Results)
                //{
                //    lat = place.Geometry.Location.Lat;
                //    lng = place.Geometry.Location.Lng;
                //}

                //address.Lat = lat;
                //address.Lng = lng;

                if (request.WillBeFav == true)
                {
                    address.IsFav = true;
                    var oldFav = await _addressRepository.GetFav();
                    if (oldFav != null)
                    {
                        oldFav.IsFav = false;
                        await _addressRepository.Put(oldFav);
                    }
                }
                else
                {
                    address.IsFav = false;
                }
                await _addressRepository.Put(address);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Guncellendi";
            }
            return response;
        }
    }
}
