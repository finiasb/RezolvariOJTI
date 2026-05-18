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

namespace ONTI_2017
{
    public partial class FrmInregistrare : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";

        public FrmInregistrare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox5.Text)
            {
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                MessageBox.Show("Parolele nu coincid");
                return;
            }
            if (textBox4.Text.Length <= 3)
            {
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                MessageBox.Show("Parola trebuie sa aiba peste 3 caractere");
                return;
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Email from Utilizatori where Email = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("Emailul este in folosinta deja!");
                    textBox3.Text = string.Empty;
                    return;
                }
                rdr.Close();
            }
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text)) 
            {
                MessageBox.Show("Nu ati completat toate campurile");
                return;
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Utilizatori(Nume, Prenume, Email, Parola, TipCont) values(@Nume, @Prenume, @Email, @Parola, @TipCont)", con);
                cmd.Parameters.AddWithValue("@Nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@Prenume", textBox2.Text);
                cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                cmd.Parameters.AddWithValue("@Parola", textBox4.Text);
                cmd.Parameters.AddWithValue("@TipCont", 1);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Cont adaugat cu succes");
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
