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

namespace ONTI2019  
{
    public partial class Form1 : Form
    {
        public string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30";
        public string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            stergedb();
            incarcareCarti();
            incarcareImprumuturi();
            incarcareRezervari();
            incarcareUtilizatori();
        }

        private void stergedb()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Truncate table carti", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("Truncate table imprumuturi", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Truncate table Rezervari", con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Truncate table Utilizatori", con);
                cmd3.ExecuteNonQuery();
            }
        }

        private void incarcareCarti()
        {
            StreamReader rdr = new StreamReader(path + "carti.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Carti(Titlu, Autor, Nrpag) values(@Titlu, @Autor, @Nrpag)", con);
                    cmd.Parameters.AddWithValue("@Titlu", c[0].ToString());
                    cmd.Parameters.AddWithValue("@Autor", c[1].ToString());
                    cmd.Parameters.AddWithValue("@Nrpag", Int32.Parse(c[2].ToString()));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void incarcareImprumuturi()
        {
            StreamReader rdr = new StreamReader(path + "imprumuturi.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Imprumuturi(IdCititor, IdCarte, DataImprumut, DataRestituire) values(@IdCititor, @IdCarte, @DataImprumut, @DataRestituire)", con);
                    cmd.Parameters.AddWithValue("@IdCititor", Int32.Parse(c[0]));
                    cmd.Parameters.AddWithValue("@IdCarte", Int32.Parse(c[1]));
                    cmd.Parameters.AddWithValue("@DataImprumut", c[2].ToString());
                    cmd.Parameters.AddWithValue("@DataRestituire", c[3].ToString());

                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void incarcareRezervari()
        {
            StreamReader rdr = new StreamReader(path + "utilizatori.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Utilizatori(TipUtilizator, NumePrenume, Email, Parola) values(@TipUtilizator, @NumePrenume, @Email, @Parola)", con);
                    cmd.Parameters.AddWithValue("@TipUtilizator", Int32.Parse(c[0]));
                    cmd.Parameters.AddWithValue("@NumePrenume", c[1].ToString());
                    cmd.Parameters.AddWithValue("@Email", c[2].ToString());
                    cmd.Parameters.AddWithValue("@Parola", c[3].ToString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void incarcareUtilizatori()
        {
            StreamReader rdr = new StreamReader(path + "rezervari.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Rezervari(IdCititor, IdCarte, DataRezervare, StatusRezervare) values(@IdCititor, @IdCarte, @DataRezervare, @StatusRezervare)", con);
                    cmd.Parameters.AddWithValue("@IdCititor", Int32.Parse(c[0]));
                    cmd.Parameters.AddWithValue("@IdCarte", Int32.Parse(c[1]));
                    cmd.Parameters.AddWithValue("@DataRezervare", c[2].ToString());
                    cmd.Parameters.AddWithValue("@StatusRezervare", Int32.Parse(c[3]));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logare log = new Logare();
            log.Show();
        }
    }
}
