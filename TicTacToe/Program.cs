using Client;
using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            new PlayerClient("127.0.0.1", 100);
        }
    }
}
