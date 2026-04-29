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
    public partial class AdaugareAdmin : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";
        int _index, _id;
        string _email;
        int ok;
        public AdaugareAdmin(int index, int id, string email)
        {
            InitializeComponent();
            _index = index;
            _id = id;
            _email = email;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmInregistrare frmInregistrare = new FrmInregistrare();
            frmInregistrare.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Utilizatori set TipCont = @tip where Email = @email", con);
                cmd.Parameters.AddWithValue("@tip", 0);
                cmd.Parameters.AddWithValue("@email", comboBox1.SelectedItem.ToString());
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string email = reader[0].ToString();
                    comboBox1.Items.Add(email);
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmVacanta vacanta = new FrmVacanta(_email, _id, _index);
            vacanta.Show();
            this.Hide();
        }

        private async void AdaugareAdmin_Load(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            incarca();
        }

        private void incarca()
        {           
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Email from utilizatori where TipCont = @tip", con);
                cmd.Parameters.AddWithValue("@tip", 1);
                SqlDataReader reader = cmd.ExecuteReader();
                ok = 0;
                while (reader.Read())
                {
                    string email = reader[0].ToString();
                    comboBox1.Items.Add(email);
                    ok = 1;
                }
            }
            if(ok == 1)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nu sunt utilizatori in baza de date");
                FrmVacanta vacanta = new FrmVacanta(_email, _id, _index); 
                this.Hide();
                vacanta.Show();
            }
        }
    }
}
