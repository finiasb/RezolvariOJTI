using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Nationala2025
{
    public partial class Roza : Form
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        int n = 0;
        int ne = 0;
        int nv = 0;
        int s = 0;
        int se = 0;
        int sv = 0;
        int eee = 0;
        int v = 0;

        public Roza()
        {
            InitializeComponent();
        }
        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            n = 0;
            ne = 0;
            nv = 0;
            s = 0;
            se = 0;
            sv = 0;
            eee = 0;
            v = 0;
            i = 0;
            openFileDialog1.InitialDirectory = path;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = openFileDialog1.FileName;
                 i = 0;
                StreamReader rdr = new StreamReader( name);
                string line;
                rdr.ReadLine();
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                while ((line = rdr.ReadLine()) != null)
                {
                    string[] c = line.Split(';');
                    if (c[0].ToString() == "N")
                    {
                        n += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "NE")
                    {
                        ne += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "NV")
                    {
                        nv += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "S")
                    {
                        s += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "SE")
                    {
                        se += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "SV")
                    {
                        sv += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "V")
                    {
                        v += int.Parse(c[1]);
                    }
                    else if (c[0].ToString() == "E")
                    {
                        eee += int.Parse(c[1]);
                    }
                    i++;
                }
                if(n == 0)
                {
                    n = (nv + ne) / i; 
                }else if(ne == 0)
                {
                    ne = (eee + n) / i;
                }
                else if (eee == 0)
                {
                    eee = (ne + se) / i;
                }
                else if (se == 0)
                {
                    se = (eee + s) / i;
                }
                else if (s == 0)
                {
                    s = (se + sv) / i;
                }
                else if (sv == 0)
                {
                    sv = (s + v) / i;
                }
                else if (v == 0)
                {
                    v = (nv + sv) / i;
                }
                else if (nv == 0)
                {
                    nv = (v + n) / i;   
                }

                chart1.Series[0].Points.AddXY(n, n / i);
                chart1.Series[0].Points.AddXY(ne, ne / i);
                chart1.Series[0].Points.AddXY(eee, eee / i);
                chart1.Series[0].Points.AddXY(se, se / i);
                chart1.Series[0].Points.AddXY(s, s / i);
                chart1.Series[0].Points.AddXY(sv, sv / i);
                chart1.Series[0].Points.AddXY(v, v / i);
                chart1.Series[0].Points.AddXY(nv, nv / i);

               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
