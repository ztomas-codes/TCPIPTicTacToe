using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class PacketManager
    {
        public static void Run(string packet)
        {
            string[] packetList = packet.Split("|");
            switch (packetList[0])
            {
                case "WIN":
                    Win();
                    break;
            }

        }

        public static void Win()
        {
            
        }
    }
}
