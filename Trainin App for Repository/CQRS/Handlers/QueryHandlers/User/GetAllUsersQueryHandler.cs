using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query;
using Trainin_App_for_Repository.Data.DTO;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, ResponseBase<List<UserDto>>>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<List<UserDto>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase<List<UserDto>>();
            var users = await _userRepository.GetAll();


            if (users.Count == 0)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Liste boş";

            }
            else
            {
                var userList = _mapper.Map<IEnumerable<UsersEntity>, IEnumerable<UserDto>>(users);
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Kullanıcılar getirildi";
                response.Data = userList.ToList();
            }
            return response;
        }
    }
}

