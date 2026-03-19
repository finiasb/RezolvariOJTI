using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OJTI20222
{
    public partial class Form1 : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fineas\source\repos\OJTI20222\OJTI20222\bin\Debug\Poluare.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            stergere();
            harti();
            Masurare();
        }

        void stergere()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Truncate table Masurare", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("Truncate table Harti", con);
                cmd1.ExecuteNonQuery();
            }
        }
        int id;
        void getId(string name)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select IdHarta from harti where numeHarta = @nume", con);
                cmd.Parameters.AddWithValue("@nume", name);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    id = Convert.ToInt32(rdr[0]);
                }
            }
        }

        void Masurare()
        {
            StreamReader rdr = new StreamReader(path + "masurari.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('#');
                getId(c[0]);
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into masurare(IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) values(@id, @pozX, @pozY, @val, @data)", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pozX", int.Parse(c[1]));
                    cmd.Parameters.AddWithValue("@pozY", int.Parse(c[2]));
                    cmd.Parameters.AddWithValue("@val", int.Parse(c[3]));
                    cmd.Parameters.AddWithValue("@data", DateTime.Parse(c[4]));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        void harti()
        {
            StreamReader rdr = new StreamReader(path + "harti.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split('#');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into harti(NumeHarta, FisierHarta) values(@nume, @fisier)", con);
                    cmd.Parameters.AddWithValue("@nume", c[0]);
                    cmd.Parameters.AddWithValue("@fisier", c[1]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();    
            ContNou show = new ContNou();
            show.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from utilizatori where NumeUtilizator = @nume", con);
                cmd.Parameters.AddWithValue("@nume", textBox1.Text);
                var count = (int)cmd.ExecuteScalar();
                if (count >= 1)
                {
                    this.Hide();
                    Harti harti = new Harti(textBox1.Text);
                    harti.Show();
                    
                }
                else if(count == 0)
                {
                    MessageBox.Show("datele introduse sunt incorecte");
                    return;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Q) 
            {
                System.Environment.Exit(0);
            }

        }
    }
}
