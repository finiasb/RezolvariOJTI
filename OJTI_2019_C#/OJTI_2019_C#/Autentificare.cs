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

namespace OJTI_2019_C_
{
    public partial class Autentificare : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Fineas\\source\\repos\\SUBIECTE OJTI\\OJTI_2019_C#\\OJTI_2019_C#\\bin\\Debug\\FreeBook.mdf\";Integrated Security=True;Connect Timeout=30";

        public Autentificare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxConfirmarePass.Text) || string.IsNullOrEmpty(textBoxPass.Text) || string.IsNullOrEmpty(textBoxEmail.Text) || string.IsNullOrEmpty(textBoxNume.Text) || string.IsNullOrEmpty(textBoxPrenume.Text))
            {
                MessageBox.Show("Completati toate campurile");
            }
            else
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("select email from utilizatori", con);
                SqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    string utilizatori = red["email"].ToString();
                    if (utilizatori == textBoxEmail.Text)
                    {
                        MessageBox.Show("Emailul este deja utilizat");
                        textBoxEmail.Text = "";
                    }
                }
                red.Close();
                con.Close();
                if (textBoxConfirmarePass.Text != textBoxPass.Text)
                {
                    MessageBox.Show("Parolele nu corespund");
                    textBoxConfirmarePass.Text = "";
                    textBoxPass.Text = "";
                }
                else if (textBoxEmail.Text != "")
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    SqlCommand cmdd = new SqlCommand("insert into utilizatori(email, parola, nume, prenume) values(@email, @parola, @nume, @prenume)", conn);
                    cmdd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                    cmdd.Parameters.AddWithValue("@parola", textBoxPass.Text);
                    cmdd.Parameters.AddWithValue("@nume", textBoxNume.Text);
                    cmdd.Parameters.AddWithValue("@prenume", textBoxPrenume.Text);
                    cmdd.ExecuteNonQuery();
                    MessageBox.Show("nice");
                    Meniu meniu = new Meniu(textBoxEmail.Text);
                    meniu.Show();
                    this.Hide();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
