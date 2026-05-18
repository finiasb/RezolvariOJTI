using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace cosmos2
{
    public partial class AlegeOptiunea : Form
    {
        public AlegeOptiunea()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                Revolutie revolutie = new Revolutie();
                revolutie.Show();
                this.Hide();
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                SpaceWar spaceWar = new SpaceWar();
                spaceWar.Show();
                this.Hide();
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                MessageBox.Show("Selectati doar unul");
            }
        }
    }
}
