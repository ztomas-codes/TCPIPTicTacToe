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
            Console.WriteLine(packet);
            //string[] packetList = packet.Split("|");
            //switch (packetList[0])
            //{
            //    case WIN:
            //        Win();
            //        break;
            //    case TURN:
            //        Turn(packetList[1]);
            //        break;
            //    case BOARD:
            //        List<String> board = packetList.ToList();
            //        board.RemoveAt(0);
            //        Board(board.ToArray());
            //        break;
            //    case LOSE:
            //        Lose();
            //        break;
            //    case STARTGAME:
            //        StartGame();
            //        break;
            //    case WRONGMOVE:
            //        WrongMove();
            //        break;
            //    case DISCONNECT:
            //        Disconnect();
            //        break;
            //    default:
            //        break;
            //}

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

        private void Board(string[] board)
        {
            string sboard = string.Empty;
            board.ToList().ForEach(x => sboard += x);


            char[,] chars = new char[3, 3];

            int i = 0, j = 0;
            sboard.ToCharArray().ToList().ForEach(x => 
            {
                chars[i, j] = x;

                i++;
                if (i == 3) { j++; j = 0; }
            }
            );

            GameManager.SetGame(chars);
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
