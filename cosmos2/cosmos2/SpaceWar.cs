using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
namespace cosmos2
{
    public partial class SpaceWar : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private IWavePlayer waveOutDevice1;
        private AudioFileReader audioFileReader1;
        private IWavePlayer waveOutDevice2;
        private AudioFileReader audioFileReader2;
        private IWavePlayer waveOutDevice3;
        private AudioFileReader audioFileReader3;
        private IWavePlayer waveOutDevice4;
        private AudioFileReader audioFileReader4;

        private System.Windows.Forms.Timer timerMesaj;
        private System.Windows.Forms.Timer timerMiscare;
        private System.Windows.Forms.Timer timerInamici;
        private System.Windows.Forms.Timer timerAsteroizi;
        private System.Windows.Forms.Timer timerViata;
        private int scor = 0;
        private int vieti = 3;
        private int NavaX = 50, NavaY = 250;
        Random rnd = new Random();
        private List<PictureBox> inamici = new List<PictureBox>();
        private List<PictureBox> asteroizi = new List<PictureBox>();
        private List<PictureBox> rachetaNava = new List<PictureBox>();
        private List<PictureBox> viata = new List<PictureBox>();
        public SpaceWar()
        {
            InitializeComponent();
            MesajJoc.Visible = false;
            btnStart.TabStop = false;
            btnPauza.TabStop = false;
            btnStop.TabStop = false;


            timerMiscare = new System.Windows.Forms.Timer();
            timerMiscare.Interval = 30;
            timerMiscare.Tick += timerMiscare_Tick;

            timerInamici = new System.Windows.Forms.Timer();
            timerInamici.Interval = 2000;
            timerInamici.Tick += TimerInamici_Tick;

            timerAsteroizi = new System.Windows.Forms.Timer();
            timerAsteroizi.Interval = 1000;
            timerAsteroizi.Tick += TimerAsteroizi_Tick;

            timerViata = new System.Windows.Forms.Timer();
            timerViata.Interval = 7000;
            timerViata.Tick += TimerViata_Tick;
        }

        private void TimerAsteroizi_Tick(object sender, EventArgs e)
        {
            PictureBox picAsteroid = new PictureBox();
            picAsteroid.Image = Image.FromFile("asteroid.png");
            picAsteroid.Size = new Size(10, 10);
            picAsteroid.Location = new Point(745, rnd.Next(40, 510));
            picAsteroid.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(picAsteroid);
            asteroizi.Add(picAsteroid);
        }

        private void TimerViata_Tick(object sender, EventArgs e)
        {
            PictureBox picViata = new PictureBox();
            picViata.Image = Image.FromFile("viata.gif");
            picViata.Size = new Size(50, 50);
            picViata.Location = new Point(745, rnd.Next(40, 510));
            picViata.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(picViata);
            viata.Add(picViata);
        }

        private void TimerInamici_Tick(object sender, EventArgs e)
        {
            PictureBox picInamic = new PictureBox();
            picInamic.Image = Image.FromFile("inamic.gif");
            picInamic.Size = new Size(55, 40);
            picInamic.Location = new Point(745, rnd.Next(40, 510));
            picInamic.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(picInamic);
            inamici.Add(picInamic);
        }

        private void timerMiscare_Tick(object sender, EventArgs e)
        {
            if (IsKeyPressed(Keys.W) && NavaY != 0)
            {
                picNava.Image = Image.FromFile("NavaUp.png");
                NavaY -= 10;
                picNava.Location = new Point(NavaX, NavaY);
            }else if (IsKeyPressed(Keys.S) && NavaY != 500)
            {
                picNava.Image = Image.FromFile("NavaDown.png");
                NavaY += 10;
                picNava.Location = new Point(NavaX, NavaY);
            }
            else if (IsKeyPressed(Keys.D) && NavaX != 740)
            {
                picNava.Image = Image.FromFile("NavaMove.png");
                NavaX += 10;
                picNava.Location = new Point(NavaX, NavaY);
            }
            else if (IsKeyPressed(Keys.A) && NavaX != 0)
            {
                picNava.Image = Image.FromFile("NavaMove.png");
                NavaX -= 10;
                picNava.Location = new Point(NavaX, NavaY);
            }
            else if (IsKeyPressed(Keys.Space))
            {
                PlayFire();
                picNava.Image = Image.FromFile("NavaFire.png");
                PictureBox picRachetaNava = new PictureBox();
                picRachetaNava.Image = Image.FromFile("rachetaNava.png");
                picRachetaNava.Size = new Size(10, 10);
                picRachetaNava.Location = new Point(NavaX, NavaY + 22);
                picRachetaNava.SizeMode = PictureBoxSizeMode.StretchImage;
                panel1.Controls.Add(picRachetaNava);
                rachetaNava.Add(picRachetaNava);
            }
            else
            {
                picNava.Image = Image.FromFile("NavaStop.png");
            }

            for (int i = inamici.Count - 1; i >= 0; i--)
            {
                PictureBox inamic = inamici[i];
                inamic.Left -= 5;

                if(inamic.Right < 55)
                {
                    panel1.Controls.Remove(inamic);
                    inamici.RemoveAt(i);
                }
            }

            for (int i = asteroizi.Count - 1; i >= 0; i--)
            {
                PictureBox asteroid = asteroizi[i];
                asteroid.Left -= 5;

                if (asteroid.Right < 10)
                {
                    panel1.Controls.Remove(asteroid);
                    asteroizi.RemoveAt(i);
                }
            }

            for (int i = viata.Count - 1; i >= 0; i--)
            {
                PictureBox viataPic = viata[i];
                viataPic.Left -= 10;

                if (viataPic.Right < 10)
                {
                    panel1.Controls.Remove(viataPic);
                    viata.RemoveAt(i);
                }
            }

            for (int i = rachetaNava.Count - 1; i >= 0; i--)
            {
                PictureBox picRachetaNava = rachetaNava[i];
                picRachetaNava.Left += 7;

                if (picRachetaNava.Right > panel1.Width)
                {
                    panel1.Controls.Remove(picRachetaNava);
                    rachetaNava.RemoveAt(i);
                }
            }
            for (int i = viata.Count - 1; i >= 0; i--)
            {
                PictureBox viataPic = viata[i];
                Rectangle rectR = new Rectangle(viataPic.Location, viataPic.Size);
                Rectangle rectI = new Rectangle(picNava.Location, picNava.Size);

                if (rectI.IntersectsWith(rectR))
                {
                    PlayBonus();
                    panel1.Controls.Remove(viataPic);
                    viata.RemoveAt(i);
                    vieti++;
                }
            }

            for (int i = inamici.Count - 1; i >= 0; i--)
            {
                PictureBox inamic = inamici[i];
                Rectangle rectR = new Rectangle(inamic.Location, inamic.Size);
                Rectangle rectI = new Rectangle(picNava.Location, picNava.Size);

                if (rectI.IntersectsWith(rectR))
                {
                    PlayExplozie();
                    panel1.Controls.Remove(inamic);
                    inamici.RemoveAt(i);
                    vieti--;
                }
            }

            for (int i = rachetaNava.Count - 1; i >= 0; i--)
            {
                PictureBox racheta = rachetaNava[i];
                Rectangle rectR = new Rectangle(racheta.Location, racheta.Size);

                for (int j = inamici.Count - 1; j >= 0; j--)
                {
                    PictureBox inamic = inamici[j];
                    Rectangle rectI = new Rectangle(inamic.Location, inamic.Size);

                    rectI.Inflate(-3, -3);

                    if (rectR.IntersectsWith(rectI))
                    {
                        PlayDistrugere();
                        panel1.Controls.Remove(inamic);
                        inamici.RemoveAt(j);
                        panel1.Controls.Remove(racheta);
                        rachetaNava.RemoveAt(i);
                        scor++;
                        break;
                    }
                }
            }

            label2.Text = "Viata       " + vieti; 
            label1.Text = "Scor:       " + scor;

            if (vieti == 0 || scor == 10)
            {
                if (vieti == 0)
                {
                    MesajJoc.Text = "Nava a fost distrusa";
                }
                else if (scor == 10)
                {
                    MesajJoc.Text = "Nava a invins";
                }

                STOP();
                StartAnimatieMesaj();
                CurataObiecte();
                vieti = 3;
                scor = 0;
                btnPauza.Enabled = false;
                btnStop.Enabled = false;
                btnStart.Enabled = true;
            }
        }
        private void StartAnimatieMesaj()
        {
            MesajJoc.Location = new Point((panel1.Width - MesajJoc.Width) / 2,  -MesajJoc.Height);
            MesajJoc.Visible = true;
            timerMesaj = new System.Windows.Forms.Timer();
            timerMesaj.Interval = 20;
            timerMesaj.Tick += (s, e) =>
            {
                var y = MesajJoc.Location.Y + 5; 
                if (y < (panel1.Height - MesajJoc.Height) / 2)
                {
                    MesajJoc.Location = new Point(MesajJoc.Location.X, y);
                }
                else
                {
                    timerMesaj.Stop();
                }
            };
            timerMesaj.Start();
        }

        public void PlaySound()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }

            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader("sunetFundal.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }
        public void StopSound()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }
        public void PlayFire()
        {
            if (waveOutDevice1 != null)
            {
                waveOutDevice1.Stop();
                waveOutDevice1.Dispose();
                waveOutDevice1 = null;
            }

            waveOutDevice1 = new WaveOut();
            audioFileReader1 = new AudioFileReader("sunetFire.mp3");
            waveOutDevice1.Init(audioFileReader1);
            waveOutDevice1.Play();
        }
        public void PlayBonus()
        {
            if (waveOutDevice2 != null)
            {
                waveOutDevice2.Stop();
                waveOutDevice2.Dispose();
                waveOutDevice2 = null;
            }

            waveOutDevice2 = new WaveOut();
            audioFileReader2 = new AudioFileReader("sunetBonus.mp3");
            waveOutDevice2.Init(audioFileReader2);
            waveOutDevice2.Play();
        }
        public void PlayDistrugere()
        {
            if (waveOutDevice3 != null)
            {
                waveOutDevice3.Stop();
                waveOutDevice3.Dispose();
                waveOutDevice3 = null;
            }

            waveOutDevice3 = new WaveOut();
            audioFileReader3 = new AudioFileReader("sunetDistrugere.mp3");
            waveOutDevice3.Init(audioFileReader3);
            waveOutDevice3.Play();
        }
        public void PlayExplozie()
        {
            if (waveOutDevice4 != null)
            {
                waveOutDevice4.Stop();
                waveOutDevice4.Dispose();
                waveOutDevice4 = null;
            }

            waveOutDevice4 = new WaveOut();
            audioFileReader4 = new AudioFileReader("sunetExplozie.mp3");
            waveOutDevice4.Init(audioFileReader4);
            waveOutDevice4.Play();
        }
        private bool IsKeyPressed(Keys key)
        {
            return GetAsyncKeyState((int)key) != 0;
        }
        private void START()
        {
            PlaySound();
            timerMiscare.Start();
            timerInamici.Start();
            timerAsteroizi.Start();
            timerViata.Start();
        }

        private void STOP()
        {
            timerMiscare.Stop();
            timerInamici.Stop();
            timerAsteroizi.Stop();
            timerViata.Stop();
            StopSound();
        }
        private void CurataObiecte()
        {
            foreach (var inamic in inamici)
                panel1.Controls.Remove(inamic);
            inamici.Clear();

            foreach (var asteroid in asteroizi)
                panel1.Controls.Remove(asteroid);
            asteroizi.Clear();

            foreach (var racheta in rachetaNava)
                panel1.Controls.Remove(racheta);
            rachetaNava.Clear();

            foreach (var v in viata)
                panel1.Controls.Remove(v);
            viata.Clear();

            NavaX = 50;
            NavaY = 250;
            picNava.Location = new Point(NavaX, NavaY);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timerMiscare.Enabled)
            {
                StopSound();
                START();
                MesajJoc.Visible = false;
            }

            btnStart.Enabled = false;
            btnPauza.Enabled = true;
            btnStop.Enabled = true;
            btnStart.TabStop = false;
            btnPauza.TabStop = false;
            btnStop.TabStop = false;
        }
       
        private void btnStop_Click(object sender, EventArgs e)
        {
            STOP();
            string message = "Opriți jocul?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {

                System.Windows.Forms.Application.ExitThread();
            }
            else
            {
                START();

                btnStop.TabStop = false;
                panel1.Focus();
            }
            
        }
        private void btnPauza_Click(object sender, EventArgs e)
        {
            if (timerMiscare.Enabled)
            {
                STOP();
                MesajJoc.Location = new Point(panel1.Width / 2 - MesajJoc.Width / 2, -MesajJoc.Height);
                MesajJoc.Text = "Pauza joc";
                MesajJoc.Visible = true;
                StartAnimatieMesaj();
                btnStart.Enabled = true;
                btnPauza.Enabled = false;
            }
        }  
    }
}
