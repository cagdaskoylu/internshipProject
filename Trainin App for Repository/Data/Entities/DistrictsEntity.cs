using System;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class DistrictsEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual int CitiesEntityId { get; set; }
    }
}
