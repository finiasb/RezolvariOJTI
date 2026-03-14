using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15___Simulare__1_Olimpiadă
{
    public partial class Form1 : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\BD_NumerePrime.mdf"";Integrated Security=True;Connect Timeout=30";
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;
        int maxJocUnu = 0;
        int maxJocDoi = 0;
        int maxJocTrei = 0;
        public Form1()
        {
            InitializeComponent();
            stergeredb();
            incarcaredbRezultate();
            incarcaredbSavanti();
            incarcaredgv1();
            incarcaredgv2();
        }
        public Form1(int scor)
        {
            InitializeComponent();
            
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Rezultate(TipJoc, PunctajJoc) values(@tip, @pct)", con);
                cmd.Parameters.AddWithValue("@tip", 1);
                cmd.Parameters.AddWithValue("@pct", scor);
                cmd.ExecuteNonQuery();
            }
            incarcaredgv1();
            incarcaredgv2();
        }
        public Form1(int scor2, bool x)
        {
            InitializeComponent();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Rezultate(TipJoc, PunctajJoc) values(@tip, @pct)", con);
                cmd.Parameters.AddWithValue("@tip", 2);
                cmd.Parameters.AddWithValue("@pct", scor2);
                cmd.ExecuteNonQuery();
            }
            incarcaredgv1();
            incarcaredgv2();
        }
        public Form1(int scor2, bool x, bool y)
        {
            InitializeComponent();

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Rezultate(TipJoc, PunctajJoc) values(@tip, @pct)", con);
                cmd.Parameters.AddWithValue("@tip", 3);
                cmd.Parameters.AddWithValue("@pct", scor2);
                cmd.ExecuteNonQuery();
            }
            incarcaredgv1();
            incarcaredgv2();
        }
        private void incarcaredbRezultate()
        {
            StreamReader rdr = new StreamReader(path + "Rezultate.txt");
            string line;
            while((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Rezultate(TipJoc, PunctajJoc) values(@tip, @pct)", con);
                    cmd.Parameters.AddWithValue("@tip", Int32.Parse(c[0]));
                    cmd.Parameters.AddWithValue("@pct", Int32.Parse(c[1]));
                    cmd.ExecuteNonQuery();
                }
            }
            
        }
        private void incarcaredbSavanti()
        {
            StreamReader rdr = new StreamReader(path + "Savanti.txt");
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Savanti(FileName, Nume) values(@file, @nume)", con);
                    cmd.Parameters.AddWithValue("@file", c[0]);
                    cmd.Parameters.AddWithValue("@nume", c[1]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void stergeredb()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table rezultate", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("truncate table savanti", con);
                cmd1.ExecuteNonQuery();
            }
        }
        private void incarcaredgv1()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select PunctajJoc from rezultate where TipJoc = @tip", con);
                cmd.Parameters.AddWithValue("@tip", 1);
                SqlDataReader rdr = cmd.ExecuteReader();
                maxJocUnu = 0;    
                while(rdr.Read())
                {
                    if (Int32.Parse(rdr[0].ToString()) > maxJocUnu)
                        maxJocUnu = Int32.Parse(rdr[0].ToString());
                }
                dataGridView1.Rows.Add(1, maxJocUnu);
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select PunctajJoc from rezultate where TipJoc = @tip", con);
                cmd.Parameters.AddWithValue("@tip", 2);
                SqlDataReader rdr = cmd.ExecuteReader();
                maxJocDoi = 0;
                while (rdr.Read())
                {
                    if (Int32.Parse(rdr[0].ToString()) > maxJocDoi)
                        maxJocDoi = Int32.Parse(rdr[0].ToString());
                }
                dataGridView1.Rows.Add(2, maxJocDoi);
            }
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select PunctajJoc from rezultate where TipJoc = @tip", con);
                cmd.Parameters.AddWithValue("@tip", 3);
                SqlDataReader rdr = cmd.ExecuteReader();
                maxJocTrei = 0;
                while (rdr.Read())
                {
                    if (Int32.Parse(rdr[0].ToString()) > maxJocTrei)
                        maxJocTrei = Int32.Parse(rdr[0].ToString());
                }

                dataGridView1.Rows.Add(3, maxJocTrei);
            }
        }
        private void incarcaredgv2()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select tipJoc, PunctajJoc from rezultate", con);
                cmd.Parameters.AddWithValue("@tip", 1);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    dataGridView2.Rows.Add(rdr[0].ToString(), rdr[1].ToString());
                }
            }
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2) 
            {
                int tip = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                int pct = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value);

                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Rezultate WHERE TipJoc = @tip AND PunctajJoc = @pct", con);
                    cmd.Parameters.AddWithValue("@tip", tip);
                    cmd.Parameters.AddWithValue("@pct", pct);
                    cmd.ExecuteNonQuery();
                }

                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EnterNumbers ent = new EnterNumbers(maxJocUnu);
            ent.Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CatchTheNumbers catch1 = new CatchTheNumbers(maxJocUnu);
            catch1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CollectTheNumbers ent = new CollectTheNumbers(maxJocTrei);
            ent.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                int tip = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                int pct = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value);

                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Rezultate WHERE TipJoc = @tip AND PunctajJoc = @pct", con);
                    cmd.Parameters.AddWithValue("@tip", tip);
                    cmd.Parameters.AddWithValue("@pct", pct);
                    cmd.ExecuteNonQuery();
                }

                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
