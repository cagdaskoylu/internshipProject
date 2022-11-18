using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.FavStation;
using Trainin_App_for_Repository.Repository.FavStation;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.FavStation
{
    public class DeleteFavStationCommandHandler : IRequestHandler<DeleteFavStationCommand, ResponseBase>

    {
        private readonly IFavStationsRepository _favStationRepository;
        public DeleteFavStationCommandHandler(IFavStationsRepository favStationRepository)
        {
            _favStationRepository = favStationRepository;
        }

        public async Task<ResponseBase> Handle(DeleteFavStationCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _favStationRepository.GetById(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Istasyon Bulunamadi";
            }
            else
            {
                await _favStationRepository.Delete(request.Id);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = " Silme Islemi Basarili";
            }

            return response;
        }
    }
}
