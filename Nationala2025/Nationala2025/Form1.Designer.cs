namespace Nationala2025
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailAdmin = new System.Windows.Forms.TextBox();
            this.txtParolaAdmin = new System.Windows.Forms.TextBox();
            this.btnGestioneaza = new System.Windows.Forms.Button();
            this.btnIntra = new System.Windows.Forms.Button();
            this.btnStartCamera = new System.Windows.Forms.Button();
            this.btnDetecteaza = new System.Windows.Forms.Button();
            this.btnSalvare = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNumeControlor = new System.Windows.Forms.TextBox();
            this.pbCaptura = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUtilizator = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptura)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(849, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Introduceti datele contului de admin. pentru a adauga utilizatori sau conectati v" +
    "a cu un utilizator existent!\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cont administrator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Parola administrator";
            // 
            // txtEmailAdmin
            // 
            this.txtEmailAdmin.Location = new System.Drawing.Point(280, 80);
            this.txtEmailAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmailAdmin.Name = "txtEmailAdmin";
            this.txtEmailAdmin.Size = new System.Drawing.Size(208, 22);
            this.txtEmailAdmin.TabIndex = 3;
            this.txtEmailAdmin.Text = "onti@csharp.ro";
            // 
            // txtParolaAdmin
            // 
            this.txtParolaAdmin.Location = new System.Drawing.Point(280, 129);
            this.txtParolaAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.txtParolaAdmin.Name = "txtParolaAdmin";
            this.txtParolaAdmin.Size = new System.Drawing.Size(208, 22);
            this.txtParolaAdmin.TabIndex = 4;
            this.txtParolaAdmin.Text = "ONTI2025";
            this.txtParolaAdmin.UseSystemPasswordChar = true;
            // 
            // btnGestioneaza
            // 
            this.btnGestioneaza.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnGestioneaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestioneaza.Location = new System.Drawing.Point(561, 74);
            this.btnGestioneaza.Margin = new System.Windows.Forms.Padding(4);
            this.btnGestioneaza.Name = "btnGestioneaza";
            this.btnGestioneaza.Size = new System.Drawing.Size(240, 80);
            this.btnGestioneaza.TabIndex = 5;
            this.btnGestioneaza.Text = "Gestioneza controlori";
            this.btnGestioneaza.UseVisualStyleBackColor = false;
            this.btnGestioneaza.Click += new System.EventHandler(this.btnGestioneaza_Click);
            // 
            // btnIntra
            // 
            this.btnIntra.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnIntra.Enabled = false;
            this.btnIntra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntra.Location = new System.Drawing.Point(897, 74);
            this.btnIntra.Margin = new System.Windows.Forms.Padding(4);
            this.btnIntra.Name = "btnIntra";
            this.btnIntra.Size = new System.Drawing.Size(240, 80);
            this.btnIntra.TabIndex = 6;
            this.btnIntra.Text = "Intra in aplicatie";
            this.btnIntra.UseVisualStyleBackColor = false;
            this.btnIntra.Click += new System.EventHandler(this.btnIntra_Click);
            // 
            // btnStartCamera
            // 
            this.btnStartCamera.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnStartCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartCamera.Location = new System.Drawing.Point(96, 495);
            this.btnStartCamera.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartCamera.Name = "btnStartCamera";
            this.btnStartCamera.Size = new System.Drawing.Size(160, 44);
            this.btnStartCamera.TabIndex = 7;
            this.btnStartCamera.Text = "Start camera";
            this.btnStartCamera.UseVisualStyleBackColor = false;
            this.btnStartCamera.Click += new System.EventHandler(this.btnStartCamera_Click);
            // 
            // btnDetecteaza
            // 
            this.btnDetecteaza.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnDetecteaza.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetecteaza.Location = new System.Drawing.Point(257, 495);
            this.btnDetecteaza.Margin = new System.Windows.Forms.Padding(4);
            this.btnDetecteaza.Name = "btnDetecteaza";
            this.btnDetecteaza.Size = new System.Drawing.Size(160, 44);
            this.btnDetecteaza.TabIndex = 8;
            this.btnDetecteaza.Text = "Detecteaza";
            this.btnDetecteaza.UseVisualStyleBackColor = false;
            this.btnDetecteaza.Click += new System.EventHandler(this.btnDetecteaza_Click);
            // 
            // btnSalvare
            // 
            this.btnSalvare.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSalvare.Enabled = false;
            this.btnSalvare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvare.Location = new System.Drawing.Point(383, 26);
            this.btnSalvare.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalvare.Name = "btnSalvare";
            this.btnSalvare.Size = new System.Drawing.Size(165, 53);
            this.btnSalvare.TabIndex = 11;
            this.btnSalvare.Text = "Salvare captura";
            this.btnSalvare.UseVisualStyleBackColor = false;
            this.btnSalvare.Click += new System.EventHandler(this.btnSalvare_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbCamera);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(51, 218);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(439, 270);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detecteaza fata";
            // 
            // pbCamera
            // 
            this.pbCamera.Location = new System.Drawing.Point(10, 27);
            this.pbCamera.Margin = new System.Windows.Forms.Padding(4);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Size = new System.Drawing.Size(421, 230);
            this.pbCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCamera.TabIndex = 1;
            this.pbCamera.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNumeControlor);
            this.groupBox2.Controls.Add(this.pbCaptura);
            this.groupBox2.Controls.Add(this.btnSalvare);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(561, 202);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(556, 337);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adauga fata";
            // 
            // txtNumeControlor
            // 
            this.txtNumeControlor.Location = new System.Drawing.Point(78, 43);
            this.txtNumeControlor.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeControlor.Name = "txtNumeControlor";
            this.txtNumeControlor.Size = new System.Drawing.Size(208, 26);
            this.txtNumeControlor.TabIndex = 15;
            // 
            // pbCaptura
            // 
            this.pbCaptura.Location = new System.Drawing.Point(9, 86);
            this.pbCaptura.Margin = new System.Windows.Forms.Padding(4);
            this.pbCaptura.Name = "pbCaptura";
            this.pbCaptura.Size = new System.Drawing.Size(421, 230);
            this.pbCaptura.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCaptura.TabIndex = 0;
            this.pbCaptura.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "Nume";
            // 
            // lblUtilizator
            // 
            this.lblUtilizator.AutoSize = true;
            this.lblUtilizator.Location = new System.Drawing.Point(944, 179);
            this.lblUtilizator.Name = "lblUtilizator";
            this.lblUtilizator.Size = new System.Drawing.Size(44, 16);
            this.lblUtilizator.TabIndex = 14;
            this.lblUtilizator.Text = "label5";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1189, 554);
            this.Controls.Add(this.lblUtilizator);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDetecteaza);
            this.Controls.Add(this.btnStartCamera);
            this.Controls.Add(this.btnIntra);
            this.Controls.Add(this.btnGestioneaza);
            this.Controls.Add(this.txtParolaAdmin);
            this.Controls.Add(this.txtEmailAdmin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Autentificare";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmailAdmin;
        private System.Windows.Forms.TextBox txtParolaAdmin;
        private System.Windows.Forms.Button btnGestioneaza;
        private System.Windows.Forms.Button btnIntra;
        private System.Windows.Forms.Button btnStartCamera;
        private System.Windows.Forms.Button btnDetecteaza;
        private System.Windows.Forms.Button btnSalvare;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNumeControlor;
        private System.Windows.Forms.PictureBox pbCaptura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Label lblUtilizator;
        private System.Windows.Forms.Timer timer1;
    }
}

