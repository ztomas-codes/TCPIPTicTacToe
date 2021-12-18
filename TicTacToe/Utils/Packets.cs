using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Packets
    {
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
