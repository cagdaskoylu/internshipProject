using System;

namespace Trainin_App_for_Repository.CQRS.Response.Command
{
    public class AddressCreate
    {
        public Guid UsersEntityId { get; set; }
        public string Tag { get; set; }
        public string Detail { get; set; }
        //public string City { get; set; }
        //public string District { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsFav { get; set; }
    }
}
