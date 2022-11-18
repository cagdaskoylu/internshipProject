using System;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class CarsEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Tag { get; set; }
        /* icon icin JPEG dosyasi*/
        [Required]
        public string FuelType { get; set; }
        [Required]
        public bool IsDeleted { get; set; }  
        [Required]
        public bool IsFav { get; set; }
        [Required]
        public virtual Guid UsersEntityId { get; set; }
    }
}
