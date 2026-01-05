namespace JocEducativ
{
    partial class SarpeEducativ
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
            stopBtn = new Button();
            panelGame = new Panel();
            startBtn = new Button();
            lblPunctaj = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // stopBtn
            // 
            stopBtn.BackColor = Color.RoyalBlue;
            stopBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            stopBtn.Location = new Point(655, 478);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new Size(92, 34);
            stopBtn.TabIndex = 0;
            stopBtn.Text = "Stop Joc";
            stopBtn.UseVisualStyleBackColor = false;
            stopBtn.Click += button1_Click;
            // 
            // panelGame
            // 
            panelGame.Location = new Point(12, 12);
            panelGame.Name = "panelGame";
            panelGame.Size = new Size(500, 500);
            panelGame.TabIndex = 2;
            // 
            // startBtn
            // 
            startBtn.BackColor = Color.RoyalBlue;
            startBtn.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            startBtn.Location = new Point(543, 478);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(92, 34);
            startBtn.TabIndex = 3;
            startBtn.Text = "Start";
            startBtn.UseVisualStyleBackColor = false;
            startBtn.Click += startBtn_Click;
            // 
            // lblPunctaj
            // 
            lblPunctaj.AutoSize = true;
            lblPunctaj.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPunctaj.Location = new Point(543, 175);
            lblPunctaj.Name = "lblPunctaj";
            lblPunctaj.Size = new Size(100, 25);
            lblPunctaj.TabIndex = 4;
            lblPunctaj.Text = "Punctaj: 0";
            // 
            // button1
            // 
            button1.BackColor = Color.RoyalBlue;
            button1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            button1.Location = new Point(600, 423);
            button1.Name = "button1";
            button1.Size = new Size(92, 34);
            button1.TabIndex = 5;
            button1.Text = "Exit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // SarpeEducativ
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.RoyalBlue;
            ClientSize = new Size(767, 529);
            Controls.Add(button1);
            Controls.Add(lblPunctaj);
            Controls.Add(startBtn);
            Controls.Add(panelGame);
            Controls.Add(stopBtn);
            Name = "SarpeEducativ";
            Text = "Sarpe";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button stopBtn;
        private Panel panelGame;
        private Button startBtn;
        private Label lblPunctaj;
        private Button button1;
    }
}