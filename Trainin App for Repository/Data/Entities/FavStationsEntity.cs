using System;
using System.ComponentModel.DataAnnotations;

namespace Trainin_App_for_Repository.Data.Entities
{
    public class FavStationsEntity
    {
        [Key]
        public Guid Id { get; set; }
        public virtual Guid UsersEntityId { get; set; }
        public string SpecificName { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lng { get; set; }
        public double LpgPrice { get; set; }
        public double GasolinePrice { get; set; }
        public double GasolineKatkiliPrice { get; set; }
        public double DieselPrice { get; set; }
        public double DieselKatkiliPrice { get; set; }
        public string LastUpdate { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
