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

namespace _15___Simulare__1_Olimpiadă
{
    public partial class Rascumparare : Form
    {
        List<int> ints = new List<int>();
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\BD_NumerePrime.mdf"";Integrated Security=True;Connect Timeout=30";
        int _scor;
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Rascumparare(List<int> ints, int scor)
        {
            InitializeComponent();
            this.ints = ints;
            _scor = scor;
            int y = 30;
            int x = 30;
            for(int i = 0; i < ints.Count; i++)
            {
                if(i % 5 == 0)
                {
                    y += 30;
                    x = 30;
                }
                Button but = new Button();
                but.Size = new Size(30, 30);
                but.Location = new Point(x + 10, y);
                but.Text = ints[i].ToString();
                but.Click += button1_Click;
                x += 30;
                Controls.Add(but);
            }
            incarcacombo();
            timer1.Start();
        }
        int scorCurent;


        private void incarcacombo()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Nume from savanti", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    comboBox1.Items.Add(rdr[0].ToString());   
                }
            }
        }

        private void Rascumparare_MouseClick(object sender, MouseEventArgs e)
        {
        }
        string numepoza = "";

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Random rnd = new Random();
            int x = rnd.Next(1, 8);
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select FileName from savanti where idSavant = @id", con);
                cmd.Parameters.AddWithValue("@id", x);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    numepoza = rdr[0].ToString();
                }
            }
            pictureBox1.Image = Image.FromFile(path + $"Personaje\\{numepoza}");
            btn.Visible = false;
            scorCurent = Int32.Parse(btn.Text);
        }
        int i = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectatIndex = comboBox1.SelectedIndex;
            string nume = comboBox1.Text;
            int idFile = 1;
            int idNume = 2;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select idSavant from savanti where FileName = @file", con);
                cmd.Parameters.AddWithValue("@file", numepoza);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    idFile = Int32.Parse(rdr[0].ToString());    
                }
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select idSavant from savanti where Nume = @nume", con);
                cmd.Parameters.AddWithValue("@nume", nume);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    idNume = Int32.Parse(rdr[0].ToString());
                }
            }

            if(idFile == idNume)
            {
                _scor += scorCurent;
                MessageBox.Show("Corect");
                pictureBox1.Image = null;
                comboBox1.Text = "";
                i++;
            }
            else
            {
                MessageBox.Show("Ai Gresit");
                this.Hide();
                bool x = true;
                Form1 form = new Form1(_scor, x);
                form.Show();

                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ints.Count == i)
            {

                timer1.Stop();
                MessageBox.Show("Rascumpararea s a incheiat");
                this.Hide();
                bool x = true;
                Form1 form = new Form1(_scor, x);
                form.Show();
            }
        }
    }
}
