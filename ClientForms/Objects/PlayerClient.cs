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

        public string Name;

        public static PlayerClient Instance = null;

        public PlayerClient(string server,int port, string name)
        {
            Instance = this;
            Name = name;
            tcpclient = new TcpClient(server, port);
                
            stream = tcpclient.GetStream();
            _task = Task.Run(() => Listener());
            Output.WriteLine("client connected");
            PacketManager.Instance.SendName(name);
        }

        public void Listener()
        {
            PacketManager pm = PacketManager.Instance;
            while (true)
            { 
                byte[] bytes = new byte[100];
                stream.Read(bytes, 0, bytes.Length);
                pm.Run(Packets.GetPacket(bytes));
            }
        }
    }
}
