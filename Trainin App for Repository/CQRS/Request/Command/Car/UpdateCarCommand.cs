using MediatR;
using System;

namespace Trainin_App_for_Repository.CQRS.Request.Command.Car
{
    public class UpdateCarCommand: IRequest<ResponseBase>
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        /* icon icin JPEG dosyasi*/
        public string FuelType { get; set; }
        public string Token { get; set; }
        public bool WillBeFav { get; set; }
        //public bool YesOrNo { get; set; }
    }
}

