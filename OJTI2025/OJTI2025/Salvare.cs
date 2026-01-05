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
    public partial class Salvare : Form
    {
        private Image hartaExpeditie;

        public Salvare(Image harta)
        {
            InitializeComponent();
            hartaExpeditie = harta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Alege o insulă din listă!");
                return;
            }
            int ultimaInsula = listBox1.SelectedIndex + 2;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Imagine JPG|*.jpg";
            dlg.FileName = "TraseuExplorator.jpg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Creăm o imagine din harta curentă
                Bitmap bmp = new Bitmap(hartaExpeditie.Width, hartaExpeditie.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(hartaExpeditie, 0, 0, bmp.Width, bmp.Height);

                    Pen p = new Pen(Color.Green, 3);

                    Point[] insule = new Point[]
                                {
                    new Point(0,0),     
                    new Point(830,120),  
                    new Point(730,220),   
                    new Point(717,303),  
                    new Point(621,507),  
                    new Point(350,452),  
                    new Point(280,442),  
                    new Point(254,288), 
                    new Point(303,110),  
                    new Point(494,32),   
                    new Point(413,315),  
                    new Point(559,343)   
                    };

                    for (int i = 2; i <= ultimaInsula; i++)
                    {
                        g.DrawLine(p, insule[i - 1], insule[i]);
                    }
                }
                bmp.Save(dlg.FileName);

                MessageBox.Show("Imagine salvată cu succes!");
            }
        }
        private void buttonIesire_Click(object sender, EventArgs e)
        {
            this.Close();
            Exploratori f = new Exploratori();
            f.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Salvare_Load(object sender, EventArgs e)
        {

        }
    }
}
