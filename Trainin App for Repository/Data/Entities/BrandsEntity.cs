using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class BrandsEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string City { get; set; }    
        public string District { get; set; }    
        public string LastUpdate { get; set; }
        public double GasolinePrice { get; set; }
        public double GasolineKatkiliPrice { get; set; }    
        public double DieselPrice { get; set; }
        public double DieselKatkiliPrice { get; set; }
        public double LpgPrice { get; set; }
        [Required]
        public bool isDeleted { get; set; }
    }
}
