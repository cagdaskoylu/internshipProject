using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Command;
using Trainin_App_for_Repository.CQRS.Response.Command.User;
using Trainin_App_for_Repository.Data.Entities;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.CommandHandlers.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseBase>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _userRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseBase();
            var user = await _userRepository.FindByAsync(x => x.Email == request.Email);
            if (user != null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Bu email zaten kullaniliyor";
            }
            else
            {
                UserCreate userCreateDto = new UserCreate();
                userCreateDto.Password = request.Password;
                userCreateDto.Name = request.Name;
                userCreateDto.Surname = request.Surname;
                userCreateDto.Email = request.Email;
                await _userRepository.Post(_mapper.Map<UserCreate, UsersEntity>(userCreateDto));
                response.Success = true;
                response.StatusCode = 200;
                response.Message = "Kayit basarili";
            }
            return response;
        }
    }
}
