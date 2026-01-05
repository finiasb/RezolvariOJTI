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
    public partial class MdiForm : Form
    {
        int _x;
        public MdiForm(int x)
        {
            
            InitializeComponent();
            _x = x;
            if(x == 0)
            {
                turistToolStripMenuItem.Visible = false;

            }else if(x == 1) 
            {
                administratorToolStripMenuItem.Visible = false;
            }
        }

        public MdiForm()
        {

            InitializeComponent();
            
        }

        private void iesirerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void administratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaCroaziera lista = new ListaCroaziera();
            lista.MdiParent = this;
            lista.Show();
        }

        private void turistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formTuristi turist = new formTuristi();
            turist.MdiParent = this;
            turist.Show();
        }
    }
}
