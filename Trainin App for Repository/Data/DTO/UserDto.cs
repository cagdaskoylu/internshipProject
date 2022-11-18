using System;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Data.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Surname { get; set; }
        public string Password { get; set; }

    }
}
