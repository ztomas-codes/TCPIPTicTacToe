using Client;
using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        { 
            PlayerClient pc = new PlayerClient("127.0.0.1", 8888);
            pc.thread.Start();
        }
    }
}
