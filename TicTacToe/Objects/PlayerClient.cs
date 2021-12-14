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
            }
        }

        public void Listener()
        {
            PacketManager pm = new PacketManager();
            while (true)
            {
                byte[] bytes = new byte[100];
                stream.Read(bytes, 0, bytes.Length);
                pm.Run(Packets.GetPacket(bytes));
            }
        }
    }
}
