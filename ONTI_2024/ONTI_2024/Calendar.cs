using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI_2024
{
    public partial class Calendar : Form
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory; 
        int year = 2026;
        int month = 2;
        int index;
        public Calendar()
        {
            InitializeComponent();
            pictureBox2.Parent = pictureBox1;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Location = new Point(150, 150);
            pictureBox2.BringToFront();
        }
        List<string> numeLuni = new List<string>{ "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };
        private void Calendar_Load(object sender, EventArgs e)
        {
            incarcaCalendar();
        }

        private void incarcaCalendar()
        {
            label1.Text = numeLuni.ElementAt(month - 1) + " " + year;
            for (int i = 1; i <= 42; i++)
            {
                string butName = $"Button{i}";
                Button but = this.Controls.Find(butName, false).FirstOrDefault() as Button;
                if (but != null)
                {
                    but.Visible = false;
                    but.Text = "";
                }
            }

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            int dayIndex = (int)firstDayOfMonth.DayOfWeek;
            if (dayIndex == 0) dayIndex = 7; 

            int ziCurenta = 1;
            for (int i = dayIndex; i < dayIndex + daysInMonth; i++)
            {
                string butName = $"Button{i}";
                Button but = this.Controls.Find(butName, false).FirstOrDefault() as Button;

                if (but != null)
                {
                    but.Text = ziCurenta.ToString();
                    but.Visible = true;
                    ziCurenta++;
                }
            }
        }

        double calculFazaLunii(int y, int m, int d)
        {
            if (m <= 2)
            {
                y -= 1;
                m += 12;
            }
            double A = y / 100;
            double B = A / 4;
            double C = 2 - A + B;
            double E = (int)(365.25 * (y + 4716));
            double F = (int)(30.6001 * (m + 1));
            double JD = C + d + E + F - 1524.5;
            double ultimaZi =(double)(JD - 2451549.5) ;

            double CateLuniNoiAuExistat = ultimaZi / 29.5;

            double ParteaFractionara = CateLuniNoiAuExistat - (int)CateLuniNoiAuExistat;

            double zileDeLaUltimaLunaNoua = ParteaFractionara * 29.5;


            double zilePerFaza = 29.5 / 8;

            return zileDeLaUltimaLunaNoua / zilePerFaza;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 42; i++)
            {
                string butName = $"Button{i}";
                Button but = this.Controls.Find(butName, false).FirstOrDefault() as Button;
                if (but != null)
                {
                    but.Controls.Clear();
                }
            }

            month++;
            if (month == 13)
            {
                month = 1;
                year++;

            }

            incarcaCalendar();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 42; i++)
            {
                string butName = $"Button{i}";
                Button but = this.Controls.Find(butName, false).FirstOrDefault() as Button;
                if (but != null)
                {
                    but.Controls.Clear();
                }
            }
            month--;
            if (month == 0)
            {
                month = 12;
                year--;

            }

            incarcaCalendar();
        }
        int day;
        private void Click_btn(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (string.IsNullOrEmpty(btn.Text)) return;

            day = Int32.Parse(btn.Text);

            double rezultat = calculFazaLunii(year, month, day);
           // MessageBox.Show(rezultat.ToString());
            int codFaza = (int)rezultat;

            if (codFaza == 0) 
                codFaza = 8;
            string numeImagine = "";
            switch (codFaza)
            {
                case 1: numeImagine = "1_LunaNoua.png"; break;
                case 2: numeImagine = "2_SemilunaInCrestere.png"; break;
                case 3: numeImagine = "3_PrimulPatrar.png"; break;
                case 4: numeImagine = "4_LunaCrestere.png"; break;
                case 5: numeImagine = "5_LunaPlina.png"; break;
                case 6: numeImagine = "6_LunaDescrestere.png"; break;
                case 7: numeImagine = "7_AlTreileaPatrar.png"; break;
                case 8: numeImagine = "8_SemilunaDescrestere.png"; break;
            }
            pictureBox2.Image = Image.FromFile(path + $"ImaginiLuna\\{numeImagine}");
            PictureBox pic = new PictureBox();

            pic.Image = Image.FromFile(path + $"ImaginiLuna\\{numeImagine}");
            pic.Parent = btn;
            pic.BackColor = Color.Transparent;
            pic.Size = new Size(20, 20);
            pic.Location = new Point(10, 45);
            pic.BringToFront();
            pic.SizeMode = PictureBoxSizeMode.StretchImage;

            string zodieImg = getImgZodie(day, month);

            PictureBox pic2 = new PictureBox();

            pic2.Image = Image.FromFile(path + $"ImaginiZodii\\{zodieImg}");
            pic2.Parent = btn;
            pic2.BackColor = Color.Transparent;
            pic2.Size = new Size(20, 20);
            pic2.Location = new Point(40, 45);
            pic2.BringToFront();
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            pic2.Click += pictureBox3_Click;
        }


        

        string getImgZodie(int d, int m)
        {
            if ((m == 12 && d >= 22) || (m == 1 && d <= 19)) return "Z_5.png";
            if ((m == 1 && d >= 20) || (m == 2 && d <= 18)) return "Z_12.png";
            if ((m == 2 && d >= 19) || (m == 3 && d <= 20)) return "Z_6.png";
            if ((m == 3 && d >= 21) || (m == 4 && d <= 20)) return "Z_2.png";
            if ((m == 4 && d >= 21) || (m == 5 && d <= 20)) return "Z_3.png";
            if ((m == 5 && d >= 21) || (m == 6 && d <= 21)) return "Z_1.png";
            if ((m == 6 && d >= 22) || (m == 7 && d <= 22)) return "Z_4.png";
            if ((m == 7 && d >= 23) || (m == 8 && d <= 22)) return "Z_8.png";
            if ((m == 8 && d >= 23) || (m == 9 && d <= 22)) return "Z_9.png";
            if ((m == 9 && d >= 23) || (m == 10 && d <= 22)) return "Z_7.png";
            if ((m == 10 && d >= 23) || (m == 11 && d <= 21)) return "Z_11.png";
            if ((m == 11 && d >= 22) || (m == 12 && d <= 21)) return "Z_10.png";
            return "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e; 
            if(me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Constelatii constelatii = new Constelatii(day, month);
                this.Hide();
                constelatii.Show();
            }
        }
    }
}
