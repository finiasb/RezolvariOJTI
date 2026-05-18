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

namespace OJTI20222
{
    public partial class AdaugaMasurare : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Poluare.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        int id;
        DateTime dt;
        Point p;
        public AdaugaMasurare(int id, int x, int y, DateTime dt)
        {
            InitializeComponent();
            this.id = id;
            this.dt = dt;
            this.p.X = x;
            this.p.Y = y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value = textBox1.Text;
            int n;
            if(int.TryParse(value, out n))
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into masurare(IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) values(@id, @pozX, @pozY, @val, @data)", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pozX", p.X);
                    cmd.Parameters.AddWithValue("@pozY", p.Y);
                    cmd.Parameters.AddWithValue("@val", n);
                    cmd.Parameters.AddWithValue("@data", dt);
                    cmd.ExecuteNonQuery();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Valoarea nu este un numar valid");
                textBox1.Text = string.Empty; 
            }

        }

        private void AdaugaMasurare_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void AdaugaMasurare_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Q) 
            {
                System.Environment.Exit(0);
            }
        }
    }
}