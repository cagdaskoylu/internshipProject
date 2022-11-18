using System;

namespace Trainin_App_for_Repository.CQRS.Response.Command.User
{
    public class UserCreate
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public bool isDeleted { get; set; }

    }
}
