using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHTTPService.Services
{
    internal interface IEmailSenderService
    {
        //subject -файлы приема
        void SendEmail(string name, string lastName, string birthDate, string phoneNumber, string email);
    }
}
