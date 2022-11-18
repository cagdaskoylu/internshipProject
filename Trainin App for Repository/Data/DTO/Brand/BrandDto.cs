using System;
using System.Collections.Generic;
using Trainin_App_for_Repository.Data.Entities;

namespace Trainin_App_for_Repository.Data.DTO.Brand
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public double GasolinePrice { get; set; }
        public double KatkiliPrice { get; set; }
        public double DieselPrice { get; set; }
        public double LpgPrice { get; set; }
    }
}

