using MyHTTPService;
using System.Net;
using System.Text;
using System.Text.Json;




internal class Programm 
{ 
    static async Task Main(string[] args)
    {
        var server = new Server();
        server.Start();
       
/*        while (Console.ReadLine() != "stop")
        {
            server.Start();
        }*/
    }
}






