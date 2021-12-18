using Client;
using MetroFramework.Controls;

namespace ClientForms
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public static Form1 Instance;
        public static List<MetroButton> buttons = new List<MetroButton>();
        public static Label scoreLabel;

        public Form1()
        {
            InitializeComponent();
            Output.OutputWindow = Log;
            scoreLabel = score;

            Instance = this;

            int i = 10;
            foreach(var button in this.Controls.OfType<MetroFramework.Controls.MetroButton>())
            {
                if (button != metroButton10)
                {
                    i--;
                    button.Text = $"{i}";
                    button.Click += Button_Click;
                    buttons.Add(button);
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
            if (nameTextBox.Text.Trim() == "" || nameTextBox.Text.Trim() == " ") Output.WriteLine("Nezadane jméno");
            else
            {
                metroButton10.Hide();
                //only added your code to build app app for the first time
                try
                {
                    new PacketManager();
                    new PlayerClient(ip.Text, Int32.Parse(port.Text), nameTextBox.Text);
                    PacketManager.Instance.SendName(PlayerClient.Instance.Name);
                    nameTextBox.Hide();
                    label1.Text = $"Name: {PlayerClient.Instance.Name}";
                }
                catch
                {
                    Output.WriteLine("Server nebyl nalezen");
                    metroButton10.Show();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}