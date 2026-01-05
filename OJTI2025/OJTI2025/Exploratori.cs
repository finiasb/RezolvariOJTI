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
    public partial class Exploratori : Form
    {
        public Exploratori()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int exploratori;
            if(int.TryParse(textBox1.Text, out exploratori))
            {
                if (exploratori >= 30 && exploratori <= 200)
                {
                    this.Hide();
                    Expeditie expeditie = new Expeditie(exploratori);
                    expeditie.Show();
                }
                else
                {
                    MessageBox.Show("Exploratorii trebyuie sa fie din intervalul [30, 200]");
                    textBox1.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Nu ati introdus un numar");
                textBox1.Text = string.Empty;
            }
        }
    }
}
