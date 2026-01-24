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

namespace OJTI2017
{
    public partial class Vizualizare : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\Turism.mdf"";Integrated Security=True;Connect Timeout=30";
        public Vizualizare()
        {
            InitializeComponent();
            incarcaredgv();
        }

        private void incarcaredgv()
        {
            for(int i = 1; i <= 13; i++)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Localitati.Nume, Planificari.DataStart, Planificari.DataStop, Planificari.Frecventa, Planificari.Ziua From Localitati INNER JOIN Planificari ON Localitati.IDLocalitate = @id1 AND Planificari.IDLocalitate = @id2", con);
                    cmd.Parameters.AddWithValue("@id1", i);
                    cmd.Parameters.AddWithValue("@id2", i);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {

                        dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString().Split(' ')[0], reader[2].ToString().Split(' ')[0], reader[3].ToString(), reader[4].ToString()); ;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            for (int i = 1; i <= 13; i++)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select Localitati.Nume, Planificari.DataStart, Planificari.DataStop, Planificari.Frecventa, Planificari.Ziua From Localitati INNER JOIN Planificari ON Localitati.IDLocalitate = @id1 AND Planificari.IDLocalitate = @id2", con);
                    cmd.Parameters.AddWithValue("@id1", i);
                    cmd.Parameters.AddWithValue("@id2", i);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        if (!string.IsNullOrEmpty(reader[1].ToString()) || !string.IsNullOrEmpty(reader[2].ToString()))
                        {

                            DateTime myDate = (DateTime)reader[1];
                            DateTime myDate1 = (DateTime)reader[2];

                            if (dateTimePicker1.Value <= myDate && dateTimePicker2.Value >= myDate1)
                            {
                                dataGridView2.Rows.Add(reader[0].ToString(), reader[1].ToString().Split(' ')[0], reader[2].ToString().Split(' ')[0], reader[3].ToString(), reader[4].ToString());
                            }
                        }
                        else if (reader[3].ToString() == "anual")
                        {
                            DateTime dateTime = new DateTime(2026, 1, 1).AddDays(Int32.Parse(reader[4].ToString()) - 1);
                            if (dateTimePicker1.Value <= dateTime && dateTimePicker2.Value >= dateTime)
                            {
                                dataGridView2.Rows.Add(reader[0].ToString(), dateTime.ToString().Split(' ')[0], dateTime.ToString().Split(' ')[0], reader[3].ToString(), reader[4].ToString());
                            }
                        }
                        else if (reader[3].ToString() == "lunar")
                        {
                            for(int j = 1; j <= 12; j++)
                            {
                                DateTime dateTime = new DateTime(2026, j, 1).AddDays(Int32.Parse(reader[4].ToString()) - 1);
                                if (dateTimePicker1.Value <= dateTime && dateTimePicker2.Value >= dateTime)
                                {
                                    dataGridView2.Rows.Add(reader[0].ToString(), dateTime.ToString().Split(' ')[0], dateTime.ToString().Split(' ')[0], reader[3].ToString(), reader[4].ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
