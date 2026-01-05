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

namespace OJTI2022
{
    public partial class Form1 : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|Poluare3.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";
        
        public Form1()
        {
            InitializeComponent();
            stergere();
            incarcare();
        }

        private void stergere()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Truncate table Harti", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("Truncate table Masurare", con);
                cmd1.ExecuteNonQuery();
                /*SqlCommand cmd2 = new SqlCommand("Truncate table Utilizatori", con);
                cmd2.ExecuteNonQuery();*/
            }
        }
        private void incarcare()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "harti.txt";
                StreamReader rdr = new StreamReader(path);
                string line;
                while((line = rdr.ReadLine()) != null)
                {
                    string[] c = line.Split('#');
                    SqlCommand cmd = new SqlCommand("insert into harti(NumeHarta, FisierHarta) values(@nume, @fisier)", con);
                    cmd.Parameters.AddWithValue("@nume", c[0]);
                    cmd.Parameters.AddWithValue("@fisier", c[1]);
                    cmd.ExecuteNonQuery();
                }
                rdr.Close();
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "masurari.txt";
                StreamReader rdr = new StreamReader(path);
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    string[] c = line.Split('#');
                    int id = 0;
                    if (c[0] == "Harta Bucuresti")
                    {
                        id = 1;
                    }
                    else if (c[0] == "Harta Cluj-Napoca")
                    {
                        id = 2;
                    }
                    else if (c[0] == "Harta Constanta")
                    {
                        id = 3;
                    }
                    else if (c[0] == "Harta Iasi")
                    {
                        id = 4;
                    }
                    else if (c[0] == "Harta Sibiu")
                    {
                        id = 5;
                    }
                    DateTime myDate = DateTime.ParseExact(c[4], "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    SqlCommand cmd = new SqlCommand("insert into masurare(IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) values(@id, @pozX, @pozY, @val, @data)", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pozX", Int32.Parse(c[1]));
                    cmd.Parameters.AddWithValue("@pozY", Int32.Parse(c[2]));
                    cmd.Parameters.AddWithValue("@val", Int32.Parse(c[3]));
                    cmd.Parameters.AddWithValue("@data", myDate);
                    cmd.ExecuteNonQuery();
                }
                rdr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Parola from Utilizatori where NumeUtilizator = @nume", con);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    string pass = rdr[0].ToString();
                    if(pass == textBox2.Text)
                    {
                        using (SqlConnection con1 = new SqlConnection(constr))
                        {
                            con1.Open();

                            SqlCommand cmd1 = new SqlCommand("UPDATE Utilizatori SET UltimaUtilizare = @ultima WHERE NumeUtilizator = @nume", con1);
                            cmd1.Parameters.AddWithValue("@nume", textBox1.Text);
                            cmd1.Parameters.AddWithValue("@ultima", DateTime.Now);
                            cmd1.ExecuteNonQuery();
                        }

                        Vizualizare vizualizare = new Vizualizare(textBox1.Text);
                        vizualizare.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Parola este incorecta");
                        textBox2.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Email-ul este invalid sau nu exista in baza de date");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Inregistrare inregistrare = new Inregistrare();
            inregistrare.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
