using MediatR;
using System;
using Trainin_App_for_Repository.Data.DTO;


namespace Trainin_App_for_Repository.CQRS.Request.Command
{
    public class UpdateUserCommand : IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}
