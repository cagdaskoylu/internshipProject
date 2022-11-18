using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using RestSharp;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request;
using Trainin_App_for_Repository.Data.DTO.Brand;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Brands;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers
{
    public class UpdateBrandsManuelQueryHandler : IRequestHandler<UpdateBrandsManualQueryRequest, ResponseBase>
    {
        private readonly IBrandsRepository _brandsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateBrandsManuelQueryHandler(IBrandsRepository brandsRepository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _brandsRepository = brandsRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UpdateBrandsManuelQueryHandler>();
        }

        public async Task<ResponseBase> Handle(UpdateBrandsManualQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _brandsRepository.DeleteAllOldBrands();
                await _brandsRepository.SoftDeleteAll();

                string apiKey = "key";

                var cities = await _brandsRepository.GetAllCitiesNames();

                foreach (var city in cities)
                {
                    Encoding srcEncoding = Encoding.UTF8;
                    Encoding destEncoding = Encoding.GetEncoding(1252);
                    var cityName = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(city)));
                    string normalizedCity = cityName.Normalize(NormalizationForm.FormD);
                    StringBuilder resultCity = new StringBuilder();

                    foreach (var item in normalizedCity)
                    {
                        if (!CharUnicodeInfo.GetUnicodeCategory(item).Equals(UnicodeCategory.NonSpacingMark))
                        {
                            resultCity.Append(item);
                        }
                    }
                    cityName = resultCity.ToString().ToLower();
                    cityName = cityName.Replace('ı', 'i');
                    if (cityName == "afyon")
                    {
                        cityName = "afyonkarahisar";
                    }

                    var cityId = await _brandsRepository.GetCityIdByName(city);
                    var districts = await _brandsRepository.GetDistrictById(cityId);

                    var clientLpg = new RestClient("https://api.collectapi.com/gasPrice/turkeyLpg?city=" + cityName);
                    var requestLpg = new RestRequest();
                    requestLpg.AddHeader("authorization", apiKey);
                    requestLpg.AddHeader("content-type", "application/json");
                    var responseLpg = clientLpg.Execute(requestLpg);

                    foreach (var district in districts)
                    {
                        var districtName = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(district)));
                        string normalizedDistrict = districtName.Normalize(NormalizationForm.FormD);
                        StringBuilder resultDistrict = new StringBuilder();

                        foreach (var item in normalizedDistrict)
                        {
                            if (!CharUnicodeInfo.GetUnicodeCategory(item).Equals(UnicodeCategory.NonSpacingMark))
                            {
                                resultDistrict.Append(item);
                            }
                        }
                        districtName = resultDistrict.ToString().ToLower();
                        districtName = districtName.Replace('ı', 'i');
                        var clientGasoline = new RestClient("https://api.collectapi.com/gasPrice/turkeyGasoline?district=" + districtName + "&city=" + cityName);
                        var requestGasoline = new RestRequest();
                        requestGasoline.AddHeader("authorization", apiKey);
                        requestGasoline.AddHeader("content-type", "application/json");
                        var responseGasoline = clientGasoline.Execute(requestGasoline);

                        var clientDiesel = new RestClient("https://api.collectapi.com/gasPrice/turkeyDiesel?district=" + districtName + "&city=" + cityName);
                        var requestDiesel = new RestRequest();
                        requestDiesel.AddHeader("authorization", apiKey);
                        requestDiesel.AddHeader("content-type", "application/json");
                        var responseDiesel = clientDiesel.Execute(requestDiesel);

                        var options = new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        var jsonLpg = responseLpg.Content;
                        var resultLpg = JsonSerializer.Deserialize<BrandGetLpgInfoDto>(jsonLpg, options);

                        var jsonGasoline = responseGasoline.Content;
                        var resultGasoline = JsonSerializer.Deserialize<BrandGetGasolineInfoDto>(jsonGasoline, options);

                        var jsonDiesel = responseDiesel.Content;
                        var resultDiesel = JsonSerializer.Deserialize<BrandGetDieselInfoDto>(jsonDiesel, options);

                        foreach (var lpg in resultLpg.Result)
                        {
                            if (lpg.Marka == null || lpg.Lpg == null)
                            {
                                if (lpg.Marka == null)
                                {
                                    lpg.Marka = "";
                                }
                                if (lpg.Lpg == null)
                                {
                                    lpg.Lpg = "0,0";
                                }
                            }
                            double priceResultLpg = 0;
                            double.TryParse(lpg.Lpg.ToString(), out priceResultLpg);
                            var markaLpg = lpg.Marka.ToString();
                            var fiyatLpg = priceResultLpg;
                            //_logger.LogInformation(fiyatLpg.ToString());
                            var checkBrand = await _brandsRepository.GetBrandByNameAndCity(markaLpg, cityName);
                            if (checkBrand.Count == 0)
                            {
                                var brand = new BrandCreateDto();
                                brand.Name = markaLpg;
                                brand.City = cityName;
                                brand.LpgPrice = fiyatLpg;
                                brand.LastUpdate = resultLpg.Lastupdate;
                                var brandDto = _mapper.Map<BrandCreateDto, BrandsEntity>(brand);
                                await _brandsRepository.Post(brandDto);
                            }
                            else
                            {
                                foreach (var oldBrand in checkBrand)
                                {
                                    var brandLpg = new BrandCreateDto();
                                    brandLpg.Name = oldBrand.Name;
                                    brandLpg.City = oldBrand.City;
                                    brandLpg.District = oldBrand.District;
                                    brandLpg.LpgPrice = fiyatLpg;
                                    brandLpg.GasolinePrice = oldBrand.GasolinePrice;
                                    brandLpg.GasolineKatkiliPrice = oldBrand.GasolineKatkiliPrice;
                                    brandLpg.DieselPrice = oldBrand.DieselPrice;
                                    brandLpg.DieselKatkiliPrice = oldBrand.DieselKatkiliPrice;
                                    brandLpg.LastUpdate = resultLpg.Lastupdate;
                                    await _brandsRepository.DeleteBrand(oldBrand);
                                    var newBrand = _mapper.Map<BrandCreateDto, BrandsEntity>(brandLpg);
                                    await _brandsRepository.Post(newBrand);
                                }
                            }
                            if (markaLpg == "Aygaz")
                            {
                                markaLpg = "Opet";
                                var checkBrandOpet = await _brandsRepository.GetBrandByNameAndCity(markaLpg, cityName);
                                if (checkBrandOpet != null)
                                {
                                    foreach (var oldBrand in checkBrandOpet)
                                    {
                                        var brandLpg = new BrandCreateDto();
                                        brandLpg.Name = oldBrand.Name;
                                        brandLpg.City = oldBrand.City;
                                        brandLpg.District = oldBrand.District;
                                        brandLpg.LpgPrice = fiyatLpg;
                                        brandLpg.GasolinePrice = oldBrand.GasolinePrice;
                                        brandLpg.GasolineKatkiliPrice = oldBrand.GasolineKatkiliPrice;
                                        brandLpg.DieselPrice = oldBrand.DieselPrice;
                                        brandLpg.DieselKatkiliPrice = oldBrand.DieselKatkiliPrice;
                                        brandLpg.LastUpdate = resultLpg.Lastupdate;
                                        await _brandsRepository.DeleteBrand(oldBrand);
                                        var newBrand = _mapper.Map<BrandCreateDto, BrandsEntity>(brandLpg);
                                        await _brandsRepository.Post(newBrand);
                                    }
                                }
                            }
                        }
                        foreach (var gasoline in resultGasoline.Result)
                        {
                            if (gasoline.Marka == null || gasoline.Benzin == null || gasoline.Katkili == null)
                            {
                                if (gasoline.Marka == null)
                                {
                                    gasoline.Marka = "";
                                }
                                if (gasoline.Benzin == null)
                                {
                                    gasoline.Benzin = "0,0";
                                }
                                if (gasoline.Katkili == null)
                                {
                                    gasoline.Katkili = "0,0";
                                }
                            }
                            var markaGasoline = gasoline.Marka.ToString();
                            double priceResultGasoline = 0;
                            double.TryParse(gasoline.Benzin.ToString().Replace('.', ','), out priceResultGasoline);
                            var fiyatGasoline = priceResultGasoline;
                            var fiyatGasolineKatkili = 0.0;
                            //_logger.LogInformation(fiyatGasolineKatkili.ToString());
                            var oldBrand = await _brandsRepository.GetBrandByNameAndCityAndDistrict(markaGasoline, cityName, districtName);
                            if (oldBrand == null)
                            {
                                var newBrand = new BrandCreateDto();
                                newBrand.City = cityName;
                                newBrand.District = districtName;
                                newBrand.Name = markaGasoline;
                                newBrand.GasolinePrice = fiyatGasoline;
                                if (gasoline.Katkili.ToString() == "-")
                                {
                                    gasoline.Katkili = "0,0";
                                }
                                double priceResultGasolineKatki = 0;
                                double.TryParse(gasoline.Katkili.ToString().Replace('.', ','), out priceResultGasolineKatki);
                                fiyatGasolineKatkili = priceResultGasolineKatki;
                                newBrand.GasolineKatkiliPrice = fiyatGasolineKatkili;
                                newBrand.LastUpdate = resultGasoline.Lastupdate;
                                var newBrandDto = _mapper.Map<BrandCreateDto, BrandsEntity>(newBrand);
                                await _brandsRepository.Post(newBrandDto);
                            }
                            else
                            {
                                var brand = new BrandCreateDto();
                                brand.City = oldBrand.City;
                                brand.District = oldBrand.District;
                                brand.Name = oldBrand.Name;
                                brand.LpgPrice = oldBrand.LpgPrice;
                                brand.GasolinePrice = fiyatGasoline;
                                if (gasoline.Katkili.ToString() == "-")
                                {
                                    gasoline.Katkili = 0.0;
                                }
                                double priceResultGasolineKatki = 0;
                                double.TryParse(gasoline.Katkili.ToString().Replace('.', ','), out priceResultGasolineKatki);
                                fiyatGasolineKatkili = priceResultGasolineKatki;
                                brand.GasolineKatkiliPrice = fiyatGasolineKatkili;
                                brand.DieselPrice = oldBrand.DieselPrice;
                                brand.DieselKatkiliPrice = oldBrand.DieselKatkiliPrice;
                                brand.LastUpdate = resultGasoline.Lastupdate;

                                await _brandsRepository.DeleteBrand(oldBrand);
                                var newBrand = _mapper.Map<BrandCreateDto, BrandsEntity>(brand);
                                await _brandsRepository.Post(newBrand);
                            }
                        }
                        foreach (var diesel in resultDiesel.Result)
                        {
                            if (diesel.Marka == null || diesel.Dizel == null || diesel.Katkili == null)
                            {
                                if (diesel.Marka == null)
                                {
                                    diesel.Marka = "";
                                }
                                if (diesel.Dizel == null)
                                {
                                    diesel.Dizel = "0,0";
                                }
                                if (diesel.Katkili == null)
                                {
                                    diesel.Katkili = "0,0";
                                }
                            }
                            var markaDiesel = diesel.Marka.ToString();
                            double priceResultDiesel = 0;
                            double.TryParse(diesel.Dizel.ToString().Replace('.', ','), out priceResultDiesel);
                            var fiyatDiesel = priceResultDiesel;
                            var fiyatDieselKatkili = 0.0;
                            //_logger.LogInformation(fiyatDiesel.ToString());
                            var brandOld = await _brandsRepository.GetBrandByNameAndCityAndDistrict(markaDiesel, cityName, districtName);
                            if (brandOld == null)
                            {
                                var newBrand = new BrandCreateDto();
                                newBrand.City = cityName;
                                newBrand.District = districtName;
                                newBrand.Name = markaDiesel;
                                newBrand.DieselPrice = fiyatDiesel;
                                if (diesel.Katkili.ToString() == "-")
                                {
                                    diesel.Katkili = 0.0;
                                }
                                double priceResultDieselKatkili = 0;
                                double.TryParse(diesel.Katkili.ToString().Replace('.', ','), out priceResultDieselKatkili);
                                fiyatDieselKatkili = priceResultDieselKatkili;
                                newBrand.DieselKatkiliPrice = fiyatDieselKatkili;
                                newBrand.LastUpdate = resultDiesel.Lastupdate;
                                var newBrandDto = _mapper.Map<BrandCreateDto, BrandsEntity>(newBrand);
                                await _brandsRepository.Post(newBrandDto);
                            }
                            else
                            {
                                var brand = new BrandCreateDto();
                                brand.City = brandOld.City;
                                brand.District = brandOld.District;
                                brand.Name = brandOld.Name;
                                brand.LpgPrice = brandOld.LpgPrice;
                                brand.GasolinePrice = brandOld.GasolinePrice;
                                brand.GasolineKatkiliPrice = brandOld.GasolineKatkiliPrice;

                                brand.DieselPrice = fiyatDiesel;
                                if (diesel.Katkili.ToString() == "-")
                                {
                                    diesel.Katkili = 0.0;
                                }
                                double priceResultDieselKatkili = 0;
                                double.TryParse(diesel.Katkili.ToString().Replace('.', ','), out priceResultDieselKatkili);
                                fiyatDieselKatkili = priceResultDieselKatkili;
                                brand.DieselKatkiliPrice = fiyatDieselKatkili;
                                brand.LastUpdate = resultDiesel.Lastupdate;
                                await _brandsRepository.DeleteBrand(brandOld);
                                var newBrand = _mapper.Map<BrandCreateDto, BrandsEntity>(brand);
                                await _brandsRepository.Post(newBrand);
                            }
                        }
                    }
                }
                var response = new ResponseBase();
                response.Success = true;
                response.Message = "Ok";
                response.StatusCode = 200;
                return response;
            }
            catch
            {
                var response = new ResponseBase();
                response.Success = false;
                response.Message = "Basarisiz";
                response.StatusCode = 400;
                return response;
            }
        }
    }
}
