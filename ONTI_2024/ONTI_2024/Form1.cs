using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ONTI_2024
{
    public partial class Form1 : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True;Connect Timeout=30";

        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            stergere();
            incarcadb();
            incarcadb2();
            textBox1.Text = "albua@gmail.com";
            textBox2.Text = "ZbatxetZ901";
        }

        private void stergere()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table utilizatori", con);        
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("truncate table Inregistrari", con);
                cmd1.ExecuteNonQuery();
            }
        }

        private void incarcadb()
        {
            StreamReader rdr = new StreamReader(path + "Utilizatori.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into utilizatori(Email, Nume, Prenume, Parola, DataNastere) values(@Email, @Nume, @Prenume, @Parola, @DataNastere)", con);
                    cmd.Parameters.AddWithValue("@Email", c[0]);
                    cmd.Parameters.AddWithValue("@Nume", c[1]);
                    cmd.Parameters.AddWithValue("@Prenume", c[2]);
                    cmd.Parameters.AddWithValue("@Parola", c[3]);
                    cmd.Parameters.AddWithValue("@DataNastere", c[4].ToString());
                    cmd.ExecuteNonQuery();

                }
            }
            
        }
        private void incarcadb2()
        {
            StreamReader rdr = new StreamReader(path + "Inregistrari.txt");
            string line;

            while ((line = rdr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] c = line.Split(';');
                if (c.Length != 4)
                    continue;

                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();

                    DateTime myDate = DateTime.ParseExact(
                        c[2],
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture
                    );

                    SqlCommand cmd = new SqlCommand(
                        "insert into Inregistrari(Email, Data, CodFazaLuna, CodZodia) values(@Email, @Data, @CodFazaLuna, @CodZodia)",
                        con
                    );

                    cmd.Parameters.AddWithValue("@Email", c[0]);
                    cmd.Parameters.Add("@Data", SqlDbType.Date).Value = myDate;
                    cmd.Parameters.AddWithValue("@CodFazaLuna", int.Parse(c[1]));
                    cmd.Parameters.AddWithValue("@CodZodia", int.Parse(c[3]));

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inregistrare Inregistrare = new Inregistrare();
            Inregistrare.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr)) 
            { 
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from utilizatori where Email = @email and Parola = @pass", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                var count = (int)cmd.ExecuteScalar();
                if(count == 1)
                {
                    this.Hide();
                    /*Imagini imag = new Imagini();   
                    imag.Show();*/
                    Calendar calend = new Calendar();
                    calend.Show();
                }
                else
                {
                    MessageBox.Show("Eroare autentificare");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;

                }
            }


        }
    }
}
