using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023
{
    public partial class ContNou : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";
        public ContNou()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text)) 
            {
                MessageBox.Show("Completeaza toate campurile");
            }
            else 
            { 
                if(textBox3.Text == textBox4.Text) 
                {
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Select count(*)  from utilizatori where EmailUtilizator = @email", con);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        SqlCommand cmd2 = new SqlCommand("insert into  Utilizatori(EmailUtilizator, NumeUtilizator, Parola) values(@email, @nume, @parola)", con);
                        cmd2.Parameters.AddWithValue("@email", textBox1.Text);
                        cmd2.Parameters.AddWithValue("@nume", textBox2.Text);
                        cmd2.Parameters.AddWithValue("@parola", textBox3.Text);
                        cmd2.ExecuteNonQuery();
                        AlegeJoc joc = new AlegeJoc(textBox1.Text);
                        this.Hide();
                        joc.Show();
                    }
                    else
                    {
                        MessageBox.Show("Adresa de email este deja utilizata");
                        textBox1.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Parolele nu coincid");
                    textBox4.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
