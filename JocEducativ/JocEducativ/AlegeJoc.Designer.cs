namespace JocEducativ
{
    partial class AlegeJoc
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
            components = new System.ComponentModel.Container();
            Ghiceste = new Button();
            SarpeEducativ = new Button();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            dgGhiceste = new DataGridView();
            dgSarpe = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgGhiceste).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgSarpe).BeginInit();
            SuspendLayout();
            // 
            // Ghiceste
            // 
            Ghiceste.BackColor = Color.CornflowerBlue;
            Ghiceste.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            Ghiceste.Location = new Point(132, 329);
            Ghiceste.Name = "Ghiceste";
            Ghiceste.Size = new Size(126, 74);
            Ghiceste.TabIndex = 1;
            Ghiceste.Text = "Ghiceste";
            Ghiceste.UseVisualStyleBackColor = false;
            Ghiceste.Click += Ghiceste_Click;
            // 
            // SarpeEducativ
            // 
            SarpeEducativ.BackColor = Color.CornflowerBlue;
            SarpeEducativ.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            SarpeEducativ.Location = new Point(516, 329);
            SarpeEducativ.Name = "SarpeEducativ";
            SarpeEducativ.Size = new Size(125, 74);
            SarpeEducativ.TabIndex = 2;
            SarpeEducativ.Text = "SarpeEducativ";
            SarpeEducativ.UseVisualStyleBackColor = false;
            SarpeEducativ.Click += SarpeEducativ_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 25);
            label1.Name = "label1";
            label1.Size = new Size(57, 21);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // dgGhiceste
            // 
            dgGhiceste.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgGhiceste.Location = new Point(23, 108);
            dgGhiceste.Name = "dgGhiceste";
            dgGhiceste.Size = new Size(345, 200);
            dgGhiceste.TabIndex = 5;
            // 
            // dgSarpe
            // 
            dgSarpe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSarpe.Location = new Point(399, 108);
            dgSarpe.Name = "dgSarpe";
            dgSarpe.Size = new Size(370, 200);
            dgSarpe.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label2.Location = new Point(109, 80);
            label2.Name = "label2";
            label2.Size = new Size(166, 25);
            label2.TabIndex = 7;
            label2.Text = "Top scor Ghiceste";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            label3.Location = new Point(483, 80);
            label3.Name = "label3";
            label3.Size = new Size(233, 25);
            label3.TabIndex = 8;
            label3.Text = "Top scor SarpeleEducativ";
            // 
            // button1
            // 
            button1.BackColor = Color.RoyalBlue;
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            button1.Location = new Point(341, 404);
            button1.Name = "button1";
            button1.Size = new Size(92, 34);
            button1.TabIndex = 9;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // AlegeJoc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RoyalBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgSarpe);
            Controls.Add(dgGhiceste);
            Controls.Add(label1);
            Controls.Add(SarpeEducativ);
            Controls.Add(Ghiceste);
            Name = "AlegeJoc";
            Text = "AlegeJoc";
            Load += AlegeJoc_Load;
            ((System.ComponentModel.ISupportInitialize)dgGhiceste).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgSarpe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Ghiceste;
        private Button SarpeEducativ;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private DataGridView dgGhiceste;
        private DataGridView dgSarpe;
        private Label label2;
        private Label label3;
        private Button button1;
    }
}