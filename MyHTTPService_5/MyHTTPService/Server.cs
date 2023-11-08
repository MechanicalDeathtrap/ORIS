using MyHTTPService.Handlers;
using MyHTTPService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Text;

namespace MyHTTPService
{
    public class Server
    {
        private HttpListener _listener;
        private AppSettingConfig _settingConfig = (new UpdateConfig()).UpdateConfiguration();

        public async Task Start()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"{_settingConfig.Address}:{_settingConfig.Port}/");
            _listener.Start();
            Console.WriteLine("Сервер запущен");


            while (true)
            {
                var context = _listener.GetContext();

                Respond(context);
            }
        }

        private async void Respond(HttpListenerContext context)
        {
            var request = context.Request;

            if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) && request.Url.AbsolutePath == "/dodo-email")
            {
                var stream = new StreamReader(request.InputStream);

                string str = await stream.ReadToEndAsync();
                string[] strl = str.Split("\r\n");

                await SendMessageToEmailAsync(strl[0].Split("=")[1], strl[1].Split("=")[1], strl[2].Split("=")[1], strl[3].Split("=")[1], strl[4].Split("=")[1]);
            }

            var response = context.Response;
            var path = AppSettingConfig.StaticFilePath;

            if (Directory.Exists(AppSettingConfig.StaticFilePath))
            {

                    byte[] siteBytes = File.ReadAllBytes(path + request.RawUrl.Substring(7));

                    response.ContentLength64 = siteBytes.Length;

                    using Stream output = response.OutputStream;

                    await output.WriteAsync(siteBytes);
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

        private async Task SendMessageToEmailAsync(string name, string lastName, string birthDate, string phoneNumber, string email)
        {
            string toEmail = email;

            string subject = "Дз";
            string body = String.Format("Не вводите свои данные в непроверенные инпуты \r\nemail: {0}\r\nlast name: {1}\r\nname: {2}\r\n" +
                "birth date: {3}\r\nphone number", email, lastName, name, birthDate, phoneNumber);

            var sender = new EmailSenderService();
            await sender.SendEmailAsync(toEmail, subject, body);
            
        }
    }
}
