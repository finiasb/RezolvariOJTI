using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI_2019_C_
{
    public partial class AfiseazaCarte : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fineas\source\repos\OJTI_2019\OJTI_2019\bin\Debug\FreeBook.mdf;Integrated Security=True;Connect Timeout=30";
        private int _idCarte;
        private string _titlu;
        public AfiseazaCarte(string titlu)
        {
            InitializeComponent(); 
            _titlu = titlu;

        }
    }
}
