using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command.Address;
using Trainin_App_for_Repository.Repository.Address;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.Address
{
    public class DeleteAddressCommandHandler: IRequestHandler<DeleteAddressCommand, ResponseBase>
    {
        private readonly IAddressRepository _addressRepository;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<ResponseBase> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _addressRepository.GetById(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Adres Bulunamadi";
            }
            else
            {
                await _addressRepository.Delete(request.Id);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = " Silme Islemi Basarili";
            }

            return response;
        }
    }
}
