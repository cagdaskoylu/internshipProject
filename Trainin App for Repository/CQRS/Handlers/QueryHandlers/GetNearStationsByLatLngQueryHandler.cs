using AutoMapper;
using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query;
using Trainin_App_for_Repository.CQRS.Response.Query;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Repository.Brands;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers 
{
    public class GetNearStationsByLatLngQueryHandler : IRequestHandler<GetNearStationsByLatLngQueryRequest, ResponseBase<List<Station>>>
    {
        private readonly IMapper _mapper;
        private readonly IBrandsRepository _brandRepository;

        public GetNearStationsByLatLngQueryHandler(IBrandsRepository brandRepository, IMapper mapper)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<ResponseBase<List<Station>>> Handle(GetNearStationsByLatLngQueryRequest request, CancellationToken cancellationToken)
        {
            var apiKey = "key";
            string lat = request.Lat.ToString();
            string lng = request.Lng.ToString();
            var clientStation = new RestClient("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lat.Replace(',', '.') + "," + lng.Replace(',', '.') +
                                                                        "&radius=15000&type=gas_station&key=" + apiKey);
            var requestStation = new RestRequest();
            var responseStation = clientStation.Execute(requestStation);

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonStation = responseStation.Content;
            var resultStation = JsonSerializer.Deserialize<StationDto>(jsonStation, options);
            var response = new ResponseBase<List<Station>>();
            var stationList = new List<Station>();

            foreach (var station in resultStation.Results)
            {
                string[] nameAndBrand = station.Name.Split(new char[2] { ' ', '-' }, StringSplitOptions.None);
                var clientGeocoding = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?place_id=" + station.place_id + "&key=" + apiKey);
                var requestGeocoding = new RestRequest();
                var responseGeocoding = clientGeocoding.Execute(requestGeocoding);
                var jsonGeocoding = responseGeocoding.Content;
                var resultGeocoding = JsonSerializer.Deserialize<PlaceDto>(jsonGeocoding, options);

                foreach (var place in resultGeocoding.Results)
                {
                    string city = "";
                    string district = "";
                    foreach (var address in place.address_components)
                    {
                        foreach (var type in address.Types)
                        {
                            if (type == "administrative_area_level_1")
                            {
                                Encoding srcEncoding = Encoding.UTF8;
                                Encoding destEncoding = Encoding.GetEncoding(1252);
                                city = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(address.long_name)));
                                string normalized = city.Normalize(NormalizationForm.FormD);
                                StringBuilder result = new StringBuilder();

                                foreach (var item in normalized)
                                {
                                    if (!CharUnicodeInfo.GetUnicodeCategory(item).Equals(UnicodeCategory.NonSpacingMark))
                                    {
                                        result.Append(item);
                                    }
                                }
                                city = result.ToString().ToLower();
                                city = city.Replace('ı', 'i');

                            }
                            if (type == "administrative_area_level_2")
                            {
                                Encoding srcEncoding = Encoding.UTF8;
                                Encoding destEncoding = Encoding.GetEncoding(1252);
                                district = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(address.long_name)));
                                string normalized = district.Normalize(NormalizationForm.FormD);
                                StringBuilder result = new StringBuilder();

                                foreach (var item in normalized)
                                {
                                    if (!CharUnicodeInfo.GetUnicodeCategory(item).Equals(UnicodeCategory.NonSpacingMark))
                                    {
                                        result.Append(item);
                                    }
                                }
                                district = result.ToString().ToLower();
                            }
                        }
                    }

                    for (int i = 0; i < nameAndBrand.Length; i++)
                    {
                        nameAndBrand[i]=nameAndBrand[i].ToLower();  
                    }

                    foreach (var word in nameAndBrand)
                    {
                        var name = "";
                        if (word.Contains("bp") || word.Contains("moil") || word.Contains("milangaz") || word.Contains("shell") || word.Contains("alpet")
                            || word.Contains("ipragaz") || word.Contains("total") || word.Contains("aygaz") || word.Contains("opet") || word.Contains("mogaz")
                            || word.Contains("tp") || word.Contains("türkiye petrolleri") || word.Contains("aytemiz") || word.Contains("po") || word.Contains("petrol")
                            || word.Contains("termo") || word.Contains("lukoil") || word.Contains("akpet"))
                        {
                            if (word == "türkiye petrolleri")
                            {
                                name = "tp";
                            }
                            if (word == "moil")
                            {
                                name = "m oil";
                            }
                            if (word == "po" || word == "petrol")
                            {
                                name = "petrol ofisi";
                            }

                         
                            var brandInfo = await _brandRepository.GetNotDeletedSimilarBrand(name, city, district);
                            if (brandInfo != null)
                            {
                                var stationResponse = new Station();
                                stationResponse.Brand = brandInfo.Name;
                                stationResponse.Lat = station.Geometry.Location.Lat;
                                stationResponse.Lng = station.Geometry.Location.Lng;
                                stationResponse.LpgPrice = brandInfo.LpgPrice;
                                stationResponse.GasolinePrice = brandInfo.GasolinePrice;
                                stationResponse.GasolineKatkiliPrice = brandInfo.GasolineKatkiliPrice;
                                stationResponse.DieselPrice = brandInfo.DieselPrice;
                                stationResponse.DieselKatkiliPrice = brandInfo.DieselKatkiliPrice;
                                stationResponse.LastUpdate = brandInfo.LastUpdate;
                                stationList.Add(stationResponse);
                            }
                            else
                            {
                                var oldBrandInfo = await _brandRepository.GetDeletedSimilarBrand(name, city, district);
                                if (oldBrandInfo != null)
                                {
                                    var stationResponse = new Station();
                                    stationResponse.Brand = oldBrandInfo.Name;
                                    stationResponse.Lat = station.Geometry.Location.Lat;
                                    stationResponse.Lng = station.Geometry.Location.Lng;
                                    stationResponse.LpgPrice = oldBrandInfo.LpgPrice;
                                    stationResponse.GasolinePrice = oldBrandInfo.GasolinePrice;
                                    stationResponse.GasolineKatkiliPrice = oldBrandInfo.GasolineKatkiliPrice;
                                    stationResponse.DieselPrice = oldBrandInfo.DieselPrice;
                                    stationResponse.DieselKatkiliPrice = oldBrandInfo.DieselKatkiliPrice;
                                    stationResponse.LastUpdate = oldBrandInfo.LastUpdate;
                                    stationList.Add(stationResponse);
                                }
                            }
                        }
                    }
                }
            }
            response.StatusCode = 200;
            response.Success = true;
            response.Message = "Yakindaki istasyonlar getirildi";
            response.Data = stationList;
            return response;
        }

    }
}
