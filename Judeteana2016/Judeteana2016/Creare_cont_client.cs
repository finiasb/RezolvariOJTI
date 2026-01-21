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

namespace Judeteana2016
{
    public partial class Creare_cont_client : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";

        public Creare_cont_client()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Parolele nu coincid");
                textBox5.Text = string.Empty;
                textBox4.Text = string.Empty;
                return;
            }
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select email from clienti where email = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read()) 
                {
                    MessageBox.Show("Adresa de email este utilizata deja");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                    textBox6.Text = string.Empty;
                    return;
                }reader.Close();
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Clienti(parola, nume, prenume, adresa, email) values(@parola, @nume, @prenume, @adresa, @email)", con);
                cmd.Parameters.AddWithValue("@parola", textBox4.Text);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@prenume", textBox2.Text);
                cmd.Parameters.AddWithValue("@adresa", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox6.Text);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Cont creat cu succes");
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void Creare_cont_client_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
