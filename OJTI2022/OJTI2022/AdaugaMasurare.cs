using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OJTI2022
{
    public partial class AdaugaMasurare : Form
    {
        private string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"|DataDirectory|Poluare3.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private int _id, _eX, _eY;
        private Vizualizare _parent;

        public AdaugaMasurare(Vizualizare parent, int id, int eX, int eY)
        {
            InitializeComponent();
            _parent = parent;
            _id = id;
            _eX = eX;
            _eY = eY;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int nr;
            if(Int32.TryParse(textBox1.Text, out nr)){
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();

                    DateTime myDate = DateTime.Now;
                    SqlCommand cmd = new SqlCommand("insert into masurare(IdHarta, PozitieX, PozitieY, ValoareMasurare, DataMasurare) values(@id, @pozX, @pozY, @val, @data)", con);
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.Parameters.AddWithValue("@pozX", _eX);
                    cmd.Parameters.AddWithValue("@pozY", _eY);
                    cmd.Parameters.AddWithValue("@val", nr);
                    cmd.Parameters.AddWithValue("@data", myDate);
                    cmd.ExecuteNonQuery();
                }
                _parent.desenare();
                this.Close();
            }
            else
            {
                MessageBox.Show("Introduceti un numar valid");
                textBox1.Text = string.Empty;   
            }
            
        }
    }
}
