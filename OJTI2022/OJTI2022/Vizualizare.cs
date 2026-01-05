using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OJTI2022
{
    public partial class Vizualizare : Form
    {
        private string path;
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|Poluare3.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private int id = 0;
        private int mouseX, mouseY;
        string _nume;
        public Vizualizare(string nume)
        {
            InitializeComponent();
            incarcareComboBox();
            comboBox2.SelectedIndex = 0;
            pictureBox1.Tag = "default";
            _nume = nume;   
            label2.Text = "Utilizator: " + nume;
        }
        private void incarcareComboBox()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select NumeHarta from Harti", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comboBox1.Items.Add(rdr[0].ToString());
                }rdr.Close();
                comboBox1.Text = "Selecteaza o harta";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select FisierHarta from Harti where NumeHarta = @nume", con);
                cmd.Parameters.AddWithValue("@nume", comboBox1.SelectedItem.ToString());
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    string fisier = rdr[0].ToString();
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "Harti\\" + fisier;
                    pictureBox1.Tag = "altceva";
                    pictureBox1.Image = Image.FromFile(path);
                    pictureBox2.Image = Image.FromFile(path);
                }
                rdr.Close();
            }
        }
        private void returnareId()
        {
            if (comboBox1.Text == "Harta Bucuresti")
            {
                id = 1;
            }
            else if (comboBox1.Text == "Harta Cluj-Napoca")
            {
                id = 2;
            }
            else if (comboBox1.Text == "Harta Constanta")
            {
                id = 3;
            }
            else if (comboBox1.Text == "Harta Iasi")
            {
                id = 4;
            }
            else if (comboBox1.Text == "Harta Sibiu")
            {
                id = 5;
            }
        }

        public void desenare()
        {
            if (string.IsNullOrEmpty(path)) 
                return;

            Bitmap bmp = new Bitmap(Image.FromFile(path));
            returnareId();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select PozitieX, PozitieY, ValoareMasurare, DataMasurare from Masurare where IdHarta = @id", con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int x = Convert.ToInt32(rdr[0]);
                    int y = Convert.ToInt32(rdr[1]);
                    int v = Convert.ToInt32(rdr[2]);
                    DateTime d = Convert.ToDateTime(rdr[3]);

                    if (d.Date == dateTimePicker1.Value.Date && comboBox2.SelectedIndex == 0)
                    {
                        Color c = v < 20 ? Color.Green : v <= 40 ? Color.Yellow : Color.Red;

                        using (Graphics g = Graphics.FromImage(bmp))
                        using (Pen pen = new Pen(c, 2))
                        using (Brush brush = new SolidBrush(c))
                        using (Font font = new Font("Times New Roman", 12))
                        {
                            g.DrawEllipse(pen, x, y, 20, 20);
                            g.DrawString(v.ToString(), font, brush, x - 1, y);
                        }
                    }
                    else if (d.Date == dateTimePicker1.Value.Date && comboBox2.SelectedIndex == 1 && v < 20)
                    {
                        Color c = Color.Green;

                        using (Graphics g = Graphics.FromImage(bmp))
                        using (Pen pen = new Pen(c, 2))
                        using (Brush brush = new SolidBrush(c))
                        using (Font font = new Font("Times New Roman", 12))
                        {
                            g.DrawEllipse(pen, x, y, 20, 20);
                            g.DrawString(v.ToString(), font, brush, x - 1, y);
                        }
                    }
                    else if (d.Date == dateTimePicker1.Value.Date && comboBox2.SelectedIndex == 2 && v >= 20 && v <= 40)
                    {
                        Color c = Color.Yellow;

                        using (Graphics g = Graphics.FromImage(bmp))
                        using (Pen pen = new Pen(c, 2))
                        using (Brush brush = new SolidBrush(c))
                        using (Font font = new Font("Times New Roman", 12))
                        {
                            g.DrawEllipse(pen, x, y, 20, 20);
                            g.DrawString(v.ToString(), font, brush, x - 1, y);
                        }
                    }
                    else if (d.Date == dateTimePicker1.Value.Date && comboBox2.SelectedIndex == 3 && v > 40)
                    {
                        Color c = Color.Red;

                        using (Graphics g = Graphics.FromImage(bmp))
                        using (Pen pen = new Pen(c, 2))
                        using (Brush brush = new SolidBrush(c))
                        using (Font font = new Font("Times New Roman", 12))
                        {
                            g.DrawEllipse(pen, x, y, 20, 20);
                            g.DrawString(v.ToString(), font, brush, x - 1, y);
                        }
                    }
                    //MessageBox.Show($"{d.Date}, {dateTimePicker1.Value.Date}");
                }
            }
            pictureBox1.Image = bmp;
            pictureBox2.Image = bmp;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            desenare();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "Selecteaza o harta";
            comboBox2.SelectedIndex = 0;
            pictureBox1.Image = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + "Harti\\default_harta.png");
            pictureBox2.Image = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + "Harti\\default_harta.png");
            dateTimePicker1.Value = DateTime.Now;
            pictureBox1.Tag = "default";
            id = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        /*private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
{
   using(SqlConnection con = new SqlConnection(constr))
   {
       con.Open();
       SqlCommand cmd = new SqlCommand("SELECT TOP 2 PozitieX, PozitieY, ValoareMasurare FROM Masurare WHERE IdHarta = @idHarta ORDER BY ValoareMasurare DESC", con);
       cmd.Parameters.AddWithValue("@idHarta", id);
       SqlDataReader dr = cmd.ExecuteReader();
       while (dr.Read())
       {
           SqlCommand cmd1 = new SqlCommand("SELECT ValoareMasurare FROM Masurare WHERE IdHarta = @id AND ABS(PozitieX - @x) <= 10 AND ABS(PozitieY - @y) <= 10", con);
           cmd1.Parameters.AddWithValue("@x", e.X);
           cmd1.Parameters.AddWithValue("@y", e.Y);
           SqlDataReader dr2 = cmd1.ExecuteReader();
           if (dr2.Read())
           {
               if (Int32.Parse(dr2[0].ToString()) < 40)
               {
                   MessageBox.Show("Selectați un punct de pe hartă corespunzător unei măsurări existente în baza de date!");
                   return;
               }
           }
       }
   }
}*/



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        { 
            //de verificat daca e click ul intr un punct ales deja
            if (pictureBox1.Tag.ToString() != "default")
            {
                mouseX = e.X;
                mouseY = e.Y;
                AdaugaMasurare adaugaMasurare = new AdaugaMasurare(this, id, e.X, e.Y);
                adaugaMasurare.Show();
            }
            else
            {
                MessageBox.Show("Pe imaginea default nu poti pune puncte");
            }
        }
    }
}
