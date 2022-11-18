using MediatR;
using Trainin_App_for_Repository.CQRS.Response.Query.User;

namespace Trainin_App_for_Repository.CQRS.Request.Query.User
{
    public class UserLoginQueryRequest: IRequest<ResponseBase<UserLogin>>
    {
        public string Email { get; set; }   
        public string Password { get; set; }    
    }
}
