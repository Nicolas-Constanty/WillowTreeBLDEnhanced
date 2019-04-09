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
    partial class ucDebug
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
            this.DebugTab = new WillowTree.CustomControls.CCPanel();
            this.LockerTreetry2 = new WillowTree.CustomControls.WTTreeView();
            this.InOutPartsBox = new WillowTree.CustomControls.WTTextBox();
            this.DebugText1 = new WillowTree.CustomControls.WTTextBox();
            this.Version = new WillowTree.CustomControls.WTLabel();
            this.DebugButton1 = new WillowTree.CustomControls.WTButton();
            this.Breakpoint = new WillowTree.CustomControls.WTButton();
            this.DebugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // DebugTab
            // 
            this.DebugTab.Controls.Add(this.LockerTreetry2);
            this.DebugTab.Controls.Add(this.InOutPartsBox);
            this.DebugTab.Controls.Add(this.DebugText1);
            this.DebugTab.Controls.Add(this.Version);
            this.DebugTab.Controls.Add(this.DebugButton1);
            this.DebugTab.Controls.Add(this.Breakpoint);
            this.DebugTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebugTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugTab.Location = new System.Drawing.Point(0, 0);
            this.DebugTab.Name = "DebugTab";
            this.DebugTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.DebugTab.Size = new System.Drawing.Size(956, 591);
            this.DebugTab.TabIndex = 8;
            this.DebugTab.Text = "Debug";
            // 
            // LockerTreetry2
            // 
            this.LockerTreetry2.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.LockerTreetry2.AllowDrop = true;
            this.LockerTreetry2.DefaultToolTipProvider = null;
            this.LockerTreetry2.DragDropMarkColor = System.Drawing.Color.Black;
            this.LockerTreetry2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerTreetry2.Indent = 12;
            this.LockerTreetry2.LineColor = System.Drawing.SystemColors.ControlDark;
            this.LockerTreetry2.Location = new System.Drawing.Point(9, 28);
            this.LockerTreetry2.Name = "LockerTreetry2";
            this.LockerTreetry2.SelectedNode = null;
            this.LockerTreetry2.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.LockerTreetry2.Size = new System.Drawing.Size(392, 460);
            this.LockerTreetry2.TabIndex = 39;
            this.LockerTreetry2.Text = "advTree1";
            // 
            // InOutPartsBox
            // 
            this.InOutPartsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InOutPartsBox.Location = new System.Drawing.Point(639, 241);
            this.InOutPartsBox.Multiline = true;
            this.InOutPartsBox.Name = "InOutPartsBox";
            this.InOutPartsBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InOutPartsBox.Size = new System.Drawing.Size(277, 268);
            this.InOutPartsBox.TabIndex = 38;
            this.InOutPartsBox.TabStop = false;
            // 
            // DebugText1
            // 
            this.DebugText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugText1.Location = new System.Drawing.Point(639, 28);
            this.DebugText1.Multiline = true;
            this.DebugText1.Name = "DebugText1";
            this.DebugText1.Size = new System.Drawing.Size(300, 169);
            this.DebugText1.TabIndex = 32;
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.Location = new System.Drawing.Point(565, 272);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(31, 13);
            this.Version.TabIndex = 37;
            this.Version.Text = "2.2.1";
            // 
            // DebugButton1
            // 
            this.DebugButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugButton1.Location = new System.Drawing.Point(9, 505);
            this.DebugButton1.Name = "DebugButton1";
            this.DebugButton1.Size = new System.Drawing.Size(75, 23);
            this.DebugButton1.TabIndex = 34;
            this.DebugButton1.Text = "button1";
            this.DebugButton1.UseVisualStyleBackColor = false;
            this.DebugButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Breakpoint
            // 
            this.Breakpoint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Breakpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Breakpoint.Location = new System.Drawing.Point(492, 392);
            this.Breakpoint.Name = "Breakpoint";
            this.Breakpoint.Size = new System.Drawing.Size(104, 20);
            this.Breakpoint.TabIndex = 33;
            this.Breakpoint.Text = "ForceBreakpoint";
            this.Breakpoint.UseVisualStyleBackColor = false;
            this.Breakpoint.Click += new System.EventHandler(this.Breakpoint_Click);
            // 
            // ucDebug
            // 
            this.Controls.Add(this.DebugTab);
            this.Name = "ucDebug";
            this.Size = new System.Drawing.Size(956, 591);
            this.DebugTab.ResumeLayout(false);
            this.DebugTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public CustomControls.CCPanel DebugTab;
        public CustomControls.WTTreeView LockerTreetry2;
        public CustomControls.WTTextBox InOutPartsBox;
        public CustomControls.WTTextBox DebugText1;
        public CustomControls.WTLabel Version;
        public CustomControls.WTButton DebugButton1;
        public CustomControls.WTButton Breakpoint;
    }
}
