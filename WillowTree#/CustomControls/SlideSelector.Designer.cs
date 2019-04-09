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

namespace WillowTree.CustomControls
{
    partial class SlideSelector
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
            this.SliderLabel = new WillowTree.CustomControls.WTLabel();
            this.UpDown = new WillowTree.CustomControls.WTNumericUpDown();
            this.Slider = new WillowTree.CustomControls.Slider();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // SliderLabel
            // 
            this.SliderLabel.AutoSize = true;
            this.SliderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SliderLabel.Location = new System.Drawing.Point(2, 2);
            this.SliderLabel.Name = "SliderLabel";
            this.SliderLabel.Size = new System.Drawing.Size(43, 13);
            this.SliderLabel.TabIndex = 3;
            this.SliderLabel.Text = "Caption";
            // 
            // UpDown
            // 
            this.UpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpDown.Hexadecimal = true;
            this.UpDown.Location = new System.Drawing.Point(2, 18);
            this.UpDown.Name = "UpDown";
            this.UpDown.Size = new System.Drawing.Size(179, 20);
            this.UpDown.TabIndex = 5;
            this.UpDown.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // Slider
            // 
            this.Slider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Slider.BackColor = System.Drawing.Color.Transparent;
            this.Slider.Location = new System.Drawing.Point(2, 12);
            this.Slider.Maximum = 100;
            this.Slider.Minimum = 0;
            this.Slider.Name = "Slider";
            this.Slider.Size = new System.Drawing.Size(179, 35);
            this.Slider.TabIndex = 4;
            this.Slider.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Slider.Value = 50;
            this.Slider.ValueChanged += new System.EventHandler(this.Slider_ValueChanged);
            this.Slider.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.Slider_ChangeUICues);
            // 
            // SlideSelector
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.SliderLabel);
            this.Controls.Add(this.UpDown);
            this.Controls.Add(this.Slider);
            this.Name = "SlideSelector";
            this.Size = new System.Drawing.Size(183, 51);
            ((System.ComponentModel.ISupportInitialize)(this.UpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public WillowTree.CustomControls.WTLabel SliderLabel;
        public WillowTree.CustomControls.WTNumericUpDown UpDown;
        public WillowTree.CustomControls.Slider Slider;
    }
}
