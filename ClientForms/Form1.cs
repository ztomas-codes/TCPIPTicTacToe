using Client;

namespace ClientForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //only added your code to build app app for the first time
            new PacketManager();
            new PlayerClient("127.0.0.1", 8888);
            try
            {
                PlayerClient.Instance._task.Start();
            }catch
            {

            }
        }
    }
}