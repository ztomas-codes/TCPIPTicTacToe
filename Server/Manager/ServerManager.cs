using Server.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Manager
{
    public class ServerManager
    {
        public List<TcpClient> Client { get; private set; }
        public NetworkStream _streamPlayer1 { get; set; }
        public NetworkStream _streamPlayer2 { get; set; }
        private string NamePlayer1 {get; set;}
        private string NamePlayer2 { get; set;}
        private Task task { get; set; }
        private Task task2 { get; set; }
 
        private bool Running { get; set; }
        public ServerManager(NetworkStream streamPlayer1, NetworkStream streamPlayer2 , TcpClient client , TcpClient client2)
        {
            _streamPlayer1 = streamPlayer1;
            _streamPlayer2 = streamPlayer2;
       
        }

        public  void StartServer()
        {
            Client = new List<TcpClient>();
            task = Task.Run(async () => NamePlayerOne());
            task2 = Task.Run(() => NamePlayerTwo());

            while (true)
            {
                
                  
                if (NamePlayer1 != null && NamePlayer2 != null)
                {
                    while (Client.Count <= 1)
                    {
                        if (Running) return;
                        Running = true;

                        Console.WriteLine(" >>" + "Server started");

                        Player player1 = new Player(NamePlayer1, 0, "someip", 8888);
                        Player player2 = new Player(NamePlayer2, 0, "someip", 8888);
                        GameManager gameManager = new GameManager(player1, player2, _streamPlayer1, _streamPlayer2);

                        Client.Clear();
                        gameManager.StartGame();
                    }
                }
            }
        }

        private Task NamePlayerOne()
        {
            var bytes = new byte[1024];
            _streamPlayer1.Read(bytes , 0, bytes.Length);
            string name = PacketManager.GetPacket(bytes);
            NamePlayer1 = name.Substring(0, name.IndexOf("0") + 1);
            return Task.CompletedTask;
        }
        private Task NamePlayerTwo()
        {
            var bytes = new byte[1024];
            _streamPlayer2.Read(bytes, 0, bytes.Length);
            string name = PacketManager.GetPacket(bytes);
            NamePlayer2 = name.Substring(0, name.IndexOf("0") + 1);
            return Task.CompletedTask;
        }
    }
}
