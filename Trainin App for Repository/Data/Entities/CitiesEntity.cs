using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class CitiesEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<DistrictsEntity> DistrictsEntity {get; set;}


    }
}
