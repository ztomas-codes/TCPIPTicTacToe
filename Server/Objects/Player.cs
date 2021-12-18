using System.Collections.Generic;
using System.Net.Sockets;

namespace Server.Objects
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        public string IP { get; private set; }
        public int Port { get; private set; }
        public  char Char { get; set; }
        public bool Win {get; set;}
        public bool Turn { get; set; }
        public TcpClient Client { get; set; }
        public NetworkStream NetworkStream { get; set; }

        public  static List<Player> Players = new List<Player>();

        public Player(string name, int score, string ip, int port)
        {
            Name = name;
            Score = score;
            IP = ip;
            Port = port;
            Players.Add(this);
        }

        
    }
}