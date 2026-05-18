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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JocEducativ
{
    public partial class Întrebare : Form
    {
        private string raspunsCorect;
        private SarpeEducativ _sarpeForm;
        private string punctajItem;
        private int punctaj;
        private string constr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}JocEducativ.mdf;Integrated Security=True;Connect Timeout=30";
        Random Random = new Random();
        public Întrebare(SarpeEducativ sarpeForm)
        {
            InitializeComponent();
            _sarpeForm = sarpeForm;
        }

        private void Intrebare_Load(object sender, EventArgs e)
        {
            int x = Random.Next(1, 21);
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT enuntItem, Raspuns1, Raspuns2, Raspuns3, RaspunsCorect, punctajItem FROM Itemi WHERE idItem = @id", con);
            cmd.Parameters.AddWithValue("@id", x);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                textBox1.Text = reader["enuntItem"].ToString();
                radioButton1.Text = reader["Raspuns1"].ToString();
                radioButton2.Text = reader["Raspuns2"].ToString();
                radioButton3.Text = reader["Raspuns3"].ToString();
                raspunsCorect = reader["RaspunsCorect"].ToString();
                punctajItem = reader["PunctajItem"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int RaspunsCorectInt = Int32.Parse(raspunsCorect);
            punctaj = Int32.Parse(punctajItem);

            if ((RaspunsCorectInt == 1 && radioButton1.Checked) ||(RaspunsCorectInt == 2 && radioButton2.Checked) ||(RaspunsCorectInt == 3 && radioButton3.Checked))
            {
                MessageBox.Show("Ai Răspuns Corect");
                _sarpeForm.AdaugaPunctajBonus(punctaj); 
            }
            else
            {
                MessageBox.Show("Răspuns greșit!");
            }
            this.Close();
        }

    }
}
