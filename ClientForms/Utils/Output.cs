using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForms
{
    public class Output
    {
        public static Label OutputWindow;

        public static void WriteLine(string text)
        {
            OutputWindow.Text = text;
        }
    }
}
