namespace Nationala2025
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optiuniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnulDeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ghideazaAterizareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozaVânturilorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ieșireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optiuniToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1294, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optiuniToolStripMenuItem
            // 
            this.optiuniToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.turnulDeControlToolStripMenuItem,
            this.ghideazaAterizareToolStripMenuItem,
            this.rozaVânturilorToolStripMenuItem,
            this.ieșireToolStripMenuItem});
            this.optiuniToolStripMenuItem.Name = "optiuniToolStripMenuItem";
            this.optiuniToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.optiuniToolStripMenuItem.Text = "Optiuni";
            // 
            // turnulDeControlToolStripMenuItem
            // 
            this.turnulDeControlToolStripMenuItem.Name = "turnulDeControlToolStripMenuItem";
            this.turnulDeControlToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.turnulDeControlToolStripMenuItem.Text = "Turnul de control";
            this.turnulDeControlToolStripMenuItem.Click += new System.EventHandler(this.turnulDeControlToolStripMenuItem_Click);
            // 
            // ghideazaAterizareToolStripMenuItem
            // 
            this.ghideazaAterizareToolStripMenuItem.Name = "ghideazaAterizareToolStripMenuItem";
            this.ghideazaAterizareToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ghideazaAterizareToolStripMenuItem.Text = "Ghideaza aterizare";
            this.ghideazaAterizareToolStripMenuItem.Click += new System.EventHandler(this.ghideazaAterizareToolStripMenuItem_Click);
            // 
            // rozaVânturilorToolStripMenuItem
            // 
            this.rozaVânturilorToolStripMenuItem.Name = "rozaVânturilorToolStripMenuItem";
            this.rozaVânturilorToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.rozaVânturilorToolStripMenuItem.Text = "Roza vânturilor";
            this.rozaVânturilorToolStripMenuItem.Click += new System.EventHandler(this.rozaVânturilorToolStripMenuItem_Click);
            // 
            // ieșireToolStripMenuItem
            // 
            this.ieșireToolStripMenuItem.Name = "ieșireToolStripMenuItem";
            this.ieșireToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ieșireToolStripMenuItem.Text = "Ieșire";
            this.ieșireToolStripMenuItem.Click += new System.EventHandler(this.ieșireToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(263, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 80);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aplicația este un centru de comandă digital complet, \r\ncare îmbină securitatea\r\nb" +
    "iometrică, analiza meteo și coordonarea traficului \r\naerian într-o singură inter" +
    "față intuitivă și eficientă";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1195, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 63);
            this.button2.TabIndex = 12;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1294, 563);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optiuniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnulDeControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ghideazaAterizareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rozaVânturilorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ieșireToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}