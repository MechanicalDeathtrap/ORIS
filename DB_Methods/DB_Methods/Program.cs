using Microsoft.Data.SqlClient;
using MyORM;
using System;
using System.Configuration;


var client = new Clients(2010, 3, "Жаклаева Виктория Юрьевна", 26, "+78889888988", false, false);

//new Database("Data Source=localhost;Initial Catalog=StripClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False").Insert(client);
var admin = new Database("Data Source=localhost;Initial Catalog=Oris_First_Semestrovka;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;" +
    "Application Intent=ReadWrite;Multi Subnet Failover=False")
/*    .Select<Admin_Table>();
    //.Delete<Clients>(1011);*/
    .SelectById<Admin_Table>(1);

//foreach(var ad in admin )
    Console.WriteLine(admin.admin_login);
