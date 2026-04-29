using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ONTI_2023
{
    public partial class AlegeJoc : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";

        private string _email;
        public AlegeJoc(string email)
        {
            InitializeComponent();
            _email = email; 
            label1.Text = $"Bine ai venit {_email}!";
        }

        private void AlegeJoc_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                int x = 0;
                SqlCommand cmd = new SqlCommand("Select Distinct(Data), MAX(punctajJoc) as PunctajMaxim from rezultate where TipJoc = @joc and EmailUtilizator = @email group by data Order by Data ASC", con);
                cmd.Parameters.AddWithValue("@joc", x);
                cmd.Parameters.AddWithValue("@email", _email);

                SqlDataReader red = cmd.ExecuteReader();
                while(red.Read())
                {
                    string punctaj = red["PunctajMaxim"].ToString();
                    string data = red["Data"].ToString();

                    int punctajInt = Convert.ToInt32(red["PunctajMaxim"]);

                    chart1.Series["PrimulJoc"].Points.AddXY(data, punctajInt);
                }
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                int x = 1;
                SqlCommand cmd = new SqlCommand("Select Distinct(Data), MAX(punctajJoc) as PunctajMaxim from rezultate where TipJoc = @joc and EmailUtilizator = @email group by data Order by Data ASC", con);
                cmd.Parameters.AddWithValue("@joc", x);
                cmd.Parameters.AddWithValue("@email", _email);

                SqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    string punctaj = red["PunctajMaxim"].ToString();
                    string data = red["Data"].ToString();

                    int punctajInt = Convert.ToInt32(red["PunctajMaxim"]);

                    chart1.Series["AlDoileaJoc"].Points.AddXY(data, punctajInt);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TesteazaMemoria testeaza = new TesteazaMemoria(_email);
            testeaza.Show();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrimQR primQR = new PrimQR(_email);   
            primQR.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Popice popice = new Popice(_email);
            popice.Show();
            this.Hide();
        }
    }
}
