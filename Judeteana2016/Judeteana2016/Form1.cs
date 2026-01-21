using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2016
{
    public partial class Form1 : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        
        public Form1()
        {
            InitializeComponent(); 
            stergere();

            inserare();
        }

        private void stergere()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table meniu", con);
                cmd.ExecuteNonQuery();
                /*SqlCommand cmd1 = new SqlCommand("truncate table subcomenzi", con);
                cmd1.ExecuteNonQuery();*/
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Creare_cont_client cont = new Creare_cont_client();
            cont.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Autentificare_client aut = new Autentificare_client();
            aut.Show();
        }

        private void inserare()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "meniu.txt";
            StreamReader rdr = new StreamReader(path);
            string linie;
            int i = 0;
            while(( linie = rdr.ReadLine()) != null )
            {
                if (string.IsNullOrWhiteSpace(linie))
                    continue;
                string[] c = linie.Split(';');
                if (c[0] != "id_produs")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into Meniu(id_produs, denumire_produs, descriere, pret, kcal, felul) values(@id_produs, @denumire_produs, @descriere, @pret, @kcal, @felul)", con);
                        cmd.Parameters.AddWithValue("@id_produs", Int32.Parse(c[0]));
                        cmd.Parameters.AddWithValue("@denumire_produs", c[1].ToString());
                        cmd.Parameters.AddWithValue("@descriere", c[2].ToString());
                        cmd.Parameters.AddWithValue("@pret", Int32.Parse(c[3]));
                        cmd.Parameters.AddWithValue("@kcal", Int32.Parse(c[4]));
                        cmd.Parameters.AddWithValue("@felul", Int32.Parse(c[5]));
                        cmd.ExecuteNonQuery();
                    }
                }
                
            }
        }
    }
}
