using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2025
{
    public partial class Radar : Form
    {
        int unghi = 0;
        int ora = 9, minute = 0, secunde = 0;
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Radar.mdf;Integrated Security=True;Connect Timeout=30";
        public Radar()
        {
            InitializeComponent();
            string oraText = "";
            if (ora < 10)
                oraText += "0" + ora;
            else
                oraText += ora + "";

            oraText += ":";

            if (minute < 10)
                oraText += "0" + minute;
            else
                oraText += minute + "";

            oraText += ":";

            if (secunde < 10)
                oraText += "0" + secunde;
            else
                oraText += secunde + "";
            textBox2.Text = oraText;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen penGreen = new Pen(Color.Green, 2);
            Brush bGreen = new SolidBrush(Color.DarkGreen);

            LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, 0), new Point(100, 50), Color.FromArgb(255, 0, 155, 0), Color.FromArgb(255, 0, 255, 0));  



            e.Graphics.FillEllipse(bGreen, 0, 0, 600, 600);
            e.Graphics.DrawLine(penGreen, 0, 300, 600, 300);
            e.Graphics.DrawLine(penGreen, 300, 0, 300, 600);
            e.Graphics.DrawEllipse(penGreen, 0, 0, 600, 600);
            e.Graphics.DrawEllipse(penGreen, 100, 100, 400, 400);
            e.Graphics.DrawEllipse(penGreen, 200, 200, 200, 200);


            double radieni1 = (Math.PI * (double)unghi) / 180;
            double radieni2 = (Math.PI * ((double)unghi - 10)) / 180;

            double x1 = 300 + (300 * Math.Cos(radieni1));
            double y1 = 300 + (300 * Math.Sin(radieni1));
            double x2 = 300 + (300 * Math.Cos(radieni2));
            double y2 = 300 + (300 * Math.Sin(radieni2));
            e.Graphics.DrawLine(penGreen, 300, 300, (int)x1, (int)y1);
            e.Graphics.DrawLine(penGreen, 300, 300, (int)x2, (int)y2);

            Point p1 = new Point(300, 300);
            Point p2 = new Point((int)x1, (int)y1);
            Point p3 = new Point((int)x2, (int)y2);

            PointF[] points = { p1, p2, p3 };



            e.Graphics.FillPolygon(linGrBrush, points);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            unghi++;
            panel1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Start")
            {
                panel1.Visible = true;
                button1.Text = "Stop";
                timer1.Start();
                timer2.Start();

            }
            else
            {
                panel1.Visible = false;
                button1.Text = "Start";
                timer1.Stop();
                timer2.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            secunde++;

            if(secunde == 60)
            {
                secunde = 0;
                minute++;
            }
            if(minute == 60)
            {
                ora++;
                minute = 0;
            }
            string oraText = "";
            if (ora < 10)
                oraText += "0" + ora;
            else
                oraText += ora + "";

            oraText += ":";

            if (minute < 10)
                oraText += "0" + minute;
            else
                oraText += minute + "";

            oraText += ":";

            if (secunde < 10)
                oraText += "0" + secunde;
            else
                oraText += secunde + "";
            textBox2.Text = oraText;

adaugareLista();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer2.Interval = trackBar1.Value * 1000;
            if(trackBar1.Value == 20)
            {
                timer1.Interval = 5;
            }
            else
            {
                timer1.Interval = (100) - trackBar1.Value * 5;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ora = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            secunde = DateTime.Now.Second;


            string oraText = "";
            if (ora < 10)
                oraText += "0" + ora;
            else
                oraText += ora + "";

            oraText += ":";

            if (minute < 10)
                oraText += "0" + minute;
            else
                oraText += minute + "";

            oraText += ":";

            if (secunde < 10)
                oraText += "0" + secunde;
            else
                oraText += secunde + "";
            textBox2.Text = oraText;
        }


        string transformare(string cod)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oras from aeroporturi where CodAeroport = @cod", con);
                cmd.Parameters.AddWithValue("@cod", cod);
                SqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    return rdr[0].ToString();
                }
            }

            return "";
        }

        int InSecunde(int h, int m, int s)
        {
            return h * 3600 + m * 60 + s;
        }

        void adaugareLista()
        {
            int timpCurentSecunde = InSecunde(ora, minute, secunde);

            List<string> avioaneActive = new List<string>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TimpStart, Durata, CodDecolare, CodAterizare FROM Zboruri", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DateTime timpStart = (DateTime)rdr["TimpStart"];
                    int durata = Convert.ToInt32(rdr["Durata"]);
                    string codDec = rdr["CodDecolare"].ToString();
                    string codAter = rdr["CodAterizare"].ToString();

                    // Convertim TimpStart în secunde
                    int startSecunde = InSecunde(timpStart.Hour, timpStart.Minute, timpStart.Second);
                    int sfarsitSecunde = startSecunde + durata;

                    // Verificăm dacă avionul este vizibil la ora curentă
                    if (timpCurentSecunde >= startSecunde && timpCurentSecunde <= sfarsitSecunde)
                    {
                        string orasDec = transformare(codDec);
                        string orasAter = transformare(codAter);
                        // Formatul cerut: CodDecolare - Oras Decolare - OrasAterizare
                        string infoZbor = $"{codDec} - {orasDec} - {orasAter}";
                        avioaneActive.Add(infoZbor);
                    }
                }
            }

            // 2. Actualizăm ListBox-ul
            // Ștergem zborurile care nu mai sunt pe radar
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (!avioaneActive.Contains(listBox1.Items[i].ToString()))
                {
                    listBox1.Items.RemoveAt(i);
                }
            }

            // Adăugăm zborurile noi care au apărut
            foreach (string zbor in avioaneActive)
            {
                if (!listBox1.Items.Contains(zbor))
                {
                    listBox1.Items.Add(zbor);
                }
            }
        }



    }
}
