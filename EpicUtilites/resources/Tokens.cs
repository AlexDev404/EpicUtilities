using fnbr.authSessions;
using System;
using System.Collections.Generic;

namespace fnbr
{
    public class Tokens
    {

        public static List<Client> ClientList = new List<Client>
        {
            new Client()
            {
                BasicToken = "NTIyOWRjZDNhYzM4NDUyMDhiNDk2NjQ5MDkyZjI1MWI6ZTNiZDJkM2UtYmY4Yy00ODU3LTllN2QtZjNkOTQ3ZDIyMGM3",
                clientId = "5229dcd3ac3845208b496649092f251b",
                secret = "e3bd2d3e-bf8c-4857-9e7d-f3d947d220c7",
                AuthClient = AuthClients.Switch
            },
            new Client()
            {
                BasicToken = "MzQ0NmNkNzI2OTRjNGE0NDg1ZDgxYjc3YWRiYjIxNDE6OTIwOWQ0YTVlMjVhNDU3ZmI5YjA3NDg5ZDMxM2I0MWE=",
                clientId = "3446cd72694c4a4485d81b77adbb2141",
                secret = "9209d4a5e25a457fb9b07489d313b41a",
                AuthClient = AuthClients.iOS
            },
            new Client()
            {
                BasicToken = "ZWM2ODRiOGM2ODdmNDc5ZmFkZWEzY2IyYWQ4M2Y1YzY6ZTFmMzFjMjExZjI4NDEzMTg2MjYyZDM3YTEzZmM4NGQ",
                clientId = "ec684b8c687f479fadea3cb2ad83f5c6",
                secret = "e1f31c211f28413186262d37a13fc84d",
                AuthClient = AuthClients.Windows
            }
        };

        public class Client
        {
            public string BasicToken;

            public string clientId;

            public string secret;

            public AuthClients AuthClient;
        }
    }
}
