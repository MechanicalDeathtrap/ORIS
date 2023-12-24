using MyHTTPService.Services;
using MyHTTPService.Handlers;
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
        private ControllerHandler controllerHandler = new ControllerHandler();
        private StaticFileHandlers staticFileHandler = new StaticFileHandlers();

        public async void Start()
        {
            var config = (new UpdateConfig()).UpdateConfiguration();
            
            _listener = new HttpListener();
            _listener.Prefixes.Add($"{config.Address}:{config.Port}/");
            _listener.Start();
            Console.WriteLine("Сервер запущен");

            try
            {
                while (true)
                {
                    var context = _listener.GetContext();
                    staticFileHandler.Successor = controllerHandler;
                    staticFileHandler.HandleRequest(context);
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
