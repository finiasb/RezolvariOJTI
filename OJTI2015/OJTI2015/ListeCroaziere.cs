using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2015
{
    public partial class ListeCroaziere : Form
    {
        private List<string> numePorturi = new List<string> { "Constanta", "Varna", "Burgas", "Istambul", "Kozlu", "Samsun", "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa", };
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Fineas\source\repos\SUBIECTE OJTI\OJTI2015\OJTI2015\bin\Debug\DBTimpSpatiu2.mdf"";Integrated Security=True;Connect Timeout=30";
        private List<string> list = new List<string>();

        public ListeCroaziere()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string x = comboBox1.SelectedItem.ToString();
            int k = Int32.Parse(x);
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Tip_Croaziera, Lista_Porturi, Pret from croaziere where Tip_croaziera = @tip", con);
                cmd.Parameters.AddWithValue("@tip", k);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string stringid = r["Tip_Croaziera"].ToString();
                    string stringLista_Porturi = r["Lista_Porturi"].ToString();
                    //string stringData_start = r["Data_start"].ToString();
                    //string stringData_final = r["Data_final"].ToString();
                    string stringPret = r["Pret"].ToString();
                    //string stringNr_Pasageri = r["Nr_Pasageri"].ToString();

                    int id = Int32.Parse(stringid);
                    int pret = Int32.Parse(stringPret);
                    //int pasageri = Int32.Parse(stringNr_Pasageri);
                    //DateTime start = DateTime.Parse(stringData_start);
                    //DateTime final = DateTime.Parse(stringData_final);


                    List<int> numbers = stringLista_Porturi.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    List<string> numePorturi2 = new List<string>();
                    foreach(int i in numbers)
                    {
                        numePorturi2.Add(numePorturi[i - 1]);
                    }


                    dataGridView1.Rows.Add(id, string.Join(", ", numePorturi2), null, null, pret, null);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
