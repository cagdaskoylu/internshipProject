using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class UsersEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]  
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool isDeleted { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }   
        public ICollection<AddressesEntity> Address { get; set; }
        public ICollection<CarsEntity> Car { get; set; }
        public ICollection<FavStationsEntity> FavStations { get; set; }
    }
}
