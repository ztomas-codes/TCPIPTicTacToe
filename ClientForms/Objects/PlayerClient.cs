using ClientForms;
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
        public Task _task;

        public static PlayerClient Instance = null;

        public PlayerClient(string server,int port)
        {
            if (Instance == null)
            {
                Instance = this;
                tcpclient = new TcpClient(server, port);
                
                stream = tcpclient.GetStream();
                _task = Task.Run(() => Listener());
                Output.WriteLine("client connected");
            }
        }

        public void Listener()
        {
            PacketManager pm = PacketManager.Instance;
            while (true)
            { 
                byte[] bytes = new byte[100];
                stream.Read(bytes, 0, bytes.Length);
                pm.Run(Packets.GetPacket(bytes));
                Output.WriteLine(Packets.GetPacket(bytes));
                
            }
        }
    }
}
