using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace ArenaSportProj.Services
{
    // Dummy email sender – не праща нищо, само изпълнява интерфейса
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
