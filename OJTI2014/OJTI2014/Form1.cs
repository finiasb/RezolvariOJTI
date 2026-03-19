using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2014
{
    public partial class Form1 : Form
    {
        ActiunileMele f;
        Grafic g;
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBBursa.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = (int)numericUpDown1.Value;
        }
        List<Point> points = new List<Point>();
        public void GetPoints(List<Point> points2, int profit)
        {
            points = points2;
            profitPierdereTotala2 = profit;
            if (g != null && !g.IsDisposed)
            {
                g.UpdatePoints(points);
            }
        }

        private void actiunileMeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g != null && !g.IsDisposed)
            {
                g.Hide();
            }

            if (f == null || f.IsDisposed)
            {
                f = new ActiunileMele(this);
                f.MdiParent = this;
                f.Show();
                f.Location = new Point(0, 60);
            }
        }
        int profitPierdereTotala2;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (f != null)
                f.StartTimer((int)numericUpDown1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (f != null)
            {
                f.StopTimer();

                using (StreamWriter sw = new StreamWriter(path + "rezultate.txt"))
                {
                    sw.WriteLine(profitPierdereTotala2.ToString());
                }
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (f != null)
                f.GetInterval((int)numericUpDown1.Value);
        }

        private void graficProfitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f != null && !f.IsDisposed)
            {
                f.Hide();
            }

            if (g == null || g.IsDisposed)
            {
                g = new Grafic(points);
                g.MdiParent = this;
                g.Show();
                g.Location = new Point(0, 60);
            }
        }
    }
}
