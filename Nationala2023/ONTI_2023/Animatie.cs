using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2023
{
    public partial class Animatie : Form
    {
        public Animatie()
        {
            InitializeComponent();
            animatie();
        }
        private async void animatie()
        {
            string numePic1 = $"PictureBox1";
            string numePic2 = $"PictureBox2";
            string numePic3 = $"PictureBox3";
            string numePic4 = $"PictureBox4";
            string numePic5 = $"PictureBox5";
            string numePic6 = $"PictureBox6";
            string numePic7 = $"PictureBox7";
            string numePic8 = $"PictureBox8";
            string numePic9 = $"PictureBox9";
            string numePic10 = $"PictureBox10";
            string numePic11 = $"PictureBox11";
            string numePic12 = $"PictureBox12";
            string numePic13 = $"PictureBox13";
            string numePic14 = $"PictureBox14";
            string numePic15 = $"PictureBox15";
            PictureBox pictureBox1 = this.Controls.Find(numePic1, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox2 = this.Controls.Find(numePic2, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox3 = this.Controls.Find(numePic3, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox4 = this.Controls.Find(numePic4, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox5 = this.Controls.Find(numePic5, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox6 = this.Controls.Find(numePic6, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox7 = this.Controls.Find(numePic7, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox8 = this.Controls.Find(numePic8, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox9 = this.Controls.Find(numePic9, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox10 = this.Controls.Find(numePic10, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox11 = this.Controls.Find(numePic11, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox12 = this.Controls.Find(numePic12, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox13 = this.Controls.Find(numePic13, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox14 = this.Controls.Find(numePic14, true).FirstOrDefault() as PictureBox;
            PictureBox pictureBox15 = this.Controls.Find(numePic15, true).FirstOrDefault() as PictureBox;
            for (int j = 1; j <= 33; j++)
            {
                if (j < 10)
                {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Artificii\" + $"artificie_0{j}.png";
                    pictureBox1.Image = Image.FromFile(path);
                    pictureBox2.Image = Image.FromFile(path);
                    pictureBox3.Image = Image.FromFile(path);
                    pictureBox4.Image = Image.FromFile(path);
                    pictureBox5.Image = Image.FromFile(path);
                    pictureBox6.Image = Image.FromFile(path);
                    pictureBox7.Image = Image.FromFile(path);
                    pictureBox8.Image = Image.FromFile(path);
                    pictureBox9.Image = Image.FromFile(path);
                    pictureBox10.Image = Image.FromFile(path);
                    pictureBox11.Image = Image.FromFile(path);
                    pictureBox12.Image = Image.FromFile(path);
                    pictureBox13.Image = Image.FromFile(path);
                    pictureBox14.Image = Image.FromFile(path);
                    pictureBox15.Image = Image.FromFile(path);
                }
                else
                {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Artificii\" + $"artificie_{j}.png";
                    pictureBox1.Image = Image.FromFile(path);
                    pictureBox2.Image = Image.FromFile(path);
                    pictureBox3.Image = Image.FromFile(path);
                    pictureBox4.Image = Image.FromFile(path);
                    pictureBox5.Image = Image.FromFile(path);
                    pictureBox6.Image = Image.FromFile(path);
                    pictureBox7.Image = Image.FromFile(path);
                    pictureBox8.Image = Image.FromFile(path);
                    pictureBox9.Image = Image.FromFile(path);
                    pictureBox10.Image = Image.FromFile(path);
                    pictureBox11.Image = Image.FromFile(path);
                    pictureBox12.Image = Image.FromFile(path);
                    pictureBox13.Image = Image.FromFile(path);
                    pictureBox14.Image = Image.FromFile(path);
                    pictureBox15.Image = Image.FromFile(path);
                }
                await Task.Delay(60);
            }
        }
    }
}
