using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024
{
    public partial class Imagini : Form
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        int pozaAleasa;
        List<int> undeInLunile = new List<int>();
        List<int> undeIsPamant = new List<int>();
        List<int> undeESoarele = new List<int>();
        List<int> nuIsCeTrebe = new List<int>();

        public Imagini()
        {
            InitializeComponent();
        }

        private void Imagini_Load(object sender, EventArgs e)
        {
            IncarcareImagini();

            this.Hide();
            Calendar calendar = new Calendar(); 
            calendar.Show();
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            Pen pen = new Pen(Color.Red, 10);
            if ((bool)pic.Tag)
            {
                e.Graphics.DrawRectangle(pen, 0, 0, pic.Width - 1, pic.Height - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int corecteSelectate = 0;
            bool selectieGresita = false;

            List<int> listaCorecta = new List<int>();
            if (pozaAleasa == 1) listaCorecta = undeInLunile;
            else if (pozaAleasa == 2) listaCorecta = undeIsPamant;
            else if (pozaAleasa == 3) listaCorecta = undeESoarele;

            for (int i = 1; i <= 6; i++)
            {
                string picName = $"pictureBox{i}";
                PictureBox pic = this.Controls.Find(picName, true).FirstOrDefault() as PictureBox;
                bool esteSelectat = (bool)pic.Tag;

                if (esteSelectat)
                {
                    if (listaCorecta.Contains(i))
                    {
                        corecteSelectate++;
                    }
                    else
                    {
                        selectieGresita = true;
                        break;
                    }
                }
            }

            if (selectieGresita || corecteSelectate != 3)
            {
                MessageBox.Show("Nu ai selectat imaginile corecte");
                IncarcareImagini();
            }
            else if (corecteSelectate == 3)
            {
                this.Hide();
                Calendar calendar = new Calendar();
                calendar.Show();
            }
        }

        private void IncarcareImagini()
        {
            pictureBox1.Tag = false;
            pictureBox2.Tag = false;
            pictureBox3.Tag = false;
            pictureBox4.Tag = false;
            pictureBox5.Tag = false;
            pictureBox6.Tag = false;
            Random rnd = new Random();
            HashSet<string> folosite = new HashSet<string>();
            HashSet<int> hashSet2 = new HashSet<int>();
            folosite.Clear();
            hashSet2.Clear();
            nuIsCeTrebe.Clear();
            undeESoarele.Clear();
            undeInLunile.Clear();   
            undeIsPamant.Clear();
            pozaAleasa = 0;

            while (hashSet2.Count < 6)
                hashSet2.Add(rnd.Next(1, 7));

            pozaAleasa = rnd.Next(1, 4);

            if (pozaAleasa == 1)
            {
                int index = 0;
                label1.Text = $"Selectați 3 imagini care conțin Luna, \n\r apoi apăsați butonul Am selectat 3 imagini";

                for (int i = 1; i <= 3; i++)
                {
                    int y = rnd.Next(1, 4);
                    string nume = "Luna";
                    string fisier = $"{nume}{y}.png";
                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;
                    undeInLunile.Add(hashSet2.ElementAt(index));
                    index++;
                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
                for (int i = 4; i <= 6; i++)
                {
                    string nume;
                    int y;
                    string fisier;

                    do
                    {
                        int x = rnd.Next(2, 4);
                        y = rnd.Next(1, 5);

                        if (x == 1) nume = "Luna";
                        else if (x == 2) nume = "Pamant";
                        else nume = "Soare";

                        fisier = $"{nume}{y}.png";

                    } while (folosite.Contains(fisier));

                    folosite.Add(fisier);

                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;
                    nuIsCeTrebe.Add(hashSet2.ElementAt(index));
                    index++;
                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
            }
            else if (pozaAleasa == 2)
            {
                label1.Text = $"Selectați 3 imagini care conțin Pamant, \n\r apoi apăsați butonul Am selectat 3 imagini";
                int index = 0;
                for (int i = 1; i <= 3; i++)
                {
                    int y = rnd.Next(1, 4);
                    string nume = "Pamant";
                    string fisier = $"{nume}{y}.png";
                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    undeIsPamant.Add(hashSet2.ElementAt(index));

                    index++;
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;

                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
                for (int i = 4; i <= 6; i++)
                {
                    string nume;
                    int y;
                    string fisier;

                    do
                    {
                        int x = rnd.Next(1, 4);
                        y = rnd.Next(1, 5);

                        if (x == 1) nume = "Luna";
                        else nume = "Soare";

                        fisier = $"{nume}{y}.png";

                    } while (folosite.Contains(fisier));

                    folosite.Add(fisier);

                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    nuIsCeTrebe.Add(hashSet2.ElementAt(index));
                    index++;
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;

                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
            }
            else if (pozaAleasa == 3)
            {
                label1.Text = $"Selectați 3 imagini care conțin Soare,\n\r  apoi apăsați butonul Am selectat 3 imagini";
                int index = 0;

                for (int i = 1; i <= 3; i++)
                {
                    int y = rnd.Next(1, 4);
                    string nume = "Soare";
                    string fisier = $"{nume}{y}.png";
                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    undeESoarele.Add(hashSet2.ElementAt(index));

                    index++;
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;

                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
                for (int i = 4; i <= 6; i++)
                {
                    string nume;
                    int y;
                    string fisier;

                    do
                    {
                        int x = rnd.Next(1, 3);
                        y = rnd.Next(1, 5);

                        if (x == 1) nume = "Luna";
                        else if (x == 2) nume = "Pamant";
                        else nume = "Soare";

                        fisier = $"{nume}{y}.png";

                    } while (folosite.Contains(fisier));

                    folosite.Add(fisier);

                    string picNume = $"pictureBox{hashSet2.ElementAt(index)}";
                    nuIsCeTrebe.Add(hashSet2.ElementAt(index));
                    index++;
                    PictureBox pic = this.Controls.Find(picNume, true).FirstOrDefault() as PictureBox;

                    pic.Image = Image.FromFile(path + $"ImaginiValidare\\{fisier}");
                    pic.Tag = false;
                }
            }
        }

        private void pic_clickk(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            pic.Invalidate();
            pic.Tag = !(bool)pic.Tag;
        }
    }
}
