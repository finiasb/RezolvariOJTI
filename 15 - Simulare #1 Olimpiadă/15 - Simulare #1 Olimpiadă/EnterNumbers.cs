using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15___Simulare__1_Olimpiadă
{
    public partial class EnterNumbers : Form
    {
        int _max;
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""|DataDirectory|\BD_NumerePrime.mdf"";Integrated Security=True;Connect Timeout=30";
        int timp = 10;
        int scor;
        List<int> numbers = new List<int>();
        public EnterNumbers(int max)
        {
            InitializeComponent();
            _max = max;
            label2.Text = "Timp: " + timp;
            label3.Text = "Scor Maxim: " + _max;
            label4.Text = "Scor: " + scor;
        }

        private bool verificaPrim(int n)
        {
            if(n < 2) return false;
            if(n == 2) return true;
            for(int d = 2; d  < n; d++)
            {
                if(n % d == 0) 
                    return false;
            }
            return true;
        }

        


        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            int numar;
            if(Int32.TryParse(textBox1.Text, out numar))
            {
                if(numar > 100)
                {
                    textBox1.Text = string.Empty;
                    return;
                }
                if (verificaPrim(numar))
                {
                    if (numbers.Contains(numar))
                    {
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        scor += numar;
                        numbers.Add(numar);
                        label4.Text = "Scor: " + scor;
                    }
                }
            }
            
            textBox1.Text = string.Empty;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(timp == 0)
            {
                timer1.Stop();
                MessageBox.Show($"Jocul s-a incheat, ati obtinut {scor}");
                
                this.Hide();
                Form1 form = new Form1(scor);
                form.Show();
            }
            timp -= 1;
            label2.Text = "Timp: " + timp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
