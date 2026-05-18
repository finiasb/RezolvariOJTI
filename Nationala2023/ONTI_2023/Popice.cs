using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023
{
    public partial class Popice : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";
        int xMinge = 350, yMinge = 318;
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        private string _email;
        HashSet<int> list = new HashSet<int>();
        Random rnd = new Random();
        int km = 1;
        string litere1, litere2;    
        string litereToate;
        int x1, y1;
        int timp = 100;
        Dictionary<int, string> numeImagini = new Dictionary<int, string>()
            {
                {1, "AVION"}, {2, "BLOC"}, {3, "CAINE"}, {4, "CAPRIOARA"},
                {5, "IEPURE"}, {6, "LEU"}, {7, "LUP"}, {8, "MASINA"},
                {9, "MINGE"}, {10, "PISICA"}, {11, "TAUR"}, {12, "URS"},
                {13, "VULPE"}, {14, "PATINE"}
            };
        public Popice(string email)
        {
            InitializeComponent();
            _email = email;
            int x = rnd.Next(1, 7), y = rnd.Next(8, 15);

            pictureBox2.Image = Image.FromFile(path + $"Imagini\\{x}.jpg");
            pictureBox3.Image = Image.FromFile(path + $"Imagini\\{y}.jpg");

            litere1 = numeImagini[x];
            litere2 = numeImagini[y];

            litereToate = litere1 + litere2;

            int lengthLitere = litere1.Length + litere2.Length;

            x1 = 100;
            y1 = 100;
            List<int> indici = new List<int>();

            for (int i = 0; i < lengthLitere; i++)
                indici.Add(i);

            for (int i = lengthLitere - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                int temp = indici[i];
                indici[i] = indici[j];
                indici[j] = temp;
            }

            foreach (int index in indici)
            {
                Label label = new Label();
                label.AutoSize = true;
                label.Font = new Font("Arial", 25, FontStyle.Bold);
                label.Text = litereToate[index].ToString().ToUpper();
                label.Location = new Point(x1, y1);
                label.Name = "label" + km;
                x1 += 50;
                km++;
                this.Controls.Add(label);
                label.BringToFront();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timp--;
            labelTimp.Text = "Scor: " + timp;
        }
            int gata1 = 0, gata2 = 0;
        string cuvant = "";
        private void timer2_Tick(object sender, EventArgs e)
        {


            yMinge -= 10;
            pictureBox1.Location = new Point(xMinge, yMinge);

            if (yMinge < 0) 
            {
                timer2.Stop();
                ResetMinge();
            }

            foreach (Control c in this.Controls)
            {
                if (c is Label && c.Name.StartsWith("label") && c.Name != "labelTimp")
                {
                    if (pictureBox1.Bounds.IntersectsWith(c.Bounds))
                    {
                        timer2.Stop();
                        cuvant += c.Text;
                        cuvantlabel.Text = "Cuvant: " + cuvant;
                        this.Controls.Remove(c); 
                        c.Dispose();       
                        ResetMinge();      
                        break;                   
                    }
                }
            }

            int ok1 = 0, ok2 = 0;
            for(int i = 0; i < cuvant.Length; i++)
            {
                if(litere1.Length < cuvant.Length)
                {
                    ok1 = 1;
                    break;
                }
                if (litere1[i] != cuvant[i])
                    ok1 = 1;
            }
            for (int i = 0; i < cuvant.Length; i++)
            {
                if (litere2.Length < cuvant.Length)
                {
                    ok1 = 1;
                    break;
                }
                if (litere2[i] != cuvant[i])
                    ok2 = 1;
            }
            if(cuvant == litere1)
            {
                pictureBox2.Image = null;
                cuvant = "";
                cuvantlabel.Text = "Cuvant: " + cuvant;
                gata1 = 1;
                MessageBox.Show(gata1 + " " + gata2);

            }
            if (cuvant == litere2)
            {
                pictureBox3.Image = null;
                cuvant = "";
                cuvantlabel.Text = "Cuvant: " + cuvant;
                gata2 = 1;
                MessageBox.Show(gata1 + " " + gata2);
            }

            if(gata1 == 1 && gata2 == 1) { 
                MessageBox.Show("Ati castigat");
                timer1.Stop();
                timer2.Stop();
                jocGata();
            }


            if ((ok1 == 1 && ok2 == 1) || timp == 0)
            {
                MessageBox.Show("Ati pierdut");
                timer1.Stop();
                timer2.Stop();

                timp = 0;
                jocGata();
            }

        }
        private void jocGata()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                DateTime dt = DateTime.Now;
                int tip = 1;
                SqlCommand cmd = new SqlCommand("Insert into Rezultate(EmailUtilizator, TipJoc, PunctajJoc, Data) values(@email, @TipJoc, @PunctajJoc, @Data)", con);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@TipJoc", tip);
                cmd.Parameters.AddWithValue("@PunctajJoc", timp);
                cmd.Parameters.AddWithValue("@Data", dt);
                cmd.ExecuteNonQuery();
            }
            this.Hide();
            AlegeJoc alege = new AlegeJoc(_email);
            alege.Show();
        }

        private void ResetMinge()
        {
            yMinge = 318;
            pictureBox1.Location = new Point(xMinge, yMinge);
        }


        private void Popice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && pictureBox1.Location.X > 100 && timer2.Enabled == false)
            {
                xMinge -= 20;
                pictureBox1.Location = new Point(xMinge, yMinge);
                timer1.Start();
            }
            else if (e.KeyCode == Keys.D && pictureBox1.Location.X < x1 - 40&& timer2.Enabled == false)
            {
                xMinge += 20;
                timer1.Start();
                pictureBox1.Location = new Point(xMinge, yMinge);
            }
            else if (e.KeyCode == Keys.W)
            {
                timer1.Start();
                timer2.Start();
            }
        }
    }
}
