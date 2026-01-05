namespace cosmos2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "oji@csharp.ro" && textBox2.Text == "Ojti2024")
            {
                this.Hide();
                AlegeOptiunea alege = new AlegeOptiunea();
                alege.Show();
            }
            else
            {
                MessageBox.Show("Ceva nu a mers bine, mai încercați!");
                textBox1.Text = textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
