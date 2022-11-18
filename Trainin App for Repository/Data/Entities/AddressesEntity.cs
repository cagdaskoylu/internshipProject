using System;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class AddressesEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Tag { get; set; }
        [Required]
        public string Detail { get; set; }
        //public string City { get; set; }
        //[Required]
        //public string District { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lng { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool IsFav { get; set; }
        [Required]
        public virtual Guid UsersEntityId { get; set; }
    }

}
