using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService
{
    public class Server
    {
        private HttpListenerContext _listenerContext;
        private HttpListener _listener;///////////

        public async void Start()
        {
            var config = (new UpdateConfig()).UpdateConfiguration();

            _listener = new HttpListener();/////////
            _listener.Prefixes.Add($"{config.Address}:{config.Port}/");
            _listener.Start();////
            Console.WriteLine("Сервер запущен");


            while (true)
            {
                Receive();
                Response();
            }

        }

        private void Receive()
        {
            _listenerContext = _listener.GetContext();
        }

        private async void Response()
        {
            
            var response = _listenerContext.Response;
            var request = _listenerContext.Request;

            var absoluteUrl = request.Url.AbsolutePath;
            var staticFilePath = Path.Combine(AppSettingConfig.StaticFilePath, absoluteUrl.Trim('/'));

            if (File.Exists(staticFilePath.Replace("\\", "/")))
            {

                byte[] bytes = File.ReadAllBytes(staticFilePath);
                response.ContentLength64 = bytes.Length; 

                using Stream output = response.OutputStream;

                await output.WriteAsync(bytes); 
                await output.WriteAsync(bytes);
                await output.FlushAsync();

                Console.WriteLine("Запрос обработан");
            }
            else {

                byte[] siteBytes = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), @"static\404.html"));

                response.ContentLength64 = siteBytes.Length;
                response.StatusCode = 404;
                using Stream output = response.OutputStream;
                await output.WriteAsync(siteBytes);
                await output.FlushAsync();
            }

        }

        public void Stop()
        {
            if (_listener != null)
            {
                _listener.Stop();
                Console.WriteLine("Сервер остановлен");
            }

        }
    }
}
