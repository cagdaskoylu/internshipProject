using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.Car;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.Car
{
    public class GetCarsByTokenQueryHandler : IRequestHandler<GetCarsByTokenQueryRequest, ResponseBase<List<CarDto>>>
    {
        private readonly ICarsRepository _carRepository;
        private readonly IMapper _mapper;
        public GetCarsByTokenQueryHandler(ICarsRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase<List<CarDto>>> Handle(GetCarsByTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var info = handler.ReadJwtToken(request.Token);
            var id = info.Claims.First(c => c.Type == "nameid").Value;

            var response = new ResponseBase<List<CarDto>>();
            var cars = await _carRepository.GetByUserId(Guid.Parse(id));


            if (cars.Count == 0)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Liste boş";

            }
            else
            {
                var carList = _mapper.Map<IEnumerable<CarsEntity>, IEnumerable<CarDto>>(cars);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Araçlar getirildi";
                response.Data = carList.ToList();
            }
            return response;
        }
    }
}
