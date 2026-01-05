using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2019_C_
{
    public partial class Form1 : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Fineas\\source\\repos\\SUBIECTE OJTI\\OJTI_2019_C#\\OJTI_2019_C#\\bin\\Debug\\FreeBook.mdf\";Integrated Security=True;Connect Timeout=30";
        private static bool initializat = false;

        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            if (!initializat)
            {
                stergere();
                inserare();
                initializat = true;
            }
        }
        private void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            new SqlCommand("truncate table carti", con).ExecuteNonQuery();
            new SqlCommand("truncate table imprumut", con).ExecuteNonQuery();
            new SqlCommand("truncate table utilizatori", con).ExecuteNonQuery();

        }
        private void inserare()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            StreamReader reader = new StreamReader(path + @"\utilizatori.txt");
            string sir;
            char[] sep = { '*' };
            while ((sir = reader.ReadLine()) != null)
            {
                string[] siruri = sir.Split(sep);
                SqlCommand cmd = new SqlCommand("insert into utilizatori(email, parola, nume, prenume) values(@email, @pass, @nume, @prenume)", con);
                cmd.Parameters.AddWithValue("@email", siruri[0]);
                cmd.Parameters.AddWithValue("@pass", siruri[1]);
                cmd.Parameters.AddWithValue("@nume", siruri[2]);
                cmd.Parameters.AddWithValue("@prenume", siruri[3]);
                cmd.ExecuteNonQuery();
            }
            StreamReader rd = new StreamReader(path + @"\carti.txt");
            string sir2;

            while ((sir2 = rd.ReadLine()) != null)
            {
                string[] siruri = sir2.Split(sep);
                SqlCommand cmd = new SqlCommand("insert into carti(titlu, autor, gen) values(@titlu, @autor, @gen)", con);
                cmd.Parameters.AddWithValue("@titlu", siruri[0]);
                cmd.Parameters.AddWithValue("@autor", siruri[1]);
                cmd.Parameters.AddWithValue("@gen", siruri[2]);
                cmd.ExecuteNonQuery();
            }
            StreamReader sr = new StreamReader(path + @"\imprumuturi.txt");
            string sir3;
            DateTime dt;

            while ((sir3 = sr.ReadLine()) != null)
            {
                string[] siruri = sir3.Split(sep);
                string titluCarte = siruri[0].Trim();
                string email = siruri[1].Trim();
                string rawDate = siruri[2].Trim();

                SqlCommand findCmd = new SqlCommand("SELECT id_carte FROM carti WHERE titlu = @titlu", con);
                findCmd.Parameters.AddWithValue("@titlu", titluCarte);
                object idResult = findCmd.ExecuteScalar();

                if (idResult != null)
                {
                    int idCarte = Convert.ToInt32(idResult);

                    SqlCommand cmd = new SqlCommand("insert into imprumut(id_carte, email, data_imprumut) values(@id_carte, @imprumut, @data_imprumut)", con);
                    cmd.Parameters.AddWithValue("@id_carte", idCarte);
                    cmd.Parameters.AddWithValue("@imprumut", email);
                    cmd.Parameters.AddWithValue("@data_imprumut", DateTime.Parse(rawDate, CultureInfo.InvariantCulture));
                    cmd.ExecuteNonQuery();
                }
            }
            sr.Close();
            con.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logare log = new Logare();
            this.Hide();
            log.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Autentificare auth = new Autentificare();
            this.Hide();
            auth.Show();
        }
    }
}
