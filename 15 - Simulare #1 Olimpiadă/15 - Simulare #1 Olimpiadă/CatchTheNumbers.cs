using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15___Simulare__1_Olimpiadă
{
    public partial class CatchTheNumbers : Form
    {
        int scor = 0;
        Random rnd = new Random();
        List<int> list = new List<int>();    
        public CatchTheNumbers(int scormax)
        {
            InitializeComponent();
            label2.Text = "Scor Maxim: " + scormax.ToString();
            label3.Text = "Scor " + scor;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox2.Top > 500)
            {
                pictureBox2.Top = rnd.Next(-200, 0);
                pictureBox2.Left = rnd.Next(0, 450);
                list.Add(1);

            }
            if (pictureBox3.Top > 500)
            {
                pictureBox3.Top = rnd.Next(-200, 0);
                pictureBox3.Left = rnd.Next(0, 450);
                list.Add(2);

            }
            if (pictureBox4.Top > 500)
            {
                pictureBox4.Top = rnd.Next(-200, 0);
                pictureBox4.Left = rnd.Next(0, 450);
                list.Add(3);

            }

            pictureBox2.Top += 20;
            pictureBox3.Top += 20;
            pictureBox4.Top += 20;

            if (pictureBox1.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                scor += 1;
                pictureBox2.Top = rnd.Next(-200, 0);
                pictureBox2.Left = rnd.Next(0, 450);
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                scor += 2;
                pictureBox3.Top = rnd.Next(-200, 0);
                pictureBox3.Left = rnd.Next(0, 450);
            }
            if (pictureBox1.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                scor += 3;
                pictureBox4.Top = rnd.Next(-200, 0);
                pictureBox4.Left = rnd.Next(0, 450);
            }
            label3.Text = "Scor " + scor;

            if (verificaPrim(scor))
            {
                label3.Visible = true;
                label4.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                timer1.Stop();
                return;
            }
            if(scor > 30)
            {
                timer1.Stop();
                MessageBox.Show("Ati depasit valoarea 30, aveti scorul 0");
                scor = 0;
            }
        }
        private bool verificaPrim(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            for (int d = 2; d < n; d++)
            {
                if (n % d == 0)
                    return false;
            }
            return true;
        }

        private void CatchTheNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A)
            {
                if(pictureBox1.Left > 0)
                {

                    timer1.Start();
                    pictureBox1.Left -= 10;
                }
            }else if (e.KeyCode == Keys.D)
            {
                if (pictureBox1.Left < 460d)
                {
                    timer1.Start();
                    pictureBox1.Left += 10;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rascumparare rascumparare = new Rascumparare(list, scor);
            rascumparare.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            bool x = true;
            Form1 form = new Form1(scor, x);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
