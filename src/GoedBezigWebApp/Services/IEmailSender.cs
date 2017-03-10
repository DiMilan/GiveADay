using System.Threading.Tasks;

namespace GoedBezigWebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
