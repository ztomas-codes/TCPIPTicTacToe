using ClientForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{


    class GameManager
    {
        public static int OnTurn;
        public static char[,] Game;

        public static void SetGame(char[,] game)
        {
            Game = game;
            int i = 0;
            int j = 0;
            foreach (var button in Form1.Instance.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                if (button.Text != "Start")
                {
                    button.Text = $"{Game[i,j]}";
                    i++;
                    if (i == 3) j++;
                }
            }
        }
    }
}
