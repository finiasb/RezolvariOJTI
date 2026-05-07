using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Nationala2025
{
    public partial class Aterizare : Form
    {
        int unghi = 0;
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Radar.mdf;Integrated Security=True;Connect Timeout=30";
        int unghi2 = -1;
        Point p = new Point(-1, -1);

        public Aterizare()
        {
            InitializeComponent();
            timer1.Start();
            this.ClientSize = new Size(1200, 600);
            pictureBox1.Size = new System.Drawing.Size(900, 600);
            lista();
        }
        int picX, picY;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen penGreen = new Pen(Color.Yellow, 2);
            Brush bGreen = new SolidBrush(Color.Yellow);
            Pen penW = new Pen(Color.White, 2);
            Pen penB = new Pen(Color.Black, 1);

            LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(0, 0), new Point(100, 50), Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 175, 255, 0));

            e.Graphics.DrawEllipse(penGreen, 200, 0, 600, 600);
            e.Graphics.DrawEllipse(penGreen, 300, 100, 400, 400);
            e.Graphics.DrawEllipse(penGreen, 400, 200, 200, 200);

            double radieni1 = (Math.PI * (double)unghi) / 180;
            double radieni2 = (Math.PI * ((double)unghi - 10)) / 180;
            double Radieni3 = (Math.PI * ((double)unghi2)) / 180;

            double x1 = 500 + (300 * Math.Cos(radieni1));
            double y1 = 300 + (300 * Math.Sin(radieni1));
            double x2 = 500 + (300 * Math.Cos(radieni2));
            double y2 = 300 + (300 * Math.Sin(radieni2));
            if (unghi2 != -1) 
            {
                double x3 = 500 + (600 * Math.Cos(Radieni3));
                double y3 = 300 + (600 * Math.Sin(Radieni3));
                e.Graphics.DrawLine(penW, 500, 300, (int)x3, (int)y3);
                 picX = (int)x3;
                 picY = (int)y3;
            }
            if(p.X != -1 && p.Y != -1 )
            {
                e.Graphics.FillEllipse(bGreen, p.X, p.Y, 15, 15);
                e.Graphics.DrawLine(penW, 500, 300, p.X+7 , p.Y + 7);
                //y = 392
                Rectangle r = new Rectangle();
                if (p.Y > 392)
                {
                    r = new Rectangle(p.X - 50, 392, 100, p.Y - 392);

                }
                else
                {
                    r = new Rectangle(p.X - 50, p.Y, 100, Math.Abs(392 - p.Y));
                }

                float startAngle = 90.0F;
                float sweepAngle = 180.0F;

                e.Graphics.DrawArc(penW, r, startAngle, sweepAngle);

                e.Graphics.DrawLine(penW, p.X, 392, 1000, 392);
            }

            if(p.X == -1)
                e.Graphics.DrawRectangle(penB, 50, 250, 200, 200);

            e.Graphics.DrawLine(penGreen, 500, 300, (int)x1, (int)y1);
            e.Graphics.DrawLine(penGreen, 500, 300, (int)x2, (int)y2);

            Point p1 = new Point(500, 300);
            Point p2 = new Point((int)x1, (int)y1);
            Point p3 = new Point((int)x2, (int)y2);

            PointF[] points = { p1, p2, p3 };



            e.Graphics.FillPolygon(linGrBrush, points);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            unghi++;
            pictureBox1.Invalidate();
            
        }
        string transformare(string cod)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oras from aeroporturi where CodAeroport = @cod", con);
                cmd.Parameters.AddWithValue("@cod", cod);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return rdr[0].ToString();
                }
            }

            return "";
        }
        void lista()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select codDecolare, codAterizare from zboruri where azimutFinal = @az", con);
                cmd.Parameters.AddWithValue("@az", "0");
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string codDec = Convert.ToString(rdr[0].ToString());
                    string codAterizare = Convert.ToString(rdr[1].ToString());
                    string orasDec = transformare(codDec);
                    string orasAter = transformare(codAterizare);
                    listBox1.Items.Add(codDec + "-" + orasDec);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            unghi2 = -1;
            p.X = -1;
            p.Y = -1;
            string[] parts = listBox1.SelectedItem.ToString().Split('-');

            string cod = parts[0];
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select azimutinitial from zboruri where azimutFinal = @az and Coddecolare = @cod", con);
                cmd.Parameters.AddWithValue("@az", "0");
                cmd.Parameters.AddWithValue("@cod", cod);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string azimutinital = Convert.ToString(rdr[0].ToString());

                    string[] parti = azimutinital.Split(' ');

                    if (parti[0] == "N" && parti[2] == "E")
                    {
                        unghi2 = int.Parse(parti[1]);
                    }
                    if (parti[0] == "S" && parti[2] == "E")
                    {
                        unghi2 = 180 - int.Parse(parti[1]);
                    }
                    if (parti[0] == "S" && parti[2] == "V")
                    {
                        unghi2 = 180 + int.Parse(parti[1]);
                    }
                    if (parti[0] == "N" && parti[2] == "V")
                    {
                        unghi2 = 360 - int.Parse(parti[1]);
                    }

                    this.Invalidate();

                }
            }
        }
        private async void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X >= 50 && e.X <= 250 && e.Y >= 200 && e.Y <= 450 && unghi2 != -1)
            {
                p.X = e.X;
                p.Y = e.Y;
                pictureBox2.Visible = true;
                pictureBox2.Location = new Point(500, 300);
                await  Task.Delay(2000);
                pictureBox2.Location = new Point(e.X, e.Y);
                await Task.Delay(2000);
                pictureBox2.Location = new Point(e.X, 392);
                await Task.Delay(2000);
                pictureBox2.Location = new Point(600, 392);
                await Task.Delay(2000);
                pictureBox2.Visible = false;


            }
            else if(unghi2 != -1 && p.X == -1)
            {
                MessageBox.Show("Pozitionare de aterizare gresita");
            }

        }
    }
}
