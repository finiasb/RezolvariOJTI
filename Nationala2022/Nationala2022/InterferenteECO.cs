using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2022
{
    public partial class InterferenteECO : Form
    {
        string numeBack;
        string directie = "";
        int rotatie = 1;
        int k = 0;
        Point locatieInitiala, locatieInitiala2;
        bool dreaptajos = false, dreaptasus = false, stangasus = false, stangajos = false;
        string numeUtilizator;
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        PictureBox robot;
        List<Point> urmeMov = new List<Point>();
        List<Point> deflectorStangaJos = new List<Point>();
        List<Point> deflectorStangaSus = new List<Point>();
        List<Point> deflectorDreaptaJos = new List<Point>();
        List<Point> deflectorDreaptaSus = new List<Point>();

        List<PictureBox> Sticle = new List<PictureBox>();
        List<PictureBox> Hartie = new List<PictureBox>();
        List<PictureBox> Plastic = new List<PictureBox>();
        List<PictureBox> Meduze = new List<PictureBox>();


        Bitmap imagine1 = new Bitmap(800, 600);
        Bitmap imagine2 = new Bitmap(800, 600);
        Bitmap imagine3 = new Bitmap(800, 600);
        Bitmap imagine4 = new Bitmap(800, 600);
        Bitmap imagine5 = new Bitmap(800, 600);

        int ok = 1;
        int hartie = 0;
        int sticle = 0;
        int plastic = 0;
        public InterferenteECO(string numeBack, string numeUtilizator)
        {
            InitializeComponent();
            this.numeBack = numeBack;
            this.numeUtilizator = numeUtilizator;
            this.Text = "InterferenteEco - " + numeUtilizator;
            pictureBox1.Image = Image.FromFile(path + "Background\\" + numeBack);
            picDreaptaJosDesen(new Point(50, 150));
            robot = new PictureBox();
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SolidBrush brushMov = new SolidBrush(Color.Purple);

            SolidBrush brushyellow = new SolidBrush(Color.Yellow);


            foreach (Point pt in urmeMov)
            {
                Rectangle rect = new Rectangle(pt.X, pt.Y, 40, 60);
                e.Graphics.FillRectangle(brushMov, rect);
            }
            if (urmeMov.Count > 0)
            {
                Point PrimulP = urmeMov[0];
                Rectangle rect1 = new Rectangle(PrimulP.X, PrimulP.Y, 40, 60);
                e.Graphics.FillRectangle(brushyellow, rect1);
            }

            if (checkBox1.Checked)
            {
                Pen pen = new Pen(Color.Black, 2);
                for (int i = 0; i <= 800; i += 40)
                {
                    Point p1 = new Point(i, 0);
                    Point p2 = new Point(i, 600);
                    g.DrawLine(pen, p1, p2);
                }
                for (int i = 0; i <= 600; i += 60)
                {
                    Point p1 = new Point(0, i);
                    Point p2 = new Point(800, i);
                    g.DrawLine(pen, p1, p2);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
            timer1.Stop();

            var filePath = string.Empty;

            openFileDialog1.InitialDirectory = path;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            k = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                var fileStream = openFileDialog1.OpenFile();
                StreamReader rdr = new StreamReader(filePath);
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    string[] c = line.Split(' ');
                    if (c[0].ToString() == "Robot")
                    {
                        robot.Image = Image.FromFile(path + "Robot\\Robot.png");
                        robot.SizeMode = PictureBoxSizeMode.StretchImage;
                        robot.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        locatieInitiala = robot.Location;
                        locatieInitiala2 = robot.Location;

                        robot.Size = new Size(40, 60);
                        robot.Parent = pictureBox1;
                        robot.Tag = "robot";
                        robot.Name = "picImagine" + k;
                        robot.BackColor = Color.Transparent;
                        pictureBox1.Controls.Add(robot);

                    }
                    else if (c[0].ToString() == "Meduza1")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "Meduze\\Meduza1.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Size = new Size(40, 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Parent = pictureBox1;
                        Meduze.Add(pic);
                        pictureBox1.Controls.Add(pic);
                    }
                    else if (c[0].ToString() == "Meduza2")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "Meduze\\Meduza2.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.BackColor = Color.Transparent;
                        pic.Name = "picImagine" + k;
                        pic.Size = new Size(40, 60);
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Meduze.Add(pic);

                    }
                    else if (c[0].ToString() == "Meduza3")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "Meduze\\Meduza3.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Size = new Size(40, 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Meduze.Add(pic);
                    }
                    else if (c[0].ToString() == "Meduza4")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "Meduze\\Meduza4.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Size = new Size(40, 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Meduze.Add(pic);

                    }
                    else if (c[0].ToString() == "Hartie")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "MaterialeReciclabile\\Hartie.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Size = new Size(40, 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Hartie.Add(pic);

                    }
                    else if (c[0].ToString() == "Plastic")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "MaterialeReciclabile\\Plastic.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Size = new Size(40, 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Plastic.Add(pic);

                    }
                    else if (c[0].ToString() == "Sticla")
                    {
                        PictureBox pic = new PictureBox();
                        pic.Image = Image.FromFile(path + "MaterialeReciclabile\\Sticla.png");
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.Location = new Point(Int32.Parse(c[1]) * 40 - 40, Int32.Parse(c[2]) * 60 - 60);
                        pic.Name = "picImagine" + k;
                        pic.BackColor = Color.Transparent;
                        pic.Size = new Size(40, 60);
                        pic.Parent = pictureBox1;
                        pictureBox1.Controls.Add(pic);
                        Sticle.Add(pic);

                    }
                    k++;
                    controaleadaugate = true;
                    pictureBox1.DrawToBitmap(imagine1, new Rectangle(0, 0, 800, 600));

                }

            }
        }
        bool controaleadaugate = false;
        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Controls.Clear();
            controaleadaugate = false;
        }
        private void picDreaptaJosDesen(Point p)
        {
            PictureBox pic = new PictureBox();
            pic.Location = p;
            pic.Name = "picDreaptaJos";
            pic.BackColor = Color.Transparent;
            pic.Size = new Size(40, 60);
            pic.Paint += picDreaptaJos_Paint;
            pic.Parent = pictureBox2;
            pic.Click += deflector_click;
            pictureBox2.Controls.Add(pic);
            pic.Invalidate();
        }
        private void picDreaptaSusDesen(Point p)
        {
            PictureBox pic = new PictureBox();
            pic.Location = p;
            pic.Name = "picDreaptaSus";
            pic.BackColor = Color.Transparent;
            pic.Size = new Size(40, 60);
            pic.Click += deflector_click;
            pic.Paint += picDreaptaSus_Paint;
            pic.Parent = pictureBox2;
            pictureBox2.Controls.Add(pic);
            pic.Invalidate();
        }
        private void picStangaJosDesen(Point p)
        {
            PictureBox pic = new PictureBox();
            pic.Location = p;
            pic.Name = "picStangaJos";
            pic.BackColor = Color.Transparent;
            pic.Click += deflector_click;
            pic.Size = new Size(40, 60);
            pic.Paint += picStangaJos_Paint;
            pic.Parent = pictureBox2;
            pictureBox2.Controls.Add(pic);
            pic.Invalidate();
        }
        private void picStangaSusDesen(Point p)
        {
            PictureBox pic = new PictureBox();
            pic.Location = p;
            pic.Name = "picStangaSus";
            pic.BackColor = Color.Transparent;
            pic.Size = new Size(40, 60);
            pic.Click += deflector_click;
            pic.Paint += picStangaSus_Paint;
            pic.Parent = pictureBox2;
            pictureBox2.Controls.Add(pic);
            pic.Invalidate();
        }
        private void picStangaSus_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.White, 10);
            Point p1 = new Point(0, 0);
            Point p2 = new Point(40, 0);
            Point p3 = new Point(0, 60);
            Point[] points = { p1, p2, p3 };
            g.FillPolygon(brush, points);
        }
        private void picStangaJos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.White, 10);
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 60);
            Point p3 = new Point(40, 60);
            Point[] points = { p1, p2, p3 };
            g.FillPolygon(brush, points);
        }
        private void picDreaptaSus_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.White, 10);
            Point p1 = new Point(40, 0);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(40, 60);
            Point[] points = { p1, p2, p3 };
            g.FillPolygon(brush, points);
        }
        private void picDreaptaJos_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.White, 10);
            Point p1 = new Point(40, 60);
            Point p2 = new Point(0, 60);
            Point p3 = new Point(40, 0);
            Point[] points = { p1, p2, p3 };
            g.FillPolygon(brush, points);
        }
        bool selectatDirectie = false;
        private void button4_Click(object sender, EventArgs e)
        {

            if (button4.Text == "Start")
            {
                if (selectatDirectie == false && controaleadaugate == true)
                {
                    label5.Visible = true;
                    button8.Visible = true;
                    button9.Visible = true;
                    button10.Visible = true;
                    button11.Visible = true;
                    selectatDirectie = true;
                    button4.Text = "Stop";

                }

                if (controaleadaugate == false)
                {
                    MessageBox.Show("selectati o harta mai intai");
                    return;
                }
                if (controaleadaugate == true)
                {
                    timer1.Start();
                    button4.Text = "Stop";
                }
            }
            else
            {
                timer1.Stop();
                button4.Text = "Start";

            }

        }
        private void toatefalse()
        {
            label5.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            toatefalse();
            timer1.Start();
            directie = "DREAPTA";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            toatefalse();
            timer1.Start();
            directie = "JOS";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            toatefalse();
            timer1.Start();
            directie = "STANGA";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            toatefalse();
            timer1.Start();
            directie = "SUS";
        }
        private void drawMov(Point p)
        {
            urmeMov.Add(p);
            pictureBox1.Invalidate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            drawMov(robot.Location);

            if (directie == "SUS") robot.Top -= 60;
            else if (directie == "JOS") robot.Top += 60;
            else if (directie == "STANGA") robot.Left -= 40;
            else if (directie == "DREAPTA") robot.Left += 40;

            Point curLoc = robot.Location;


            if (curLoc.Y < 0) { directie = "JOS"; robot.Top = 0; return; }
            if (curLoc.Y >= 600) { directie = "SUS"; robot.Top = 540; return; }
            if (curLoc.X < 0) { directie = "DREAPTA"; robot.Left = 0; return; }
            if (curLoc.X >= 800) { directie = "STANGA"; robot.Left = 760; return; }

            if (directie == "SUS")
            {
                if (deflectorStangaSus.Contains(curLoc)) directie = "DREAPTA";
                else if (deflectorDreaptaSus.Contains(curLoc)) directie = "STANGA";
                else if (deflectorStangaJos.Contains(curLoc) || deflectorDreaptaJos.Contains(curLoc)) directie = "JOS";
            }
            else if (directie == "JOS")
            {
                if (deflectorStangaJos.Contains(curLoc)) directie = "DREAPTA";
                else if (deflectorDreaptaJos.Contains(curLoc)) directie = "STANGA";
                else if (deflectorStangaSus.Contains(curLoc) || deflectorDreaptaSus.Contains(curLoc)) directie = "SUS";
            }
            else if (directie == "STANGA")
            {
                if (deflectorStangaSus.Contains(curLoc)) directie = "JOS";
                else if (deflectorStangaJos.Contains(curLoc)) directie = "SUS";
                else if (deflectorDreaptaSus.Contains(curLoc) || deflectorDreaptaJos.Contains(curLoc)) directie = "DREAPTA";
            }
            else if (directie == "DREAPTA")
            {
                if (deflectorDreaptaSus.Contains(curLoc)) directie = "JOS";
                else if (deflectorDreaptaJos.Contains(curLoc)) directie = "SUS";
                else if (deflectorStangaSus.Contains(curLoc) || deflectorStangaJos.Contains(curLoc)) directie = "STANGA";
            }

            int sumaTotalaGunoaie = Hartie.Count + Sticle.Count + Plastic.Count;

            if (hartie + sticle + plastic < sumaTotalaGunoaie / 3)
            {
                pictureBox1.DrawToBitmap(imagine2, new Rectangle(0, 0, 800, 600));
            }
            else if (hartie + sticle + plastic < (2 * sumaTotalaGunoaie) / 3)
            {
                pictureBox1.DrawToBitmap(imagine3, new Rectangle(0, 0, 800, 600));

            }
            else if (hartie + sticle + plastic < sumaTotalaGunoaie)
            {
                pictureBox1.DrawToBitmap(imagine4, new Rectangle(0, 0, 800, 600));

            }

            foreach (PictureBox pic in Hartie)
            {
                if (robot.Bounds.IntersectsWith(pic.Bounds))
                {
                    pic.Visible = false;
                    hartie++;
                    label2.Text = "Hartie: " + hartie;

                }
            }
            foreach (PictureBox pic in Sticle)
            {
                if (robot.Bounds.IntersectsWith(pic.Bounds))
                {
                    pic.Visible = false;
                    sticle++;
                    label3.Text = "Sticle: " + sticle;
                }
            }
            foreach (PictureBox pic in Plastic)
            {
                if (robot.Bounds.IntersectsWith(pic.Bounds))
                {
                    pic.Visible = false;
                    plastic++;
                    label4.Text = "Plastic: " + plastic;

                }
            }
            foreach (PictureBox pic in Meduze)
            {
                if (robot.Bounds.IntersectsWith(pic.Bounds))
                {
                    pic.Visible = false;
                    button4.Text = "Start";
                    timer1.Stop();
                    MessageBox.Show("Ati trecut peste o meduza");
                }
            }
            int sticleCount = (int)Sticle.Count;
            int plasticCount = (int)Plastic.Count;  
            int hartieCount = (int)Hartie.Count;    

            if (sticle == sticleCount && plastic == plasticCount && hartie == hartieCount)
            {
                timer1.Stop();
                pictureBox1.DrawToBitmap(imagine5, new Rectangle(0, 0, 800, 600));

                MessageBox.Show("Ati adunat toate materialele daunatoare mediului. Felicitari");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(numeBack == "Harta1.txt")
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(4 * 40, 3 * 60);
                pic.Name = "picDreaptaSus";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picDreaptaSus_Paint;
                pic.Parent = pictureBox1;
                pictureBox1.Controls.Add(pic);
                pic.Invalidate();
                deflectorDreaptaSus.Add(new Point(4 * 40, 3 * 60));
            }else if(numeBack == "Harta2.txt")
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(15 * 40, 8 * 60);
                pic.Name = "picDreaptaJos";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picDreaptaJos_Paint;
                pic.Parent = pictureBox1;
                pic.Invalidate();
                deflectorDreaptaJos.Add(new Point(15 * 40, 8 * 60));
                PictureBox pic1 = new PictureBox();
                pic1.Location = new Point(17 * 40, 6 * 60);
                pic1.Name = "picDreaptaJos";
                pic1.BackColor = Color.Transparent;
                pic1.Size = new Size(40, 60);
                pic1.Paint += picDreaptaJos_Paint;
                pic1.Parent = pictureBox1;
                pic1.Invalidate();
                deflectorDreaptaJos.Add(new Point(17 * 40, 6 * 60));
                PictureBox pic2 = new PictureBox();
                pic2.Location = new Point(11 * 40, 7 * 60);
                pic2.Name = "picDreaptaJos";
                pic2.BackColor = Color.Transparent;
                pic2.Size = new Size(40, 60);
                pic2.Paint += picDreaptaJos_Paint;
                pic2.Parent = pictureBox1;
                pic2.Invalidate();
                deflectorDreaptaJos.Add(new Point(11 * 40, 7 * 60));

                PictureBox pic3 = new PictureBox();
                pic3.Location = new Point(17 * 40, 3 * 60);
                pic3.Name = "picDreaptaSus";
                pic3.BackColor = Color.Transparent;
                pic3.Size = new Size(40, 60);
                pic3.Paint += picDreaptaSus_Paint;
                pic3.Parent = pictureBox1;
                pic3.Invalidate();
                deflectorDreaptaSus.Add(new Point(17 * 40, 3 * 60));

                PictureBox pic4 = new PictureBox();
                pic4.Location = new Point(15 * 40, 6 * 60);
                pic4.Name = "picStangaSus";
                pic4.BackColor = Color.Transparent;
                pic4.Size = new Size(40, 60);
                pic4.Paint += picStangaSus_Paint;
                pic4.Parent = pictureBox1;
                pic4.Invalidate();
                deflectorStangaSus.Add(new Point(15 * 40, 6 * 60));
                PictureBox pic5 = new PictureBox();
                pic5.Location = new Point(11 * 40, 3 * 60);
                pic5.Name = "picStangaSus";
                pic5.BackColor = Color.Transparent;
                pic5.Size = new Size(40, 60);
                pic5.Paint += picStangaSus_Paint;
                pic5.Parent = pictureBox1;
                pic5.Invalidate();
                deflectorStangaSus.Add(new Point(11 * 40, 3 * 60));

                PictureBox pic6 = new PictureBox();
                pic6.Location = new Point(8 * 40, 7 * 60);
                pic6.Name = "picStangaJos";
                pic6.BackColor = Color.Transparent;
                pic6.Size = new Size(40, 60);
                pic6.Paint += picStangaJos_Paint;
                pic6.Parent = pictureBox1;
                pic6.Invalidate();
                deflectorStangaJos.Add(new Point(8 * 40, 7 * 60));

                pictureBox1.Controls.Add(pic);
                pictureBox1.Controls.Add(pic1);
                pictureBox1.Controls.Add(pic2);
                pictureBox1.Controls.Add(pic3);
                pictureBox1.Controls.Add(pic4);
                pictureBox1.Controls.Add(pic5);
                pictureBox1.Controls.Add(pic6);
                pictureBox1.Invalidate();
            }
        }

        private void InterferenteECO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                System.Environment.Exit(0);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {


            imagine1.Save("inceput.jpg", System.Drawing.Imaging.ImageFormat.Png);
            imagine2.Save("intermediar1.jpg", System.Drawing.Imaging.ImageFormat.Png);
            imagine3.Save("intermediar2.jpg", System.Drawing.Imaging.ImageFormat.Png);
            imagine4.Save("intermediar3.jpg", System.Drawing.Imaging.ImageFormat.Png);
            imagine5.Save("final.jpg", System.Drawing.Imaging.ImageFormat.Png);

            MessageBox.Show($"Imagine salvata!locatie: {path}");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            robot.Location = locatieInitiala;
            foreach (PictureBox pic in Meduze)
                pic.Visible = true;

            foreach (PictureBox pic in Plastic)
                pic.Visible = true;

            foreach (PictureBox pic in Sticle)
                pic.Visible = true;

            foreach (PictureBox pic in Hartie)
                pic.Visible = true;

            urmeMov.Clear();
            pictureBox1.Invalidate();
        }

        private void deflector_click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (pic.Name == "picDreaptaJos")
            {
                dreaptajos = true;
                dreaptasus = false;
                stangasus = false;
                stangajos = false;
            }
            else if (pic.Name == "picDreaptaSus")
            {
                dreaptasus = true;
                dreaptajos = false;
                stangasus = false;
                stangajos = false;
            }
            else if (pic.Name == "picStangaSus")
            {
                dreaptasus = false;
                dreaptajos = false;
                stangasus = true;
                stangajos = false;
            }
            else if (pic.Name == "picStangaJos")
            {
                dreaptasus = false;
                dreaptajos = false;
                stangasus = false;
                stangajos = true;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int adevaratulX = e.X / 40;
            int adevaratulY = e.Y / 60;
            if (dreaptajos)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(adevaratulX * 40, adevaratulY * 60);
                pic.Name = "picDreaptaJos";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picDreaptaJos_Paint;
                pic.Parent = pictureBox1;
                pictureBox1.Controls.Add(pic);
                pic.Invalidate();
                deflectorDreaptaJos.Add(new Point(adevaratulX * 40, adevaratulY * 60));
            }
            else if (dreaptasus)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(adevaratulX * 40, adevaratulY * 60);
                pic.Name = "picDreaptaSus";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picDreaptaSus_Paint;
                pic.Parent = pictureBox1;
                pictureBox1.Controls.Add(pic);
                pic.Invalidate();
                deflectorDreaptaSus.Add(new Point(adevaratulX * 40, adevaratulY * 60));
            }
            else if (stangajos)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(adevaratulX * 40, adevaratulY * 60);
                pic.Name = "picStangaJos";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picStangaJos_Paint;
                pic.Parent = pictureBox1;
                pictureBox1.Controls.Add(pic);
                pic.Invalidate();
                deflectorStangaJos.Add(new Point(adevaratulX * 40, adevaratulY * 60));
            }
            else if (stangasus)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(adevaratulX * 40, adevaratulY * 60);
                pic.Name = "picStangaSus";
                pic.BackColor = Color.Transparent;
                pic.Size = new Size(40, 60);
                pic.Paint += picStangaSus_Paint;
                pic.Parent = pictureBox1;
                pictureBox1.Controls.Add(pic);
                pic.Invalidate();
                deflectorStangaSus.Add(new Point(adevaratulX * 40, adevaratulY * 60));
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Controls.Clear();
            rotatie++;
            if (rotatie == 1)
            {
                picDreaptaJosDesen(new Point(50, 150));
            }
            else if (rotatie == 2)
            {

                picStangaJosDesen(new Point(50, 150));
            }
            else if (rotatie == 3)
            {

                picStangaSusDesen(new Point(50, 150));
            }
            else if (rotatie == 4)
            {
                picDreaptaSusDesen(new Point(50, 150));
                rotatie = 0;

            }
            pictureBox2.Invalidate();

        }
    }
}
