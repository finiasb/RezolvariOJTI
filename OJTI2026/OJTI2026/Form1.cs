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

namespace OJTI2026
{
    public partial class Form1 : Form
    {
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SpiralaDB.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            stergere();
            utilizator();
        }
        void stergere()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table utilizator", con);
                cmd.ExecuteNonQuery();
            }
        }

        void utilizator()
        {
            StreamReader rdr = new StreamReader(path + "Utilizatori.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('#');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into utilizator(nume, prenume, email, parola) values(@nume, @prenume, @email, @parola)", con);
                    cmd.Parameters.AddWithValue("@nume", c[0]);
                    cmd.Parameters.AddWithValue("@prenume", c[1]);
                    cmd.Parameters.AddWithValue("@email", c[2]);
                    cmd.Parameters.AddWithValue("@parola", c[3]);
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from utilizator where email = @email and parola = @parola", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@parola", textBox2.Text);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    Spirala joc = new Spirala(textBox1.Text);
                    this.Hide();
                    joc.Show();
                }
                else
                {
                    MessageBox.Show("Date de autentificare invalide!");
                    textBox1.Text = textBox2.Text = "";
                    textBox1.Focus();
                }
            }
        }
    }
}
