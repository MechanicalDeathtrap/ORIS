using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyHTTPService
{
    public class UpdateConfig
    {
        public const string pathConfigFile = "appsetting.json";

        public AppSettingConfig UpdateConfiguration()
        {
            try
            {
                if (!File.Exists(pathConfigFile))
                {
                    Console.WriteLine("Файл appsetting.json не найден");
                    throw new Exception();
                }

                AppSettingConfig config;

                if (!Directory.Exists(AppSettingConfig.StaticFilePath))
                {
                    Console.WriteLine("Папка static не найдена... Создание новой папки static");
                    Directory.CreateDirectory(AppSettingConfig.StaticFilePath);
                }

                if (!File.Exists(AppSettingConfig.StaticFilePath + "\\static\\index.html"))
                {
                    Console.WriteLine(AppSettingConfig.StaticFilePath + "\\static\\index.html");
                    Console.WriteLine("Файл index.html не найден");
                    throw new Exception();
                }

                using (FileStream stream = File.OpenRead(pathConfigFile))
                {
                    config = JsonSerializer.Deserialize<AppSettingConfig>(stream);
                }
                return config;
            }
            catch (Exception ex) 
            {
                return new AppSettingConfig();
            }
        }
    }
}
