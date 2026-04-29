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

namespace ONTI_2017
{
    public partial class Form1 : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";
        int id;
        public Form1()
        {
            InitializeComponent();
            stergere();
            incarcaredb();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select IdUser, parola from Utilizatori where Email = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    id = Int32.Parse(rdr[0].ToString());
                    string pass = rdr[1].ToString();
                    if(textBox2.Text == pass) 
                    {
                        FrmVacanta vacanta = new FrmVacanta(textBox1.Text, id);
                        vacanta.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Eroare de autentificare!");
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Eroare de autentificare!");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmInregistrare auth = new FrmInregistrare();
            auth.Show();
        }
        private void stergere()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table vacante", con);
                cmd.ExecuteNonQuery();
            }
        }
        private void incarcaredb()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader rdr = new StreamReader(path + "Vacante.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('|');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Vacante(NumeVacanta, Descriere, CaleFisier, Pret, NrLocuri) values(@NumeVacanta, @Descriere, @CaleFisier, @Pret, @NrLocuri)", con);
                    cmd.Parameters.AddWithValue("@NumeVacanta", c[0].ToString());
                    cmd.Parameters.AddWithValue("@Descriere", c[1].ToString());
                    cmd.Parameters.AddWithValue("@CaleFisier", path + "Imagini\\" + c[0].ToString() + ".png");
                    cmd.Parameters.AddWithValue("@Pret", Int32.Parse(c[2].ToString()));
                    cmd.Parameters.AddWithValue("@NrLocuri", Int32.Parse(c[3].ToString()));
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }
    }
}
