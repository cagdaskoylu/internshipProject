using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.User;
using Trainin_App_for_Repository.CQRS.Response.Command.User;
using Trainin_App_for_Repository.CQRS.Response.Query.User;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;
using Trainin_App_for_Repository.Security;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.User
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQueryRequest, ResponseBase<UserLogin>>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public UserLoginQueryHandler(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<UserLogin>> Handle(UserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<UserLogin>();
            var check = _mapper.Map<UserLoginQueryRequest, UsersEntity>(request);
            var user = await _userRepository.Login(check);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Boyle bir kullanici bulunamadi";
            }
            else if ((user.Email == request.Email && user.Password != request.Password ))
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Sifre hatali";
            }
            
            else if ((user.Email == request.Email && user.Password == request.Password))
            {
                var _token = _mapper.Map<UsersEntity, UserToken>(user);
                var token = CreatingToken.CreateTokenRegister(_token);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Giris basarili";
                response.Token = token;
                response.Data = _mapper.Map<UsersEntity, UserLogin>(user);
            }
            return response;
        }
    }
}
