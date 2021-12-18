using ClientForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class PacketManager
    {

        const string WIN = "WIN";
        const string LOSE = "LOSE";


        const string MOVE = "MOVE"; //Policko
        const string TURN = "TURN"; //Bool
        const string DISCONNECT = "DISCONNECT";
        const string WRONGMOVE = "WRGM";
        const string STARTGAME = "STRG";

        const string NAME = "NAME";

        public static PacketManager Instance = null;


        public PacketManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Run(string packet)
        {
            string[] packetList = packet.Split("|");
            switch (packetList[0])
            {
                case WIN:
                    Win();
                    break;
                case TURN:
                    Turn(packetList[1]);
                    break;
                case LOSE:
                    Lose();
                    break;
                case STARTGAME:
                    StartGame();
                    break;
                case WRONGMOVE:
                    WrongMove();
                    break;
                case DISCONNECT:
                    Disconnect();
                    break;
                default:
                    break;
            }

        }


        //Listeners
        private void Win()
        {
            Output.WriteLine("Vyhral jsi");
        }

        private void Lose()
        {
            Output.WriteLine("Prohral jsi");
        }

        private void WrongMove()
        {
            Output.WriteLine("Tento tah neni k dispozici");
        }
        
        private void Turn(string boolean)
        {
            if (boolean == "1")
                Output.WriteLine("Ted jsi na tahu");
            else
                Output.WriteLine("Ted nejsi na tahu");
        }

        private void Disconnect()
        {
            Output.WriteLine("Byl jsi odpojen");
        }
        private void StartGame()
        {
            Output.WriteLine("Hra byla odstartovana");
        }

        //Actions
        public void SendMove(int pole)
        {
            NetworkStream ns = PlayerClient.Instance.tcpclient.GetStream();
            ns.Write(Packets.CreatePacket($"{MOVE}|{pole}"));
        }

        public void SendName(string name)
        {
            NetworkStream ns = PlayerClient.Instance.tcpclient.GetStream();
            ns.Write(Packets.CreatePacket($"{NAME}|{name}"));
        }

    }
}
