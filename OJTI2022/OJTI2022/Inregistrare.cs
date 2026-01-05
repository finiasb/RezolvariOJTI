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
using System.Xml.Schema;

namespace OJTI2022
{
    public partial class Inregistrare : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|Poluare3.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public Inregistrare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length < 4)
            {
                MessageBox.Show("Numele utilizatorului trebuie sa aiba mai mult de 4 caractere");
                textBox1.Text = string.Empty;
                return;
            }
            if(textBox2.Text.Length < 5) 
            {
                MessageBox.Show("Parola utilizatorului trebuie sa aiba minim 6 caractere");
                textBox2.Text = string.Empty;
                return;
            }
            if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Parola nu coincide cu parola introdusa in campul 'Confirmare parola'");
                textBox3.Text = string.Empty;
                return;
            }
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select EmailUtilizator from Utilizatori where EmailUtilizator = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    MessageBox.Show("Email-ul exista deja in baza de date a aplicatiei");
                    textBox4.Text = string.Empty;
                    return;
                }
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Utilizatori(NumeUtilizator, Parola, EmailUtilizator) values(@nume, @parola, @email)", con);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@parola", textBox2.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ati fost autentificat cu succest");
            }
            Vizualizare vizualizare = new Vizualizare(textBox1.Text);
            vizualizare.Show();
            this.Hide();
        }
    }
}
