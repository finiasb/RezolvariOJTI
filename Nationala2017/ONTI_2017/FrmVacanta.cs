using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ONTI_2017
{
    public partial class FrmVacanta : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";
        string _email;
        int tip;
        int index = 1;
        int _id;
        int _index;
        public FrmVacanta(string email, int id)
        {
            InitializeComponent();
            timer1.Start();
            _email = email;
            _id = id;
            check();
            if(tip == 1)
            {
                fileToolStripMenuItem.Visible = false;
            }
            emailToolStripMenuItem.Text = _email;
            incarcareImagini(index);
        }
        public FrmVacanta(string email, int id, int index)
        {
            InitializeComponent(); 
            timer1.Start();
            timer1.Start();
            _email = email;
            _id = id;
            _index = index;

            check();
            if (tip == 1)
            {
                fileToolStripMenuItem.Visible = false;
            }
            emailToolStripMenuItem.Text = _email;
            incarcareImagini(_index);
        }

        private void check()
        {
            if (string.IsNullOrWhiteSpace(_email))
            {
                MessageBox.Show("Email lipsă! Utilizator neautentificat.");
                return;
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select TipCont from Utilizatori where Email = @email", con);
                cmd.Parameters.AddWithValue("@email", _email);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    tip = Int32.Parse(dr[0].ToString());
                }
            }
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
                    int pret = Int32.Parse(dr[3].ToString());
                    int nrLocuri = Int32.Parse(dr[4].ToString());

                    label1.Text = nume;
                    label2.Text = pret + " Lei";
                    label3.Text = nrLocuri + " locuri";
                    label4.Text = descriere + ""; 
                    pictureBox1.Image = Image.FromFile(cale);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(index == 7)
            {
                index = 0;
            }
            index++;
            incarcareImagini(index);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(index == 1)
            {
                index = 8;
            }
            index--;    
            incarcareImagini(index);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                index++;
                if (index == 8)
                    index = 1;

                incarcareImagini(index);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Detalii detalii = new Detalii(index, _id, _email);
            detalii.Show();
            this.Hide();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdaugareAdmin admin = new AdaugareAdmin(index, _id, _email);
            admin.Show();
        }

        private void deconectareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmInregistrare inr = new FrmInregistrare(); 
            inr.Show();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void vacanteleMeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VacanteleMele vacanta = new VacanteleMele(_id);
            vacanta.Show();
        }

        private void adaugaVacanteNoiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Vacante.txt";
            Process.Start("notepad.exe", path);
            stergere();
            incarcaredb();
            MessageBox.Show("Vacanta adaugata cu succes");
        }

        private void stergere()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table vacante", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("truncate table Rezervari", con);
                cmd2.ExecuteNonQuery();
            }
        }
        private void incarcaredb()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader rdr = new StreamReader(path + "Vacante.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('|');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Vacante(NumeVacanta, Descriere, CaleFisier, Pret, NrLocuri) values(@NumeVacanta, @Descriere, @CaleFisier, @Pret, @NrLocuri)", con);
                    cmd.Parameters.AddWithValue("@NumeVacanta", c[0].ToString());
                    cmd.Parameters.AddWithValue("@Descriere", c[1].ToString());
                    cmd.Parameters.AddWithValue("@CaleFisier", path + "Imagini\\" + c[0].ToString() + ".png");
                    cmd.Parameters.AddWithValue("@Pret", Int32.Parse(c[2].ToString()));
                    cmd.Parameters.AddWithValue("@NrLocuri", Int32.Parse(c[3].ToString()));
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
