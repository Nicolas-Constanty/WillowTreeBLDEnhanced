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
    partial class ucAmmo
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
            this.AmmoTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel9 = new WillowTree.CustomControls.WTGroupBox();
            this.MenuAmmo = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AmmoTree = new WillowTree.CustomControls.WTTreeView();
            this.groupPanel15 = new WillowTree.CustomControls.WTGroupBox();
            this.AmmoSDULevel = new WillowTree.CustomControls.WTNumericUpDown();
            this.AmmoPoolRemaining = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelAmmoSDULevel = new WillowTree.CustomControls.WTLabel();
            this.labelAmmoRemaining = new WillowTree.CustomControls.WTLabel();
            this.AmmoTab.SuspendLayout();
            this.groupPanel9.SuspendLayout();
            this.MenuAmmo.SuspendLayout();
            this.groupPanel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmmoSDULevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmmoPoolRemaining)).BeginInit();
            this.SuspendLayout();
            // 
            // AmmoTab
            // 
            this.AmmoTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AmmoTab.Controls.Add(this.groupPanel9);
            this.AmmoTab.Controls.Add(this.groupPanel15);
            this.AmmoTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmmoTab.ForeColor = System.Drawing.Color.White;
            this.AmmoTab.Location = new System.Drawing.Point(0, 0);
            this.AmmoTab.Name = "AmmoTab";
            this.AmmoTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.AmmoTab.Size = new System.Drawing.Size(956, 591);
            this.AmmoTab.TabIndex = 5;
            this.AmmoTab.Text = "Ammo Pools";
            // 
            // groupPanel9
            // 
            this.groupPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel9.Controls.Add(this.MenuAmmo);
            this.groupPanel9.Controls.Add(this.AmmoTree);
            this.groupPanel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel9.Location = new System.Drawing.Point(3, 3);
            this.groupPanel9.Name = "groupPanel9";
            this.groupPanel9.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel9.Size = new System.Drawing.Size(273, 585);
            this.groupPanel9.TabIndex = 24;
            this.groupPanel9.TabStop = false;
            this.groupPanel9.Text = "Current Ammo Pools";
            // 
            // MenuAmmo
            // 
            this.MenuAmmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuAmmo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem2,
            this.deleteToolStripMenuItem2});
            this.MenuAmmo.Location = new System.Drawing.Point(6, 23);
            this.MenuAmmo.Name = "MenuAmmo";
            this.MenuAmmo.Size = new System.Drawing.Size(261, 24);
            this.MenuAmmo.TabIndex = 31;
            this.MenuAmmo.Text = "menuStrip1";
            // 
            // newToolStripMenuItem2
            // 
            this.newToolStripMenuItem2.Name = "newToolStripMenuItem2";
            this.newToolStripMenuItem2.Size = new System.Drawing.Size(41, 20);
            this.newToolStripMenuItem2.Text = "New";
            this.newToolStripMenuItem2.Click += new System.EventHandler(this.NewAmmo_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(50, 20);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.DeleteAmmo_Click);
            // 
            // AmmoTree
            // 
            this.AmmoTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.AmmoTree.AllowDrop = true;
            this.AmmoTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.AmmoTree.DefaultToolTipProvider = null;
            this.AmmoTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.AmmoTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmmoTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.AmmoTree.Location = new System.Drawing.Point(6, 47);
            this.AmmoTree.Name = "AmmoTree";
            this.AmmoTree.SelectedNode = null;
            this.AmmoTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.AmmoTree.Size = new System.Drawing.Size(261, 532);
            this.AmmoTree.TabIndex = 20;
            this.AmmoTree.Text = "advTree1";
            this.AmmoTree.SelectionChanged += new System.EventHandler(this.AmmoTree_SelectionChanged);
            this.AmmoTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AmmoTree_KeyDown);
            // 
            // groupPanel15
            // 
            this.groupPanel15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel15.Controls.Add(this.AmmoSDULevel);
            this.groupPanel15.Controls.Add(this.AmmoPoolRemaining);
            this.groupPanel15.Controls.Add(this.labelAmmoSDULevel);
            this.groupPanel15.Controls.Add(this.labelAmmoRemaining);
            this.groupPanel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel15.Location = new System.Drawing.Point(282, 3);
            this.groupPanel15.Name = "groupPanel15";
            this.groupPanel15.Padding = new System.Windows.Forms.Padding(6);
            this.groupPanel15.Size = new System.Drawing.Size(671, 160);
            this.groupPanel15.TabIndex = 25;
            this.groupPanel15.TabStop = false;
            this.groupPanel15.Text = "Selected Ammo";
            // 
            // AmmoSDULevel
            // 
            this.AmmoSDULevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmmoSDULevel.Location = new System.Drawing.Point(6, 81);
            this.AmmoSDULevel.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.AmmoSDULevel.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AmmoSDULevel.Name = "AmmoSDULevel";
            this.AmmoSDULevel.Size = new System.Drawing.Size(120, 20);
            this.AmmoSDULevel.TabIndex = 3;
            this.AmmoSDULevel.ValueChanged += new System.EventHandler(this.AmmoSDULevel_ValueChanged);
            // 
            // AmmoPoolRemaining
            // 
            this.AmmoPoolRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmmoPoolRemaining.Location = new System.Drawing.Point(6, 37);
            this.AmmoPoolRemaining.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.AmmoPoolRemaining.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.AmmoPoolRemaining.Name = "AmmoPoolRemaining";
            this.AmmoPoolRemaining.Size = new System.Drawing.Size(120, 20);
            this.AmmoPoolRemaining.TabIndex = 2;
            this.AmmoPoolRemaining.ValueChanged += new System.EventHandler(this.AmmoPoolRemaining_ValueChanged);
            // 
            // labelAmmoSDULevel
            // 
            this.labelAmmoSDULevel.AutoSize = true;
            this.labelAmmoSDULevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmmoSDULevel.Location = new System.Drawing.Point(3, 65);
            this.labelAmmoSDULevel.Name = "labelAmmoSDULevel";
            this.labelAmmoSDULevel.Size = new System.Drawing.Size(103, 13);
            this.labelAmmoSDULevel.TabIndex = 1;
            this.labelAmmoSDULevel.Text = "SDU Upgrade Level";
            // 
            // labelAmmoRemaining
            // 
            this.labelAmmoRemaining.AutoSize = true;
            this.labelAmmoRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmmoRemaining.Location = new System.Drawing.Point(3, 21);
            this.labelAmmoRemaining.Name = "labelAmmoRemaining";
            this.labelAmmoRemaining.Size = new System.Drawing.Size(89, 13);
            this.labelAmmoRemaining.TabIndex = 0;
            this.labelAmmoRemaining.Text = "Remaining Ammo";
            // 
            // ucAmmo
            // 
            this.Controls.Add(this.AmmoTab);
            this.Name = "ucAmmo";
            this.Size = new System.Drawing.Size(956, 591);
            this.AmmoTab.ResumeLayout(false);
            this.groupPanel9.ResumeLayout(false);
            this.groupPanel9.PerformLayout();
            this.MenuAmmo.ResumeLayout(false);
            this.MenuAmmo.PerformLayout();
            this.groupPanel15.ResumeLayout(false);
            this.groupPanel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmmoSDULevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmmoPoolRemaining)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel AmmoTab;
        private CustomControls.WTGroupBox groupPanel9;
        private CustomControls.WTMenuStrip MenuAmmo;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private CustomControls.WTTreeView AmmoTree;
        private CustomControls.WTGroupBox groupPanel15;
        private CustomControls.WTNumericUpDown AmmoSDULevel;
        private CustomControls.WTNumericUpDown AmmoPoolRemaining;
        private CustomControls.WTLabel labelAmmoSDULevel;
        private CustomControls.WTLabel labelAmmoRemaining;
    }
}
