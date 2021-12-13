using System.Text;
using Server.Objects;

namespace Server.Manager
{
    public class Packets
    {
        
        public static byte[] MovePacket(Player player , int move)
        {
            return Encoding.Default.GetBytes(player.Name + " " + move);
        }

        public static byte[] SendBoard()
        {
            string[,] board = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = " ";
                }
            }

            return Encoding.Default.GetBytes(board[0, 0] + " " + board[0, 1] + " " + board[0, 2] + " " + board[1, 0] + " " + board[1, 1] + " " + board[1, 2] + " " + board[2, 0] + " " + board[2, 1] + " " + board[2, 2]);
        }
        public byte[] WinnerPacket(Player player)
        {
            return Encoding.Default.GetBytes("WIN|" + player.Name);
        }
    }
}