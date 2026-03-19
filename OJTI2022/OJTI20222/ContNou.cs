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

namespace OJTI20222
{
    public partial class ContNou : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fineas\source\repos\OJTI20222\OJTI20222\bin\Debug\Poluare.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public ContNou()
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
            if(textBox1.Text.Length <= 4)
            {
                MessageBox.Show("Numele trebuie sa aiba mai mult de 4 caractere");
                return;
            }
            if(textBox2.Text.Length <= 6) 
            {
                MessageBox.Show("Parola trebuie sa aiba mai mult de 6 caractere");
                return;
            }
            if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Parolele nu coincid");
                return;
            }
            if (!textBox4.Text.Contains("@gmail.com"))
            {
                MessageBox.Show("Emailul trebuie sa se termine cu '@gmail.com'");
                return;
            }
            
            using(SqlConnection con = new SqlConnection(constr)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from utilizatori where EmailUtilizator = @email", con);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                var count = (int)cmd.ExecuteScalar();   
                if(count >= 1)
                {
                    MessageBox.Show("Acest nume exista deja in db");
                    return;
                }
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into utilizatori(NumeUtilizator, Parola, EmailUtilizator) values(@nume, @pass, @email)", con);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Cont creat cu succes");
            this.Hide();
            Form1 form = new Form1();
            form.Show();

        }

        private void ContNou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                System.Environment.Exit(0);
            }
        }

        private void ContNou_Load(object sender, EventArgs e)
        {

        }
    }
}
