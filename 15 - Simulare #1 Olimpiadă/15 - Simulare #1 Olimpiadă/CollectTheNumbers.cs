using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15___Simulare__1_Olimpiadă
{
    public partial class CollectTheNumbers : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\BD_NumerePrime.mdf"";Integrated Security=True;Connect Timeout=30";
        int _scor;
        Point picpoint;
        int scor = 0;
        PictureBox pic;
        string path = System.AppDomain.CurrentDomain.BaseDirectory;
        public CollectTheNumbers(int scor)
        {
            InitializeComponent();
            Random rnd = new Random();
            List<int> list = new List<int>();
            for(int i = 1; i <= 5; i++)
            {
                int u  = rnd.Next(1, 11);
                if(list.Contains(u)){
                    i--;
                }
                else
                {
                    list.Add(u);
                }
            }
            _scor = scor;
            label1.Text = "Scor maxim: " + _scor;
            label2.Text = "Scor: " + 0;

            for (int i = 1; i <=  5; i++) 
            {
                string namePic = $"pic{i}";
                PictureBox pic = this.Controls.Find(namePic, true).FirstOrDefault() as PictureBox;
                pic.Image = Image.FromFile(path + $"{list[i - 1]}.png");
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Tag = list[i - 1] + ".png";
                pic.AllowDrop = true;
                pic.BackColor = Color.Transparent;
                pic.Parent = pictureBox1;
            }

            this.AllowDrop = true;
            pictureBox2.AllowDrop = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            bool x = true, y = false;
            Form1 form = new Form1(scor, x, y);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void pic5_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox pic2 = (PictureBox)sender;

            if (pic2.Name != "pictureBox2")
                return;

            
            if (pic.Tag.ToString() == "2.png")
            {
                scor += 2;
            }
            else if (pic.Tag.ToString() == "3.png")
            {
                scor += 3;
            }
            else if (pic.Tag.ToString() == "5.png")
            {
                scor += 5;
            }
            else if (pic.Tag.ToString() == "7.png")
            {
                scor += 7;
            }
            label2.Text = "Scor: " + scor;


            Random rnd = new Random();
            int x = rnd.Next(1, 11);
            pic.Image = Image.FromFile(path + $"{x}.png");
            pic.Tag = x + ".png";
            pic.Location = picpoint;
        }

        private void pic5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect= DragDropEffects.None;
            }
        }
        private void pic5_MouseDown(object sender, MouseEventArgs e)
        {
            pic = sender as PictureBox;
            picpoint = pic.Location;
            if(e.Button == MouseButtons.Left)
            {
                if(pic.Image != null)
                {
                    pic.DoDragDrop(pic.Image, DragDropEffects.Move);
                }
            }
        }
    }
}
