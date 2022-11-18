using MediatR;

namespace Trainin_App_for_Repository.CQRS.Request.Query.User
{
    public class UserForgetPasswordQueryRequest : IRequest<ResponseBase>
    {
        public string Email { get; set; }
    }
}
