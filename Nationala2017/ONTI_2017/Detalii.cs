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
    public partial class Detalii : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";
        int _index, _id;
        string _email;
        int nrLocuri, pret;
        public Detalii(int index, int id, string email)
        {
            InitializeComponent();
            _index = index;
            _id = id;
            _email = email;
            incarcareImagini(_index);
        }

        private void incarcareImagini(int index)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select NumeVacanta, Descriere, CaleFisier, Pret, NrLocuri from Vacante where IdVacanta = @id", con);
                cmd.Parameters.AddWithValue("@id", index);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string nume = dr[0].ToString();
                    string descriere = dr[1].ToString();
                    string cale = dr[2].ToString();
                    pret = Int32.Parse(dr[3].ToString());
                    nrLocuri = Int32.Parse(dr[4].ToString());

                    label1.Text = nume;
                    label2.Text = pret + " Lei";
                    label3.Text = nrLocuri + " locuri";
                    label4.Text = descriere + "";


                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nrOameni;
            if (Int32.TryParse(textBox1.Text, out nrOameni)) 
            {
                if (nrOameni <= nrLocuri) 
                {
                    int nrZile = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).Days;

                    if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
                    {
                        nrZile = 1;
                    }
                    label6.Text = "Suma " + nrZile * pret * nrOameni;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into rezervari(IdVacanta, IdUser, DataInceput, DataSfarsit, NrPersoane, PretTotal) values(@IdVacanta, @IdUser, @DataInceput, @DataSfarsit, @NrPersoane, @PretTotal)",con);
                        cmd.Parameters.AddWithValue("@IdVacanta", _index);
                        cmd.Parameters.AddWithValue("@IdUser", _id);
                        cmd.Parameters.AddWithValue("@DataInceput", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@DataSfarsit", dateTimePicker2.Value);
                        cmd.Parameters.AddWithValue("@NrPersoane", nrOameni);
                        cmd.Parameters.AddWithValue("@PretTotal", nrZile * pret * nrOameni);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Rezervare creata cu succes");
                        FrmVacanta FrmVacanta = new FrmVacanta(_email, _id, _index);
                        FrmVacanta.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Nu sunt locuri disponibile pentru cate persoane ati selectat");
                    textBox1.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Ati introdus date invalide");
                textBox1.Text = string.Empty;
            }

        }
    }
}
