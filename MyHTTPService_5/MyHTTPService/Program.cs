using HtmlAgilityPack;
using MyHTTPService;
using System.Net;
using System.Text;
using System.Text.Json;
internal class Programm 
{
/*    static public void GetInfo(HtmlDocument html, int itemNumber)
    {
        
        var item = html.GetElementbyId($"resultlink_{itemNumber}").ChildNodes.Where(x => x.Name == "div");
        var image = html.GetElementbyId($"resultlink_{itemNumber}").ChildNodes.Where(x => x.Name == "img");

        foreach (var node in item)
        {
            var ;
        }
*//*        var image = html.GetElementbyId($"result_{itemNumber}_image");
        var title = html.GetElementbyId($"result_{itemNumber}_name");
        var description = title.ChildNodes*//*
    }*/
    static async Task Main(string[] args)
    {
        var server = new Server();

        await Task.Run(async () => { await server.Start(); });

/*
        //////////////////////////////////////

        var itemsList = new List<SteamItem>();
        var client = new WebClient();

        HtmlDocument html = new HtmlDocument();
        html.LoadHtml(client.DownloadString("https://steamcommunity.com/market/"));

        for (int i=0; i<15; i++)
        {
            GetInfo(html, i);
        }*/

    }
}






