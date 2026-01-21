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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Judeteana2016
{
    public partial class Autentificare_client : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";

        public Autentificare_client()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select parola from clienti where nume = @nume", con);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string pass = reader[0].ToString();
                    if(textBox2.Text == pass)
                    {
                        this.Hide();
                        this.Hide();
                        Optiuni form = new Optiuni(textBox1.Text);
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Eroare autentificare!");
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                        return;
                    }
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
