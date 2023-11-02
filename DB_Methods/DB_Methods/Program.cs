using Microsoft.Data.SqlClient;
using MyORM;
using System;
using System.Configuration;


var client = new Clients(2010, 3, "Жаклаева Виктория Юрьевна", 26, "+78889888988", false, false);
//new Database("Data Source=localhost;Initial Catalog=StripClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False").Insert(client);
new Database("Data Source=localhost;Initial Catalog=StripClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
    "Application Intent=ReadWrite;Multi Subnet Failover=False")
    .Update(client);
    //.Delete<Clients>(1011);
    //.SelectById<Clients>(2010);

//Console.WriteLine(select.FIO);
