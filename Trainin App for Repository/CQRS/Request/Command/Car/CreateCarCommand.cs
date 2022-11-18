using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.Car
{
    public class CreateCarCommand: IRequest<ResponseBase>
    {
        public string Token { get; set; }   
        public string Tag { get; set; }
        public string FuelType { get; set; }
        public bool WillBeFav { get; set; }
        //public bool YesOrNo { get; set; }
    }
}
