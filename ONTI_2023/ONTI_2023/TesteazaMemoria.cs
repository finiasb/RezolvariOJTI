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

namespace ONTI_2023
{
    public partial class TesteazaMemoria : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";
        private string apasatSauNu = "", _email, ultimaApasata = "", numeLabel20;
        private int nrPanouri, n = 3, idLabel, idPoza, timp = 100;
        private List<int> listaRandom = new List<int>();
        private List<int> listaRandomCp = new List<int>();
        Random Random = new Random();
        public TesteazaMemoria(string email)     
        {
            InitializeComponent();
            _email = email;
            enabledTot();
            
        }
        private async void NivelCompletat()
        {
            if(timp > 0)
            {
                n++;
                if (n == 7)
                {
                    Animatie animatie = new Animatie();
                    animatie.Show();
                    await Task.Delay(2000);
                    animatie.Hide();
                    jocGata();
                }
                else
                {
                    nrPanouri = fibonacci(n) * 2;
                    curatare();
                    generareNumere();
                    amestecare();
                    incarcaLabelText();
                    picFront();
                    enabledLaCeNuFolosim();
                    MessageBox.Show($"Ai completat nivelul {n - 3}");
                }
            }
        }
        private void jocGata()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                DateTime dt = DateTime.Now;
                int tip = 0;
                SqlCommand cmd = new SqlCommand("Insert into Rezultate(EmailUtilizator, TipJoc, PunctajJoc, Data) values(@email, @TipJoc, @PunctajJoc, @Data)", con);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@TipJoc", tip);
                cmd.Parameters.AddWithValue("@PunctajJoc", timp);
                cmd.Parameters.AddWithValue("@Data", dt);
                cmd.ExecuteNonQuery();
            }
            this.Hide();
            AlegeJoc alege = new AlegeJoc(_email);
            alege.Show();
        }
        private void enabledTot()
        {
            
                for (int i = 1; i <= 8; i++)
                {
                    string numePic = $"poza{i}";
                    PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;
                    string numeCover = $"pictureBox{i}";
                    PictureBox coverBox = this.Controls.Find(numeCover, true).FirstOrDefault() as PictureBox;
                    string numeLab = $"nume{i}";
                    Label label = this.Controls.Find(numeLab, true).FirstOrDefault() as Label;
                    label.Visible = false;
                    pictureBox.Visible = false;
                    coverBox.Visible = false;
                }
            
        }
        private void enabledTot2()
        {

            for (int i = 1; i <= 8; i++)
            {
                string numePic = $"poza{i}";
                PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;
                string numeCover = $"pictureBox{i}";
                PictureBox coverBox = this.Controls.Find(numeCover, true).FirstOrDefault() as PictureBox;
                string numeLab = $"nume{i}";
                Label label = this.Controls.Find(numeLab, true).FirstOrDefault() as Label;
                label.Visible = true;
                pictureBox.Visible = true;
                coverBox.Visible = true;
            }

        }
        private void enabledLaCeNuFolosim()
        {
            if(nrPanouri / 2 != 8)
            {
                for (int i = nrPanouri / 2 + 1; i <= 8; i++)
                {
                    string numePic = $"poza{i}";
                    PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;
                    string numeCover = $"pictureBox{i}";
                    PictureBox coverBox = this.Controls.Find(numeCover, true).FirstOrDefault() as PictureBox;
                    string numeLab = $"nume{i}";
                    Label label = this.Controls.Find(numeLab, true).FirstOrDefault() as Label;
                    label.Visible = false;
                    pictureBox.Visible = false;
                    coverBox.Visible = false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            nrPanouri = fibonacci(n) * 2;
            enabledTot2();
            generareNumere();
            amestecare();
            incarcaLabelText();
            picFront();
            enabledLaCeNuFolosim();
            label1.Text = $"Timp ramas: {timp}";
            button1.Enabled = false;
            timer1.Start();
        }
        private bool verificareNivelGata()
        {
            for (int i = 1; i <= nrPanouri / 2; i++)
            {
                string numePic = $"poza{i}";
                PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;
                if (pictureBox.Enabled == true)
                    return false;
            }
            return true;
        }
        private void curatare()
        {
            for (int i = 1; i <= nrPanouri / 2; i++)
            {
                string numePic = $"poza{i}";
                PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;
                string numeLab = $"nume{i}";
                Label label = this.Controls.Find(numeLab, true).FirstOrDefault() as Label;
                string numeCover = $"pictureBox{i}";
                PictureBox coverBox = this.Controls.Find(numeCover, true).FirstOrDefault() as PictureBox;
                label.Visible = true;
                label.Text = string.Empty;
                label.Enabled = true;

                pictureBox.Visible = true;
                pictureBox.Image = null;
                pictureBox.Enabled = true;
                coverBox.Visible = true;
                coverBox.Enabled = true;
            }
        }

        private void incarcaLabelText()
        {
            Dictionary<int, string> numeImagini = new Dictionary<int, string>()
            {
                {1, "Avion"}, {2, "Bloc"}, {3, "Caine"}, {4, "Caprioara"},
                {5, "Iepure"}, {6, "Leu"}, {7, "Lup"}, {8, "Masina"},
                {9, "Minge"}, {10, "Pisica"}, {11, "Taur"}, {12, "Urs"},
                {13, "Vulpe"}, {14, "Patine"}
            };

            for (int i = 0; i < listaRandomCp.Count; i++)
            {
                string numeLabel = $"nume{i + 1}";
                Label label = this.Controls.Find(numeLabel, true).FirstOrDefault() as Label;

                if (label != null)
                {
                    int valoare = listaRandomCp[i];
                    label.Visible = false;
                    label.Text = $"{Gasesteindice2(valoare)} - {numeImagini[valoare]}";
                }
            }
        }


        
        private int Gasesteindice2(int x)
        {
            int a = 0;
            foreach (int b in listaRandom)
            {
                a++;
                if (b == x)
                    break;

            }
            return a;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timp--; 
            label1.Text = $"Timp ramas: {timp}";

            if (timp < 0)
            {
                timer1.Stop();
                jocGata();

            }

        }

        private int fibonacci(int n)
        {
            if (n <= 2)
                return 1;
            else
                return fibonacci(n - 1) + fibonacci(n - 2);
        }

        private void amestecare()
        {
            listaRandomCp.Clear();  
            foreach(int i in listaRandom)
                listaRandomCp.Add(i);

            for(int i = listaRandomCp.Count - 1; i > 0; i--)
            {
                int j = Random.Next(0, i + 1);
                int temp = listaRandomCp[i];
                listaRandomCp[i] = listaRandomCp[j];
                listaRandomCp[j] = temp;
            }
        }
        private async void pic_click(object sender, EventArgs e)
        {
            if (timp > 0)
            {

                if (apasatSauNu == "picture")
                {
                    PictureBox picApasat = sender as PictureBox;
                    string nume_control = picApasat.Name;
                    char x = nume_control[10];
                    string numeLabel20 = $"nume" + x;
                    Label label = this.Controls.Find(numeLabel20, true).FirstOrDefault() as Label;
                    label.Visible = true;
                    int index = x - '0';
                    idLabel = listaRandomCp[index - 1];
                    if (idPoza == idLabel)
                    {
                        label.Enabled = false;
                        PictureBox pictureBox = this.Controls.Find(numePic12, true).FirstOrDefault() as PictureBox;
                        pictureBox.Enabled = false;
                        apasatSauNu = "";
                        if (verificareNivelGata())
                        {
                            NivelCompletat();
                        }
                    }
                    else
                    {
                        await Task.Delay(1000);
                        label.Visible = false;
                        apasatSauNu = "";
                        PictureBox pictureBox = this.Controls.Find(numePic12, true).FirstOrDefault() as PictureBox;
                        pictureBox.Image = null;
                    }
                }
            }
        }

        private void picFront()
        {
            for(int i = 1; i <= 8; i++)
            {
                string numePic = $"pictureBox{i}";
                PictureBox pictureBox = this.Controls.Find(numePic, true).FirstOrDefault() as PictureBox;

                string numeLabel = $"nume{i}";
                Label label = this.Controls.Find(numeLabel, true).FirstOrDefault() as Label;

                if (pictureBox != null && label != null)
                {
                    pictureBox.SendToBack();

                    label.BringToFront();

                    label.BackColor = Color.Transparent;
                }
            }
        }
        private string numePic12;
        private async void picPoza_Click(object sender, EventArgs e)
        {
            if(timp > 0)
            {

                if (apasatSauNu == "")
                {
                    PictureBox picApasat = sender as PictureBox;
                    string nume_control = picApasat.Name;

                    char x = nume_control[4];
                    int index = x - '0';
                    numePic12 = $"poza" + x;
                    PictureBox pictureBox = this.Controls.Find(numePic12, true).FirstOrDefault() as PictureBox;
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + "Imagini";

                    pictureBox.Image = Image.FromFile(path + $"\\{listaRandom[index - 1]}.jpg");

                    idPoza = listaRandom[index - 1];
                    apasatSauNu = "picture";
                }
            }
        }
        private void generareNumere()
        {
            listaRandom.Clear();

            for(int i = 1; i <= nrPanouri / 2; i++)
            {
                int numarRnd = Random.Next(1, 15);
                if (!listaRandom.Contains(numarRnd))
                {
                    listaRandom.Add(numarRnd);
                }
                else
                    i--;
            }
        }
    }
}
