using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Linq;


namespace ONTI_2024
{
    public partial class Inregistrare : Form
    {

        /// <summary>
        /// MAI AM 4 ORE
        /// </summary>
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True;Connect Timeout=30";


        public Inregistrare()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox5.Text != textBox4.Text)
            {
                MessageBox.Show("Parolele nu coincid"); 
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                return;
            }

            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(-7);
            if(dateTimePicker1.Value > dateTime)
            {
                MessageBox.Show("Varsta minima este de 7 ani");
                return;
            }

            char[] parts = textBox4.Text.ToCharArray();

            bool charMare = false;
            bool charMic = false;
            bool number = false;
            foreach (char ch in parts)
            {
                if(char.IsUpper(ch))
                {
                    charMare = true;
                }
                else if(char.IsLower(ch))
                {
                    charMic = true;
                }
                else if(char.IsNumber(ch))
                {
                    number = true;
                }
            }
            if (charMare == false || charMic == false || number == false) 
            {
                MessageBox.Show("parola trebuie sa aiba cel putin un caracter mare, unul mic si cel putin un numar");
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                return;
            }
            if(textBox4.Text.Length < 6) 
            {
                MessageBox.Show("parola trebuie sa aiba mai mult de 6 caractere");
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                return;
            }

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from utilizatori where Email = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                var count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    // TRE SA INCARC SI IN DB
                    MessageBox.Show("Cont creat cu succes");
                    this.Hide();
                    Imagini imag = new Imagini();
                    imag.Show();
                }
                else
                {
                    MessageBox.Show("Eroare autentificare, contul exista deja in baza de date");
                    textBox1.Text = string.Empty;
                }
            }
        }
    }
}
