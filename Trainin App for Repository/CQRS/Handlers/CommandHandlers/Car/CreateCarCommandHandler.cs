using AutoMapper;
using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Car;
using Trainin_App_for_Repository.CQRS.Response.Command;
using Trainin_App_for_Repository.Data.Entities;

using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Car
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, ResponseBase>
    {

        private readonly ICarsRepository _carRepository;
        private readonly IMapper _mapper;
        public CreateCarCommandHandler(ICarsRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var info = handler.ReadJwtToken(request.Token);
            var id = info.Claims.First(c => c.Type == "nameid").Value;

            var response = new ResponseBase();
            CarCreate carCreateDto = new CarCreate();
            carCreateDto.UsersEntityId = Guid.Parse(id);
            carCreateDto.Tag = request.Tag;
            carCreateDto.FuelType = request.FuelType;

            if (request.WillBeFav == true) 
            {
                carCreateDto.IsFav = true;
                var oldFav = await _carRepository.GetFav();
                if (oldFav != null)
                {
                    oldFav.IsFav = false;
                    await _carRepository.Put(oldFav);
                }
            }
            else
            {
                carCreateDto.IsFav = false;
            }

            await _carRepository.Post(_mapper.Map<CarCreate, CarsEntity>(carCreateDto));
            response.Success = true;
            response.StatusCode = 200;
            response.Message = "Kayit basarili";

            return response;
        }
    }
}
