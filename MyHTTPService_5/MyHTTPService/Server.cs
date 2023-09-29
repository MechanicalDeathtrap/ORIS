using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService
{
    public class Server 
    {
        private HttpListener _listener;
        public void Start()
        {
            var config = (new UpdateConfig()).UpdateConfiguration();

            _listener = new HttpListener();
            _listener.Prefixes.Add($"{config.Address}:{config.Port}/");
            _listener.Start();
            Console.WriteLine("Сервер запущен");

            while (true)
            {
                var context = _listener.GetContext();

                Task.Run(() => Respond(context));
            }
        }

        private async void Respond(HttpListenerContext context)
        {
            var request = context.Request;
            var url = request.UrlReferrer.ToString().Split("/")[3];

            if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) && url == "?dodo-email=")
            {
                var stream = new StreamReader(request.InputStream);

                string str = await stream.ReadToEndAsync();
                string[] strl = str.Split('&');
                await SendMessageToEmailAsync(strl[0], strl[1],strl[2],strl[3],strl[4]);
            }

            Console.WriteLine(request.Url.AbsolutePath);
            var response = context.Response;
            var path = AppSettingConfig.StaticFilePath;

            var filePath = path + request.Url.LocalPath.Replace('/', '\\' );

            if (Directory.Exists(AppSettingConfig.StaticFilePath))
            {
                byte[] siteBytes = File.ReadAllBytes(path + "\\index.html");

                byte[] imagesBytes = null;


                if (request.Url.LocalPath.Split('/')[1] == "images" || request.Url.LocalPath.Split('/')[1] == "styles" && File.Exists(filePath))
                {
                    imagesBytes = File.ReadAllBytes(filePath);
                }

                response.ContentLength64 = siteBytes.Length;

                if (imagesBytes != null)
                {
                    response.ContentLength64 += imagesBytes.Length;
                }

                using Stream output = response.OutputStream;

                await output.WriteAsync(siteBytes);

                if (imagesBytes != null)
                {
                    await output.WriteAsync(imagesBytes);
                }

                await output.FlushAsync();
            }
            else
            {
                response.StatusCode = 404;
                byte[] notFoundMessage = Encoding.UTF8.GetBytes("404 - Not Found");

                response.ContentLength64 = notFoundMessage.Length;
                using Stream output = response.OutputStream;
                await output.WriteAsync(notFoundMessage);
                await output.FlushAsync();
            }


            response.Close();
            Console.WriteLine("Запрос обработан");
        }

        public void Stop()
        {
            _listener.Stop();
            Console.WriteLine("Сервер остановлен");
        }

        private async Task SendMessageToEmailAsync(string email, string lastName, string name, string birthDate, string phoneNumber)
        {
            // меняются постоянно, поэтому выкинуть их в appsetting.json - emailSEnder passwordSender , from name
            // smtpServerHost / smtpServerPOrt
            // emailiServerServise - класс лежит в папке сервисес вместе с интерфейсом подключать данные из конфигов
            // pattern handlings? Ihandler - метод (возвращаемый тип?)handler(HttpContext context) : создать класс с этим интерфейсом staticFilesHandler добавить и переместить
            // сюда логику работы с папкой static
            // подставить в listening  staticFilesHandler  и его вызывать
            string emailSender = "korikova-04@mail.ru";
            string passwordSender = "PAKMEN101!";

            string formName = "Liza";
            string toEmail = "korikova-04@mail.ru";

            string subject = "subject";
            string body = String.Format("<h1> Не вводите свои данные в непроверенные инпуты <h1><p>email:{0}<p><p>last name:{1}<p><p>name:{2}<p>" +
                "<p>birth date:{3}<p><p>phone number<p>", email, lastName, name, birthDate, phoneNumber);

            string smtpServerHost = "smtp.mail.ru";
            ushort smtpServerPort = 465;


            MailAddress from = new MailAddress(emailSender, formName);
            MailAddress to = new MailAddress(toEmail);

            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = body;

            SmtpClient smtp = new SmtpClient(smtpServerHost, smtpServerPort);
            smtp.Credentials = new NetworkCredential(emailSender, passwordSender);
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(m);

            Console.WriteLine(body);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
