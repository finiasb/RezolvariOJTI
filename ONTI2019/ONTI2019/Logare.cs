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

namespace ONTI2019
{
    public partial class Logare : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30";
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Logare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "tutor@gmail.com";
            textBox2.Text = "tudor";
            using (SqlConnection con = new SqlConnection(constr)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from utilizatori where Email = @email and Parola = @pass", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                var count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    BibliotecarBiblioteca bilioteca = new BibliotecarBiblioteca(textBox1.Text);
                    this.Hide();
                    bilioteca.Show();
                }
                else
                {
                    MessageBox.Show("Datele sunt gresite");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                }
            }
        }
    }
}
