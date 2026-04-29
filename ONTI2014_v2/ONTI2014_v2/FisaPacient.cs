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

namespace ONTI2014_v2
{
    public partial class FisaPacient : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Riscuri.mdf;Integrated Security=True;Connect Timeout=30";


        public FisaPacient()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        int countProgramari;
        void GetIdDinData(DateTime dt)
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from AnalizePacienti where Data_Analize = @data", con);
                cmd.Parameters.AddWithValue("@data", dt);
                countProgramari = (int)cmd.ExecuteScalar();
            }
        }
        List<int> cod = new List<int>();
        List<string> Nume = new List<string>();
        List<string> Prenume = new List<string>();
        List<int> varsta = new List<int>();
        List<DateTime> data = new List<DateTime>();
        List<string> email = new List<string>();
        List<bool> gen = new List<bool>();
        List<int> colesterolTotal = new List<int>();
        List<int> colesterolHDL = new List<int>();
        List<int> tensiune = new List<int>();
        List<float> proteina = new List<float>();
        List<bool> fumator = new List<bool>();
        List<bool> BCVF = new List<bool>();
        void getInformatiiAnalize(DateTime dt)
        {
            cod.Clear();
            colesterolHDL.Clear();
            colesterolTotal.Clear();
            tensiune.Clear();
            proteina.Clear();
            BCVF.Clear();
            fumator.Clear();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from AnalizePacienti where Data_Analize = @data", con);
                cmd.Parameters.AddWithValue("@data", dt);
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    cod.Add(int.Parse(rdr[1].ToString()));
                    colesterolTotal.Add(int.Parse(rdr[3].ToString()));
                    colesterolHDL.Add(int.Parse(rdr[4].ToString()));
                    tensiune.Add(int.Parse(rdr[5].ToString()));
                    proteina.Add(float.Parse(rdr[6].ToString()));

                    if (int.Parse(rdr[7].ToString()) == 1)
                        BCVF.Add(true);
                    else
                        BCVF.Add(false);

                    if (int.Parse(rdr[8].ToString()) == 1)
                        fumator.Add(true);
                    else
                        fumator.Add(false);
                }
            }
        }
        void getInformatiiDatePersonale()
        {
            Nume.Clear();
            Prenume.Clear();
            gen.Clear();
            varsta.Clear();
            data.Clear();
            email.Clear();
            foreach(int i in cod)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from DatePersonale where ID_Pacient = @id", con);
                    cmd.Parameters.AddWithValue("@id", i);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Nume.Add(rdr[1].ToString());
                        Prenume.Add(rdr[2].ToString());
                        if (rdr[3].ToString() == "M")
                            gen.Add(true);
                        else
                            gen.Add(false);

                        varsta.Add(int.Parse(rdr[4].ToString()));
                        data.Add(DateTime.Parse(rdr[5].ToString()));
                        email.Add(rdr[6].ToString());   
                    }
                }
            }
        }
        int index = 0;
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GetIdDinData(dateTimePicker1.Value.Date);
            incarcaINtext();
        }
        void incarcaINtext()
        {
            if (countProgramari > 0)
            {
                getInformatiiAnalize(dateTimePicker1.Value.Date);
                getInformatiiDatePersonale();
                textBox1.Text = cod[index] + "";
                textBox2.Text = Prenume[index] + "";
                textBox3.Text = colesterolTotal[index] + "";
                textBox4.Text = Nume[index] + "";
                textBox5.Text = varsta[index] + "";
                textBox6.Text = data[index] + "";
                textBox7.Text = email[index] + "";
                textBox8.Text = colesterolHDL[index] + "";
                textBox9.Text = tensiune[index] + "";
                textBox10.Text = proteina[index] + "";

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 0;
            incarcaINtext();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(index != 0)
            index--;
            incarcaINtext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(index <= countProgramari - 2)
                index++;
            
            incarcaINtext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            index = countProgramari - 1;
            incarcaINtext();
        }
    }
}
