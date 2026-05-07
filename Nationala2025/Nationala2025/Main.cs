using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Nationala2025
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(1324, 942);
        }

        private void ieșireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void rozaVânturilorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            Roza roza = new Roza();
            roza.MdiParent = this;
            roza.Show();
        }

        private void ghideazaAterizareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            Aterizare aterizare = new Aterizare();
            aterizare.MdiParent = this;
            aterizare.Show();
        }

        private void turnulDeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            Radar radar = new Radar();
            radar.MdiParent = this;
            radar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f .Show();
            this.Hide();
        }
    }
}
