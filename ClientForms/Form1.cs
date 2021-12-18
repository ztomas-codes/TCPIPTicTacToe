using Client;

namespace ClientForms
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public static Form1 Instance;

        public Form1()
        {
            InitializeComponent();
            Output.OutputWindow = Log;

            Instance = this;

            int i = 10;
            foreach(var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                if (button != metroButton10)
                {
                    i--;
                    button.Text = $"{i}";
                    button.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            MetroFramework.Controls.MetroButton button = (MetroFramework.Controls.MetroButton) sender;
            PacketManager.Instance.SendMove(button.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            metroButton10.Hide();
            //only added your code to build app app for the first time
            try
            {
                new PacketManager();
                new PlayerClient("127.0.0.1", 8888);
            }
            catch
            {
                Output.WriteLine("Server nebyl nalezen");
                metroButton10.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}