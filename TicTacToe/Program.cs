using Client;
using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            new PacketManager();
            new PlayerClient("127.0.0.1", 8888);
            //PlayerClient.Instance._task.Start();
        }
    }
}
