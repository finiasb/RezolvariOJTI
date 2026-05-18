using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2017
{
    public partial class VacanteleMele : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\TurismDB.mdf"";Integrated Security=True;Connect Timeout=30";
        int _id;
        public VacanteleMele(int id)
        {
            InitializeComponent();
            _id = id;
            incarca();
        }
        int id;
        private void incarca()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Vacante.NumeVacanta, Rezervari.DataInceput, Rezervari.DataSfarsit, Rezervari.NrPersoane, Rezervari.PretTotal, Rezervari.IdVacanta from Rezervari inner join Vacante on Rezervari.IdUser = @id and Vacante.IdVacanta = Rezervari.IdVacanta", con);
                cmd.Parameters.AddWithValue("@id", _id);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string nume = rdr[0].ToString();
                    string datainceput = rdr[1].ToString();
                    string datafinal = rdr[2].ToString();
                    string nrpers = rdr[3].ToString();
                    string pret = rdr[4].ToString();
                    id = Convert.ToInt32(rdr[5].ToString());
                    dataGridView1.Rows.Add(nume, datainceput, datafinal, nrpers, pret);
                }
            }
        }
        int idfinal;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Sterge") 
            {
                string nume = dataGridView1.Rows[e.RowIndex].Cells["Vacanta"].Value.ToString();
                DateTime dataInc = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells["DataInceput"].Value.ToString());
                DateTime dataFinal = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells["DataFinal"].Value.ToString());
                int nrpers = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["NrPers"].Value.ToString());
                int pret = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["PretTotal"].Value.ToString());
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select IdVacanta from Vacante where NumeVacanta = @nume", con);
                    cmd.Parameters.AddWithValue("@nume", nume);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        idfinal = Int32.Parse(rdr[0].ToString());  
                    }

                }
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                using(SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Rezervari WHERE IdVacanta = @id and NrPersoane = @nr and PretTotal = @pret", con);
                    cmd.Parameters.AddWithValue("@id", idfinal);
                    cmd.Parameters.AddWithValue("@nr", nrpers);
                    cmd.Parameters.AddWithValue("@pret", pret);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
