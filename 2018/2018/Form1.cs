using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

namespace _2018
{
    public partial class Form1 : Form
    {
        private SqlConnection con;
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
        List<Imagini> imagini;
        private int index = -1;
        private System.Windows.Forms.Timer timer;
        
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(constr);
            con.Open();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;

            LoadImagini();
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            if(index < imagini.Count - 1)
            {
                progressBar1.Maximum = imagini.Count - 1;
                index++; 
                progressBar1.Value++;
                if(progressBar1.Maximum == progressBar1.Value)
                {
                    butonManual.Enabled = true;
                }
                LoadForIntex();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Stergere();
            initializare();
            pictureBox1.BackgroundImage = Image.FromFile("user.bmp");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            butonInapoi.Enabled = false;
            butonInainte.Enabled = false;
        }
        private void Stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();

            new SqlCommand("TRUNCATE TABLE Evaluari", con).ExecuteNonQuery();
            new SqlCommand("TRUNCATE TABLE Itemi", con).ExecuteNonQuery();
            new SqlCommand("TRUNCATE TABLE Utilizatori", con).ExecuteNonQuery();
        }


        private void initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd;
            StreamReader sr = new StreamReader(Application.StartupPath + @"\..\..\date.txt");
            string sir;
            char[] split = { ';' };
            DateTime dt;
            string sectiune = "";
            while((sir = sr.ReadLine()) != null) 
            {
                if(sir.EndsWith(":")) 
                {
                    sectiune = sir.Replace(":", "");
                    continue;
                }
                string[] siruri = sir.Split(split);
                switch (sectiune)
                {
                    case "Utilizatori":
                        cmd = new SqlCommand(@"Insert into Utilizatori(numeprenumeutilizator, parolautilizator, emailutilizator, clasautilizator) values(@numeprenumeutilizator, @parolautilizator, @emailutilizator, @clasautilizator)", con);
                        cmd.Parameters.AddWithValue("numeprenumeutilizator", siruri[0]);
                        cmd.Parameters.AddWithValue("parolautilizator", siruri[1]);
                        cmd.Parameters.AddWithValue("emailutilizator", siruri[2]);
                        cmd.Parameters.AddWithValue("clasautilizator", siruri[3]);
                        cmd.ExecuteNonQuery();
                        break;
                    case "Itemi":
                        cmd = new SqlCommand(@"Insert into Itemi(tipitem, enuntitem, raspuns1item, raspuns2item, raspuns3item, raspuns4item, raspunscorectitem) values(@tipitem, @enuntitem, @raspuns1item, @raspuns2item, @raspuns3item, @raspuns4item, @raspunscorectitem)", con);
                        cmd.Parameters.AddWithValue("tipitem", Null(siruri[0]));
                        cmd.Parameters.AddWithValue("enuntitem", Null(siruri[1]));
                        cmd.Parameters.AddWithValue("raspuns1item", Null(siruri[2]));
                        cmd.Parameters.AddWithValue("raspuns2item", Null(siruri[3]));
                        cmd.Parameters.AddWithValue("raspuns3item", Null(siruri[4]));
                        cmd.Parameters.AddWithValue("raspuns4item", Null(siruri[5]));
                        cmd.Parameters.AddWithValue("raspunscorectitem", Null(siruri[6]));
                        cmd.ExecuteNonQuery();
                        break;
                    case "Evaluari": string d = siruri[1];
                        string raw = siruri[1].Trim();           
                        dt = DateTime.ParseExact(raw,"M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        cmd = new SqlCommand(@"Insert into Evaluari(idelev, dataevaluare, notaevaluare) values(@idelev, @dataevaluare, @notaevaluare)", con);
                        cmd.Parameters.AddWithValue("idelev", siruri[0]);
                        cmd.Parameters.AddWithValue("dataevaluare", dt);
                        cmd.Parameters.AddWithValue("notaevaluare", siruri[2]);
                        cmd.ExecuteNonQuery();
                        break;                        
                }             
            }
            con.Close();
        }
      
        private object Null(string s)
        {
            if (s == "NULL")          
            {
                return DBNull.Value;  
            }
            else
            {
                return s.Trim();      
            }
        }

        private void butonLogin_Click(object sender, EventArgs e)
        { 
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Utilizatori where emailutilizator = @email and parolautilizator = @parola", con);
            cmd.Parameters.AddWithValue("email", textBox1.Text);
            cmd.Parameters.AddWithValue("parola", textBox2.Text);
            SqlDataReader red = cmd.ExecuteReader();
            if (red.Read())
            {
                int id = Convert.ToInt32(red["IdUtilizator"]);
                red.Close();
                MenuStrip strip = new MenuStrip();
                strip.Show();


                eLearning1918_Elev elev = new eLearning1918_Elev(id);
                elev.Show();
                this.Hide();
                elev.FormClosed += (a, b) =>
                {
                   this.Show();
                    textBox1.Text = textBox2.Text = "";
                };
            }
            else 
                MessageBox.Show("Eroare de autentificare");

            con.Close();
        }
        public void LoadImagini()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            imagini = new List<Imagini>();
            SqlCommand cmd = new SqlCommand("Select * from imagini", con);
            var red = cmd.ExecuteReader();
            while (red.Read())
            {
                imagini.Add(new Imagini
                {
                    CaleFisier = red[0].ToString(),
                });
            }
            if (imagini.Count > 0 && index == -1)
                index = 0;
            LoadForIntex();
            con.Close();
        }

        public void LoadForIntex()
        {
            panel1.BackgroundImage = Image.FromFile(imagini[index].CaleFisier);
        }


        private void butonManual_Click(object sender, EventArgs e)
        {
            butonInainte.Enabled = true;
            butonInapoi.Enabled = true;
            butonManual.Visible = false;
            butonAuto.Visible = true;
            timer.Stop();
        }

        private void butonAuto_Click(object sender, EventArgs e)
        {
            butonManual.Visible = true;
            butonAuto.Visible = false;
            butonInainte.Enabled = false;
            butonInapoi.Enabled = false;
            timer.Start();
        }

        private void butonInainte_Click(object sender, EventArgs e)
        {
            if (index < imagini.Count - 1)
            {
                progressBar1.Maximum = imagini.Count - 1;
                index++;
                progressBar1.Value++;
                if (progressBar1.Minimum == progressBar1.Value)
                {
                    butonInapoi.Enabled = false;
                    butonInainte.Enabled = true;
                }
                if (progressBar1.Minimum != progressBar1.Value && progressBar1.Maximum != progressBar1.Value)
                {
                    butonInapoi.Enabled = true;
                    butonInainte.Enabled = true;
                }
                if (progressBar1.Maximum == progressBar1.Value)
                {
                    butonInainte.Enabled = false;
                    butonInapoi.Enabled = true;
                }
                LoadForIntex();
            }
        }

        private void butonInapoi_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                progressBar1.Minimum = 0;
                index--;
                progressBar1.Value--;
                if (progressBar1.Minimum == progressBar1.Value)
                {
                    butonInapoi.Enabled = false;
                    butonInainte.Enabled = true;
                }
                if(progressBar1.Minimum != progressBar1.Value && progressBar1.Maximum != progressBar1.Value)
                {
                    butonInapoi.Enabled = true;
                    butonInainte.Enabled = true;
                }
                if(progressBar1.Maximum == progressBar1.Value)
                {
                    butonInainte.Enabled = false;
                    butonInapoi.Enabled = true;
                }
                LoadForIntex();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
