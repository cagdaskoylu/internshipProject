using System;

namespace Trainin_App_for_Repository.Data.DTO
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        /* icon icin JPEG dosyasi*/
        public string FuelType { get; set; }
        public bool IsFav { get; set; }
    }
}
