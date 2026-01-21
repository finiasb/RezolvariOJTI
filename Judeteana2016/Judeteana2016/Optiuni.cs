using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Judeteana2016
{
    public partial class Optiuni : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GOOD_FOOD.mdf;Integrated Security=True;Connect Timeout=30";
        public int kal;
        private int totalKal, totalPret;
        string _nume;
        public Optiuni(string nume)
        {
            InitializeComponent();
            textBox4.Enabled = false;
            _nume = nume;
            incarcaDGV();
            incarcareChart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numar;
            if(!Int32.TryParse(textBox1.Text, out numar) || !Int32.TryParse(textBox2.Text, out numar) || !Int32.TryParse(textBox3.Text, out numar))
            {
                MessageBox.Show("Cel putin una dintre valori nu este acceptata");
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                return;
            }
            int ani = Int32.Parse(textBox1.Text);
            int cm = Int32.Parse(textBox2.Text);
            int kg = Int32.Parse(textBox3.Text);
            int suma = ani + cm + kg;
            if(suma < 250)
                kal = 1800;
            else if(suma >=  250 && suma <= 275)
                kal = 2200;
            else if(suma > 275)
                kal = 2500;


            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Clienti SET kcal_zilnice = @kcal_zilnice WHERE nume = @nume", con);
                cmd.Parameters.AddWithValue("@kcal_zilnice", kal);
                cmd.Parameters.AddWithValue("@nume", _nume);
                cmd.ExecuteNonQuery();
            }

            textBox4.Text = "" + kal;
            textBox5.Text = "" + kal;
            textBox9.Text = "" + kal;
        }

        private void incarcaDGV()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select id_produs, denumire_produs, descriere, pret, kcal, felul from meniu", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int id = Int32.Parse(reader[0].ToString());
                    string denumire = reader[1].ToString();
                    string descriere = reader[2].ToString();
                    int pret = Int32.Parse(reader[3].ToString());
                    int kcal = Int32.Parse(reader[4].ToString());
                    int felul = Int32.Parse(reader[5].ToString());

                    dataGridView1.Rows.Add(id, denumire, descriere, pret, kcal, felul, 1);
                }
            }

        }
        int id;
        int index = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            index++;
            this.Hide();
            Vizualizarer viz = new Vizualizarer(index - 1, kal, _nume);
            viz.Show();
        }

        string NumeFel1;
        int pretFel1;
        int kcalFel1;
        string NumeFel2;
        int pretFel2;
        int kcalFel2;
        string NumeFel3;
        int pretFel3;
        int kcalFel3;
        private void getfel1(int index)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select denumire_produs, pret, kcal from meniu where id_produs = @id", con);
                cmd.Parameters.AddWithValue("@id", index);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NumeFel1 = rdr[0].ToString();
                    pretFel1 = Int32.Parse(rdr[1].ToString());
                    kcalFel1 = Int32.Parse(rdr[2].ToString());
                }
                rdr.Close();
            }
        }
        private void getfel2(int index)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select denumire_produs, pret, kcal from meniu where id_produs = @id", con);
                cmd.Parameters.AddWithValue("@id", index);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NumeFel2 = rdr[0].ToString();
                    pretFel2 = Int32.Parse(rdr[1].ToString());
                    kcalFel2 = Int32.Parse(rdr[2].ToString());
                }
                rdr.Close();
            }
        }
        private void getfel3(int index)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select denumire_produs, pret, kcal from meniu where id_produs = @id", con);
                cmd.Parameters.AddWithValue("@id", index);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    NumeFel3 = rdr[0].ToString();
                    pretFel3 = Int32.Parse(rdr[1].ToString());
                    kcalFel3 = Int32.Parse(rdr[2].ToString());
                }
                rdr.Close();
            }
        }

        private void generareMeniuri()
        {
            for(int i = 1; i <= 5;  i++)
            {
                for(int j = 6; j <= 21; j++)
                {
                    for(int k = 22; k <= 27; k++)
                    {
                        kcalFel1 = 0;
                        kcalFel2 = 0;
                        kcalFel3 = 0;
                        pretFel1 = 0;
                        pretFel2 = 0;
                        pretFel3 = 0;
                        NumeFel1 = string.Empty;
                        NumeFel2 = string.Empty;
                        NumeFel3 = string.Empty;

                        getfel1(i);
                        getfel2(j);
                        getfel3(k);
                        int sumKcal = kcalFel1 + kcalFel2 + kcalFel3;
                        int sumPret = pretFel1 + pretFel2 + pretFel3;
                        if (sumKcal <= kal)
                        {
                            int nr;
                            if(Int32.TryParse(textBox8.Text, out nr))
                            {
                                if(sumPret <= nr )
                                {
                                    dataGridView2.Rows.Add(NumeFel1, NumeFel2, NumeFel3, sumKcal, sumPret);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Bugetul nu este valid");
                                textBox8.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            generareMeniuri();
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
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderBut = (DataGridView)sender;

            if (senderBut.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                MessageBox.Show("Comanda trimisa");
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
                Form1 form = new Form1();
                form.Show();
            }
        }
        private void incarcareChart() 
        {
            chart1.Series.Clear(); 
            
            Series s = new Series("Kcal")
            {
                ChartType = SeriesChartType.Column
            };
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    @"SELECT m.denumire_produs, m.kcal, s.cantitate
              FROM subcomenzi s
              JOIN meniu m ON s.id_produs = m.id_produs
              WHERE s.id_comanda = @id"
                , con);

                cmd.Parameters.AddWithValue("@id", 1);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string nume = rdr.GetString(0);
                    int kcal = rdr.GetInt32(1);
                    int cantitate = rdr.GetInt32(2);

                    int totalKcal = kcal * cantitate;

                    s.Points.AddXY(nume, totalKcal);
                }
            }

            chart1.Series.Add(s);

            chart1.ChartAreas[0].AxisX.Interval = 1;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                int cantitate;
                string valoare = senderGrid.Rows[e.RowIndex].Cells[6].Value?.ToString();
                int nr;
                if(Int32.TryParse(valoare, out cantitate))
                {
                    if(cantitate >= 1)
                    {
                        int valkal = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[3].Value?.ToString());
                        int valpret = Int32.Parse(senderGrid.Rows[e.RowIndex].Cells[4].Value?.ToString());
                        string nume = senderGrid.Rows[e.RowIndex].Cells[1].Value?.ToString();
                        totalKal += valkal * cantitate;
                        totalPret += valpret * cantitate;
                        textBox6.Text = "" + totalPret;
                        textBox7.Text = "" + totalKal;
                        using(SqlConnection con = new SqlConnection(constr))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select id_produs from meniu where denumire_produs = @nume", con);
                            cmd.Parameters.AddWithValue("@nume", nume);
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                id = Int32.Parse(reader[0].ToString());
                            }reader.Close();
                        }
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Insert into subcomenzi(id_comanda, id_produs, cantitate) values(@id_comanda, @id_produs, @cantitate)", con);
                            cmd.Parameters.AddWithValue("@id_comanda", index.ToString());
                            cmd.Parameters.AddWithValue("@id_produs", id);
                            cmd.Parameters.AddWithValue("@cantitate", cantitate);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nu mai este cantitate din acest produs in stoc");
                    }
                }
                else
                {
                    MessageBox.Show("Cantitatea nu este un numar valid");
                }
            }
        }
    }
}
