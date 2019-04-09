/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
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
    partial class SamplePlugin
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
            this.wtGroupBox1 = new WillowTree.CustomControls.WTGroupBox();
            this.wtButton1 = new WillowTree.CustomControls.WTButton();
            this.wtMenuStrip1 = new WillowTree.CustomControls.WTMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wtTextBox1 = new WillowTree.CustomControls.WTTextBox();
            this.wtGroupBox1.SuspendLayout();
            this.wtMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wtGroupBox1
            // 
            this.wtGroupBox1.Controls.Add(this.wtButton1);
            this.wtGroupBox1.Controls.Add(this.wtMenuStrip1);
            this.wtGroupBox1.Controls.Add(this.wtTextBox1);
            this.wtGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wtGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.wtGroupBox1.Name = "wtGroupBox1";
            this.wtGroupBox1.Size = new System.Drawing.Size(488, 446);
            this.wtGroupBox1.TabIndex = 1;
            this.wtGroupBox1.Text = "Plugin Stuff";
            // 
            // wtButton1
            // 
            this.wtButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wtButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wtButton1.Location = new System.Drawing.Point(6, 400);
            this.wtButton1.Name = "wtButton1";
            this.wtButton1.Size = new System.Drawing.Size(479, 23);
            this.wtButton1.TabIndex = 0;
            this.wtButton1.Text = "Dump AppThemes Reflection Info";
            this.wtButton1.UseVisualStyleBackColor = false;
            this.wtButton1.Click += new System.EventHandler(this.wtButton1_Click);
            // 
            // wtMenuStrip1
            // 
            this.wtMenuStrip1.AutoSize = false;
            this.wtMenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.wtMenuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wtMenuStrip1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.wtMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.wtMenuStrip1.Location = new System.Drawing.Point(6, 23);
            this.wtMenuStrip1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.wtMenuStrip1.Name = "wtMenuStrip1";
            this.wtMenuStrip1.Padding = new System.Windows.Forms.Padding(6);
            this.wtMenuStrip1.Size = new System.Drawing.Size(476, 29);
            this.wtMenuStrip1.TabIndex = 8;
            this.wtMenuStrip1.Text = "wtMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 17);
            this.fileToolStripMenuItem.Text = "&Log";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // wtTextBox1
            // 
            this.wtTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wtTextBox1.Location = new System.Drawing.Point(6, 52);
            this.wtTextBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.wtTextBox1.Multiline = true;
            this.wtTextBox1.Name = "wtTextBox1";
            this.wtTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wtTextBox1.Size = new System.Drawing.Size(476, 345);
            this.wtTextBox1.TabIndex = 17;
            // 
            // SamplePlugin
            // 
            this.Controls.Add(this.wtGroupBox1);
            this.Name = "SamplePlugin";
            this.Size = new System.Drawing.Size(533, 452);
            this.wtGroupBox1.ResumeLayout(false);
            this.wtGroupBox1.PerformLayout();
            this.wtMenuStrip1.ResumeLayout(false);
            this.wtMenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.WTGroupBox wtGroupBox1;
        private CustomControls.WTMenuStrip wtMenuStrip1;
        private CustomControls.WTTextBox wtTextBox1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private CustomControls.WTButton wtButton1;

    }
}
