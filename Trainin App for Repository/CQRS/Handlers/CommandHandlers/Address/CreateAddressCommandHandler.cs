using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Address;
using Trainin_App_for_Repository.CQRS.Response.Command;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Address;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Address
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ResponseBase>
    {

        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {

            var handler = new JwtSecurityTokenHandler();
            var info = handler.ReadJwtToken(request.Token);
            var id = info.Claims.First(c => c.Type == "nameid").Value;
            var responseBase = new ResponseBase();
            AddressCreate addressCreateDto = new AddressCreate();
            addressCreateDto.UsersEntityId = Guid.Parse(id);
            addressCreateDto.Tag = request.Tag;
            addressCreateDto.Detail = request.Detail;
            addressCreateDto.Lat = request.Lat; 
            addressCreateDto.Lng = request.Lng;
            if (request.WillBeFav == true) 
            {
                addressCreateDto.IsFav = true;
                var oldFav = await _addressRepository.GetFav();
                if (oldFav != null)
                {
                    oldFav.IsFav = false;
                    await _addressRepository.Put(oldFav);
                }
            }
            else
            {
                addressCreateDto.IsFav = false;
            }
            await _addressRepository.Post(_mapper.Map<AddressCreate, AddressesEntity>(addressCreateDto));
            responseBase.Success = true;
            responseBase.StatusCode = 200;
            responseBase.Message = "Kayit basarili";
            return responseBase;
        }
    }
}
