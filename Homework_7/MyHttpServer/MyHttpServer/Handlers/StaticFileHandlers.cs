using MyHTTPService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService.Handlers
{
    public class StaticFileHandlers : Handler
    {

        public override async void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
            var absoluteUrl = request.Url.AbsolutePath;
            var staticFilePath = Path.Combine(AppSettingConfig.StaticFilePath, absoluteUrl.Trim('/')); 


            try
            {
                if (absoluteUrl.Split('/').Last().Contains('.'))
                {
                    var fileExtension = absoluteUrl.Split('/').Last();
                    fileExtension = '.' + fileExtension.Split('.').Last();

                    if (File.Exists(staticFilePath.Replace("\\", "/")))
                    {
                        var type = GetContentType(fileExtension);
                        response.ContentType = type;
                        byte[] bytes;
                        bytes = File.ReadAllBytes(staticFilePath);
                        response.ContentLength64 = bytes.Length;

                        using Stream output = response.OutputStream;

                        await output.WriteAsync(bytes);
                        await output.FlushAsync();

                    }
                    else
                    {

                        byte[] siteBytes = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(),@"static\404.html"));

                        response.ContentLength64 = siteBytes.Length;
                        response.StatusCode = 404;
                        using Stream output = response.OutputStream;
                        await output.WriteAsync(siteBytes);
                        await output.FlushAsync();
                    }
                }
                else if (Successor != null)
                {
                    Successor.HandleRequest(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); 
                throw;
            }
        }

        private string GetContentType(string fileExtension)
        {
            Dictionary<string, string> contentTypeMappings = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".webp", "image/webp" },
                { ".svg", "image/svg+xml" },
                { ".html", "text/html" }, 
                { ".css", "text/css" },   
                { ".js", "application/javascript" }
            };

            if (contentTypeMappings.ContainsKey(fileExtension))
            {
                return contentTypeMappings[fileExtension];
            }

            return "application/octet-stream"; 
        }
    }
}
