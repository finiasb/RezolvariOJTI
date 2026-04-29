using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2014_v2
{
    public partial class CalculRisc : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Riscuri.mdf;Integrated Security=True;Connect Timeout=30";

        public CalculRisc()
        {
            InitializeComponent();
        }
        int count;
        void Getid()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from DatePersonale", con);
                count = (int)cmd.ExecuteScalar();
            }
        }

        bool VerifyEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from DatePersonale where Email = @email", con);
                cmd.Parameters.AddWithValue("@email", email);
                var count2 = (int)cmd.ExecuteScalar();
                if (count2 > 0)
                {
                    return false;
                }
            }
            return true;
        }


        private void adaugarePacientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Getid();
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            dateTimePicker1.Visible = true;
            button1.Visible = true;
            label8.Text = count + 1 + "";

        }

        private void gestionareFisePacientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FisaPacient fisa = new FisaPacient();
            fisa.Show();
        }
        int years;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;
            years = DateTime.Now.Year - dt.Year;
            label9.Visible = true;
            label9.Text = "" + years;

            
        }
        bool Ismail(string mail)
        {
            try
            {
                MailAddress mail2 = new MailAddress(mail);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox2.Text)) 
            {
                MessageBox.Show("completati toate campurile");
                return;
            }
            if(!Ismail(textBox3.Text)) 
            {
                MessageBox.Show("Adresa de email nu este valida");
                return;
            }
            if (!VerifyEmail(textBox3.Text))
            {
                MessageBox.Show("Adresa de email este deja folosita");
                return;
            }
            string gen;
            if (radioButton1.Checked)
                gen = "M";
            else
                gen = "F";

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into DatePersonale(Nume, Prenume, Gen, Varsta, Data_Nasterii, Email) values(@Nume, @Prenume, @Gen, @Varsta, @Data_Nasterii, @Email)", con);
                cmd.Parameters.AddWithValue("@Nume", textBox1.Text);
                cmd.Parameters.AddWithValue("@Prenume", textBox2.Text);
                cmd.Parameters.AddWithValue("@Gen", gen);
                cmd.Parameters.AddWithValue("@Varsta", years);
                cmd.Parameters.AddWithValue("@Data_Nasterii", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
