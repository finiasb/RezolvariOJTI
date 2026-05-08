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
            // Resetăm contoarele (frecvențele)
            n = ne = eee = se = s = sv = v = nv = 0;

            openFileDialog1.InitialDirectory = path;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string name = openFileDialog1.FileName;
                using (StreamReader rdr = new StreamReader(name))
                {
                    string line;
                    // Citim liniile și numărăm aparițiile fiecărei direcții
                    while ((line = rdr.ReadLine()) != null)
                    {
                        string[] c = line.Split(';');
                        if (c.Length < 1) continue;

                        string directie = c[0].Trim().ToUpper();
                        switch (directie)
                        {
                            case "N": n++; break;
                            case "NE": ne++; break;
                            case "E": eee++; break;
                            case "SE": se++; break;
                            case "S": s++; break;
                            case "SV": sv++; break;
                            case "V": v++; break;
                            case "NV": nv++; break;
                        }
                    }
                }

                // Curățăm datele vechi din grafic
                chart1.Series[0].Points.Clear();

                // Adăugăm punctele: X este eticheta (direcția), Y este frecvența
                chart1.Series[0].Points.AddXY("N", n);
                chart1.Series[0].Points.AddXY("NE", ne);
                chart1.Series[0].Points.AddXY("E", eee);
                chart1.Series[0].Points.AddXY("SE", se);
                chart1.Series[0].Points.AddXY("S", s);
                chart1.Series[0].Points.AddXY("SV", sv);
                chart1.Series[0].Points.AddXY("V", v);
                chart1.Series[0].Points.AddXY("NV", nv);
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
