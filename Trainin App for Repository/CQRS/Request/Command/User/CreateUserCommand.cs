using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.CQRS.Request.Command
{
    public class CreateUserCommand: IRequest<ResponseBase>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }

    }
}
