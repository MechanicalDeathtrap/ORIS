using MyHttpServer.Attributes;
using MyHTTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Controllers
{
    [Controller("Account")]
    public class AccountController
    {
        public void SendEmail(string name, string lastName, string birthDate, string phoneNumber, string email)
        {
            var config = (new UpdateConfig()).UpdateConfiguration();
            string toEmail = email;


            string subject = "Письмо";
            string body = String.Format("Не вводите свои данные в непроверенные инпуты \r\nemail: {0}\r\nlast name: {1}\r\nname: {2}\r\n" +
                "birth date: {3}\r\nphone number", email, lastName, name, birthDate, phoneNumber);

            string smtpServerHost = "smtp.mail.ru";
            ushort smtpServerPort = 587;

            MailAddress from = new MailAddress(config.EmailSender, "name");
            MailAddress to = new MailAddress(toEmail);

            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = body;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            SmtpClient smtp = new SmtpClient(smtpServerHost, smtpServerPort);
            smtp.Credentials = new NetworkCredential(config.EmailSender, config.PasswordSender);
            smtp.EnableSsl = true;

            smtp.Send(m);

            Console.WriteLine(body);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
