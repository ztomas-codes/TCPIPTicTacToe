using System.Text;

namespace Server.Manager
{
    public class PacketManager
    {
        public static readonly string WIN = "WIN";
        public static readonly string MOVE = "MOVE";
        public static readonly string TURN = "TURN";
        public static readonly string PLAYERTURN = "PLT";
        public static readonly string NOTYOURTURN = "NYT";
        public static readonly string DISCONNECT = "DIS";
        public static readonly string WRONGMOVE = "WRGM";
        public static readonly string STARTGAME = "STRG";
        public static readonly string SCORE = "SCORE";

        public static byte[] CreatePacket(string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        public static string GetPacket(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }
    }
}