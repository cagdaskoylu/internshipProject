using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.Address
{
    public class CreateAddressCommand: IRequest<ResponseBase>
    {
        public string Token { get; set; }
        public string Tag { get; set; }
        public string Detail { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        //public string City { get; set; }
        //public string District { get; set; }
        public bool WillBeFav { get; set; }
        //public bool YesOrNo { get; set; }   
    }
}
