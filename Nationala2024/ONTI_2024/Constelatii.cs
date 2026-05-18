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
    public partial class Constelatii : Form
    {
        int index;
        public Constelatii(int day, int month)
        {
            InitializeComponent();
            pictureBox3.Parent = pictureBox1;
            pictureBox4.Parent = pictureBox1;
            pictureBox5.Parent = pictureBox1;
            pictureBox6.Parent = pictureBox1;
            pictureBox7.Parent = pictureBox1;
            pictureBox8.Parent = pictureBox1;
            pictureBox9.Parent = pictureBox1;
            pictureBox10.Parent = pictureBox1;
            pictureBox11.Parent = pictureBox1;
            pictureBox12.Parent = pictureBox1;
            pictureBox13.Parent = pictureBox1;
            pictureBox14.Parent = pictureBox1;
            pictureBox15.Parent = pictureBox1;
            pictureBox16.Parent = pictureBox1;
            index = getZodieByIndex(day, month);
            string nume = getZodie(index);
            string detalii = getZodieDetalii(index);
            label1.Text = nume;
            label2.Text = detalii;
        }

        string getZodie(int index)
        {
            if (index == 5) return "Capricorn";
            if (index == 12) return "Vărsător";
            if (index == 6) return "Pești";
            if (index == 2) return "Berbec";
            if (index == 3) return "Taur";
            if (index == 1) return "Gemeni";
            if (index == 4) return "Rac";
            if (index == 8) return "Leu";
            if (index == 9) return "Fecioară";
            if (index == 7) return "Balanță";
            if (index == 11) return "Scorpion";
            if (index == 10) return "Săgetător";
            return "";
        }

        string getZodieDetalii(int index)
        {
            if (index == 5) return "Se știe că nativul Capricorn este responsabil și prudent, trăsături care îi fac cinste. Munca îl defineşte cel mai bine pe ambiţiosul Capricorn";
            if (index == 12) return "Nativul Vărsător are un suflet mare şi o fire caritabilă. Independent, idealist şi visător cu toate acestea.Vărsătorul nu acceptă compromisuri este atașat de familie, dar are și un soi de independență pe care nu o poate condiționa nimeni";
            if (index == 6) return "Nativul din zodia Pești este un artist cu suflet sensibil, este un mare visător și are o imaginație bogată. Este un prieten de nădejde, gata să îţi asculte necazurile şi să îţi ridice moralul";
            if (index == 2) return "Nativul Berbec. nu lasă pe nimeni la nevoie, este generos, le sare tuturor în ajutor și este un om pe care te poți baza în situațiile critice";
            if (index == 3) return "Nativul Taur poate fi un prieten minunat, dacă îl respecţi. Este preocupat să afle întotdeauna adevărul şi să facă dreptate tuturor";
            if (index == 1) return "Nativul Gemeni este înzestrat cu un spirit vesel şi cu o minte ascuţită, are o fire dezinvoltă, sociabilă și prietenoasă. Curios şi avid de noutăţi, nativul din Gemeni iubește libertatea şi aventura";
            if (index == 4) return "Nativul Rac este deosebit de sensibil, fapt care poate fi considerat, în egală măsură, o calitate, dar şi un defect. El are principii de viață foarte clare și nu se abate de la ele";
            if (index == 8) return "Nativul Leu se poate lăuda cu o fire deschisă şi fermecătoare, care îi cucereşte imediat pe cei din jur. În familie, o face pe șeful, dar este afectuos și generos";
            if (index == 9) return "Nativul din Fecioară pune raţiunea înaintea inimii, dar acest lucru nu înseamnă că este un individ rece și calculat. Punctual, disciplinat şi bine organizat, nativul zodiei este mereu apreciat";
            if (index == 7) return "Nativul din Balanţe – este în firea lor să fie prietenoase şi comunicative, atente şi îngăduitoare cu toţi. Este un optimist, știe să vadă partea bună din fiecare, să aprecieze calităţile şi să accepte defectele";
            if (index == 11) return "Nativul Scorpion este greu de descifrat. Are multe calități, are multe de oferit, dar foarte rar își deschide sufletul către cei din jur. Este inteligent, descurcăreț, dinamic, carierist";
            if (index == 10) return "Direct, deschis şi onest uneori este superficial, dar, când ai nevoie de ajutorul cuiva. Cu firea lui optimistă, vede întotdeauna jumătatea plină a paharului şi te ajută să treci peste necazuri";
            return "";
        }


        int getZodieByIndex(int d, int m)
        {
            if ((m == 12 && d >= 22) || (m == 1 && d <= 19)) return 5;
            if ((m == 1 && d >= 20) || (m == 2 && d <= 18)) return 12;
            if ((m == 2 && d >= 19) || (m == 3 && d <= 20)) return 6;
            if ((m == 3 && d >= 21) || (m == 4 && d <= 20)) return 2;
            if ((m == 4 && d >= 21) || (m == 5 && d <= 20)) return 3;
            if ((m == 5 && d >= 21) || (m == 6 && d <= 21)) return 1;
            if ((m == 6 && d >= 22) || (m == 7 && d <= 22)) return 4;
            if ((m == 7 && d >= 23) || (m == 8 && d <= 22)) return 8;
            if ((m == 8 && d >= 23) || (m == 9 && d <= 22)) return 9;
            if ((m == 9 && d >= 23) || (m == 10 && d <= 22)) return 7;
            if ((m == 10 && d >= 23) || (m == 11 && d <= 21)) return 11;
            if ((m == 11 && d >= 22) || (m == 12 && d <= 21)) return 10;
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index++;
            if (index > 12) index = 1; 
            ActualizeazaInterfata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index--;
            if (index < 1) index = 12;
            ActualizeazaInterfata();
        }
        void ActualizeazaInterfata()
        {
            label1.Text = getZodie(index);
            label2.Text = getZodieDetalii(index);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int zoomFactor = trackBar1.Value;

            
            int sourceWidth = 250 / zoomFactor;
            int sourceHeight = 200 / zoomFactor;

            int sourceX = e.X - (sourceWidth / 2);
            int sourceY = e.Y - (sourceHeight / 2);

            sourceX = Math.Max(0, Math.Min(sourceX, pictureBox1.Width - sourceWidth));
            sourceY = Math.Max(0, Math.Min(sourceY, pictureBox1.Height - sourceHeight));

            Bitmap fullSnapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            pictureBox1.DrawToBitmap(fullSnapshot, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            Bitmap zoomedSection = new Bitmap(sourceWidth, sourceHeight);

            Rectangle rect1 = new Rectangle(0, 0, sourceWidth, sourceHeight);
            Rectangle rect2 = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);


            using (Graphics g = Graphics.FromImage(zoomedSection))
            {
                g.DrawImage(fullSnapshot,rect1, rect2, GraphicsUnit.Pixel);
            }

            if (pictureBox2.Image != null)
                pictureBox2.Image.Dispose();
            
            pictureBox2.Image = zoomedSection;

            fullSnapshot.Dispose();
        }
    }
}
