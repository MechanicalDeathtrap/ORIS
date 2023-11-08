using System;
using System.Configuration;

class Program
{
    static void Main(string[] args)
    {

        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StripClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        Console.WriteLine(connectionString);

        Console.Read();
    }
}


