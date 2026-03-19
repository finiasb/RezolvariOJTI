using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI20222
{
    public partial class Harti : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fineas\source\repos\OJTI20222\OJTI20222\bin\Debug\Poluare.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        int id = 0;
        public Harti(string nume)
        {
            InitializeComponent();
            label1.Text = "utilizator: " + nume;
            label2.Text = "Harta";
            label3.Text = "Data";
            label4.Text = "Filtru";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image = Image.FromFile(path + "Harti\\harta_bucuresti.png");
                pictureBox2.Image = Image.FromFile(path + "Harti\\harta_bucuresti.png");
                id = 1;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image = Image.FromFile(path + "Harti\\harta_cluj.png");
                pictureBox2.Image = Image.FromFile(path + "Harti\\harta_cluj.png");
                id = 2;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                pictureBox1.Image = Image.FromFile(path + "Harti\\harta_constanta.png");
                pictureBox2.Image = Image.FromFile(path + "Harti\\harta_constanta.png");
                id = 3;
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                pictureBox1.Image = Image.FromFile(path + "Harti\\harta_iasi.png");
                pictureBox2.Image = Image.FromFile(path + "Harti\\harta_iasi.png");
                id = 4;
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                pictureBox1.Image = Image.FromFile(path + "Harti\\harta_sibiu.png");
                pictureBox2.Image = Image.FromFile(path + "Harti\\harta_sibiu.png");
                id = 5;
            }
        }
        private bool esteLiber(int x, int y)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int x1 = int.Parse(rdr[0].ToString());   
                    int y1 = int.Parse(rdr[1].ToString());
                    if(x >= x1 && x <= x1 + 20 && y >= y1 && y <= y1 + 20)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool estePunctMaiMareCaForty(int x, int y)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY, ValoareMasurare from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int x1 = int.Parse(rdr[0].ToString());
                    int y1 = int.Parse(rdr[1].ToString());
                    int val = int.Parse(rdr[2].ToString());
                   
                    if (x >= x1 && x <= x1 + 20 && y >= y1 && y <= y1 + 20 && val > 40)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool Forty(int x, int y)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY, ValoareMasurare from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int x1 = int.Parse(rdr[0].ToString());
                    int y1 = int.Parse(rdr[1].ToString());
                    int val = int.Parse(rdr[2].ToString());

                    if (x >= x1 && x <= x1 + 20 && y >= y1 && y <= y1 + 20 && val < 40)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Font font = new Font("Arial", 15);
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY, ValoareMasurare, DataMasurare from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DateTime dt = DateTime.Parse(rdr[3].ToString()).Date;
                    Point p = new Point(int.Parse(rdr[0].ToString()), int.Parse(rdr[1].ToString()));
                    Rectangle rect = new Rectangle(p.X, p.Y, 30, 30);
                    if (dt.ToString() == dateTimePicker1.Value.Date.ToString())
                    {
                        if (comboBox2.SelectedIndex == 0)
                        {
                            if (int.Parse(rdr[2].ToString()) <= 20)
                            {
                                Pen pen = new Pen(Color.Green, 3);
                                Brush b = new SolidBrush(Color.Green);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                            else
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 1)
                        {
                            if (int.Parse(rdr[2].ToString()) <= 20)
                            {
                                Pen pen = new Pen(Color.Green, 3);
                                Brush b = new SolidBrush(Color.Green);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 2)
                        {
                            if (int.Parse(rdr[2].ToString()) >= 20 && int.Parse(rdr[2].ToString()) <= 40)
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 3)
                        {
                            if (int.Parse(rdr[2].ToString()) > 40)
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            button2.Visible = false;
            button1.Visible = true;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            button1.Visible = false;
            button2.Visible = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (esteLiber(e.X, e.Y))
            {
                using(AdaugaMasurare frm = new AdaugaMasurare(id, e.X, e.Y, dateTimePicker1.Value))
                {
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox2.Invalidate();
                        pictureBox1.Invalidate();
                    }
                }
                pictureBox2.Invalidate();
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show("Acolo este deja o valoare");
            }
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Font font = new Font("Arial", 15);
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY, ValoareMasurare, DataMasurare from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DateTime dt = DateTime.Parse(rdr[3].ToString()).Date;
                    Point p = new Point(int.Parse(rdr[0].ToString()), int.Parse(rdr[1].ToString()));
                    Rectangle rect = new Rectangle(p.X, p.Y, 30, 30);
                    if (dt.ToString() == dateTimePicker1.Value.Date.ToString())
                    {
                        if (comboBox2.SelectedIndex == 0)
                        {
                            if (int.Parse(rdr[2].ToString()) <= 20)
                            {
                                Pen pen = new Pen(Color.Green, 3);
                                Brush b = new SolidBrush(Color.Green);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                            else
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 1)
                        {
                            if (int.Parse(rdr[2].ToString()) <= 20)
                            {
                                Pen pen = new Pen(Color.Green, 3);
                                Brush b = new SolidBrush(Color.Green);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 2)
                        {
                            if (int.Parse(rdr[2].ToString()) >= 20 && int.Parse(rdr[2].ToString()) <= 40)
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                        else if (comboBox2.SelectedIndex == 3)
                        {
                            if (int.Parse(rdr[2].ToString()) > 40)
                            {
                                Pen pen = new Pen(Color.Red, 3);
                                Brush b = new SolidBrush(Color.Red);
                                g.DrawEllipse(pen, rect);
                                g.DrawString(rdr[2].ToString(), font, b, p);
                            }
                        }
                    }
                }
            }
        }
        Point p1 = new Point();
        Point p2 = new Point();
        Point p3 = new Point();
        void getPunctePoluate()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Top (2) PozitieX, PozitieY, ValoareMasurare, DataMasurare from Masurare where IdHarta = @id ORDER BY ValoareMasurare DESC", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader rdr = cmd.ExecuteReader();
                int index = 0;
                while(rdr.Read())
                {
                    DateTime dt = DateTime.Parse(rdr[3].ToString()).Date;
                    if (dt.ToString() == dateTimePicker1.Value.Date.ToString())
                    {
                        int x = int.Parse(rdr[0].ToString());
                        int y = int.Parse(rdr[1].ToString());
                        if (index == 0)
                        {
                            p2.X = x; p2.Y = y;
                            index++;
                        }
                        else if (index == 1)
                        {
                            p3.X = x; p3.Y = y;
                        }
                    }
                }
            }
        }
        private async void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = pictureBox2.CreateGraphics();
            Pen pen = new Pen(Color.Red, 3);
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);
            p3 = new Point(0, 0);

            if(Forty(e.X, e.Y)) 
            {
                MessageBox.Show("Selectați un punct de pe hartă corespunzător unei măsurări existente în baza de date");
                return;
            }

            if (estePunctMaiMareCaForty(e.X, e.Y))
            {
                p1.X = e.X; p1.Y = e.Y;
                getPunctePoluate();
                if(p1.X == p2.X && p1.X == p2.Y)
                {
                    g.DrawLine(pen, p1, p3);
                    double DistantaP1P3 = Math.Sqrt((p3.X - p1.X) * (p3.X - p1.X) + (p3.Y - p1.Y) * (p3.Y - p1.Y));
                    MessageBox.Show(DistantaP1P3 + "");

                }
                else if((p1.X == p3.X && p1.Y == p3.Y))
                {
                    g.DrawLine(pen, p1, p2);
                    double DistantaP1P2 = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
                    MessageBox.Show(DistantaP1P2 + "");
                }
                else
                {
                    double DistantaP1P2 = Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
                    double DistantaP1P3 = Math.Sqrt((p3.X - p1.X) * (p3.X - p1.X) + (p3.Y - p1.Y) * (p3.Y - p1.Y));
                    double suma = DistantaP1P2 + DistantaP1P3;
                    MessageBox.Show(suma + "");
                    if (DistantaP1P2 > DistantaP1P3)
                    {
                        g.DrawLine(pen, p1, p3);
                        await Task.Delay(1000);
                        g.DrawLine(pen, p3, p2);
                    }
                    else
                    {
                        g.DrawLine(pen, p1, p2);
                        await Task.Delay(1000);
                        g.DrawLine(pen, p2, p3);
                    }

                }
            }
        }

        private void Harti_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Q) 
            {
            System.Environment.Exit(0);
            }
        }
    }
}
