using Client;
using MetroFramework.Controls;

namespace ClientForms
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public static Form1 Instance;
        public static List<MetroButton> buttons = new List<MetroButton>();

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
            metroButton10.Hide();
            //only added your code to build app app for the first time
            try
            {
                new PacketManager();
                if (!(nameTextBox.Text == "" && nameTextBox.Text == " "))
                {
                    new PlayerClient("127.0.0.1", 8888, nameTextBox.Text);
                    nameTextBox.Hide();
                }
                else Output.WriteLine("Nezadane jméno");
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