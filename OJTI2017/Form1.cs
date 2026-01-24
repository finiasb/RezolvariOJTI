using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace OJTI2017
{
    public partial class Form1 : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\Turism.mdf"";Integrated Security=True;Connect Timeout=30";
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;
        private List <string> paths = new List <string> ();
        private int id;
        public Form1()
        {
            InitializeComponent();
            stergere();
            initializare();
            incarcareComboBox1();
            comboBox1.SelectedIndex = 0;
            GetIdFromName(comboBox1.SelectedItem.ToString());
            incarcareComboBox2();
        }

        private void stergere()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Truncate table imagini", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Truncate table Localitati", con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Truncate table Planificari", con);
                cmd3.ExecuteNonQuery();

            }
        }
        private void initializare()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                var lines = File.ReadAllLines(Path.Combine(path, "planificari.txt"));
                int idLocalitate = 1;

                foreach (var line in lines)
                {
                    var c = line.Split('*').Select(x => x.Trim()).ToArray();
                    string nume = c[0];
                    string frecventa = c[1].ToLower();

                    SqlCommand cmdLocalitate = new SqlCommand("INSERT INTO Localitati(Nume) VALUES(@nume)", con);
                    cmdLocalitate.Parameters.Add("@nume", SqlDbType.VarChar).Value = nume;
                    cmdLocalitate.ExecuteNonQuery();

                    SqlCommand cmdPlan = new SqlCommand("INSERT INTO Planificari(IDLocalitate, Frecventa, DataStart, DataStop, Ziua) VALUES(@id, @freq, @dStart, @dStop, @ziua)", con);
                    cmdPlan.Parameters.Add("@id", SqlDbType.Int).Value = idLocalitate;
                    cmdPlan.Parameters.Add("@freq", SqlDbType.VarChar).Value = frecventa;
                    cmdPlan.Parameters.Add("@dStart", SqlDbType.DateTime).Value = DBNull.Value;
                    cmdPlan.Parameters.Add("@dStop", SqlDbType.DateTime).Value = DBNull.Value;
                    cmdPlan.Parameters.Add("@ziua", SqlDbType.Int).Value = DBNull.Value;
                    if (frecventa == "ocazional")
                    {
                        cmdPlan.Parameters["@dStart"].Value = DateTime.ParseExact(c[2], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        cmdPlan.Parameters["@dStop"].Value = DateTime.ParseExact(c[3], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    }
                    else if (frecventa == "anual" || frecventa == "lunar")
                    {
                        cmdPlan.Parameters["@ziua"].Value = int.Parse(c[2]);
                    }
                    cmdPlan.ExecuteNonQuery();

                    int startIndex = (frecventa == "ocazional") ? 4 : 3;
                    for (int j = startIndex; j < c.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(c[j]))
                        {
                            string cale = Path.Combine(path, "Imagini", c[j]);
                            SqlCommand cmdImg = new SqlCommand("INSERT INTO Imagini(IDLocalitate, CaleFisier) VALUES(@id, @cale)", con);
                            cmdImg.Parameters.Add("@id", SqlDbType.Int).Value = idLocalitate;
                            cmdImg.Parameters.Add("@cale", SqlDbType.VarChar).Value = cale;
                            cmdImg.ExecuteNonQuery();
                        }
                    }
                    idLocalitate++;
                }
            }
        }

        private void incarcareComboBox1()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select Nume from Localitati", con);
                SqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Close();
            }
        }
        private void incarcareComboBox2()
        {
            comboBox2.Items.Clear();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select CaleFisier from Imagini where IDLocalitate = @id", con);
                cmd1.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd1.ExecuteReader();
                while(reader.Read())
                {
                    string calecompleta = reader[0].ToString();
                    string rezultat = calecompleta.Replace(path + "Imagini\\", "");
                    comboBox2.Items.Add(rezultat);
                }
                reader.Close();
            }
            comboBox2.SelectedIndex = 0;
            pictureBox1.Image = Image.FromFile(path + "Imagini\\" + comboBox2.SelectedItem.ToString());

        }
        private void GetIdFromName(string localitate)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select IDLocalitate from Localitati where Nume = @nume", con);
                cmd1.Parameters.AddWithValue("@nume", localitate);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    id = Int32.Parse(reader[0].ToString());
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetIdFromName(comboBox1.SelectedItem.ToString());
            incarcareComboBox2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count != 11)
                listBox1.Items.Add(comboBox2.SelectedItem.ToString());
            else
                MessageBox.Show("Maximum 10 imagini");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + "Imagini\\" + comboBox2.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Adăugați cel puțin o imagine!");
                return;
            }

            saveFileDialog1.Filter = "Imagine PNG (*.png)|*.png";
            saveFileDialog1.Title = "Salvează posterul";
            saveFileDialog1.FileName = "poster.png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap poster = new Bitmap(900, 1200);

                using (Graphics g = Graphics.FromImage(poster))
                {
                    g.Clear(Color.White);

                    Font titluFont = new Font("Arial", 32, FontStyle.Bold);
                    Font footerFont = new Font("Arial", 14);
                    Brush brush = Brushes.Black;

                    int y = 40;

                    string titlu = textBox1.Text;
                    if (titlu == "")
                        titlu = "Poster turistic";

                    g.DrawString(titlu, titluFont, brush, 100, y);
                    y += 80;

                    g.DrawLine(Pens.Black, 100, y, 800, y);
                    y += 30;

                    int x = 100;
                    int imgWidth = 300;
                    int imgHeight = 200;
                    int spatiu = 30;
                    int coloana = 0;

                    foreach (string img in listBox1.Items)
                    {
                        Image imagine = Image.FromFile(path + "Imagini\\" + img);
                        g.DrawImage(imagine, x, y, imgWidth, imgHeight);
                        imagine.Dispose();

                        x += imgWidth + spatiu;
                        coloana++;

                        if (coloana == 2)
                        {
                            coloana = 0;
                            x = 100;
                            y += imgHeight + spatiu;
                        }
                    }

                    y += 40;

                    g.DrawLine(Pens.Gray, 100, y, 800, y);
                    y += 20;
                    g.DrawString("OJTI 2017", footerFont, brush, 100, y);
                }

                poster.Save(saveFileDialog1.FileName, ImageFormat.Png);
                poster.Dispose();

                MessageBox.Show("Poster generat cu succes!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vizualizare viz = new Vizualizare();
            viz.Show();
            this.Hide();    
        }
    }
}

