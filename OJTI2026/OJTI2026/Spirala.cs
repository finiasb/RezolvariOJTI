using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2026
{
    public partial class Spirala : Form
    {
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\SpiralaDB.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        List<Point> points = new List<Point>();
        Point centru = new Point(300, 300);
        double unghi = 0;
        int indexCurent = 0;
        int raza = 50;

        Color[] culoriCurcubeu = new Color[]
        {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Indigo,
            Color.Violet
        };
        int cateTriunghiuri;
        Bitmap bmpfinal = new Bitmap(600, 600);
        bool desenat = false;
        string _email;
        string nume, prenume;
        public Spirala(string email)
        {
            InitializeComponent();
            label1.Text = email;
            _email = email;
            numePrenume();
            genereazaLista();
            timer1.Interval = (trackBar1.Value) * 100;
        }

        void genereazaLista()
        {
            points.Clear();
            points.Add(centru);
            unghi = 0;
            cateTriunghiuri = int.Parse(textBox1.Text);
            for (int n = 1; n <= cateTriunghiuri + 5; n++)
            {
                int x = (int)(centru.X + (Math.Sqrt(n) * Math.Cos(unghi) * raza));
                int y = (int)(centru.Y - (Math.Sqrt(n) * Math.Sin(unghi) * raza));

                points.Add(new Point(x, y));
                unghi += Math.Atan(1.0 / Math.Sqrt(n));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (desenat == false)
            {
                genereazaLista();
                indexCurent = 0;
                timer1.Start();
                desenat = true;
            }
        }

        void numePrenume()
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select nume, prenume from utilizator where email = @e", con);
                cmd.Parameters.AddWithValue("@e", _email);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nume = reader[0].ToString();
                    prenume = reader[1].ToString();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (indexCurent < cateTriunghiuri)
            {
                indexCurent++;
                pictureBox1.Invalidate();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Graphics g2 = Graphics.FromImage(bmpfinal);
            Pen penLinii = new Pen(Color.Black, 2);
            Font fontEticheta = new Font("Arial", 9);
            Pen penConturCerc = new Pen(Color.Black, 1);
            SolidBrush brushText = new SolidBrush(Color.Black);

            if (pictureBox1.Image != null)
            {
                g2.DrawImage(pictureBox1.Image, 0, 0, bmpfinal.Width, bmpfinal.Height);
            }
            else
            {
                g2.Clear(pictureBox1.BackColor);
            }

            for (int i = 1; i <= indexCurent; i++)
            {
                Point[] varfuriTriunghi = new Point[] { centru, points[i], points[i + 1] };
                Color culoareCurenta = culoriCurcubeu[(i - 1) % 7];

                if (checkBox1.Checked)
                {
                    g.DrawLine(penLinii, points[i], points[i + 1]);
                    g.DrawLine(penLinii, centru, points[i]);
                    g.DrawLine(penLinii, centru, points[i + 1]);

                    g2.DrawLine(penLinii, points[i], points[i + 1]);
                    g2.DrawLine(penLinii, centru, points[i]);
                    g2.DrawLine(penLinii, centru, points[i + 1]);
                }

                if (checkBox2.Checked)
                {
                    using (SolidBrush brushUmplere = new SolidBrush(culoareCurenta))
                    {
                        g.FillPolygon(brushUmplere, varfuriTriunghi);
                        g2.FillPolygon(brushUmplere, varfuriTriunghi);
                    }
                }
               
                if (checkBox3.Checked)
                {
                    Color fundalEticheta = culoriCurcubeu[(i + 4) % 7];
                    SolidBrush brushFundalCerc = new SolidBrush(fundalEticheta);

                    int cG_X = (centru.X + points[i].X + points[i + 1].X) / 3;
                    int cG_Y = (centru.Y + points[i].Y + points[i + 1].Y) / 3;

                    int diametru = 30;
                    int raza = diametru / 2;

                    int cercX = cG_X - raza;
                    int cercY = cG_Y - raza;

                    g.DrawEllipse(penConturCerc, cercX, cercY, diametru, diametru);
                    g.FillEllipse(brushFundalCerc, cercX, cercY, diametru, diametru);
                    g2.DrawEllipse(penConturCerc, cercX, cercY, diametru, diametru);
                    g2.FillEllipse(brushFundalCerc, cercX, cercY, diametru, diametru);

                    string textEticheta = "\u221A" + (i + 1).ToString();

                    SizeF marimeText = g.MeasureString(textEticheta, fontEticheta);
                    float textX = cG_X - (marimeText.Width / 2f);
                    float textY = cG_Y - (marimeText.Height / 2f);

                    g.DrawString(textEticheta, fontEticheta, brushText, textX, textY);
                    g2.DrawString(textEticheta, fontEticheta, brushText, textX, textY);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = (trackBar1.Value) * 100;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int n))
            {
                cateTriunghiuri = n;
            }
            else
            {
                MessageBox.Show("Acest text trebuie sa fie exclusiv un numar");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (desenat == true)
            {
                desenat = false;
                checkBox1.Checked = true;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                textBox1.Text = "17";
                indexCurent = 0;
                trackBar1.Value = 1;
                
                bmpfinal = new Bitmap(600, 600);

                pictureBox1.Image = null;
                pictureBox1.BackColor = Color.Empty;
                pictureBox1.Invalidate();
            }
        }

        private void imagineDeFundalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = path + "Imagini";
            openFileDialog1.Filter = "Fișiere Imagine|*.jpg;*.jpeg;*.png;*.bmp|Toate fișierele|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Invalidate();
            }
        }

        private void culoareDeFundalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = null;
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        private void tiparireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (indexCurent == 0)
            {
                MessageBox.Show("Trebuie să generați spirala lui Theodorus!");
                return;
            }

            saveFileDialog1.Filter = "Fișiere PDF (*.pdf)|*.pdf";
            saveFileDialog1.FileName = "Spirala.pdf";
            saveFileDialog1.InitialDirectory = path;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings.PrintToFile = true;
                printDocument1.PrinterSettings.PrintFileName = saveFileDialog1.FileName;

                printDocument1.Print();
            }
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Spirala_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics gPdf = e.Graphics;
            Font fontText = new Font("Arial", 16);
            SolidBrush brushNegru = new SolidBrush(Color.Black);

            string textTitlu = "Spirala lui Theodorus";
            SizeF marimeText = gPdf.MeasureString(textTitlu, fontText);
            float xMijloc = (e.PageBounds.Width / 2) - (marimeText.Width / 2);
            gPdf.DrawString(textTitlu, fontText, brushNegru, xMijloc, 40);

            int numarTriunghiuri = indexCurent - 1;
            gPdf.DrawString($"Număr triunghiuri generate: {numarTriunghiuri}", fontText, brushNegru, 50, 100);
            gPdf.DrawString($"Utilizator: {nume} {prenume}", fontText, brushNegru, 50, 140);
            gPdf.DrawString($"Data: {DateTime.Now.ToString("dd.MM.yyyy")}", fontText, brushNegru, 50, 180);

            gPdf.DrawImage(bmpfinal, 50, 250);

            fontText.Dispose();
            brushNegru.Dispose();

            e.HasMorePages = false;
        }
    }
}