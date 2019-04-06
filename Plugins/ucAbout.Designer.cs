/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
 *  Copyright (C) 2010, 2011 XanderChaos
 *  Copyright (C) 2011 Thomas Kaiser
 *  Copyright (C) 2010 JackSchitt
 * 
 *  WillowTree# is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  WillowTree# is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with WillowTree#.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace WillowTree.Plugins
{
    partial class ucAbout
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAbout));
            this.MainTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel7 = new WillowTree.CustomControls.WTGroupBox();
            this.textBoxX1 = new WillowTree.CustomControls.WTTextBox();
            this.UpdateButton = new WillowTree.CustomControls.WTButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MainTab.SuspendLayout();
            this.groupPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainTab.Controls.Add(this.groupPanel7);
            this.MainTab.Controls.Add(this.UpdateButton);
            this.MainTab.Controls.Add(this.pictureBox1);
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTab.ForeColor = System.Drawing.Color.White;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Name = "MainTab";
            this.MainTab.Size = new System.Drawing.Size(956, 591);
            this.MainTab.TabIndex = 9;
            this.MainTab.Text = "About";
            // 
            // groupPanel7
            // 
            this.groupPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel7.Controls.Add(this.textBoxX1);
            this.groupPanel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel7.Location = new System.Drawing.Point(379, 56);
            this.groupPanel7.Name = "groupPanel7";
            this.groupPanel7.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel7.Size = new System.Drawing.Size(563, 403);
            this.groupPanel7.TabIndex = 1;
            this.groupPanel7.TabStop = false;
            this.groupPanel7.Text = "Welcome to WillowTree#";
            // 
            // textBoxX1
            // 
            this.textBoxX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxX1.Location = new System.Drawing.Point(6, 23);
            this.textBoxX1.Multiline = true;
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.ReadOnly = true;
            this.textBoxX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxX1.Size = new System.Drawing.Size(551, 374);
            this.textBoxX1.TabIndex = 0;
            this.textBoxX1.Text = resources.GetString("textBoxX1.Text");
            // 
            // UpdateButton
            // 
            this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.UpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.Location = new System.Drawing.Point(0, 0);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(956, 33);
            this.UpdateButton.TabIndex = 2;
            this.UpdateButton.Text = "There is a new version of WillowTree# available! Click here to download.";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(979, 503);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucAbout
            // 
            this.Controls.Add(this.MainTab);
            this.Name = "ucAbout";
            this.Size = new System.Drawing.Size(956, 591);
            this.MainTab.ResumeLayout(false);
            this.groupPanel7.ResumeLayout(false);
            this.groupPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel MainTab;
        private CustomControls.WTGroupBox groupPanel7;
        private CustomControls.WTTextBox textBoxX1;
        public CustomControls.WTButton UpdateButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}
