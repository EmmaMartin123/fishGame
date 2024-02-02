namespace fishGame
{
    partial class frmGameScreen
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRedLabel = new System.Windows.Forms.Label();
            this.lblBlueLabel = new System.Windows.Forms.Label();
            this.lblRedPoints = new System.Windows.Forms.Label();
            this.lblBluePoints = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(853, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.rulesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // rulesToolStripMenuItem
            // 
            this.rulesToolStripMenuItem.Name = "rulesToolStripMenuItem";
            this.rulesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rulesToolStripMenuItem.Text = "Rules";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblRedLabel
            // 
            this.lblRedLabel.AutoSize = true;
            this.lblRedLabel.Location = new System.Drawing.Point(12, 38);
            this.lblRedLabel.Name = "lblRedLabel";
            this.lblRedLabel.Size = new System.Drawing.Size(45, 20);
            this.lblRedLabel.TabIndex = 1;
            this.lblRedLabel.Text = "Red:";
            // 
            // lblBlueLabel
            // 
            this.lblBlueLabel.AutoSize = true;
            this.lblBlueLabel.Location = new System.Drawing.Point(12, 58);
            this.lblBlueLabel.Name = "lblBlueLabel";
            this.lblBlueLabel.Size = new System.Drawing.Size(46, 20);
            this.lblBlueLabel.TabIndex = 2;
            this.lblBlueLabel.Text = "Blue:";
            // 
            // lblRedPoints
            // 
            this.lblRedPoints.AutoSize = true;
            this.lblRedPoints.Location = new System.Drawing.Point(63, 38);
            this.lblRedPoints.Name = "lblRedPoints";
            this.lblRedPoints.Size = new System.Drawing.Size(18, 20);
            this.lblRedPoints.TabIndex = 3;
            this.lblRedPoints.Text = "0";
            // 
            // lblBluePoints
            // 
            this.lblBluePoints.AutoSize = true;
            this.lblBluePoints.Location = new System.Drawing.Point(63, 58);
            this.lblBluePoints.Name = "lblBluePoints";
            this.lblBluePoints.Size = new System.Drawing.Size(14, 16);
            this.lblBluePoints.TabIndex = 4;
            this.lblBluePoints.Text = "0";
            // 
            // frmGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 646);
            this.Controls.Add(this.lblBluePoints);
            this.Controls.Add(this.lblRedPoints);
            this.Controls.Add(this.lblBlueLabel);
            this.Controls.Add(this.lblRedLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmGameScreen";
            this.Text = "gameScreen";
            this.Load += new System.EventHandler(this.frmGameScreen_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblRedLabel;
        private System.Windows.Forms.Label lblBlueLabel;
        private System.Windows.Forms.Label lblRedPoints;
        private System.Windows.Forms.Label lblBluePoints;
    }
}

