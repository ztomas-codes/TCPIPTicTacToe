using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class PlayerClient
    {
        public string Name;
        public string IP;

        public TcpClient tcpclient;
        public NetworkStream stream;
        public Thread thread;

        public static PlayerClient Instance = null;

        public PlayerClient(string server,int port)
        {
            if (Instance == null)
            {
                tcpclient = new TcpClient(server, port);
                stream = tcpclient.GetStream();

                thread = new Thread(Listener);
                thread.Start();
            }
        }

        public void Listener()
        {
            while (true)
            {
            }
        }

        public void RenderGame(string Map)
        {
            int i = 0;
            foreach (var character in Map.Split(" "))
            {
                Console.Write($"{character}\t");
                if (i == 3) Console.Write("\n");
                i++;
                if (i == 4) i = 0;
            }
        }
    }
}
