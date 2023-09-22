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
        public static string StaticFilePath = "C:\\Users\\korik\\source\\repos\\MechanicalDeathtrap\\ReposCsNew\\MyHTTPService\\static";
    }
}
