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
    public partial class BibliotecarBiblioteca : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30";
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;
        string _email, name;
        int id, tip;
        public BibliotecarBiblioteca(string email)
        {
            InitializeComponent();
            timer1.Start();
            _email = email;
            getID(email);
            pictureBox1.Image = Image.FromFile(path + $"\\Imagini\\utilizatori\\{id}.jpg");
            if(tip == 1)
                label1.Text = "Bibliotecar: " + name;
            else
                label1.Text = "Cititor: " + name;
        }


        private void getID(string email)
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select IdUtilizator, TipUtilizator, NumePrenume from utilizatori where email = @email", con);
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    id = Int32.Parse(rdr[0].ToString());
                    tip = Int32.Parse(rdr[1].ToString());
                    name = rdr[2].ToString();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
    }
}
