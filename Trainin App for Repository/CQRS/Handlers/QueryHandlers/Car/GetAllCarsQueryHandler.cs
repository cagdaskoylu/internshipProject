using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.Car;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.Car
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQueryRequest, ResponseBase<List<CarDto>>>
    {
        private readonly ICarsRepository _carRepository;
        private readonly IMapper _mapper;
        public GetAllCarsQueryHandler(ICarsRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<CarDto>>> Handle(GetAllCarsQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<List<CarDto>>();
            var cars = await _carRepository.GetAll();


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
