using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var response = context.Response;
            var path = AppSettingConfig.StaticFilePath;

            var filePath = path + request.Url.LocalPath.Replace('/', '\\' );

            if (Directory.Exists(AppSettingConfig.StaticFilePath))
            {
                byte[] siteBytes = File.ReadAllBytes(path + "\\index.html");
                byte[] stylesBytes = File.ReadAllBytes(path + "\\styles\\styles.css");
                byte[] imagesBytes = null;


                if (request.Url.LocalPath.Split('/')[1] == "images")
                {
                    if (File.Exists(filePath))
                    {
                        imagesBytes = File.ReadAllBytes(filePath);
                    }
                }

                response.ContentLength64 = siteBytes.Length + stylesBytes.Length;

                if (imagesBytes != null)
                {
                    response.ContentLength64 += imagesBytes.Length;
                }

                using Stream output = response.OutputStream;

                await output.WriteAsync(siteBytes);
                await output.WriteAsync(stylesBytes);

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
    }
}
