﻿namespace fishGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGameScreen));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highScoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRedPoints = new System.Windows.Forms.Label();
            this.lblBluePoints = new System.Windows.Forms.Label();
            this.txtBlue = new System.Windows.Forms.TextBox();
            this.txtRed = new System.Windows.Forms.TextBox();
            this.cgbxbMute = new System.Windows.Forms.CheckBox();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(853, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.rulesToolStripMenuItem,
            this.highScoresToolStripMenuItem,
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
            this.rulesToolStripMenuItem.Click += new System.EventHandler(this.rulesToolStripMenuItem_Click);
            // 
            // highScoresToolStripMenuItem
            // 
            this.highScoresToolStripMenuItem.Name = "highScoresToolStripMenuItem";
            this.highScoresToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.highScoresToolStripMenuItem.Text = "High Scores";
            this.highScoresToolStripMenuItem.Click += new System.EventHandler(this.highScoresToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lblRedPoints
            // 
            this.lblRedPoints.AutoSize = true;
            this.lblRedPoints.Location = new System.Drawing.Point(63, 36);
            this.lblRedPoints.Name = "lblRedPoints";
            this.lblRedPoints.Size = new System.Drawing.Size(14, 16);
            this.lblRedPoints.TabIndex = 3;
            this.lblRedPoints.Text = "0";
            // 
            // lblBluePoints
            // 
            this.lblBluePoints.AutoSize = true;
            this.lblBluePoints.Location = new System.Drawing.Point(63, 60);
            this.lblBluePoints.Name = "lblBluePoints";
            this.lblBluePoints.Size = new System.Drawing.Size(14, 16);
            this.lblBluePoints.TabIndex = 4;
            this.lblBluePoints.Text = "0";
            // 
            // txtBlue
            // 
            this.txtBlue.BackColor = System.Drawing.Color.Blue;
            this.txtBlue.ForeColor = System.Drawing.SystemColors.Window;
            this.txtBlue.Location = new System.Drawing.Point(18, 58);
            this.txtBlue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBlue.Name = "txtBlue";
            this.txtBlue.Size = new System.Drawing.Size(45, 22);
            this.txtBlue.TabIndex = 6;
            this.txtBlue.TextChanged += new System.EventHandler(this.txtBlue_TextChanged);
            // 
            // txtRed
            // 
            this.txtRed.BackColor = System.Drawing.Color.Red;
            this.txtRed.ForeColor = System.Drawing.SystemColors.Window;
            this.txtRed.Location = new System.Drawing.Point(18, 34);
            this.txtRed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRed.Name = "txtRed";
            this.txtRed.Size = new System.Drawing.Size(45, 22);
            this.txtRed.TabIndex = 7;
            this.txtRed.TextChanged += new System.EventHandler(this.txtRed_TextChanged);
            // 
            // cgbxbMute
            // 
            this.cgbxbMute.AutoSize = true;
            this.cgbxbMute.BackColor = System.Drawing.SystemColors.MenuBar;
            this.cgbxbMute.Location = new System.Drawing.Point(790, 7);
            this.cgbxbMute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cgbxbMute.Name = "cgbxbMute";
            this.cgbxbMute.Size = new System.Drawing.Size(58, 20);
            this.cgbxbMute.TabIndex = 8;
            this.cgbxbMute.Text = "Mute";
            this.cgbxbMute.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cgbxbMute.UseVisualStyleBackColor = false;
            this.cgbxbMute.CheckedChanged += new System.EventHandler(this.cgbxbMute_CheckedChanged);
            // 
            // frmGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 646);
            this.Controls.Add(this.cgbxbMute);
            this.Controls.Add(this.txtRed);
            this.Controls.Add(this.txtBlue);
            this.Controls.Add(this.lblBluePoints);
            this.Controls.Add(this.lblRedPoints);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmGameScreen";
            this.RightToLeftLayout = true;
            this.Text = "Oi! That\'s My Sea Creature";
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
        private System.Windows.Forms.Label lblRedPoints;
        private System.Windows.Forms.Label lblBluePoints;
        private System.Windows.Forms.TextBox txtBlue;
        private System.Windows.Forms.TextBox txtRed;
        private System.Windows.Forms.CheckBox cgbxbMute;
        private System.Windows.Forms.ToolStripMenuItem highScoresToolStripMenuItem;
    }
}

