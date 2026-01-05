using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2015
{
    public partial class ListaCroaziera : Form
    {
        private int[,] distantePorturi;

        bool RclickPutem = false;
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Fineas\source\repos\SUBIECTE OJTI\OJTI2015\OJTI2015\bin\Debug\DBTimpSpatiu2.mdf"";Integrated Security=True;Connect Timeout=30";
        bool oMers = false;
        public List<string> numePorturi = new List<string> { "Constanta", "Varna", "Burgas", "Istambul", "Kozlu", "Samsun", "Batumi", "Sokhumi", "Soci", "Anapa", "Yalta", "Sevastopol", "Odessa", };
        private List<int> portX = new List<int>();
        private List<int> portY = new List<int>();
        int ConstantaX = 82;
        int ConstantaY = 270;
        int VarnaX = 46;
        int VarnaY = 364;
        int BurgasX = 22;
        int BurgasY = 431;
        int IstambulX = 101;
        int IstambulY = 573;
        int KozluX = 242;
        int KozluY = 530;
        int SamsunX = 473;
        int SamsunY = 546;
        int BatumiX = 747;
        int BatumiY = 513;
        int SokhumiX = 711;
        int SokhumiY = 382;
        int SociX = 649;
        int SociY = 326;
        int AnapaX = 527;
        int AnapaY = 198;
        int YaltaX = 363;
        int YaltaY = 235;
        int SevastopolX = 332;
        int SevastopolY = 223;
        int OdessaX = 189;
        int OdessaY = 37;
        public ListaCroaziera()
        {
            InitializeComponent();
        }

        private void ListaCroaziera_MouseClick(object sender, MouseEventArgs e)
        {
        }
        int mouseApasat = 0;
        

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (RclickPutem)
            {
                if (e.X <= ConstantaX + 5 && e.X >= ConstantaX - 5 && e.Y <= ConstantaY + 5 && e.Y >= ConstantaY - 5 && mouseApasat == 0)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= VarnaX + 5 && e.X >= VarnaX - 5 && e.Y <= VarnaY + 5 && e.Y >= VarnaY - 5 && mouseApasat == 1)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);

                }
                else if (e.X <= BurgasX + 5 && e.X >= BurgasX - 5 && e.Y <= BurgasY + 5 && e.Y >= BurgasY - 5 && mouseApasat == 2)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= IstambulX + 5 && e.X >= IstambulX - 5 && e.Y <= IstambulY + 5 && e.Y >= IstambulY - 5 && mouseApasat == 3)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= KozluX + 5 && e.X >= KozluX - 5 && e.Y <= KozluY + 5 && e.Y >= KozluY - 5 && mouseApasat == 4)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= SamsunX + 5 && e.X >= SamsunX - 5 && e.Y <= SamsunY + 5 && e.Y >= SamsunY - 5 && mouseApasat == 5)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= BatumiX + 5 && e.X >= BatumiX - 5 && e.Y <= BatumiY + 5 && e.Y >= BatumiY - 5 && mouseApasat == 6)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= SokhumiX + 5 && e.X >= SokhumiX - 5 && e.Y <= SokhumiY + 5 && e.Y >= SokhumiY - 5 && mouseApasat == 7)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= SociX + 5 && e.X >= SociX - 5 && e.Y <= SociY + 5 && e.Y >= SociY - 5 && mouseApasat == 8)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= AnapaX + 5 && e.X >= AnapaX - 5 && e.Y <= AnapaY + 5 && e.Y >= AnapaY - 5 && mouseApasat == 9)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= YaltaX + 5 && e.X >= YaltaX - 5 && e.Y <= YaltaY + 5 && e.Y >= YaltaY - 5 && mouseApasat == 10)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= SevastopolX + 5 && e.X >= SevastopolX - 5 && e.Y <= SevastopolY + 5 && e.Y >= SevastopolY - 5 && mouseApasat == 11)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                }
                else if (e.X <= OdessaX + 5 && e.X >= OdessaX - 5 && e.Y <= OdessaY + 5 && e.Y >= OdessaY - 5 && mouseApasat == 12)
                {
                    mouseApasat++;
                    portX.Add(e.X);
                    portY.Add(e.Y);
                    oMers = true;
                }
                else
                {
                    MessageBox.Show("Nu ati apasat in ordine sau inauntrul cercului care urmeaza");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RclickPutem = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(oMers)
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("truncate table porturi", con);
                    cmd1.ExecuteNonQuery();
                    for (int i = 0; i <= 12; i++)
                    {
                        SqlCommand cmd = new SqlCommand("insert into porturi(Nume_port, Pozitie_X, Pozitie_Y) values(@Nume_port123, @Pozitie_X2, @Pozitie_Y2)", con);

                        cmd.Parameters.AddWithValue("@Nume_port123", numePorturi[i]);
                        cmd.Parameters.AddWithValue("@Pozitie_X2", portX[i]);
                        cmd.Parameters.AddWithValue("@Pozitie_Y2", portY[i]);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("sql updated");
                }
            }
            else
            {
                MessageBox.Show("prima completati orasele");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            distantePorturi = new int[13, 13];
            int i = 0;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Harta_Distantelor.txt";
            StreamReader rdr = new StreamReader(path);
            string linie;
            while((linie = rdr.ReadLine()) != null)
            {
                string[] c = linie.Trim().Split(' ');
                for(int j = 0;  j < c.Length; j++)
                {
                    int strToChar = Int32.Parse(c[j]);
                    distantePorturi[i, j] = strToChar;
                }
                i++;
            }rdr.Close();
              

            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("truncate table Distante", con);
                cmd1.ExecuteNonQuery();
                for (int m = 0; m <= 12; m++)
                {
                    for (int n = 0; n <= 12; n++)
                    {
                        SqlCommand cmd = new SqlCommand("insert into distante(ID_Port, ID_Port_Destinatie, Nume_Port_Destinatie, Distanta) values(@ID_Port, @ID_Port_Destinatie, @Nume_Port_Destinatie, @Distanta)", con);
                        cmd.Parameters.AddWithValue("@ID_Port", m + 1);
                        cmd.Parameters.AddWithValue("@ID_Port_Destinatie", n + 1);
                        cmd.Parameters.AddWithValue("@Nume_Port_Destinatie", numePorturi[n]);
                        cmd.Parameters.AddWithValue("@Distanta", distantePorturi[m, n]);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Test");
            }
        }

        private void combinariDeTrei()
        {
            distantePorturi = new int[13, 13];
            int b = 0;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Harta_Distantelor.txt";
            StreamReader rdr = new StreamReader(path);
            string linie;
            while ((linie = rdr.ReadLine()) != null)
            {
                string[] c = linie.Trim().Split(' ');
                for (int j = 0; j < c.Length; j++)
                {
                    int strToChar = Int32.Parse(c[j]);
                    distantePorturi[b, j] = strToChar;
                }
                b++;
            }
            rdr.Close();
            for (int i = 1; i < 11; i++)
            {
                for (int j = i + 1; j < 12; j++)
                {
                    for (int k = j + 1; k < 13; k++)
                    {
                        int distantaTotala = distantePorturi[0, i] + distantePorturi[i, j] + distantePorturi[j, k] + distantePorturi[k, 0];
                        string listaPorturi = $"{1}, {i + 1}, {j + 1}, {k + 1}, {1}";
                        if (distantaTotala >= 800 && distantaTotala <= 1100)
                        {
                            using(SqlConnection con = new SqlConnection(constr))
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("insert into croaziere(Tip_Croaziera, Lista_Porturi, Pret) values(@Tip_Croaziera, @Lista_Porturi, @Pret)", con);
                                cmd.Parameters.AddWithValue("@Tip_Croaziera", 3);
                                cmd.Parameters.AddWithValue("@Lista_Porturi", listaPorturi);
                                cmd.Parameters.AddWithValue("@Pret", 2 * distantaTotala);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private void combinariDeCinci()
        {
            distantePorturi = new int[13, 13];
            int b = 0;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Harta_Distantelor.txt";
            StreamReader rdr = new StreamReader(path);
            string linie;
            while ((linie = rdr.ReadLine()) != null)
            {
                string[] c = linie.Trim().Split(' ');
                for (int j = 0; j < c.Length; j++)
                {
                    int strToChar = Int32.Parse(c[j]);
                    distantePorturi[b, j] = strToChar;
                }
                b++;
            }
            rdr.Close();
            for (int i = 1; i < 9; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    for (int k = j + 1; k < 11; k++)
                    {
                        for(int l = k + 1; l < 12; l++)
                        {
                            for(int m = l + 1; m < 13; m++)
                            {
                                int distantaTotala = distantePorturi[0, i] +  distantePorturi[i, j] + distantePorturi[j, k] + distantePorturi[k, l] + distantePorturi[l, m] + distantePorturi[m, 0];
                                string listaPorturi = $"{1}, {i + 1}, {j + 1}, {k + 1}, {l + 1}, {m + 1}, {1}";
                                if (distantaTotala >= 800 && distantaTotala <= 1600)
                                {
                                    using (SqlConnection con = new SqlConnection(constr))
                                    {
                                        con.Open();
                                        SqlCommand cmd = new SqlCommand("insert into croaziere(Tip_Croaziera, Lista_Porturi, Pret) values(@Tip_Croaziera, @Lista_Porturi, @Pret)", con);
                                        cmd.Parameters.AddWithValue("@Tip_Croaziera", 5);
                                        cmd.Parameters.AddWithValue("@Lista_Porturi", listaPorturi);
                                        cmd.Parameters.AddWithValue("@Pret", 2 * distantaTotala);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void combinariDeOpt()
        {
            distantePorturi = new int[13, 13];
            int b = 0;
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Harta_Distantelor.txt";
            StreamReader rdr = new StreamReader(path);
            string linie;
            while ((linie = rdr.ReadLine()) != null)
            {
                string[] c = linie.Trim().Split(' ');
                for (int j = 0; j < c.Length; j++)
                {
                    int strToChar = Int32.Parse(c[j]);
                    distantePorturi[b, j] = strToChar;
                }
                b++;
            }
            rdr.Close();
            for (int i = 1; i < 6; i++)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    for (int k = j + 1; k < 8; k++)
                    {
                        for (int l = k + 1; l < 9; l++)
                        {
                            for (int m = l + 1; m < 10; m++)
                            {
                                for(int n = m + 1; n < 11; n++)
                                {
                                    for( int o = n + 1; o < 12; o++)
                                    {
                                        for(int p = o + 1; p < 13; p++)
                                        {
                                            int distantaTotala = distantePorturi[0, i] + distantePorturi[i, j] + distantePorturi[j, k] + distantePorturi[k, l] + distantePorturi[l, m] + distantePorturi[m, n] + distantePorturi[n, o] +  distantePorturi[o, p] + distantePorturi[p, 0];
                                            string listaPorturi = $"{1}, {i + 1}, {j + 1}, {k + 1}, {l + 1}, {m + 1}, {n + 1}, {o + 1}, {p + 1}, {1}";
                                            if (distantaTotala >= 800 && distantaTotala <= 1900)
                                            {
                                                using (SqlConnection con = new SqlConnection(constr))
                                                {
                                                    con.Open();
                                                    SqlCommand cmd = new SqlCommand("insert into croaziere(Tip_Croaziera, Lista_Porturi, Pret) values(@Tip_Croaziera, @Lista_Porturi, @Pret)", con);
                                                    cmd.Parameters.AddWithValue("@Tip_Croaziera", 8);
                                                    cmd.Parameters.AddWithValue("@Lista_Porturi", listaPorturi);
                                                    cmd.Parameters.AddWithValue("@Pret", 2 * distantaTotala);
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("truncate table Croaziere", con);
                cmd1.ExecuteNonQuery();
            }
            combinariDeTrei();
            combinariDeCinci();
            combinariDeOpt();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListeCroaziere liste = new ListeCroaziere();
            liste.Show();
        }
    }
}
