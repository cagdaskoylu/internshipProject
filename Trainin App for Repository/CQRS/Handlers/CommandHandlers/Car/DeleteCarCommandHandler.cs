using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS;
using Trainin_App_for_Repository.CQRS.Request.Command.Car;
using Trainin_App_for_Repository.Repository.Cars;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Car
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, ResponseBase>
    {
        private readonly ICarsRepository _carRepository;
        public DeleteCarCommandHandler(ICarsRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<ResponseBase> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _carRepository.GetById(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Arac Bulunamadi";
            }
            else
            {
                await _carRepository.Delete(request.Id);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = " Silme Islemi Basarili";
            }
            return response;
        }
    }
}

