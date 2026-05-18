namespace cosmos2
{
    partial class Revolutie
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
            picFundal = new PictureBox();
            picPamant = new PictureBox();
            picSoare = new PictureBox();
            picLuna = new PictureBox();
            startBtn = new Button();
            StopBtn = new Button();
            lblData = new Label();
            lblDistanta = new Label();
            lblAnotimp = new Label();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)picFundal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPamant).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSoare).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLuna).BeginInit();
            SuspendLayout();
            // 
            // picFundal
            // 
            picFundal.Image = Properties.Resources.Back;
            picFundal.Location = new Point(12, 12);
            picFundal.Name = "picFundal";
            picFundal.Size = new Size(800, 600);
            picFundal.SizeMode = PictureBoxSizeMode.StretchImage;
            picFundal.TabIndex = 1;
            picFundal.TabStop = false;
            // 
            // picPamant
            // 
            picPamant.Location = new Point(121, 112);
            picPamant.Name = "picPamant";
            picPamant.Size = new Size(100, 50);
            picPamant.SizeMode = PictureBoxSizeMode.StretchImage;
            picPamant.TabIndex = 2;
            picPamant.TabStop = false;
            // 
            // picSoare
            // 
            picSoare.Location = new Point(237, 276);
            picSoare.Name = "picSoare";
            picSoare.Size = new Size(100, 50);
            picSoare.SizeMode = PictureBoxSizeMode.StretchImage;
            picSoare.TabIndex = 3;
            picSoare.TabStop = false;
            // 
            // picLuna
            // 
            picLuna.Location = new Point(634, 185);
            picLuna.Name = "picLuna";
            picLuna.Size = new Size(50, 50);
            picLuna.TabIndex = 4;
            picLuna.TabStop = false;
            // 
            // startBtn
            // 
            startBtn.BackColor = Color.RoyalBlue;
            startBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            startBtn.ForeColor = SystemColors.ActiveCaptionText;
            startBtn.Location = new Point(861, 43);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(209, 30);
            startBtn.TabIndex = 5;
            startBtn.Text = "Start";
            startBtn.UseVisualStyleBackColor = false;
            startBtn.Click += startBtn_Click;
            // 
            // StopBtn
            // 
            StopBtn.BackColor = Color.RoyalBlue;
            StopBtn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            StopBtn.ForeColor = SystemColors.ActiveCaptionText;
            StopBtn.Location = new Point(861, 89);
            StopBtn.Name = "StopBtn";
            StopBtn.Size = new Size(209, 30);
            StopBtn.TabIndex = 6;
            StopBtn.Text = "Stop";
            StopBtn.UseVisualStyleBackColor = false;
            StopBtn.Click += StopBtn_Click;
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblData.ForeColor = SystemColors.ActiveCaptionText;
            lblData.Location = new Point(861, 197);
            lblData.Name = "lblData";
            lblData.Size = new Size(50, 21);
            lblData.TabIndex = 7;
            lblData.Text = "Data:";
            lblData.Click += lblData_Click;
            // 
            // lblDistanta
            // 
            lblDistanta.AutoSize = true;
            lblDistanta.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDistanta.ForeColor = SystemColors.ActiveCaptionText;
            lblDistanta.Location = new Point(861, 252);
            lblDistanta.Name = "lblDistanta";
            lblDistanta.Size = new Size(96, 21);
            lblDistanta.TabIndex = 8;
            lblDistanta.Text = "Anotimpul:";
            lblDistanta.Click += lblDistanta_Click;
            // 
            // lblAnotimp
            // 
            lblAnotimp.AutoSize = true;
            lblAnotimp.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAnotimp.ForeColor = SystemColors.ActiveCaptionText;
            lblAnotimp.Location = new Point(861, 320);
            lblAnotimp.Name = "lblAnotimp";
            lblAnotimp.Size = new Size(78, 21);
            lblAnotimp.TabIndex = 9;
            lblAnotimp.Text = "Distanța:";
            lblAnotimp.Click += lblAnotimp_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.RoyalBlue;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.ForeColor = SystemColors.ActiveCaptionText;
            btnClose.Location = new Point(861, 465);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(209, 30);
            btnClose.TabIndex = 10;
            btnClose.Text = "Închidere";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // Revolutie
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CornflowerBlue;
            ClientSize = new Size(1100, 626);
            Controls.Add(btnClose);
            Controls.Add(lblAnotimp);
            Controls.Add(lblDistanta);
            Controls.Add(lblData);
            Controls.Add(StopBtn);
            Controls.Add(startBtn);
            Controls.Add(picLuna);
            Controls.Add(picSoare);
            Controls.Add(picPamant);
            Controls.Add(picFundal);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Revolutie";
            Text = "Revolutie";
            Shown += Revolutie_Shown;
            ((System.ComponentModel.ISupportInitialize)picFundal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPamant).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSoare).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLuna).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox picFundal;
        private PictureBox picPamant;
        private PictureBox picSoare;
        private PictureBox picLuna;
        private Button startBtn;
        private Label label1;
        private Button StopBtn;
        private Label lblData;
        private Label lblDistanta;
        private Label lblAnotimp;
        private Button btnClose;
    }
}