using System;

namespace Trainin_App_for_Repository.CQRS.Response.Query.User
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
