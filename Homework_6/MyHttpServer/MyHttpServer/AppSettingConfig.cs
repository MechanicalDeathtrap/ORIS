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

        public static string StaticFilePath = Directory.GetCurrentDirectory();
        public string EmailSender { get; set; }
        public string PasswordSender { get; set; }
        public string SmtpServerHost { get; set; }
        public  int SmtpServerPort { get; set; }
    }
}
