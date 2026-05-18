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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace JocEducativ
{
    public partial class Guess : Form
    {
        string cuvant;
        Label[] litereLbl;
        Label[] underscoreLbl;
        private string constr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}JocEducativ.mdf;Integrated Security=True;Connect Timeout=30";
        int x = 6;
        int punctaj = 100;
        int greseli = 0;
        int literegasite = 0;
        private readonly string __email;

        public Guess(string _email)
        {
            InitializeComponent();
            __email = _email;
        }

        private void Guess_Load(object sender, EventArgs e)
        {
            var cuvinte = File.ReadAllLines(
                 Path.Combine(Application.StartupPath, @"..\..\Cuvinte.txt"))
                 .Where(l => !string.IsNullOrWhiteSpace(l))
                 .ToArray();

            Random rnd = new Random();
            cuvant = cuvinte[rnd.Next(cuvinte.Length)].Trim().ToUpper();

            litereLbl = new[]
            {
            label1,label2,label3,label4,label5,
            label6,label7,label8,label9,label10
            };
            underscoreLbl = new[]
            {
            label12,label13,label14,label15,label16,
            label17,label18,label19,label20,label21
            };
            label11.Text = $"Punctaj : {punctaj}";
            label22.Text = "";
            for (int i = 0; i < litereLbl.Length; i++)
            {
                if (i < cuvant.Length)
                {
                    litereLbl[i].Text = cuvant[i].ToString();
                    litereLbl[i].Visible = false;
                    underscoreLbl[i].Visible = true;
                }
                else
                {
                    litereLbl[i].Visible = false;
                    underscoreLbl[i].Visible = false;
                }
            }
            pictureBox1.Visible = true;
            pictureBox1.Image = Image.FromFile("6.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ButonLitera_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Visible = false;

            char lit = btn.Text[0];
            pictureBox1.Visible = false;
            label11.Visible = false;
            bool gasit = false;
            for (int i = 0; i < cuvant.Length && i < litereLbl.Length; i++)
            {
                if (cuvant[i] == lit)
                {
                    litereLbl[i].Visible = true;
                    underscoreLbl[i].Visible = false;
                    gasit = true;
                    literegasite++;
                }
            }

            if (literegasite == cuvant.Length)
            {
                MessageBox.Show($"Ai castigat! Punctaj : {punctaj}");
                string email = __email;
                int d = 0;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Rezultate(TipJoc, EmailUtilizator, PunctajJoc) values(@TipJoc, @EmailUtilizator, @PunctajJoc)", con);
                cmd.Parameters.AddWithValue("@TipJoc", d);
                cmd.Parameters.AddWithValue("@EmailUtilizator", email);
                cmd.Parameters.AddWithValue("@PunctajJoc", punctaj);
                cmd.ExecuteNonQuery();
                this.Hide();
                AlegeJoc joc = new AlegeJoc(email);
                joc.Show();
            }

            if (gasit && x != 6)
            {
                x++;
                pictureBox2.Image = Image.FromFile($"{x}.png");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (gasit && x == 6)
            {
                pictureBox2.Image = Image.FromFile($"{x}.png");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (!gasit && x > 1)
            {
                x--;
                pictureBox2.Image = Image.FromFile($"{x}.png");
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (!gasit && x == 1)
            {
                punctaj = 0;
                MessageBox.Show($"Ai pierdut! Punctaj : {punctaj}");
                label22.Text = $"Punctaj : {punctaj}";
                string email = __email;
                int d = 0;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Rezultate(TipJoc, EmailUtilizator, PunctajJoc) values(@TipJoc, @EmailUtilizator, @PunctajJoc)", con);
                cmd.Parameters.AddWithValue("@TipJoc", d);
                cmd.Parameters.AddWithValue("@EmailUtilizator", email);
                cmd.Parameters.AddWithValue("@PunctajJoc", punctaj);
                cmd.ExecuteNonQuery();
                AlegeJoc joc = new AlegeJoc(email);
                this.Hide();
                joc.Show();
            }

            if (!gasit)
            {
                punctaj = punctaj - 4;
                label22.Text = $"Punctaj : {punctaj}";
            }
            else if (gasit)
            {
                label22.Text = $"Punctaj : {punctaj}";
            }


        }

        private void logIn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
