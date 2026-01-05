using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "ojti@csharp.ro";
            textBox2.Text = "Ojti2025";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "ojti@csharp.ro" && textBox2.Text == "Ojti2025")
            {
                this.Hide();
                Exploratori exploratori = new Exploratori();
                exploratori.Show();
            }
            else
            {
                MessageBox.Show("Ceva nu a mers bine");
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;   
            }
        }
    }
}
