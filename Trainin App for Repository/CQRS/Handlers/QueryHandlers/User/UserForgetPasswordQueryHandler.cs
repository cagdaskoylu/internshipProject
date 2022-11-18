using AutoMapper;
using MailKit.Security;
using MediatR;
using MimeKit;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Trainin_App_for_Repository.CQRS.Request.Query.User;
using Trainin_App_for_Repository.Data;
using Trainin_App_for_Repository.Repository;

namespace Trainin_App_for_Repository.CQRS.Handlers.QueryHandlers.User
{
    public class UserForgetPasswordQueryHandler : IRequestHandler<UserForgetPasswordQueryRequest, ResponseBase>
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;
        public UserForgetPasswordQueryHandler(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseBase> Handle(UserForgetPasswordQueryRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(request.Email);
            var response = new ResponseBase();
            if (user.Result == null)
            {
                response.Success = false;
                response.StatusCode = 400;
                response.Message = "Boyle bir kullanici bulunamadi";

            }
            else
            {
                var sender = new MailInfo();
                //var email = new MimeMessage
                //{
                //    Sender = MailboxAddress.Parse(sender.Email)
                //};
                //email.To.Add(MailboxAddress.Parse(request.Email));
                //email.Subject = "Şifremi unuttum";
                //var builder = new BodyBuilder
                //{
                //    HtmlBody = "Şifreniz: " + user.Result.Password
                //};
                //email.Body = builder.ToMessageBody();
                //using var smtp = new SmtpClient();
                //smtp.Connect(sender.Host, sender.Port, SecureSocketOptions.StartTls);
                //smtp.Authenticate(sender.Email, sender.Password);
                //await smtp.SendAsync(email);
                //smtp.Disconnect(true);

                var email = new MailMessage();
                email.From = new MailAddress(sender.Email);
                email.To.Add(request.Email);
                email.Subject = "Şifremi Unuttum";
                email.Body = user.Result.Password;
                var smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sender.Email, sender.Password);
                smtp.Host = sender.Host;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(email);
                response.Success = true;
                response.Message = "Sifre mail olarak gonderildi";
                response.StatusCode = 200;
            }
            return response;
        }
    }
}
