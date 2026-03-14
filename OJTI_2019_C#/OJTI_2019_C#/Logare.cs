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

namespace OJTI_2019_C_
{
    public partial class Logare : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\FreeBook.mdf\";Integrated Security=True;Connect Timeout=30";

        public Logare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*) from utilizatori where email=@email and parola=@parola", con);
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@parola", textBox2.Text);

                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("V-ati logat cu succes");
                    this.Hide();
                    Meniu meniu = new Meniu(textBox1.Text);
                    meniu.Show();
                }
                else
                {
                    MessageBox.Show("Nu ati introdus datele corecte");
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
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
