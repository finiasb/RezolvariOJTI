using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace OJTI2025
{
    public partial class Expeditie : Form
    {
        int _exploratori;
        int hrana, bogatiiBarca = 0, greutate;
        private System.Windows.Forms.Timer timer;
        int speed = 3;
        Point target = new Point(0, 0);
        bool insula2Bool = true, insula3Bool = true, insula4Bool = true, insula5Bool = true, insula6Bool = true, insula7Bool = true, insula8Bool = true, insula9Bool = true, insula10Bool = true, insula11Bool = true;
        private string insulaPrecedenta = "Cadiz";
        int distanta;
        int zile;
        string descriere;
        private Bitmap hartaCurenta;
        private Bitmap hartaCurentaCopie;

        Random rnd = new Random();
        private List<int> inOrdine = new List<int>();
        private PictureBox ultimaInsulaPic;
        public Expeditie(int exploratori)
        {
            this.KeyPreview = true;
            InitializeComponent();
            _exploratori = exploratori;
            hrana = 2 * _exploratori * 100;
            greutate = 90 * exploratori + hrana;
            label2.Text = exploratori.ToString();
            label3.Text = hrana.ToString();
            label4.Text = bogatiiBarca.ToString();
            label5.Text = greutate.ToString();
            inOrdine.Add(1);
            hartaCurenta = new Bitmap(this.BackgroundImage);
            hartaCurentaCopie = new Bitmap(this.BackgroundImage);

            ultimaInsulaPic = insula1Pic;

        }

        private bool Check()
        {
            if (inOrdine.Count < 7)
                return false;

            for (int i = 0; i < 6; i++)
            {
                if (inOrdine[i] != i + 1)
                    return false;
            }

            return true;
        }

        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void distantaInsule(string ultimaInsula, int actuala)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "/distante.txt";
            StreamReader sr = new StreamReader(path);
            string linie;
            char[] sep = {'#'};
            while((linie = sr.ReadLine()) != null)
            {
                string[] l = linie.Split(sep);
                if (l[0] == ultimaInsula)
                {
                    distanta = Int32.Parse(l[actuala]);
                 
                }
            }
        }

        private void descriereInsula(int actuala)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "/insule.txt";
            StreamReader sr = new StreamReader(path);
            string linie;
            char[] sep = { '#' };
            while ((linie = sr.ReadLine()) != null)
            {
                string[] l = linie.Split(sep);
                int m;
                if(int.TryParse(l[0], out m))
                {
                    if (m == actuala)
                    {
                        descriere = l[6];
                    }
                }
                
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            PointF current = BarcaPic.Location;
            float dx = target.X - current.X;
            float dy = target.Y - current.Y;

            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            if (dist <= speed)
            {
                BarcaPic.Location = target;
                timer.Stop();
                return;
            }

            float nx = dx / dist;
            float ny = dy / dist;

            current.X += nx * speed;
            current.Y += ny * speed;

            BarcaPic.Location = new Point((int)current.X, (int)current.Y);
        }

        private void insula1Pic_Click(object sender, EventArgs e)
        {
            target = insula1Pic.Location;
            InitTimer();

            if (Check())
            {
                MessageBox.Show("Felicitări, ai refăcut traseul lui Columb! \n Vei primi pe desktop o harta a acestui traseu");
            }
            else
            {
                MessageBox.Show("Traseu incomplet!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0); 
        }

        private void insula2Pic_Click(object sender, EventArgs e)
        {
            if (insula2Bool)
            {
                target = insula2Pic.Location;
                InitTimer();
                insula2Lbl.Visible = true;
                insula2Bool = false;
                int k = 2;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula2Pic), distanta);
                ultimaInsulaPic = insula2Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();    
                    exploratori2.Show();
                }
                else{
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate-cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(2);
                }
                insulaPrecedenta = "Madeira";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }

        }
        private void insula3Pic_Click(object sender, EventArgs e)
        {
            if (insula3Bool)
            {
                target = insula3Pic.Location;
                InitTimer();
                insula3Lbl.Visible = true;
                insula3Bool = false;
                int k = 3;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula3Pic), distanta);
                ultimaInsulaPic = insula3Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate - cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(3);

                }
                insulaPrecedenta = "Canare";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void insula4Pic_Click(object sender, EventArgs e)
        {
            if (insula4Bool)
            {

                target = insula4Pic.Location;
                InitTimer();
                insula4Lbl.Visible = true;
                insula4Bool = false;
                int k = 4;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula4Pic), distanta);
                ultimaInsulaPic = insula4Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate - cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(4);

                }
                insulaPrecedenta = "Capul Verde";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }

        private void insula5Pic_Click(object sender, EventArgs e)
        {
            if (insula5Bool)
            {
                target = insula5Pic.Location;
                InitTimer();
                insula5Lbl.Visible = true;
                insula5Bool = false; 
                int k = 5;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula5Pic), distanta);
                ultimaInsulaPic = insula5Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate - cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(5);

                }
                insulaPrecedenta = "Trinidad";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }

        private void insula6Pic_Click(object sender, EventArgs e)
        {
            if (insula6Bool)
            {

                target = insula6Pic.Location;
                InitTimer();
                insula6Lbl.Visible = true;
                insula6Bool = false;
                int k = 6;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula6Pic), distanta);
                ultimaInsulaPic = insula6Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate - cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(6);

                }
                insulaPrecedenta = "Margarita";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void insula7Pic_Click(object sender, EventArgs e)
        {
            if (insula7Bool)
            {
                target = insula7Pic.Location;
                InitTimer();
                insula7Lbl.Visible = true;
                insula7Bool = false;
                int k = 7;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula7Pic), distanta);
                ultimaInsulaPic = insula7Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    int toneDeAur = rnd.Next(9, 101);
                    descriereInsula(k);
                    int cpGreutate = greutate;
                    greutate = greutate + toneDeAur * 1000;
                    if (greutate > 100000)
                        greutate = 100000;
                    MessageBox.Show($"{descriere} \n pe insula sunt {toneDeAur} tone de bogatii \n Exploratorii incarca {(greutate - cpGreutate) / 1000} tone de bogatii");
                    label5.Text = greutate.ToString();
                    bogatiiBarca += greutate - cpGreutate;
                    label4.Text = bogatiiBarca.ToString();
                    inOrdine.Add(7);

                }
                insulaPrecedenta = "Hispaniola";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void insula8Pic_Click(object sender, EventArgs e)
        {
            if (insula8Bool)
            {
                target = insula8Pic.Location;
                InitTimer();
                insula8Lbl.Visible = true;
                insula8Bool = false;
                int k = 8;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula8Pic), distanta);
                ultimaInsulaPic = insula8Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    greutate -= hrana;
                    hrana = hrana - hranaAcum;
                    label3.Text = hrana.ToString();
                    int greutateExploratori = 90 * _exploratori;
                    _exploratori = _exploratori / 2; 
                    greutate = greutate - greutateExploratori / 2 + hrana;
                    label2.Text = _exploratori.ToString(); 
                    if(_exploratori < 30)
                    {
                        MessageBox.Show("Esuat, numar insuficient de exploaratori");
                        this.Hide();
                        Exploratori exploratori2 = new Exploratori();
                        exploratori2.Show();
                    }
                    descriereInsula(k);
                    MessageBox.Show($"{descriere} \n pe insula sunt 0 tone de bogatii \n Exploratorii incarca 0 tone de bogatii");
                    label5.Text = greutate.ToString();
                    inOrdine.Add(8);

                }
                insulaPrecedenta = "Bermude";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void insula9Pic_Click(object sender, EventArgs e)
        {
            if (insula9Bool)
            {
                target = insula9Pic.Location;
                InitTimer();
                insula9Lbl.Visible = true;
                insula9Bool = false;
                int k = 9;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula9Pic), distanta);
                ultimaInsulaPic = insula9Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    greutate -= hrana;
                    hrana = hrana - hranaAcum;
                    label3.Text = hrana.ToString();
                    int greutateExploratori = 90 * _exploratori;
                    _exploratori = _exploratori / 2;
                    greutate = greutate - greutateExploratori / 2 + hrana;
                    label2.Text = _exploratori.ToString();
                    if (_exploratori < 30)
                    {
                        MessageBox.Show("Esuat, numar insuficient de exploaratori");
                        this.Hide();
                        Exploratori exploratori2 = new Exploratori();
                        exploratori2.Show();
                    }
                    descriereInsula(k);
                    MessageBox.Show($"{descriere} \n pe insula sunt 0 tone de bogatii \n Exploratorii incarca 0 tone de bogatii");
                    label5.Text = greutate.ToString();
                    inOrdine.Add(9);

                }
                insulaPrecedenta = "Azore";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }

        }
        private void insula10Pic_Click(object sender, EventArgs e)
        {
            if (insula10Bool)
            {
                target = insula10Pic.Location;
                InitTimer();
                insula10Lbl.Visible = true;
                insula10Bool = false;
                int k = 10;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula10Pic), distanta);
                ultimaInsulaPic = insula10Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    greutate -= hrana;
                    hrana = hrana - hranaAcum;
                    label3.Text = hrana.ToString();
                    int greutateExploratori = 90 * _exploratori;
                    _exploratori = _exploratori / 2;
                    greutate = greutate - greutateExploratori / 2 + hrana;
                    label2.Text = _exploratori.ToString();
                    if (_exploratori < 30)
                    {
                        MessageBox.Show("Esuat, numar insuficient de exploaratori");
                        this.Hide();
                        Exploratori exploratori2 = new Exploratori();
                        exploratori2.Show();
                    }
                    descriereInsula(k);
                    MessageBox.Show($"{descriere} \n pe insula sunt 0 tone de bogatii \n Exploratorii incarca 0 tone de bogatii");
                    label5.Text = greutate.ToString();
                    inOrdine.Add(10);

                }
                insulaPrecedenta = "Martinica";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void insula11Pic_Click(object sender, EventArgs e)
        {
            if (insula11Bool)
            {
                target = insula11Pic.Location;
                InitTimer();
                insula11Lbl.Visible = true;
                insula11Bool = false;
                int k = 11;
                distantaInsule(insulaPrecedenta, k);
                zile = distanta / 100;
                int hranaAcum = 2 * _exploratori * zile;
                Draw(GetCenter(ultimaInsulaPic), GetCenter(insula11Pic), distanta);
                ultimaInsulaPic = insula11Pic;
                if (hranaAcum > hrana)
                {
                    MessageBox.Show("Esuat, hrana insuficienta");
                    this.Hide();
                    Exploratori exploratori2 = new Exploratori();
                    exploratori2.Show();
                }
                else
                {
                    MessageBox.Show($"Ai navigat {zile} zile si ai consumat {hranaAcum} kg hrana");
                    greutate -= hrana;
                    hrana = hrana - hranaAcum;
                    label3.Text = hrana.ToString();
                    int greutateExploratori = 90 * _exploratori;
                    _exploratori = _exploratori / 2;
                    greutate = greutate - greutateExploratori / 2 + hrana;
                    label2.Text = _exploratori.ToString();
                    if (_exploratori < 30)
                    {
                        MessageBox.Show("Esuat, numar insuficient de exploaratori");
                        this.Hide();
                        Exploratori exploratori2 = new Exploratori();
                        exploratori2.Show();
                    }
                    descriereInsula(k);
                    MessageBox.Show($"{descriere} \n pe insula sunt 0 tone de bogatii \n Exploratorii incarca 0 tone de bogatii");
                    label5.Text = greutate.ToString();
                    inOrdine.Add(11);

                }
                insulaPrecedenta = "Guadelupa";
            }
            else
            {
                MessageBox.Show("Ati vizitat deja aceasta insula. Alegeti alta");
            }
        }
        private void Expeditie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Salvare formSalvare = new Salvare(hartaCurentaCopie);
                formSalvare.Show();
                this.Hide();
            }
            if (e.Control && e.KeyCode == Keys.Q)
            {
                Application.Exit();
            }
            else if (e.KeyCode == Keys.Escape) 
            {
                Application.Exit();
            }
        }
        private Point GetCenter(PictureBox pb)
        {
            return new Point(pb.Left + pb.Width / 2, pb.Top + pb.Height / 2);
        }
        private void Draw(Point start, Point end, int distanta)
        {
            using (Graphics g = Graphics.FromImage(hartaCurenta))
            {
                Pen pen = new Pen(Color.Green, 3);
                g.DrawLine(pen, start, end);

                int textX = (start.X + end.X) / 2 + 5;
                int textY = (start.Y + end.Y) / 2 - 15;
                string text = distanta + " km";
                Font font = new Font("Arial", 10, FontStyle.Bold);
                Brush brush = Brushes.Green;
                g.DrawString(text, font, brush, new PointF(textX, textY));
            }
            BackgroundImage = (Bitmap)hartaCurenta.Clone();
        }
    }
}
