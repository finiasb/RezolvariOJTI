namespace ONTI_2017
{
    partial class VacanteleMele
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Vacanta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataInceput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NrPers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PretTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sterge = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Vacanta,
            this.DataInceput,
            this.DataFinal,
            this.NrPers,
            this.PretTotal,
            this.Sterge});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(750, 260);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Vacanta
            // 
            this.Vacanta.HeaderText = "Vacanta";
            this.Vacanta.Name = "Vacanta";
            this.Vacanta.ReadOnly = true;
            // 
            // DataInceput
            // 
            this.DataInceput.HeaderText = "DataInceput";
            this.DataInceput.Name = "DataInceput";
            this.DataInceput.ReadOnly = true;
            // 
            // DataFinal
            // 
            this.DataFinal.HeaderText = "Data Final";
            this.DataFinal.Name = "DataFinal";
            this.DataFinal.ReadOnly = true;
            // 
            // NrPers
            // 
            this.NrPers.HeaderText = "Nr Persoane";
            this.NrPers.Name = "NrPers";
            this.NrPers.ReadOnly = true;
            // 
            // PretTotal
            // 
            this.PretTotal.HeaderText = "Pret Total";
            this.PretTotal.Name = "PretTotal";
            this.PretTotal.ReadOnly = true;
            // 
            // Sterge
            // 
            this.Sterge.HeaderText = "Sterge";
            this.Sterge.Name = "Sterge";
            this.Sterge.Text = "Sterge";
            this.Sterge.UseColumnTextForButtonValue = true;
            // 
            // VacanteleMele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(800, 295);
            this.Controls.Add(this.dataGridView1);
            this.Name = "VacanteleMele";
            this.Text = "VacanteleMele";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vacanta;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataInceput;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NrPers;
        private System.Windows.Forms.DataGridViewTextBoxColumn PretTotal;
        private System.Windows.Forms.DataGridViewButtonColumn Sterge;
    }
}