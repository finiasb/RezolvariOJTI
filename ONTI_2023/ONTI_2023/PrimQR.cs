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
    public partial class PrimQR : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|\\Jocuri.mdf\";Integrated Security=True;Connect Timeout=30";    
        MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder(); 
        private List<(string Email, int Punctaj)> listaOrd = new List<(string, int)>();
        private string _email;
        public PrimQR(string email)
        {
            InitializeComponent();
            _email = email;
            sortare();
        }
        private bool Prim(int n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;  
            if (n % 2 == 0 || n % 3 == 0) return false; 

            int limit = (int)Math.Sqrt(n);
            for (int i = 5; i <= limit; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int maxDist = -1;
            List<(string email, int punctaj, int prim)> candidati = new List<(string, int, int)>();

            foreach (var juc in listaOrd) 
            {
                int p = juc.Punctaj;
                int primMaiMare = p + 1;
                while (!Prim(primMaiMare))
                {
                    primMaiMare++;
                }

                int dist = primMaiMare - p;

                if (dist > maxDist)
                {
                    maxDist = dist;
                    candidati.Clear();
                    candidati.Add((juc.Email, p, primMaiMare));
                }
                else if (dist == maxDist)
                {
                    candidati.Add((juc.Email, p, primMaiMare));
                }
            }

            var celAles = candidati.OrderBy(c => c.email).First();

            string txtEncode = $"{celAles.email};{celAles.punctaj};{celAles.prim}";


            encoder.QRCodeScale = 8;
            Bitmap bmp = encoder.Encode(txtEncode);
            pictureBox1.Image = bmp;

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Prim\Logo_C#.png";
            Image imageLogo = Image.FromFile(path);

            int logoSize = pictureBox1.Width / 5;
            Bitmap bitmap = new Bitmap(imageLogo, new Size(logoSize, logoSize));

            int x = (bmp.Width - logoSize) / 2;

            using(Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(bitmap, new Point(x, x));
            }
            pictureBox1.Image = bmp;
        }
        private void sortare()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Rezultate Order By PunctajJoc DESC", con);
                SqlDataReader reader = cmd.ExecuteReader();  

                while (reader.Read())
                {
                    string email = reader["EmailUtilizator"].ToString();
                    int punctaj = Convert.ToInt32(reader["PunctajJoc"]);
                    listaOrd.Add((email, punctaj));
                }
                reader.Close();
            }
        }
    }
}
