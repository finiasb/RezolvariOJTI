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
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Fineas\\source\\repos\\SUBIECTE OJTI\\OJTI_2019_C#\\OJTI_2019_C#\\bin\\Debug\\FreeBook.mdf\";Integrated Security=True;Connect Timeout=30";

        public Logare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select email, parola from utilizatori", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string email = reader["email"].ToString();
                string parola = reader["parola"].ToString();

                if (textBox1.Text == email && textBox2.Text == parola && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("V-ati logat cu succes");
                    this.Hide();
                    Meniu meniu = new Meniu(textBox1.Text);
                    meniu.Show();
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
