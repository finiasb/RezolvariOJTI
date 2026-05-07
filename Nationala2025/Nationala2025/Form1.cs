using OpenTK.Graphics.OpenGL;
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

namespace Nationala2025
{
    public partial class Form1 : Form
    {
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Radar.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            sterge();
            incarcaZboruri();
            incarcaAeroporturi();
        }

        void sterge()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table zboruri", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("truncate table aeroporturi", con);
                cmd2.ExecuteNonQuery();
            }
        }

        void incarcaZboruri()
        {
            StreamReader rdr = new StreamReader(path + "Zboruri.txt");
            string line;
            rdr.ReadLine();
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into Zboruri(CodDecolare, CodAterizare, TimpStart, Durata, AzimutInitial, AzimutFinal, Descriere) values(@CodDecolare, @CodAterizare, @TimpStart, @Durata, @AzimutInitial, @AzimutFinal, @Descriere)", con);
                    cmd.Parameters.AddWithValue("@CodDecolare", c[0]);
                    cmd.Parameters.AddWithValue("@CodAterizare", c[1]);
                    cmd.Parameters.AddWithValue("@TimpStart", c[2]);
                    cmd.Parameters.AddWithValue("@Durata", int.Parse(c[3]));
                    cmd.Parameters.AddWithValue("@AzimutInitial", c[4]);
                    cmd.Parameters.AddWithValue("@AzimutFinal", c[5]);
                    cmd.Parameters.AddWithValue("@Descriere", c[6]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        void incarcaAeroporturi()
        {
            StreamReader rdr = new StreamReader(path + "Aeroporturi.txt");
            string line;
            rdr.ReadLine();
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into Aeroporturi(CodAeroport, Oras, Pasageri) values(@CodAeroport, @Oras, @Pasageri)", con);
                    cmd.Parameters.AddWithValue("@CodAeroport", c[0]);
                    cmd.Parameters.AddWithValue("@Oras", c[1]);
                    cmd.Parameters.AddWithValue("@Pasageri", int.Parse(c[2]));
                    cmd.ExecuteNonQuery();
                }
            }
        }
         
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "onti@csharp.ro" && textBox2.Text == "ONTI2025")
            {
                button2.Enabled = true;
                button5.Enabled = true;
                textBox3.Text = "Administrator";
            }
            else
            {
                MessageBox.Show("Eroare");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }
    }
}
