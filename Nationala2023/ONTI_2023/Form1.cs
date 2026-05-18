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
using MessagingToolkit.QRCode.Codec.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace ONTI_2023
{
    public partial class Form1 : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "ion@oti.ro";
            textBox2.Text = "noi";
            stergere();
            initializare();
        }

        private void initializare()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            StreamReader rdr = new StreamReader(path + "/utilizatori.txt");
            string linie;
            while ((linie = rdr.ReadLine()) != null)
            {
                string[] sir = linie.Split(';');

                SqlCommand cmd = new SqlCommand("insert into  Utilizatori(EmailUtilizator, NumeUtilizator, Parola) values(@email, @nume, @parola)", con);
                cmd.Parameters.AddWithValue("@email", sir[0]);
                cmd.Parameters.AddWithValue("@nume", sir[1]);
                cmd.Parameters.AddWithValue("@parola", sir[2]);
                cmd.ExecuteNonQuery();
            }
            rdr.Close();
            StreamReader rdr2 = new StreamReader(path + "/rezultate.txt");
            string linie2;
            while ((linie2 = rdr2.ReadLine()) != null)
            {
                string[] sir2 = linie2.Split(';');
                SqlCommand cmd = new SqlCommand("insert into Rezultate(TipJoc, EmailUtilizator, PunctajJoc, Data) values(@TipJoc, @EmailUtilizator, @PunctajJoc, @data)", con);
                cmd.Parameters.AddWithValue("@TipJoc", sir2[0]);
                cmd.Parameters.AddWithValue("@EmailUtilizator", sir2[1]);
                cmd.Parameters.AddWithValue("@PunctajJoc", sir2[2]);
                cmd.Parameters.AddWithValue("@data", sir2[3]);
                cmd.ExecuteNonQuery();
            }
            rdr2.Close();
            con.Close();
        }
        private void stergere()
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("truncate table utilizatori", con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("truncate table rezultate", con);
            cmd2.ExecuteNonQuery();
            con.Close();   
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*)  from utilizatori where EmailUtilizator = @email", con);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            int count = (int)cmd.ExecuteScalar();
            if (count == 1)
            {
                AlegeJoc joc = new AlegeJoc(textBox1.Text);
                this.Hide();
                joc.Show();
            }
            else
            {
                MessageBox.Show("Adresa de email nu exista in baza de date");
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
        }
        private string decodare(PictureBox pictureBox)
        {
            MessagingToolkit.QRCode.Codec.QRCodeDecoder objDecodare = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();

            Bitmap bmp = pictureBox.Image as Bitmap;

            return objDecodare.decode(new MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(bmp));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            string path = AppDomain.CurrentDomain.BaseDirectory + "QRCode";
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.Title = "Incarca QRCode";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var imgTemp = Image.FromFile(openFileDialog1.FileName))
                {
                    pictureBox1.Image = new Bitmap(imgTemp);
                }
            }
            string sirCodare = decodare(pictureBox1);
            char prima = sirCodare[0];
            char doi = sirCodare[1];
            if (prima == 'd' && doi == 'a')
            {
                textBox1.Text = "dana@oti.ro";
                textBox2.Text = "data";
            }
            else if (prima == 'd' && doi == 'i')
            {
                textBox1.Text = "dinu@oti.ro";
                textBox2.Text = "onti";
            }
            else if (prima == 'a')
            {
                textBox1.Text = "ana@oti.ro";
                textBox2.Text = "oti";
            }
            else if (prima == 'i')
            {
                textBox1.Text = "ion@oti.ro";
                textBox2.Text = "noi";
            }
            else if (prima == 'g')
            {
                textBox1.Text = "georgescu@oti.ro";
                textBox2.Text = "a1@A";
            }
            else if (prima == 'm')
            {
                textBox1.Text = "marinescu@oti.ro";
                textBox2.Text = "a1@A";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ContNou cont = new ContNou();
            cont.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
