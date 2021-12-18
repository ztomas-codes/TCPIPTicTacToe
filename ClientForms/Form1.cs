using Client;

namespace ClientForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Output.OutputWindow = output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //only added your code to build app app for the first time
            new PacketManager();
            try
            {
                new PlayerClient("127.0.0.1", 8888);
                PlayerClient.Instance._task.Start();
            }catch
            {
                Output.WriteLine("Server nebyl nalezen");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(6);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(9);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(8);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PacketManager.Instance.SendMove(7);
        }
    }
}