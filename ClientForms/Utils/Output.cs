using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientForms
{
    public class Output
    {
        public static RichTextBox OutputWindow;

        public static void WriteLine(string text)
        {
            OutputWindow.Text += text + "\n";
        }
        public static void Write(string text)
        {
            OutputWindow.Text += text;
        }
    }
}
