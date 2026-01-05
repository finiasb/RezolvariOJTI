using Accessibility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace JocEducativ
{
    public partial class AlegeJoc : Form
    {
        private readonly string _email;
        private string constr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}JocEducativ.mdf;Integrated Security=True;Connect Timeout=30";
        public AlegeJoc(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void AlegeJoc_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select numeUtilizator from utilizatori  where Emailutilizator = @email", con);
            cmd.Parameters.AddWithValue("@email", _email);
            object rez = cmd.ExecuteScalar();
            string nume = rez.ToString();
            label1.Text = $"Bine ai venit {nume}! ({_email})";
            incarcaGhiceste();
            incarcaSarpe();
        }

        private void Ghiceste_Click(object sender, EventArgs e)
        {
            Guess ghiceste = new Guess(_email);
            ghiceste.Show();
            this.Hide();
        }

        private void incarcaGhiceste()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT TOP 3 u.NumeUtilizator, u.EmailUtilizator, r.PunctajJoc FROM Rezultate r INNER JOIN Utilizatori u ON r.EmailUtilizator = u.EmailUtilizator WHERE r.TipJoc = 0  ORDER BY r.PunctajJoc DESC", con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgGhiceste.DataSource = table;
        }
        private void incarcaSarpe()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT TOP 3 u.NumeUtilizator, u.EmailUtilizator, r.PunctajJoc FROM Rezultate r INNER JOIN Utilizatori u ON r.EmailUtilizator = u.EmailUtilizator WHERE r.TipJoc = 1 ORDER BY r.PunctajJoc DESC", con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgSarpe.DataSource = table;
        }

        private void SarpeEducativ_Click(object sender, EventArgs e)
        {
            SarpeEducativ sarpe = new SarpeEducativ(_email);
            sarpe.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
