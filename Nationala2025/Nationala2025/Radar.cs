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

           // adaugareLIsta();

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


        void adaugareLIsta()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select timpStart, durata, codDecolare, codAterizare from zboruri where ", con);
                SqlDataReader rdr= cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string timp = rdr[0].ToString();
                    int durata = Convert.ToInt32(rdr[1].ToString());
                    string codDec = Convert.ToString(rdr[2].ToString());
                    string codAter = Convert.ToString(rdr[3].ToString());

                    string orasDec = transformare(codDec);
                    string orasAter = transformare(codAter);

                    int durataMinute = durata / 60;

                    int durataSecunde = durata -  durataMinute * 60;

                    string[] parti = timp.Split(':');
                    int partiSecunde;
                    int partiMinute;
                    int partiOre;


                    if (parti[0].StartsWith("0"))
                    {
                        partiOre = int.Parse(parti[0].Substring(1));
                    }
                    else
                    {
                        partiOre = int.Parse(parti[2]);
                    }

                    if (partiOre != ora)
                        continue;

                    if (parti[1].StartsWith("0"))
                    {
                        partiMinute = int.Parse(parti[1].Substring(1));
                    }
                    else
                    {
                        partiMinute = int.Parse(parti[1]);
                    }

                    if (parti[2].StartsWith("0"))
                    {
                        partiSecunde = int.Parse(parti[2].Substring(1));
                    }
                    else
                    {
                        partiSecunde = int.Parse(parti[2]);
                    }

                    if (partiSecunde >= secunde && partiMinute >= minute && partiOre >= ora && secunde >= partiSecunde + durataSecunde && minute >= partiMinute + durataMinute) 
                    {
                    }
                    else if (listBox1.Items.Contains(orasDec + "-" + orasDec) && !(partiSecunde >= secunde && partiMinute >= minute && partiOre >= ora && secunde >= partiSecunde + durataSecunde && minute >= partiMinute + durataMinute))
                    {
                        listBox1.Items.Remove(orasDec + "-" + orasDec);

                    }


                }
            }
        }
    }
}
