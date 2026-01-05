using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OJTI_2019_C_
{
    public partial class Meniu : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Fineas\\source\\repos\\SUBIECTE OJTI\\OJTI_2019_C#\\OJTI_2019_C#\\bin\\Debug\\FreeBook.mdf\";Integrated Security=True;Connect Timeout=30";
        private string _email;
        private List<int> idCarti = new List<int>();
        private List<int> idCartiImprumutate = new List<int>();
        private List<DateTime> dataCartiImprumutate = new List<DateTime>();
        private bool ok;
        private int i = 0, k = 0;
        public Meniu(string email)
        {
            InitializeComponent();
            _email = email;
            label1.Text = "Email Utilizator: " + _email;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
     
            inserare();
            inserare2();
            IncarcaAni();
        }
        private void inserare()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select id_carte, data_imprumut from imprumut", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DateTime azi = DateTime.Now;

            idCarti.Clear();

            while (reader.Read())
            {
                int idCarte = Convert.ToInt32(reader["id_carte"]);
                DateTime dataImprumut = Convert.ToDateTime(reader["data_imprumut"]);
                DateTime dataExpirare = dataImprumut.AddDays(30);

                if (dataExpirare > azi)
                    idCarti.Add(idCarte);
            }

            reader.Close();
            con.Close();

            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmdd = new SqlCommand("select titlu, autor, gen, id_carte from carti", conn);
            SqlDataReader red = cmdd.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (red.Read())
            {
                string titlu = red["titlu"].ToString();
                string autor = red["autor"].ToString();
                string gen = red["gen"].ToString();
                int idCarte = Convert.ToInt32(red["id_carte"]);

                ok = true;
                foreach (int id in idCarti)
                {
                    if (id == idCarte)
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                    dataGridView1.Rows.Add(titlu, autor, gen);
            }

            red.Close();
            conn.Close();
        }
        private void inserare2()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select id_carte, data_imprumut from imprumut where email = @e", con);
            cmd.Parameters.AddWithValue("@e", _email);
            SqlDataReader reader = cmd.ExecuteReader();

            idCartiImprumutate.Clear();
            dataCartiImprumutate.Clear();

            while (reader.Read())
            {
                idCartiImprumutate.Add(Convert.ToInt32(reader["id_carte"]));
                dataCartiImprumutate.Add(Convert.ToDateTime(reader["data_imprumut"]));
            }
            reader.Close();
            con.Close();

            SqlConnection conn = new SqlConnection(constr);
            conn.Open();

            dataGridView2.Rows.Clear();

            int index = 0;
            int cartiActive = 0;

            for (int i = 0; i < idCartiImprumutate.Count; i++)
            {
                int idCarte = idCartiImprumutate[i];
                DateTime dataImprumut = dataCartiImprumutate[i];
                DateTime dataReturnare = dataImprumut.AddDays(30);

                SqlCommand cmdd = new SqlCommand("select titlu, autor from carti where id_carte = @id", conn);
                cmdd.Parameters.AddWithValue("@id", idCarte);
                SqlDataReader red = cmdd.ExecuteReader();

                if (red.Read())
                {
                    string titlu = red["titlu"].ToString();
                    string autor = red["autor"].ToString();

                    index++;
                    int rowIndex = dataGridView2.Rows.Add(index, titlu, autor,
                        dataImprumut.ToShortDateString(), dataReturnare.ToShortDateString());

                    if (dataReturnare < DateTime.Now)
                    {
                        dataGridView2.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dataGridView2.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        dataGridView2.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Green;
                        cartiActive++;
                    }
                }
                red.Close();
            }
            conn.Close();

            progressBar1.Maximum = 3;
            progressBar1.Value = cartiActive > 3 ? 3 : cartiActive;
            label2.Text = $"{cartiActive}/3 cărți active";
        }
        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "ImpurmutaCarte")
                return;
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ImpurmutaCarte")
            {
                DateTime dt = DateTime.Now;
                int countCarti = cartiActive(_email);

                if (countCarti >= 3)
                {
                    MessageBox.Show("Ați atins limita de 3 cărți împrumutate în ultimele 30 de zile.");
                    return;
                }

                var cell = dataGridView1.Rows[e.RowIndex].Cells["ImpurmutaCarte"];
                if (countCarti < 3 && (cell.Value == null || cell.Value.ToString() != "Împrumutată"))
                {
                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    string titlu = dataGridView1.Rows[e.RowIndex].Cells["titlu"].Value.ToString();

                    SqlCommand findCmd = new SqlCommand("SELECT id_carte FROM carti WHERE titlu = @titlu", con);
                    findCmd.Parameters.AddWithValue("@titlu", titlu);
                    object idResult = findCmd.ExecuteScalar();
                    int idCarte = Convert.ToInt32(idResult);

                    SqlCommand cmd = new SqlCommand("insert into imprumut(id_carte, email, data_imprumut) values(@id_carte, @email, @data_imprumut)", con);
                    cmd.Parameters.AddWithValue("@id_carte", idCarte);
                    cmd.Parameters.AddWithValue("@email", _email);
                    cmd.Parameters.AddWithValue("@data_imprumut", dt);
                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Carte împrumutată cu succes!");
                    cell.Value = "Împrumutată";
                    cell.Style.BackColor = Color.LightGray;
                    cell.Style.ForeColor = Color.DarkGray;
                    cell.ReadOnly = true;

                    inserare2();
                }
            }
        }
        private int cartiActive(string email)
        {
            using (var con = new SqlConnection(constr))
            {
                con.Open();
                var cmd = new SqlCommand(@"select count(*) from imprumut where email = @e and data_imprumut >= DATEADD(DAY, -30, CAST(GETDATE() AS date))", con);
                cmd.Parameters.AddWithValue("@e", email);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Selectați un an din listă!");
                return;
            }

            int anSelectat = int.Parse(comboBox1.SelectedItem.ToString());
            Dictionary<int, int> utilizatoriPeLuna = new Dictionary<int, int>();

            for (int i = 1; i <= 12; i++)
                utilizatoriPeLuna[i] = 0;

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                SELECT MONTH(data_imprumut) AS luna, COUNT(DISTINCT email) AS utilizatori
                FROM imprumut
                WHERE YEAR(data_imprumut) = @an
                GROUP BY MONTH(data_imprumut)
                ORDER BY luna", con);
                cmd.Parameters.AddWithValue("@an", anSelectat);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int luna = Convert.ToInt32(reader["luna"]);
                    int numarUtilizatori = Convert.ToInt32(reader["utilizatori"]);
                    utilizatoriPeLuna[luna] = numarUtilizatori;
                }
                reader.Close();
            }

            chart1.Series.Clear();
            Series serie = new Series("Număr utilizatori")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue
            };

            foreach (var item in utilizatoriPeLuna)
            {
                string numeLuna = new DateTime(2000, item.Key, 1).ToString("MMMM");
                serie.Points.AddXY(numeLuna, item.Value);
            }

            chart1.Series.Add(serie);
            chart1.ChartAreas[0].AxisX.Title = "Lunile anului";
            chart1.ChartAreas[0].AxisY.Title = "Număr utilizatori";
            chart1.ChartAreas[0].RecalculateAxesScale();
    }

        private void tabPage4_Click(object sender, EventArgs e)
        {

            inserare2();
        }

        private void IncarcaAni()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT YEAR(data_imprumut) AS an FROM imprumut ORDER BY an", con);
            SqlDataReader reader = cmd.ExecuteReader();

            comboBox1.Items.Clear();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["an"].ToString());
            }

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            reader.Close();
        }



    }
}
