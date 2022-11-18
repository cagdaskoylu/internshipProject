using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseBase>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _userRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _userRepository.FindByAsync(x => x.Id == request.Id); 
            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Kullanici bulunamadi";
            }
            else if(user.Name == request.Name && user.Surname == request.Surname && user.Password == request.Password)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Herhangi bir değişiklik yapılmadı";
            }
            else 
            {
                user = _mapper.Map<UpdateUserCommand, UsersEntity>(request);
                await _userRepository.Put(user);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Guncellendi";
            }
            return response;
        }

    }
}
