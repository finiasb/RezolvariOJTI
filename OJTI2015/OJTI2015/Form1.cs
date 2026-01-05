using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2015
{
    public partial class Form1 : Form
    {
        int x;
        public Form1()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "agentie2015";
            if (textBox1.Text == "agentie2015" && textBox2.Text == "")
            {
                x = 0;
                MdiForm form = new MdiForm(x);
                form.Show();
                this.Hide();
            }
            else if(textBox2.Text == "oti2015" && textBox1.Text == "")
            {
                x = 1;
                MdiForm form = new MdiForm(x);
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Parola gresita sau campuri necompletate");
                textBox2.Text = string.Empty;
                textBox1.Text = string.Empty;
            }
        }
    }
}
