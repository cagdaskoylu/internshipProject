using System;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Data.DTO.Brand
{
    public class BrandUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public double LpgPrice { get; set; }
        public double GasolinePrice { get; set; }
        public double GasolineKatkiliPrice { get; set; }    
        public double DieselPrice { get; set; }
        public double DieselKatkiliPrice { get; set; }
    }
}
