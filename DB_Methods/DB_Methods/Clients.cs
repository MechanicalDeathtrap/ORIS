using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM
{
    internal class Clients
    {
        public int ClientID { get; set; }
        public int Client_status {  get; set; }
        public string FIO { get; set; }
        public int Age { get; set; }
        public string Contacts { get; set; }
        public bool Is_blocked { get; set; }
        public bool Is_anonymous { get; set; }

        public Clients(int clientID, int clientStatus, string fio, int age, string contacts, bool isBlocked, bool isAnon) 
        {
            ClientID = clientID;
            Client_status = clientStatus;
            FIO = fio;
            Age = age;
            Contacts = contacts;
            Is_blocked = isBlocked;
            Is_anonymous = isAnon;
        }

        public Clients() { }
    }
}
