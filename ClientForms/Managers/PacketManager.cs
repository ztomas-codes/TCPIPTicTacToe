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
        const string BOARD = "BOARD";
        const string SCORE = "SCORE";

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
                case TURN:
                    Turn(packet);
                    break;
                case SCORE:
                    Score(packet);
                    break;
                case BOARD:
                    Board(packet);
                    break;
                case STARTGAME:
                    StartGame(packet);
                    break;
                case WRONGMOVE:
                    WrongMove(packet);
                    break;
                case DISCONNECT:
                    Disconnect(packet);
                    break;
                default:
                    break;
            }

        }


        //Listeners
        private void Score(string packet)
        {
            List<string> split = packet.Split('|').ToList();
            split.RemoveAt(0);

            TicTacToeClient.scoreLabel.Text = $"You: {split[0]} Enemy: {split[1]}";
        }

        private void WrongMove(string packet)
        {
            Output.WriteLine("Tento tah neni k dispozici");
        }

        private void Turn(string packet)
        {
            Output.WriteLine(packet);
        }

        private void Disconnect(string packet)
        {
            Output.WriteLine("Byl jsi odpojen");
        }
        private void StartGame(string packet)
        {
            Output.WriteLine("Hra byla odstartovana");
        }

        private void Board(string packet)
        {
            List<string> split = packet.Split('|').ToList();
            split.RemoveAt(0);

            split.Reverse();

            int i = 0;
            foreach(var button in TicTacToeClient.buttons)
            {
                button.Text = $"{split[i].Remove(1)}";
                switch (split[i])
                {
                    case "X":
                        button.ForeColor = MetroFramework.MetroColors.Red;
                        break;
                    case "O":
                        button.ForeColor = MetroFramework.MetroColors.Blue;
                        break;
                    default:
                        button.ForeColor = MetroFramework.MetroColors.Black;
                        break;
                }
                button.UseCustomForeColor = true;
                i++;
            }
        }

        //Actions
        public void SendMove(string pole)
        {
            NetworkStream ns = PlayerClient.Instance.tcpclient.GetStream();
            ns.Write(Packets.CreatePacket($"{MOVE}|{pole}"));
            Console.WriteLine(pole);
        }

        public void SendName(string name)
        {
            NetworkStream ns = PlayerClient.Instance.tcpclient.GetStream();
            ns.Write(Packets.CreatePacket($"{NAME}|{name}"));
        }

    }
}
