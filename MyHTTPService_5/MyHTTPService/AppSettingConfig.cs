using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService
{
    public class AppSettingConfig
    {
        public string Address { get; set; }
        public uint Port { get; set; }
        public static string StaticFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "static");

        //public static string StaticFilePath = "C:\\Users\\korik\\source\\repos\\MechanicalDeathtrap\\ReposCsNew\\MyHTTPService_5\\MyHTTPService\\bin\\Debug\\net6.0\\static";
    }
}
