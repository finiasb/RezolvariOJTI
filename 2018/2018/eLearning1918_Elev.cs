using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;
using System.Diagnostics.CodeAnalysis;

namespace _2018
{
    public partial class eLearning1918_Elev : Form
    {
        private string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\eLearning1918.mdf;Integrated Security=True;Connect Timeout=30";
        private int _id;
        Random randomCerinta = new Random();
        private List<int> cerinte = new List<int>();
        private int index = 1, numarCerinta;
        private string raspunsCorect;
        private int punctaj = 1, raspunsRadio;
        private string raspuns1Item, raspuns2Item, raspuns3Item, raspuns4Item;
        DataTable raport = new DataTable();
        DataTable carnet = new DataTable();
        private string clasa;
        Series seriaElev = new Series("Notele mele");
        Series seriaMediaClasa = new Series("Media clasei");
        private bool oData = false;
        private int k = 1;
        public eLearning1918_Elev(int id)
        {
            InitializeComponent();
            _id = id;
            ResetControale();
            printDocument2.PrintPage += new PrintPageEventHandler(printDocument2_PrintPage);

            seriaElev.ChartType = SeriesChartType.Line;
            seriaElev.Color = System.Drawing.Color.Blue;

            seriaMediaClasa.ChartType = SeriesChartType.Line;
            seriaMediaClasa.Color = System.Drawing.Color.Red;

            chart1.Series.Add(seriaElev);
            chart1.Series.Add(seriaMediaClasa);

            raport.Columns.Add("NumarOrdine");
            raport.Columns.Add("TipItem");
            raport.Columns.Add("Enunt");
            raport.Columns.Add("RaspunsElev");
            raport.Columns.Add("RaspunsCorect");

            if (oData == false)
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("Select NumePrenumeUtilizator, ClasaUtilizator from utilizatori where IdUtilizator = @id", con);
                cmd.Parameters.AddWithValue("@id", _id);
                SqlDataReader red = cmd.ExecuteReader();
                if (red.Read())
                {
                    string nume = red["NumePrenumeUtilizator"].ToString();
                    clasa = red["ClasaUtilizator"].ToString();
                    label4.Text = "Carnetul de note al elevului " + nume;
                }
                red.Close();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                carnet.Columns.Add("Nota");
                carnet.Columns.Add("Data");
                dataGridView1.DataSource = carnet;

                SqlCommand cmdd = new SqlCommand("Select DataEvaluare, NotaEvaluare from evaluari where IdElev = @id", con);
                cmdd.Parameters.AddWithValue("id", _id);
                SqlDataReader redd = cmdd.ExecuteReader();
                k = 1;
                while (redd.Read())
                {
                    string data = redd["DataEvaluare"].ToString();
                    string nota = redd["NotaEvaluare"].ToString();

                    carnet.Rows.Add(data, nota);
                    seriaElev.Points.AddXY(k, nota);
                    k++;
                }
                redd.Close();

                SqlCommand command = new SqlCommand("SELECT AVG(CAST(E.NotaEvaluare AS FLOAT)) as MediaGenerala FROM Evaluari E JOIN Utilizatori U ON E.IdElev = U.IdUtilizator WHERE U.ClasaUtilizator = @clasa", con);
                command.Parameters.AddWithValue("@clasa", clasa);
                SqlDataReader reader = command.ExecuteReader();

                double mediaGenerala = 0;
                if (reader.Read())
                    mediaGenerala = Convert.ToDouble(reader["MediaGenerala"]);

                reader.Close();

                for (int i = 1; i < k; i++)
                {
                    seriaMediaClasa.Points.AddXY(i, mediaGenerala);
                }
                oData = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listaItemi();
            numarCerinta = cerinte[0];
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            if (numarCerinta <= 9)
            {
                SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem from Itemi where id = @id", con);
                cmd.Parameters.AddWithValue("@id", numarCerinta);
                SqlDataReader red = cmd.ExecuteReader();
                if (red.Read())
                {
                    textBoxIntrebare.Text = red["EnuntItem"].ToString();
                    raspunsCorect = red["RaspunsCorectItem"].ToString();
                    ResetControale();
                    label3.Visible = true;

                    textBoxRaspuns.Visible = true;
                    lblRaspuns.Visible = true;
                }
                red.Close();
                label2.Text = "Item de tip 1";

            }
            else if(numarCerinta <= 19)
            {
                SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem, Raspuns1Item, Raspuns2Item,Raspuns3Item, Raspuns4Item  from Itemi where id = @id", con);
                cmd.Parameters.AddWithValue("@id", numarCerinta);
                SqlDataReader red = cmd.ExecuteReader();
                if (red.Read())
                {
                    textBoxIntrebare.Text = red["EnuntItem"].ToString();
                    raspunsCorect = red["RaspunsCorectItem"].ToString();
                    raspuns1Item = red["Raspuns1Item"].ToString();
                    raspuns2Item = red["Raspuns2Item"].ToString();
                    raspuns3Item = red["Raspuns3Item"].ToString();
                    raspuns4Item = red["Raspuns4Item"].ToString();

                    raspunsRadio = Int32.Parse(raspunsCorect);
                    label3.Visible = true;

                    radioButton1.Visible = true;    
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;
                    radioButton4.Visible = true;
                    radioButton1.Text = raspuns1Item;
                    radioButton2.Text = raspuns2Item;
                    radioButton3.Text = raspuns3Item;
                    radioButton4.Text = raspuns4Item;
                }
                red.Close();
                label2.Text = "Item de tip 2";

            }
            else if (numarCerinta <= 25)
            {
                SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem, Raspuns1Item, Raspuns2Item,Raspuns3Item, Raspuns4Item  from Itemi where id = @id", con);
                cmd.Parameters.AddWithValue("@id", numarCerinta);
                SqlDataReader red = cmd.ExecuteReader();
                if (red.Read())
                {
                    textBoxIntrebare.Text = red["EnuntItem"].ToString();
                    raspunsCorect = red["RaspunsCorectItem"].ToString();
                    raspuns1Item = red["Raspuns1Item"].ToString();
                    raspuns2Item = red["Raspuns2Item"].ToString();
                    raspuns3Item = red["Raspuns3Item"].ToString();
                    raspuns4Item = red["Raspuns4Item"].ToString();
                    checkBox1.Visible = true;
                    checkBox2.Visible = true;
                    checkBox3.Visible = true;  
                    checkBox4.Visible = true;
                    checkBox1.Text = raspuns1Item;
                    checkBox2.Text = raspuns2Item;
                    checkBox3.Text = raspuns3Item;
                    checkBox4.Text = raspuns4Item;
                    label3.Visible = true;

                }
                red.Close();
                label2.Text = "Item de tip 3";

            }
            else if (numarCerinta <= 32)
            {
                SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem from Itemi where id = @id", con);
                cmd.Parameters.AddWithValue("@id", numarCerinta);
                SqlDataReader red = cmd.ExecuteReader();
                if (red.Read())
                {
                    textBoxIntrebare.Text = red["EnuntItem"].ToString();
                    raspunsCorect = red["RaspunsCorectItem"].ToString();
                    raspunsRadio = Int32.Parse(raspunsCorect);
                    ResetControale();
                    label3.Visible = true;
                    radioButton5.Visible = true;
                    radioButton6.Visible = true;
                }
                red.Close();
                label2.Text = "Item de tip 4";
            }
            label3.Text = "Intrebarea numarul 1";
            button1.Enabled = false;    
        }

        private void btnRaspune_Click(object sender, EventArgs e)
        {
            if (numarCerinta >= 1 && numarCerinta <= 9)
            {
                if (textBoxRaspuns.Text.ToLower() == raspunsCorect.ToLower() && numarCerinta >= 1 && numarCerinta <= 9)
                {
                    punctaj++;
                    label1.Text = "Punctaj: " + punctaj;
                    MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {textBoxRaspuns.Text}");
                }
                else if (textBoxRaspuns.Text.ToLower() != raspunsCorect.ToLower() && numarCerinta >= 1 && numarCerinta <= 9)
                {
                    label1.Text = "Punctaj: " + punctaj;
                    MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {textBoxRaspuns.Text}");
                }
                raport.Rows.Add(index.ToString(), 1.ToString(), textBoxIntrebare.Text, textBoxRaspuns.Text, raspunsCorect.ToString());
                textBoxRaspuns.Clear();
            }
            if (numarCerinta >= 10 && numarCerinta <= 19)
            {
                string numeControl = $"radioButton{raspunsRadio}";
                int a = 0;
                RadioButton radio = this.Controls.Find(numeControl, true).FirstOrDefault() as RadioButton;
                if (radio.Checked == true)
                {
                    punctaj++;
                    label1.Text = "Punctaj: " + punctaj;
                }
                else
                {
                    label1.Text = "Punctaj: " + punctaj;
                }
                if (radioButton1.Checked == true)
                    a = 1;
                else if (radioButton2.Checked == true)
                    a = 2;
                else if (radioButton3.Checked == true)
                    a = 3;
                else if (radioButton4.Checked == true)
                    a = 4;
                radio.Checked = false;
                raport.Rows.Add(index.ToString(), 2.ToString(), textBoxIntrebare.Text, a.ToString(), raspunsCorect.ToString());
                MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {a.ToString()}");
            }
            if (numarCerinta >= 20 && numarCerinta <= 25)
            {
                int nr1 = int.Parse(raspunsCorect[0].ToString());
                int nr2 = int.Parse(raspunsCorect[1].ToString());
                int nr3 = 0;
                string numeControl1 = $"checkBox{nr1}";
                CheckBox cb1 = this.Controls.Find(numeControl1, true).FirstOrDefault() as CheckBox;
                string numeControl2 = $"checkBox{nr2}";
                CheckBox cb2 = this.Controls.Find(numeControl2, true).FirstOrDefault() as CheckBox;
                string numeControl3;
                if (raspunsCorect.Length == 2)
                {
                    if (cb1.Checked == true && cb2.Checked == true)
                    {
                        punctaj++;
                        label1.Text = "Punctaj: " + punctaj;
                    }
                    else
                    {
                        label1.Text = "Punctaj: " + punctaj;
                    }
                    int x = 0;
                    if (checkBox1.Checked == true)
                        x = x * 10 + 1;
                    if (checkBox2.Checked == true)
                        x = x * 10 + 2;
                    if (checkBox3.Checked == true)
                        x = x * 10 + 3;
                    if (checkBox4.Checked == true)
                        x = x * 10 + 4;

                    raport.Rows.Add(index.ToString(), 3.ToString(), textBoxIntrebare.Text, x.ToString(), raspunsCorect.ToString());
                    MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {x.ToString()}");
                }

                if (raspunsCorect.Length == 3)
                {
                    int.Parse(raspunsCorect[2].ToString());
                    numeControl3 = $"checkBox{nr3}";
                    CheckBox cb3 = this.Controls.Find(numeControl3, true).FirstOrDefault() as CheckBox;
                    if (cb1.Checked == true && cb2.Checked == true && cb3.Checked == true)
                    {
                        punctaj++;
                        label1.Text = "Punctaj: " + punctaj;
                    }
                    else
                    {
                        label1.Text = "Punctaj: " + punctaj;
                    }
                    int x = 0;
                    if (checkBox1.Checked == true)
                        x = x * 10 + 1;
                    if (checkBox2.Checked == true)
                        x = x * 10 + 2;
                    if (checkBox3.Checked == true)
                        x = x * 10 + 3;
                    if (checkBox4.Checked == true)
                        x = x * 10 + 4;

                    raport.Rows.Add(index.ToString(), 3.ToString(), textBoxIntrebare.Text, x.ToString(), raspunsCorect.ToString());
                    MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {x.ToString()}");
                }
            }
            if (numarCerinta >= 26 && numarCerinta <= 32)
            {
                int k = 0;
                if (raspunsRadio == 0)
                    k = 5;
                else if (raspunsRadio == 1)
                    k = 6;
                
                string numeControl = $"radioButton{k}";
                RadioButton radio = this.Controls.Find(numeControl, true).FirstOrDefault() as RadioButton;
                if(radio.Checked == true)
                {
                    punctaj++;
                    label1.Text = "Punctaj: " + punctaj;
                }
                else
                {
                    label1.Text = "Punctaj: " + punctaj;
                }
                int x = 0;
                if (radioButton6.Checked == true)
                    x = 1;
                raport.Rows.Add(index.ToString(), 4.ToString(), textBoxIntrebare.Text, x.ToString(), raspunsCorect.ToString());
                MessageBox.Show($"Raspunsul corect: {raspunsCorect}\nRaspunsul tau: {x.ToString()}");
            }

            if (index < 9)
            {
                numarCerinta = cerinte[index];
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                if (numarCerinta <= 9)
                {
                    SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem from Itemi where id = @id", con);
                    cmd.Parameters.AddWithValue("@id", numarCerinta);
                    SqlDataReader red = cmd.ExecuteReader();
                    if (red.Read())
                    {
                        textBoxIntrebare.Text = red["EnuntItem"].ToString();
                        raspunsCorect = red["RaspunsCorectItem"].ToString().ToLower();
                        ResetControale();
                        textBoxRaspuns.Visible = true;
                        lblRaspuns.Visible = true;
                        label3.Visible = true;
                    }
                    red.Close();
                    if(textBoxRaspuns.Text == raspunsCorect)
                    {
                        punctaj++;
                        label1.Text = "Punctaj: " + punctaj;
                        MessageBox.Show("Raspuns Corectt");
                    }
                    label2.Text = "Item de tip 1";
                }
                else if (numarCerinta <= 19)
                {
                    SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem, Raspuns1Item, Raspuns2Item,Raspuns3Item, Raspuns4Item  from Itemi where id = @id", con);
                    cmd.Parameters.AddWithValue("@id", numarCerinta);
                    SqlDataReader red = cmd.ExecuteReader();
                    if (red.Read())
                    {
                        textBoxIntrebare.Text = red["EnuntItem"].ToString();
                        raspunsCorect = red["RaspunsCorectItem"].ToString();
                        raspuns1Item = red["Raspuns1Item"].ToString();
                        raspuns2Item = red["Raspuns2Item"].ToString();
                        raspuns3Item = red["Raspuns3Item"].ToString();
                        raspuns4Item = red["Raspuns4Item"].ToString();

                        raspunsRadio = Int32.Parse(raspunsCorect);
                        ResetControale();
                        label3.Visible = true;

                        radioButton1.Visible = true;
                        radioButton2.Visible = true;
                        radioButton3.Visible = true;
                        radioButton4.Visible = true;
                        radioButton1.Text = raspuns1Item;
                        radioButton2.Text = raspuns2Item;
                        radioButton3.Text = raspuns3Item;
                        radioButton4.Text = raspuns4Item;
                    }
                    red.Close();

                    label2.Text = "Item de tip 2";

                }
                else if (numarCerinta <= 25)
                {
                    SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem, Raspuns1Item, Raspuns2Item,Raspuns3Item, Raspuns4Item  from Itemi where id = @id", con);
                    cmd.Parameters.AddWithValue("@id", numarCerinta);
                    SqlDataReader red = cmd.ExecuteReader();
                    if (red.Read())
                    {
                        textBoxIntrebare.Text = red["EnuntItem"].ToString();
                        raspunsCorect = red["RaspunsCorectItem"].ToString();
                        raspuns1Item = red["Raspuns1Item"].ToString();
                        raspuns2Item = red["Raspuns2Item"].ToString();
                        raspuns3Item = red["Raspuns3Item"].ToString();
                        raspuns4Item = red["Raspuns4Item"].ToString();
                        ResetControale();
                        label3.Visible = true;

                        checkBox1.Visible = true;
                        checkBox2.Visible = true;
                        checkBox3.Visible = true;
                        checkBox4.Visible = true;
                        checkBox1.Text = raspuns1Item;
                        checkBox2.Text = raspuns2Item;
                        checkBox3.Text = raspuns3Item;
                        checkBox4.Text = raspuns4Item;
                    }
                    red.Close();
                    label2.Text = "Item de tip 3";

                }
                else if (numarCerinta <= 32)
                {
                    SqlCommand cmd = new SqlCommand("Select EnuntItem, RaspunsCorectItem from Itemi where id = @id", con);
                    cmd.Parameters.AddWithValue("@id", numarCerinta);
                    SqlDataReader red = cmd.ExecuteReader();
                    if (red.Read())
                    {
                        textBoxIntrebare.Text = red["EnuntItem"].ToString();
                        raspunsCorect = red["RaspunsCorectItem"].ToString();
                        raspunsRadio = Int32.Parse(raspunsCorect);
                        ResetControale();
                        radioButton5.Visible = true;
                        radioButton6.Visible = true;
                        label3.Visible = true;

                    }
                    red.Close();
                    label2.Text = "Item de tip 4";

                }
                label3.Text = $"Intrebarea numarul {index + 1}";

            }
            else if(index == 9)
            {
                DateTime thisDay = DateTime.Now;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Evaluari(DataEvaluare, NotaEvaluare, IdElev) values(@data, @nota, @id)" ,con);
                cmd.Parameters.AddWithValue("@data", thisDay);
                cmd.Parameters.AddWithValue("@nota", punctaj);
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
                btnRaspune.Enabled = false;
                FormRaport f = new FormRaport(raport);
                f.ShowDialog();
                
                carnet.Rows.Add(thisDay, punctaj);
                seriaElev.Points.AddXY(k, punctaj);
                seriaMediaClasa.Points.Clear();
                SqlCommand command = new SqlCommand("SELECT AVG(CAST(E.NotaEvaluare AS FLOAT)) as MediaGenerala FROM Evaluari E JOIN Utilizatori U ON E.IdElev = U.IdUtilizator WHERE U.ClasaUtilizator = @clasa", con);
                command.Parameters.AddWithValue("@clasa", clasa);
                SqlDataReader reader = command.ExecuteReader();

                double mediaGenerala = 0;
                if (reader.Read())
                    mediaGenerala = Convert.ToDouble(reader["MediaGenerala"]);

                reader.Close();

                for (int i = 1; i <= k; i++)
                {
                    seriaMediaClasa.Points.AddXY(i, mediaGenerala);
                }
                oData = true;
            }
            index++;
        }

        private void listaItemi()
        {
            textBoxIntrebare.Visible = true;
            btnRaspune.Visible = true;
            label2.Visible = true;
            bool cerinta1 = false, cerinta2 = false, cerinta3 = false, cerinta4 = false;
            for (int i = 1; i <= 6; i++)
            {
                int x = randomCerinta.Next(1, 33);
                if (x >= 1 && x <= 9)
                {
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta1 = true;
                    }
                    else
                        i--;
                }
                else if (x >= 10 && x <= 19)
                {
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta2 = true;
                    }
                    else
                        i--;
                }
                else if (x >= 20 && x <= 25)
                {
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta3 = true;
                    }
                    else
                        i--;
                }
                else if (x >= 26 && x <= 32)
                {
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta4 = true;
                    }
                    else
                        i--;
                }
            }

            for(int i = 1; i <= 3; i++)
            {

                if (cerinta1 == false)
                {
                    int x = randomCerinta.Next(1, 10);
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta1 = true;
                    }
                    else
                        i--;
                }
                else if(cerinta2 == false)
                {
                    int x = randomCerinta.Next(10, 20);
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta2 = true;
                    }
                    else
                        i--;
                }
                else if(cerinta3 == false)
                {
                    int x = randomCerinta.Next(20, 26);
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta3 = true;
                    }
                    else
                        i--;
                }
                else if(cerinta4 == false)
                {
                    int x = randomCerinta.Next(26, 33);
                    if (!cerinte.Contains(x))
                    {
                        cerinte.Add(x);
                        cerinta4 = true;
                    }
                    else
                        i--;
                }
                else if(cerinta1 && cerinta2 && cerinta3 && cerinta4)
                {
                    int x = randomCerinta.Next(1, 33);
                    if (!cerinte.Contains(x))
                        cerinte.Add(x);
                    else
                        i--;
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_c(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument2;
            printPreviewDialog1.ShowDialog();
        }
       
        int currentRow = 0;
        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);
            int y = 100;
            int leftMargin = e.MarginBounds.Left;

            
            e.Graphics.DrawString("Carnet de note", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, leftMargin, y);
            y += 40;

            e.Graphics.DrawString("Data", font, Brushes.Black, leftMargin, y);
            e.Graphics.DrawString("Nota", font, Brushes.Black, leftMargin + 200, y);
            y += 30;

            while (currentRow < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[currentRow];

                string data = row.Cells["Data"].Value?.ToString() ?? "";
                string nota = row.Cells["Nota"].Value?.ToString() ?? "";

                e.Graphics.DrawString(data, font, Brushes.Black, leftMargin, y);
                e.Graphics.DrawString(nota, font, Brushes.Black, leftMargin + 200, y);

                y += 25;
                currentRow++;

                if (y > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
            currentRow = 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument2;
            printPreviewDialog1.ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxRaspuns_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void ResetControale()
        {
            textBoxRaspuns.Clear();
            textBoxRaspuns.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;

            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
        }
        private void testeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        }

        private void graficToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }

        private void iesireToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void carnetDeNoteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
        }

    }
}
