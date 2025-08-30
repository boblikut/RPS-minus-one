using RPS.Classes;

namespace RPS
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            BackgroundImage = Image.FromFile("Images/floor2.jpg");
            pictureBox1.Image = Image.FromFile("Images/logo.png");
            Icon = Global.Icon;
            Global.Menu = this;
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Hide();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Hide();
            form2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            Hide();
            info.Show();
        }
    }
}
