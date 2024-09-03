using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebUygulamaProjesi1.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Sizler buraya email gönderme işlemlerini ekleyebilirsiniz.
            return Task.CompletedTask;
        }
    }
}
