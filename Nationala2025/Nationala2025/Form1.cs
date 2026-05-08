using Emgu.CV.Structure;
using Emgu.CV;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Face;
using System.Security.Cryptography;
namespace Nationala2025
{
    public partial class Form1 : Form
    {
        string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Radar.mdf;Integrated Security=True;Connect Timeout=30";
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        private Capture capture;
        private CascadeClassifier cascadeClassifier;
        private EigenFaceRecognizer faceRecognizer;
        private Image<Bgr, byte> currentFrame;
        List<string> numeControlori = new List<string>();
        private Image<Gray, byte> faceImageThumb;
        public Form1()
        {
            InitializeComponent();
            sterge();
            incarcaZboruri();
            incarcaAeroporturi();
        }

        void sterge()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("truncate table zboruri", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("truncate table aeroporturi", con);
                cmd2.ExecuteNonQuery();
            }
        }
        void incarcaZboruri()
        {
            StreamReader rdr = new StreamReader(path + "Zboruri.txt");
            string line;
            rdr.ReadLine();
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into Zboruri(CodDecolare, CodAterizare, TimpStart, Durata, AzimutInitial, AzimutFinal, Descriere) values(@CodDecolare, @CodAterizare, @TimpStart, @Durata, @AzimutInitial, @AzimutFinal, @Descriere)", con);
                    cmd.Parameters.AddWithValue("@CodDecolare", c[0]);
                    cmd.Parameters.AddWithValue("@CodAterizare", c[1]);
                    cmd.Parameters.AddWithValue("@TimpStart", DateTime.Parse(c[2]));
                    cmd.Parameters.AddWithValue("@Durata", int.Parse(c[3]));
                    cmd.Parameters.AddWithValue("@AzimutInitial", c[4]);
                    cmd.Parameters.AddWithValue("@AzimutFinal", c[5]);
                    cmd.Parameters.AddWithValue("@Descriere", c[6]);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        void incarcaAeroporturi()
        {
            StreamReader rdr = new StreamReader(path + "Aeroporturi.txt");
            string line;
            rdr.ReadLine();
            while ((line = rdr.ReadLine()) != null)
            {
                string[] c = line.Split(';');
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into Aeroporturi(CodAeroport, Oras, Pasageri) values(@CodAeroport, @Oras, @Pasageri)", con);
                    cmd.Parameters.AddWithValue("@CodAeroport", c[0]);
                    cmd.Parameters.AddWithValue("@Oras", c[1]);
                    cmd.Parameters.AddWithValue("@Pasageri", int.Parse(c[2]));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void btnIntra_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }
        private void btnGestioneaza_Click(object sender, EventArgs e)
        {
            if (txtEmailAdmin.Text == "onti@csharp.ro" && txtParolaAdmin.Text == "ONTI2025")
            {
                btnIntra.Enabled = true;
                btnSalvare.Enabled = true;
                txtNumeControlor.Text = "Administrator";
            }
            else
            {
                MessageBox.Show("Eroare");
            }
        }

        public bool learn()
        {
            string folderImagini = Path.Combine(Application.StartupPath, "Imagini");

            if (!Directory.Exists(folderImagini))
                Directory.CreateDirectory(folderImagini);

            string[] files = Directory.GetFiles(folderImagini, "*.jpg");

            if (files.Length == 0) return false;

            var faceImages = new List<Image<Gray, byte>>();
            var faceLabels = new List<int>();
            numeControlori.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                    var img = new Image<Gray, byte>(files[i]).Resize(64, 64, Emgu.CV.CvEnum.Inter.Cubic);
                    faceImages.Add(img);
                    faceLabels.Add(i);
                    numeControlori.Add(Path.GetFileNameWithoutExtension(files[i]));
            }

            if (faceImages.Count > 0)
            {
                faceRecognizer.Train(faceImages.ToArray(), faceLabels.ToArray());
                return true;
            }
            return false;
        }

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml");
                capture = new Capture();
                faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
                learn();
                
                timer1.Start();
            }
        }
        private void btnSalvare_Click(object sender, EventArgs e)
        {
            if (faceImageThumb != null && !string.IsNullOrWhiteSpace(txtNumeControlor.Text))
            {
                try
                {
                    string folderPath = Path.Combine(Application.StartupPath, "Imagini");
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string pathSalvare = Path.Combine(folderPath, txtNumeControlor.Text + ".jpg");
                    faceImageThumb.Save(pathSalvare);

                    learn(); 
                    MessageBox.Show("Salvat");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare");
                }
            }
            else
            {
                MessageBox.Show("Eroare"); 
            }
        }

        private void btnDetecteaza_Click(object sender, EventArgs e)
        {
            if (currentFrame == null) return;

            var grayFrame = currentFrame.Convert<Gray, byte>();
            var faces = cascadeClassifier.DetectMultiScale(grayFrame, 1.3, 6, Size.Empty, Size.Empty);

            if (faces.Length > 0)
            {
                var face = faces[0]; 

                currentFrame.Draw(face, new Bgr(Color.Red), 2);
                pbCamera.Image = currentFrame.ToBitmap();

                faceImageThumb = currentFrame.Copy(face).Convert<Gray, byte>().Resize(64, 64, Emgu.CV.CvEnum.Inter.Cubic);
                pbCaptura.Image = faceImageThumb.ToBitmap();

                if (lblUtilizator.Text != "Administrator")
                {
                    if (numeControlori != null && numeControlori.Count > 0)
                    {
                        int predictedId = facePredict(grayFrame);
                        if (predictedId >= 0 && predictedId < numeControlori.Count)
                        {
                            lblUtilizator.Text = numeControlori[predictedId];
                            btnIntra.Enabled = true;
                        }
                        else
                        {
                            lblUtilizator.Text = "necunoscut";
                            btnIntra.Enabled = false;
                        }
                    }
                    else
                    {
                        lblUtilizator.Text = "fără date";
                        btnIntra.Enabled = false;
                    }
                }
            }
        }

        private int facePredict(Image<Gray, byte> frame)
        {
            try
            {
                var result = faceRecognizer.Predict(frame.Resize(64, 64, Emgu.CV.CvEnum.Inter.Cubic));
                if (result.Label != -1 && result.Distance < 4000)
                    return result.Label;
            }
            catch (Exception ex)
            {
                return -1;
            }
            return -1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var imageFrame = capture.QueryFrame().ToImage<Bgr, byte>())
            {
                if (imageFrame != null)
                {
                    currentFrame = imageFrame.Clone();
                    pbCamera.Image = currentFrame.ToBitmap();
                }
            }
        }
    }
}
