using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Attributes
{
    public class HttpMethodAttribute : Attribute
    {
        public HttpMethodAttribute(string actionName)
        {
            ActionName = actionName;
        }

        public string ActionName { get; set; }
    }
}
