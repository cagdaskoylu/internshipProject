using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, ResponseBase<UserDto>>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<UserDto>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<UserDto>();
            var user = await _userRepository.GetById(request.Id);


            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Kullanıcı Bulunamadı";

            }
            else
            {
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Kullanıcı getirildi";
                response.Data = _mapper.Map<UsersEntity, UserDto>(user);
            }
            return response;
        }
    }
}
