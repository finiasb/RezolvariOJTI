using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                
        }

        private void pic_click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "eco";
            PictureBox pic = (PictureBox)sender;    
            string numeBack = "";
            if(pic.Name == "pictureBox1")
            {
                numeBack = "Back1.jpg";
            }else if (pic.Name == "pictureBox2")
            {
                numeBack = "Back2.jpg";
            }
            else if (pic.Name == "pictureBox3")
            {
                numeBack = "Back3.jpg";
            }
            else if (pic.Name == "pictureBox4")
            {
                numeBack = "Back4.jpg";
            }
            else if (pic.Name == "pictureBox5")
            {
                numeBack = "Back5.jpg";
            }
            

            if (comboBox1.SelectedIndex == 0 && textBox1.Text == "eco")
            {
                InterferenteECO inter = new InterferenteECO(numeBack, "Ioana");
                this.Hide();
                inter.Show();   
            }
            else if (comboBox1.SelectedIndex == 1 && textBox1.Text == "123")
            {
                InterferenteECO inter = new InterferenteECO(numeBack, "Radu");
                this.Hide();
                inter.Show();
            }
            else if (comboBox1.SelectedIndex == 2 && textBox1.Text == "abc")
            {
                InterferenteECO inter = new InterferenteECO(numeBack, "Maria");
                this.Hide();
                inter.Show();
            }
            else if (comboBox1.SelectedIndex == 3 && textBox1.Text == "a")
            {
                InterferenteECO inter = new InterferenteECO(numeBack, "Florin");
                this.Hide();
                inter.Show();
            }
            else if (comboBox1.SelectedIndex == 4 && textBox1.Text == "tg")
            {
                InterferenteECO inter = new InterferenteECO(numeBack, "Mihai");
                this.Hide();
                inter.Show();
            }
            else
            {
                MessageBox.Show("Parola este gresita");
                textBox1.Text = string.Empty;
            }
        }
    }
}
