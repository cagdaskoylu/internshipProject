using System;

namespace Trainin_App_for_Repository.Data.DTO
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string Detail { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool WillBeFav { get; set; }
        public bool IsFav { get; set; }
    }
}
