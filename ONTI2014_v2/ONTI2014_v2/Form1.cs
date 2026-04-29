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
    public partial class Form1 : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Riscuri.mdf;Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
            inserare();
        }

        void inserare()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("truncate table AnalizePacienti", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("truncate table DatePersonale", con);
                cmd3.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("INSERT INTO DatePersonale \r\n(Nume, Prenume, Gen, Varsta, Data_Nasterii, Email) VALUES\r\n\r\n('Popescu', 'Ion', 'M', 47, '1978-05-12', 'ion.popescu@mail.com'),\r\n('Ionescu', 'Maria', 'F', 53, '1972-11-03', 'maria.ionescu@mail.com'),\r\n('Georgescu', 'Andrei', 'M', 40, '1986-02-20', 'andrei.georgescu@mail.com'),\r\n('Dumitrescu', 'Elena', 'F', 61, '1964-07-15', 'elena.dumitrescu@mail.com'),\r\n('Stan', 'Mihai', 'M', 30, '1995-09-10', 'mihai.stan@mail.com');", con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("DELETE FROM AnalizePacienti;\r\n\r\nINSERT INTO AnalizePacienti \r\n(ID_Pacient, Data_Analize, Colesterol_Total, HDL, TAS, PCR, BCVF, Fumator) VALUES\r\n\r\n-- Pacient 1\r\n(1, '2025-09-15', 210, 45, 135, 0.70, 1, 1),\r\n(1, '2026-01-10', 220, 50, 140, 0.80, 1, 1),\r\n(1, '2026-03-18', 205, 52, 138, 0.75, 1, 1),\r\n\r\n-- Pacient 2\r\n(2, '2025-10-20', 190, 60, 125, 0.50, 0, 0),\r\n(2, '2026-02-05', 195, 58, 130, 0.55, 0, 0),\r\n(2, '2026-03-19', 200, 62, 132, 0.60, 0, 0),\r\n\r\n-- Pacient 3\r\n(3, '2025-11-01', 230, 40, 145, 0.90, 1, 1),\r\n(3, '2026-02-20', 240, 42, 150, 1.00, 1, 1),\r\n(3, '2026-03-20', 235, 45, 148, 0.95, 1, 1),\r\n\r\n-- Pacient 4\r\n(4, '2025-12-10', 250, 38, 155, 1.20, 1, 0),\r\n(4, '2026-02-28', 245, 40, 150, 1.10, 1, 0),\r\n(4, '2026-03-20', 260, 35, 160, 1.30, 1, 0),\r\n\r\n-- Pacient 5\r\n(5, '2025-09-05', 180, 65, 120, 0.40, 0, 0),\r\n(5, '2026-01-25', 185, 68, 118, 0.45, 0, 0),\r\n(5, '2026-03-17', 175, 70, 115, 0.35, 0, 0);", con);
                cmd.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "oti2014" && textBox2.Text == string.Empty)
            {
                CalculRisc calcul = new CalculRisc();
                calcul.Show();
                this.Hide();
            }
        }
    }
}
