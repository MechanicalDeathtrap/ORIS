using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService
{
    internal interface IEmailSenderService
    {
        //subject -файлы приема
        Task SendEmailAsync(string toEmail, string subject, string messages);
    }
}
