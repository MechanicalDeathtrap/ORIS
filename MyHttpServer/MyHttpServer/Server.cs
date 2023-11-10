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


            byte[] siteBytes = File.ReadAllBytes(AppSettingConfig.StaticFilePath + "\\index.html");
            byte[] stylesBytes = File.ReadAllBytes(AppSettingConfig.StaticFilePath + "\\styles\\styles.css");
            //byte[] imagesBytes = File.ReadAllBytes(AppSettingConfig.StaticFilePath + "\\images");


            response.ContentLength64 = siteBytes.Length + stylesBytes.Length;
            using Stream output = response.OutputStream;

            await output.WriteAsync(siteBytes); 
            await output.WriteAsync(stylesBytes);
            await output.FlushAsync();

            Console.WriteLine("Запрос обработан");


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
