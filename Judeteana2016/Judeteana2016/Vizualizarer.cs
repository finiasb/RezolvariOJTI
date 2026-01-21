using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Judeteana2016
{
    public partial class Vizualizarer : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        int _index;
        int _necesar;
        int totalpret;
        int totalCaloarii; 
        int id;
        string _nume;
        public Vizualizarer(int index, int necesar, string nume)
        {
            InitializeComponent();
            _index = index;
            incarcaredgv();
            _necesar = necesar;
            textBox5.Text = "" + _necesar;
            textBox6.Text = "" + totalCaloarii;
            textBox7.Text = "" + totalpret;
            _nume = nume;   
        }

        private void incarcaredgv()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT s.cantitate, m.denumire_produs, m.pret, m.kcal FROM subcomenzi s JOIN meniu m ON s.id_produs = m.id_produs WHERE s.id_comanda = @comanda", con);
                cmd.Parameters.AddWithValue("@comanda", _index);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int cantitate = Int32.Parse(rdr[0].ToString());
                    string denumire = rdr[1].ToString();
                    int pret = Int32.Parse(rdr[2].ToString());
                    int kcal = Int32.Parse(rdr[3].ToString());

                    totalpret += pret * cantitate;
                    totalCaloarii += kcal * cantitate;

                    dataGridView1.Rows.Add(denumire, kcal, pret, cantitate);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                int valkal = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[1].Value?.ToString());
                int valpret = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[2].Value?.ToString());
                int cantitate = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[3].Value?.ToString());
                string nume = senderGrid.Rows[e.RowIndex].Cells[0].Value?.ToString();   
                totalCaloarii -= valkal * cantitate;
                totalpret -= valpret * cantitate;

                textBox6.Text = "" + totalCaloarii;
                textBox7.Text = "" + totalpret;

                MessageBox.Show("" + valkal + " " + cantitate );
                MessageBox.Show("" + valpret + " " + cantitate);


                dataGridView1.Rows.RemoveAt(e.RowIndex);

                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select id_produs from meniu where denumire_produs = @nume", con);
                    cmd.Parameters.AddWithValue("@nume", nume);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0].ToString());
                    }reader.Close();
                }



                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM subcomenzi WHERE id_comanda = @comanda AND id_produs = @produs",con);
                    cmd.Parameters.AddWithValue("@comanda", _index); 
                    cmd.Parameters.AddWithValue("@produs", id);    
                    cmd.ExecuteNonQuery();
                }
            }
        }
        int id2;
        private void GetId()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select id_client from Clienti where nume = @nume", con);
                cmd.Parameters.AddWithValue("@nume", _nume);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id2 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comanda trimisa!");
            GetId();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                DateTime dt = DateTime.Now;
                SqlCommand cmd = new SqlCommand("INSERT into comenzi(id_client, data_comanda) values(@id_client, @data_comanda)", con);

                cmd.Parameters.AddWithValue("@id_client", id2);
                cmd.Parameters.AddWithValue("@data_comanda", dt);
                cmd.ExecuteNonQuery();
            }
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
