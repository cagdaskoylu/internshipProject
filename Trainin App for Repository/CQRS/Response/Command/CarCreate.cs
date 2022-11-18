using System;

namespace Trainin_App_for_Repository.CQRS.Response.Command
{
    public class CarCreate
    {
        public Guid UsersEntityId { get; set; }
        public string Tag { get; set; }
        /* icon icin JPEG dosyasi*/
        public string FuelType { get; set; }
        public bool IsFav { get; set; }
    }
}
