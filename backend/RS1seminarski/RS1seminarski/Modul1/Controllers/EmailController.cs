using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using RS1seminarski.Modul1.ViewModels;

namespace RS1seminarski.Modul1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public IActionResult PosaljiMail([FromBody] EmailVM obj)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(obj.AdresaFrom));
            email.To.Add(MailboxAddress.Parse(obj.AdresaTo));
            email.Subject = obj.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = obj.Sadrzaj };

            
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.Auto);
            smtp.Authenticate(obj.AdresaFrom, obj.Sifra);  
            smtp.Send(email);
            smtp.Disconnect(true);


            return Ok();            
        }
    }
}
