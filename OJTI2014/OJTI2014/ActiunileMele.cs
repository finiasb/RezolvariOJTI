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

namespace OJTI2014
{
    public partial class ActiunileMele : Form
    {
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBBursa.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        Random r = new Random();
        int timpTrecut;
        List<int> valoareCrestere = new List<int> { 0, 0, 0, 0, 0 };
        Form1 f;
        public ActiunileMele(Form1 f1)
        {
            InitializeComponent();
            f = f1;
        }
        public void StartTimer(int interval)
        {
            timer1.Interval = interval;
            timer1.Start();
        }
        public void StopTimer()
        {
            timer1.Stop();
        }
        public void GetInterval(int interval)
        {
            timer1.Interval = interval;
        }
        int profitPierdereTotala2;
        List<Point> points = new List<Point>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            timpTrecut += timer1.Interval;
            dataGridView1.Rows.Clear();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Denumire, NrActiuni, Valoare from Actiuni", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                int index = 0;
                profitPierdereTotala2 = 0;
                while (rdr.Read())
                {
                    string denumire = rdr[0].ToString();
                    int nrActiuni = int.Parse(rdr[1].ToString());
                    int Valoare = int.Parse(rdr[2].ToString());
                    int r2 = r.Next(-5, 6);
                    
                    valoareCrestere[index] += r2;
                    
                    int valoareActiuneMomentan = Valoare + valoareCrestere[index];
                    int TotalValoareInitiala = nrActiuni * Valoare;
                    int TotalValoareMomentan = nrActiuni * valoareActiuneMomentan;
                    int ProfitSauPierdereMomentana = nrActiuni * valoareActiuneMomentan;
                    int ProfitSauPierdereTotala = TotalValoareMomentan - TotalValoareInitiala;
                    profitPierdereTotala2 += ProfitSauPierdereTotala;
                    dataGridView1.Rows.Add(denumire, nrActiuni, Valoare, valoareActiuneMomentan, valoareCrestere[index], TotalValoareInitiala, TotalValoareMomentan, ProfitSauPierdereMomentana, ProfitSauPierdereTotala);
                    index++;
                }
                textBox1.Text = profitPierdereTotala2 + "";
                points.Add(new Point(timpTrecut,profitPierdereTotala2));
            }
            f.GetPoints(points, profitPierdereTotala2);
        }
    }
}
