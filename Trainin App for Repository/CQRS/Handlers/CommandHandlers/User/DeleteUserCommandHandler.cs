using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseBase>
    {
        private readonly IUsersRepository _usersRepository;

        public DeleteUserCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ResponseBase> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _usersRepository.GetById(request.Id);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Kullanici Bulunamadi";
            }
            else
            {
                await _usersRepository.Delete(request.Id);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = " Silme Islemi Basarili";
            }

            return response;

        }

    }
}
