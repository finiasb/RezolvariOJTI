using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace JocEducativ
{
    public partial class Form1 : Form
    {
        private string constr =$@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}JocEducativ.mdf;Integrated Security=True;Connect Timeout=30";

        public Form1()
        {
            InitializeComponent();
            stergere();
            inserare();
        }

        private void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            new SqlCommand("truncate table itemi", con).ExecuteNonQuery();
            new SqlCommand("truncate table rezultate", con).ExecuteNonQuery();
            new SqlCommand("truncate table utilizatori", con).ExecuteNonQuery();

            con.Close();
        }

        private void inserare()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            using var srItemi = new StreamReader(Application.StartupPath + @"..\..\Itemi.txt");
            using var srRezultate = new StreamReader(Application.StartupPath + @"..\..\Rezultate.txt");
            using var srUtilizatori = new StreamReader(Application.StartupPath + @"..\..\Utilizatori.txt");
            char[] split = { ';' };
            string linie1, linie2, linie3;
            while ((linie1 = srItemi.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linie1)) continue;

                string[] c = linie1.TrimEnd(split).Split(split);

                SqlCommand cmd = new SqlCommand("Insert into Itemi(EnuntItem, Raspuns1, Raspuns2, Raspuns3, RaspunsCorect, PunctajItem) values(@EnuntItem, @Raspuns1, @Raspuns2, @Raspuns3, @RaspunsCorect, @PunctajItem)", con);
                cmd.Parameters.AddWithValue("@EnuntItem", c[1]);
                cmd.Parameters.AddWithValue("@Raspuns1", c[2]);
                cmd.Parameters.AddWithValue("@Raspuns2", c[3]);
                cmd.Parameters.AddWithValue("@Raspuns3", c[4]);
                cmd.Parameters.AddWithValue("@RaspunsCorect", c[5]);
                cmd.Parameters.AddWithValue("@PunctajItem", c[6]);
                cmd.ExecuteNonQuery();
            }
            while ((linie2 = srRezultate.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linie2)) continue;

                string[] c = linie2.TrimEnd(split).Split(split);

                SqlCommand cmd = new SqlCommand("Insert into Rezultate(TipJoc, EmailUtilizator, PunctajJoc) values(@TipJoc, @EmailUtilizator, @PunctajJoc)", con);
                cmd.Parameters.AddWithValue("@TipJoc", c[1]);
                cmd.Parameters.AddWithValue("@EmailUtilizator", c[2]);
                cmd.Parameters.AddWithValue("@PunctajJoc", c[3]);
                cmd.ExecuteNonQuery();
            }
            while ((linie3 = srUtilizatori.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linie3)) continue;

                string[] c = linie3.TrimEnd(split).Split(split);

                SqlCommand cmd = new SqlCommand("Insert into Utilizatori(EmailUtilizator, NumeUtilizator, Parola) values(@EmailUtilizator, @NumeUtilizator, @Parola)", con);
                cmd.Parameters.AddWithValue("@EmailUtilizator", c[0]);
                cmd.Parameters.AddWithValue("@NumeUtilizator", c[1]);
                cmd.Parameters.AddWithValue("@Parola", c[2]);
                cmd.ExecuteNonQuery();
            }
        }
        private void logIn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from utilizatori where EmailUtilizator = @email and Parola = @parola", con);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@parola", textBox2.Text);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            string email = textBox1.Text;

            if (count > 0)
            {
                AlegeJoc joc = new AlegeJoc(email);
                this.Hide();
                joc.Show();
            }
            else
            {
                MessageBox.Show("Date de autentificare invalide!");
                textBox1.Text = textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }
    }
}
