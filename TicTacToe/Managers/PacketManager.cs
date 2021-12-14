using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class PacketManager
    {

        const string WIN = "WIN";
        const string MOVE = "MOVE";
        const string TURN = "TURN";
        const string PLAYERTURN = "PLT";
        const string NOTYOURTURN = "NYT";
        const string DISCONNECT = "DISCONNECT";
        const string WRONGMOVE = "WRGM";
        const string STARTGAME = "STRG";


        public void Run(string packet)
        {
            string[] packetList = packet.Split("|");
            switch (packetList[0])
            {
                case WIN:
                    Win();
                    break;
                case MOVE:
                    Move();
                    break;
                case TURN:
                    Turn();
                    break;
                case PLAYERTURN:
                    PlayerTurn();
                    break;
                case NOTYOURTURN:
                    NotYourTurn();
                    break;
                case DISCONNECT:
                    Disconnect();
                    break;
                case WRONGMOVE:
                    WrongMove();
                    break;
                case STARTGAME:
                    StartGame();
                    break;
            }

        }

        private void Win()
        {
            
        }
        private void Move()
        {

        }
        private void PlayerTurn()
        {

        }
        private void Turn()
        {

        }
        private void NotYourTurn()
        {

        }

        private void Disconnect()
        {

        }

        private void WrongMove()
        {

        }

        private void StartGame()
        {

        }
    }
}
