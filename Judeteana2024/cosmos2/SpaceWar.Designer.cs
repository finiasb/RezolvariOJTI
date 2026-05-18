namespace cosmos2
{
    partial class SpaceWar
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
            panel1 = new Panel();
            picNava = new PictureBox();
            btnStart = new Button();
            btnStop = new Button();
            btnPauza = new Button();
            label1 = new Label();
            label2 = new Label();
            MesajJoc = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picNava).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Controls.Add(picNava);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 550);
            panel1.TabIndex = 0;
            // 
            // picNava
            // 
            picNava.Image = Properties.Resources.navaStop;
            picNava.Location = new Point(50, 250);
            picNava.Name = "picNava";
            picNava.Size = new Size(70, 50);
            picNava.SizeMode = PictureBoxSizeMode.StretchImage;
            picNava.TabIndex = 0;
            picNava.TabStop = false;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.RoyalBlue;
            btnStart.Image = Properties.Resources.Start1;
            btnStart.Location = new Point(818, 497);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(76, 76);
            btnStart.TabIndex = 1;
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.RoyalBlue;
            btnStop.Image = Properties.Resources.Stop;
            btnStop.Location = new Point(1003, 497);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(75, 76);
            btnStop.TabIndex = 2;
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnPauza
            // 
            btnPauza.BackColor = Color.RoyalBlue;
            btnPauza.Image = Properties.Resources.Pauza;
            btnPauza.Location = new Point(911, 497);
            btnPauza.Name = "btnPauza";
            btnPauza.Size = new Size(75, 75);
            btnPauza.TabIndex = 3;
            btnPauza.UseVisualStyleBackColor = false;
            btnPauza.Click += btnPauza_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(849, 86);
            label1.Name = "label1";
            label1.Size = new Size(117, 30);
            label1.TabIndex = 4;
            label1.Text = "Scor:       0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(849, 137);
            label2.Name = "label2";
            label2.Size = new Size(153, 30);
            label2.TabIndex = 5;
            label2.Text = "Viață:      3      ";
            // 
            // MesajJoc
            // 
            MesajJoc.AutoSize = true;
            MesajJoc.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MesajJoc.Location = new Point(868, 303);
            MesajJoc.Name = "MesajJoc";
            MesajJoc.Size = new Size(0, 25);
            MesajJoc.TabIndex = 6;
            // 
            // SpaceWar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CornflowerBlue;
            ClientSize = new Size(1090, 585);
            Controls.Add(MesajJoc);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnPauza);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(panel1);
            Name = "SpaceWar";
            Text = "SpaceWar";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picNava).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnStart;
        private Button btnStop;
        private Button btnPauza;
        private Label label1;
        private Label label2;
        private PictureBox picNava;
        private Label MesajJoc;
    }
}