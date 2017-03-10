using System.Threading.Tasks;

namespace GoedBezigWebApp.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
