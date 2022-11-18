using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.Car;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.Car
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQueryRequest, ResponseBase<CarDto>>
    {
        private readonly ICarsRepository _carRepository;
        private readonly IMapper _mapper;
        public GetCarByIdQueryHandler(ICarsRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<CarDto>> Handle(GetCarByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<CarDto>();
            var car = await _carRepository.GetById(request.Id);


            if (car == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Araç Bulunamadı";

            }
            else
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Araç getirildi";
                response.Data = _mapper.Map<CarsEntity, CarDto>(car);
            }
            return response;
        }
    }
}
