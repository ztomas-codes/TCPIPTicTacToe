using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.Manager;
using Server.Objects;

namespace Server
{
    class Program
    {
        public static List<TcpClient> Clients { get; set; }

        static void Main(string[] args)
        {
            Clients = new List<TcpClient>();
            TcpListener server = new TcpListener(8888);
            server.Start();
            while (true)
            {
                Clients.Add(server.AcceptTcpClient());
                if(Clients.Count == 2)
                {
                    ServerManager mng = new ServerManager(Clients[0].GetStream(), Clients[1].GetStream() , Clients[0] , Clients[1]);
                    mng.StartServer();
                    Clients.Clear();
                    
                }
            }

        }
   
        
    }
}
