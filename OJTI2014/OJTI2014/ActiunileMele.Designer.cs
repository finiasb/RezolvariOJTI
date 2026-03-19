namespace OJTI2014
{
    partial class ActiunileMele
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Denumire = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumarActiuni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValoareActiuneInitial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValoareActiuneMomentana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValoareaCuCareACrescutSauScazutActiuneaMomentan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalValoareInitial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalValoareMomentana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitPierdereMomentana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitPierdereTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Denumire,
            this.NumarActiuni,
            this.ValoareActiuneInitial,
            this.ValoareActiuneMomentana,
            this.ValoareaCuCareACrescutSauScazutActiuneaMomentan,
            this.TotalValoareInitial,
            this.TotalValoareMomentana,
            this.ProfitPierdereMomentana,
            this.ProfitPierdereTotal});
            this.dataGridView1.Location = new System.Drawing.Point(13, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(948, 361);
            this.dataGridView1.TabIndex = 0;
            // 
            // Denumire
            // 
            this.Denumire.HeaderText = "Denumire";
            this.Denumire.Name = "Denumire";
            this.Denumire.ReadOnly = true;
            // 
            // NumarActiuni
            // 
            this.NumarActiuni.HeaderText = "Numar Actiuni";
            this.NumarActiuni.Name = "NumarActiuni";
            this.NumarActiuni.ReadOnly = true;
            // 
            // ValoareActiuneInitial
            // 
            this.ValoareActiuneInitial.HeaderText = "Valoare Actiune Initial";
            this.ValoareActiuneInitial.Name = "ValoareActiuneInitial";
            this.ValoareActiuneInitial.ReadOnly = true;
            // 
            // ValoareActiuneMomentana
            // 
            this.ValoareActiuneMomentana.HeaderText = "Valoare Actiune Momentana";
            this.ValoareActiuneMomentana.Name = "ValoareActiuneMomentana";
            this.ValoareActiuneMomentana.ReadOnly = true;
            // 
            // ValoareaCuCareACrescutSauScazutActiuneaMomentan
            // 
            this.ValoareaCuCareACrescutSauScazutActiuneaMomentan.HeaderText = "Valoarea cu care a crescut sau scazut actiunea momentan";
            this.ValoareaCuCareACrescutSauScazutActiuneaMomentan.Name = "ValoareaCuCareACrescutSauScazutActiuneaMomentan";
            this.ValoareaCuCareACrescutSauScazutActiuneaMomentan.ReadOnly = true;
            // 
            // TotalValoareInitial
            // 
            this.TotalValoareInitial.HeaderText = "Total Valoare Initial";
            this.TotalValoareInitial.Name = "TotalValoareInitial";
            this.TotalValoareInitial.ReadOnly = true;
            // 
            // TotalValoareMomentana
            // 
            this.TotalValoareMomentana.HeaderText = "Total Valoare Momentana";
            this.TotalValoareMomentana.Name = "TotalValoareMomentana";
            this.TotalValoareMomentana.ReadOnly = true;
            // 
            // ProfitPierdereMomentana
            // 
            this.ProfitPierdereMomentana.HeaderText = "Profit/Pierdere Momentana";
            this.ProfitPierdereMomentana.Name = "ProfitPierdereMomentana";
            this.ProfitPierdereMomentana.ReadOnly = true;
            // 
            // ProfitPierdereTotal
            // 
            this.ProfitPierdereTotal.HeaderText = "Profit/Pierdere Total";
            this.ProfitPierdereTotal.Name = "ProfitPierdereTotal";
            this.ProfitPierdereTotal.ReadOnly = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(468, 406);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profit / pierdere totala:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ActiunileMele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ActiunileMele";
            this.Text = "ActiunileMele";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Denumire;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumarActiuni;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValoareActiuneInitial;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValoareActiuneMomentana;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValoareaCuCareACrescutSauScazutActiuneaMomentan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalValoareInitial;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalValoareMomentana;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitPierdereMomentana;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitPierdereTotal;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}