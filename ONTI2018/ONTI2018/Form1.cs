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

namespace ONTI2018
{
    public partial class Form1 : Form
    {
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|CentenarDB.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            stergere();
            inserareUtilizatori();
            inserareLectii();
        }

        private void inserareLectii()
        {
            StreamReader rdr = new StreamReader(path + "lectii.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('*');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Lectii(IdUtilizator, TitluLectie, Regiune, DataCreare, NumeImagine) values(@IdUtilizator, @TitluLectie, @Regiune, @DataCreare, @NumeImagine)", con);
                    cmd.Parameters.AddWithValue("@IdUtilizator", Int32.Parse(c[0]));
                    cmd.Parameters.AddWithValue("@TitluLectie", c[1]);
                    cmd.Parameters.AddWithValue("@Regiune", c[2]);
                    cmd.Parameters.AddWithValue("@DataCreare", c[4]);
                    cmd.Parameters.AddWithValue("@NumeImagine", c[3]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void inserareUtilizatori()
        {
            StreamReader rdr = new StreamReader(path + "utilizatori.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('*');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Utilizatori(Nume, Parola, Email) values(@Nume, @Parola, @Email)", con);
                    cmd.Parameters.AddWithValue("@Nume", c[0]);
                    cmd.Parameters.AddWithValue("@Parola", c[1]);
                    cmd.Parameters.AddWithValue("@Email", c[2]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void stergere()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Truncate table utilizatori", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Truncate table lectii", con);
                cmd2.ExecuteNonQuery();
            }
        }
    }
}
