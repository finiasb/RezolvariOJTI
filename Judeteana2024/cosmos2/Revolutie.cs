using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics.Eventing.Reader;

namespace cosmos2
{
    public partial class Revolutie : Form
    {
        private System.Windows.Forms.Timer timer;
        private double alpha = 0;
        private double alphaMoon = 0;
        private double scale = 1.7;
        private int totalDays = 366;
        private double a = 149.59;
        private double e = 0.017;
        private double b;
        private int ok = 0;
        private int centerX = 400;
        private int centerY = 300;
        private int earthCenterOffsetX = 25;
        private int earthCenterOffsetY = 21;
        private int moonCenterOffsetX = 20;
        private int moonCenterOffsetY = 16;

        int day = 3;
        public Revolutie()
        {
            InitializeComponent();
            b = a * Math.Sqrt(1 - e * e);

        }

        private void PicFundal_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // TRAIECTORIA PĂMÂNTULUI 
            using (Pen penEarth = new Pen(Color.LightBlue, 2))
            {
                PointF[] earthTrajectory = new PointF[100];
                for (int i = 0; i < earthTrajectory.Length; i++)
                {
                    double angle = 2 * Math.PI * i / earthTrajectory.Length;
                    double x = centerX + (a * Math.Cos(angle) * scale);
                    double y = centerY + (b * Math.Sin(angle) * scale);
                    earthTrajectory[i] = new PointF((float)x, (float)y);
                }

                for (int i = 0; i < earthTrajectory.Length - 1; i++)
                {
                    g.DrawLine(penEarth, earthTrajectory[i], earthTrajectory[i + 1]);
                }
                g.DrawLine(penEarth, earthTrajectory[earthTrajectory.Length - 1], earthTrajectory[0]);
            }

            // TRAIECTORIA LUNII 
            using (Pen penMoon = new Pen(Color.LightGreen, 1))
            {
                double currentEarthX = centerX + (a * Math.Cos(alpha) * scale);
                double currentEarthY = centerY + (b * Math.Sin(alpha) * scale);

                PointF[] moonTrajectory = new PointF[50];
                double moonOrbitRadius = 50;

                for (int i = 0; i < moonTrajectory.Length; i++)
                {
                    double angle = 2 * Math.PI * i / moonTrajectory.Length;
                    double x = currentEarthX + (moonOrbitRadius * Math.Cos(angle));
                    double y = currentEarthY + (moonOrbitRadius * Math.Sin(angle));
                    moonTrajectory[i] = new PointF((float)x, (float)y);
                }

                for (int i = 0; i < moonTrajectory.Length - 1; i++)
                {
                    g.DrawLine(penMoon, moonTrajectory[i], moonTrajectory[i + 1]);
                }
                g.DrawLine(penMoon, moonTrajectory[moonTrajectory.Length - 1], moonTrajectory[0]);
            }

            // LINIA DINTRE PĂMÂNT ȘI SOARE
            using (Pen penLine = new Pen(Color.Yellow, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot })
            {
                double earthX = centerX + (a * Math.Cos(alpha) * scale);
                double earthY = centerY + (b * Math.Sin(alpha) * scale);

                g.DrawLine(penLine, (float)earthX, (float)earthY, (float)centerX, (float)centerY);
            }
        }

        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            alpha -= ((Math.PI * 2) / 366);
            alphaMoon += 0.24;
            day++;
            // Poziția Pământului
            double earthCenterX = centerX + (a * Math.Cos(alpha) * scale);
            double earthCenterY = centerY + (b * Math.Sin(alpha) * scale);

            // Poziția Lunii 
            double moonOrbitRadius = 50;
            double moonCenterX = earthCenterX + (moonOrbitRadius * Math.Cos(alphaMoon));
            double moonCenterY = earthCenterY + (moonOrbitRadius * Math.Sin(alphaMoon));

            picPamant.Location = new Point((int)(earthCenterX - earthCenterOffsetX),
                                           (int)(earthCenterY - earthCenterOffsetY));
            picLuna.Location = new Point((int)(moonCenterX - moonCenterOffsetX),
                                         (int)(moonCenterY - moonCenterOffsetY));

            double x = getDistance(alpha);

            x = x * 100000000;

            lblDistanta.Text = "Distanța:    " + (long)x + " Km";

            int month = 1;
            int dayInMonth = day;

            if (day == 366) day = 1;
            if (day > 31) { month = 2; dayInMonth = day - 31; }
            if (day > 60) { month = 3; dayInMonth = day - 60; }
            if (day > 91) { month = 4; dayInMonth = day - 91; }
            if (day > 121) { month = 5; dayInMonth = day - 121; }
            if (day > 152) { month = 6; dayInMonth = day - 152; }
            if (day > 182) { month = 7; dayInMonth = day - 182; }
            if (day > 213) { month = 8; dayInMonth = day - 213; }
            if (day > 244) { month = 9; dayInMonth = day - 244; }
            if (day > 274) { month = 10; dayInMonth = day - 274; }
            if (day > 305) { month = 11; dayInMonth = day - 305; }
            if (day > 335) { month = 12; dayInMonth = day - 335; }

            lblData.Text = $"Data:            {dayInMonth:D2}.{month:D2}.2024";

            string anotimp = "";

            if (day >= 78 && day < 170)
                anotimp = "Primăvară";
            else if (day >= 170 && day < 264)
                anotimp = "Vară";
            else if (day >= 264 && day < 356)
                anotimp = "Toamnă";
            else
                anotimp = "Iarnă";

            lblAnotimp.Text = $"Anotimpul: {anotimp}";

            picFundal.Invalidate(); // redeseneaza
        }

        private void Revolutie_Shown(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            // Poziții inițiale
            double earthCenterX = centerX + (a * Math.Cos(0) * scale);
            double earthCenterY = centerY + (b * Math.Sin(0) * scale);
            double moonOrbitRadius = 50;
            double moonCenterX = earthCenterX + (moonOrbitRadius * Math.Cos(0));
            double moonCenterY = earthCenterY + (moonOrbitRadius * Math.Sin(0));

            // BACK
            picFundal.Image = Image.FromFile("back.png");
            this.Controls.Add(picFundal);
            picFundal.Paint += PicFundal_Paint;

            // LUNA
            picLuna.Image = Image.FromFile("luna.png");
            picLuna.Size = new Size(40, 32);
            picLuna.SizeMode = PictureBoxSizeMode.StretchImage;
            picLuna.BackColor = Color.Transparent;
            picLuna.Parent = picFundal;
            picLuna.Location = new Point((int)(moonCenterX - moonCenterOffsetX),
                                         (int)(moonCenterY - moonCenterOffsetY));

            // PAMANT
            picPamant.Image = Image.FromFile("Pamant.png");
            picPamant.Size = new Size(50, 42);
            picPamant.SizeMode = PictureBoxSizeMode.StretchImage;
            picPamant.BackColor = Color.Transparent;
            picPamant.Parent = picFundal;
            picPamant.Location = new Point((int)(earthCenterX - earthCenterOffsetX),
                                           (int)(earthCenterY - earthCenterOffsetY));

            // SOARE
            picSoare.Image = Image.FromFile("Adobe Express - file.png");
            picSoare.Size = new Size(100, 70);
            picSoare.SizeMode = PictureBoxSizeMode.StretchImage;
            picSoare.BackColor = Color.Transparent;
            picSoare.Parent = picFundal;
            picSoare.Location = new Point(350, 275);
        }

        private double getDistance(double alpha)
        {
            return (a * (1 - e * e)) / (1 + e * Math.Cos(alpha));
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (ok == 0)
            {
                InitTimer();
                ok = 1;
            }
            startBtn.Enabled = false;
            StopBtn.Enabled = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (ok == 1)
            {
                timer.Stop();
                ok = 0;
            }
            StopBtn.Enabled = false;
            startBtn.Enabled = true;

            StreamWriter sw = new StreamWriter("Detalii.txt", true);

            sw.WriteLine(lblData.Text);
            sw.WriteLine(lblDistanta.Text);
            sw.WriteLine(lblAnotimp.Text);
            sw.WriteLine();
            sw.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            MessageBox.Show("\"C:\\Users\\Fineas\\source\\repos\\cosmos2\\cosmos2\\bin\\Debug\\net8.0-windows\\Detalii.txt\"");
            this.Hide();
            AlegeOptiunea alege = new AlegeOptiunea();
            alege.Show();
        }

        private void lblAnotimp_Click(object sender, EventArgs e)
        {

        }
        private void lblData_Click(object sender, EventArgs e)
        {

        }
        private void lblDistanta_Click(object sender, EventArgs e)
        {
            // aici pui ce să se întâmple când se apasă pe Distanță
        }
    }
}