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
        public static List<TcpClient> Client { get; private set; }
        private static NetworkStream _streamPlayer1 { get; set; }
        private static NetworkStream _streamPlayer2 { get; set; }
        private static Task _Task { get; set; }

        static void Main(string[] args)
        {
            Client = new List<TcpClient>();

            TcpListener server = new TcpListener(8888);
            TcpClient client = default(TcpClient);
            int counter = 0;
            while (true)
            {
                while (Client.Count <= 1)
                {
                    server.Start();
                    Console.WriteLine(" >>" + "Server started");
                    client = server.AcceptTcpClient();
                    Client.Add(client);
                    Console.WriteLine($" {client.Client.RemoteEndPoint} >>" + "connected");
                    if (_streamPlayer1 == null)
                    {
                        _streamPlayer1 = client.GetStream();
                    }
                    else
                    {
                        _streamPlayer2 = client.GetStream();
                    }
                    _Task = Task.Run(async ()
                        =>
                    {
                        while (Client.Count == 1)
                        {
                            var paket = Encoding.Default.GetBytes("Waiting for players");
                            await Task.Delay(10000);
                            
                                Console.WriteLine("sent packet");
                                _streamPlayer1.Write(paket, 0, paket.Length);
                        }
                    });
                }

                if (Client.Count == 2)
                {
                    //TODO: Sort Name Packet
                    Player player1 = new Player(Client[0].Client.RemoteEndPoint.ToString(), 0, Client[0].Client.RemoteEndPoint.ToString(), 8888);
                    Player player2 = new Player(Client[1].Client.RemoteEndPoint.ToString(), 0, Client[1].Client.RemoteEndPoint.ToString(), 8888);
                    GameManager gameManager = new GameManager(player1, player2, _streamPlayer1 , _streamPlayer2);
                    _Task.Dispose();
                    Client.Clear();
                    gameManager.StartGame();
                }
            }
        }
    }
}
