using AutoMapper;
using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Car;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Car
{
    public class UpdateCarCommandHandler: IRequestHandler<UpdateCarCommand, ResponseBase>
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IMapper _mapper;
        public UpdateCarCommandHandler(ICarsRepository carsRepository, IMapper mapper)
        {
            _carsRepository = carsRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var car = await _carsRepository.FindByAsync(x => x.Id == request.Id); 
            if (car == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Arac bulunamadi";
            }
            else if (car.Tag == request.Tag && car.FuelType == request.FuelType && car.IsFav == request.WillBeFav)
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

                if (request.WillBeFav == true)
                {
                    car.IsFav = true;
                    var oldFav = await _carsRepository.GetFav();
                    if (oldFav != null)
                    {
                        oldFav.IsFav = false;
                        await _carsRepository.Put(oldFav);
                    }
                }
                else
                {
                    car.IsFav = false;
                }

                car = _mapper.Map<UpdateCarCommand, CarsEntity>(request);
                car.UsersEntityId = Guid.Parse(id);
                await _carsRepository.Put(car);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Guncellendi";
            }
            return response;
        }
    }
}
