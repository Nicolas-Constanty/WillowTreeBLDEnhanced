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
    partial class ucEchoes
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
            this.EchoesTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel4 = new WillowTree.CustomControls.WTGroupBox();
            this.MenuEcho = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.EchoList = new System.Windows.Forms.ToolStripComboBox();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSelectionToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mergeFromEchoesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeFromAnotherSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cloneFromEchoesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneFromAnotherSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAnEchoListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.eraseAllEchoesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.EchoTree = new WillowTree.CustomControls.WTTreeView();
            this.groupPanel16 = new WillowTree.CustomControls.WTGroupBox();
            this.labelEchoString = new WillowTree.CustomControls.WTLabel();
            this.EchoString = new WillowTree.CustomControls.WTTextBox();
            this.EchoDLCValue2 = new WillowTree.CustomControls.WTNumericUpDown();
            this.EchoDLCValue1 = new WillowTree.CustomControls.WTNumericUpDown();
            this.labeEchoDLCValue2 = new WillowTree.CustomControls.WTLabel();
            this.labelEchoDLCValue1 = new WillowTree.CustomControls.WTLabel();
            this.EchoesTab.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.MenuEcho.SuspendLayout();
            this.groupPanel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EchoDLCValue2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EchoDLCValue1)).BeginInit();
            this.SuspendLayout();
            // 
            // EchoesTab
            // 
            this.EchoesTab.Controls.Add(this.groupPanel4);
            this.EchoesTab.Controls.Add(this.groupPanel16);
            this.EchoesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EchoesTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoesTab.ForeColor = System.Drawing.Color.White;
            this.EchoesTab.Location = new System.Drawing.Point(0, 0);
            this.EchoesTab.Name = "EchoesTab";
            this.EchoesTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.EchoesTab.Size = new System.Drawing.Size(956, 591);
            this.EchoesTab.TabIndex = 6;
            this.EchoesTab.Text = "Echo Logs";
            // 
            // groupPanel4
            // 
            this.groupPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel4.Controls.Add(this.MenuEcho);
            this.groupPanel4.Controls.Add(this.EchoTree);
            this.groupPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel4.Location = new System.Drawing.Point(3, 3);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel4.Size = new System.Drawing.Size(273, 585);
            this.groupPanel4.TabIndex = 23;
            this.groupPanel4.TabStop = false;
            this.groupPanel4.Text = "Current Echo Logs";
            // 
            // MenuEcho
            // 
            this.MenuEcho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuEcho.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.actionsStripMenuItem1,
            this.toolStripMenuItem2});
            this.MenuEcho.Location = new System.Drawing.Point(6, 23);
            this.MenuEcho.Name = "MenuEcho";
            this.MenuEcho.Size = new System.Drawing.Size(261, 24);
            this.MenuEcho.TabIndex = 30;
            this.MenuEcho.Text = "menuStrip3";
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EchoList});
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.newToolStripMenuItem1.Text = "New";
            // 
            // EchoList
            // 
            this.EchoList.DropDownHeight = 500;
            this.EchoList.DropDownWidth = 200;
            this.EchoList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoList.IntegralHeight = false;
            this.EchoList.MaxDropDownItems = 30;
            this.EchoList.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.EchoList.Name = "EchoList";
            this.EchoList.Size = new System.Drawing.Size(230, 21);
            this.EchoList.SelectedIndexChanged += new System.EventHandler(this.EchoList_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.DeleteEcho_Click);
            // 
            // actionsStripMenuItem1
            // 
            this.actionsStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllToFileToolStripMenuItem,
            this.exportSelectionToFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.mergeFromEchoesFileToolStripMenuItem,
            this.mergeFromAnotherSaveToolStripMenuItem,
            this.toolStripSeparator2,
            this.cloneFromEchoesFileToolStripMenuItem,
            this.cloneFromAnotherSaveToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1,
            this.removeAnEchoListToolStripMenuItem,
            this.toolStripSeparator4,
            this.eraseAllEchoesToolStripMenuItem});
            this.actionsStripMenuItem1.Name = "actionsStripMenuItem1";
            this.actionsStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.actionsStripMenuItem1.Text = "Actions";
            // 
            // exportAllToFileToolStripMenuItem
            // 
            this.exportAllToFileToolStripMenuItem.Name = "exportAllToFileToolStripMenuItem";
            this.exportAllToFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exportAllToFileToolStripMenuItem.Text = "Export all to file";
            this.exportAllToFileToolStripMenuItem.Click += new System.EventHandler(this.ExportEchoes_Click);
            // 
            // exportSelectionToFileToolStripMenuItem
            // 
            this.exportSelectionToFileToolStripMenuItem.Name = "exportSelectionToFileToolStripMenuItem";
            this.exportSelectionToFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exportSelectionToFileToolStripMenuItem.Text = "Export selection to file";
            this.exportSelectionToFileToolStripMenuItem.Click += new System.EventHandler(this.ExportSelectedEchoes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // mergeFromEchoesFileToolStripMenuItem
            // 
            this.mergeFromEchoesFileToolStripMenuItem.Name = "mergeFromEchoesFileToolStripMenuItem";
            this.mergeFromEchoesFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.mergeFromEchoesFileToolStripMenuItem.Text = "Merge from echoes file";
            this.mergeFromEchoesFileToolStripMenuItem.Click += new System.EventHandler(this.MergeAllFromFileEchoes_Click);
            // 
            // mergeFromAnotherSaveToolStripMenuItem
            // 
            this.mergeFromAnotherSaveToolStripMenuItem.Name = "mergeFromAnotherSaveToolStripMenuItem";
            this.mergeFromAnotherSaveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.mergeFromAnotherSaveToolStripMenuItem.Text = "Merge from another save";
            this.mergeFromAnotherSaveToolStripMenuItem.Click += new System.EventHandler(this.MergeFromSaveEchoes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // cloneFromEchoesFileToolStripMenuItem
            // 
            this.cloneFromEchoesFileToolStripMenuItem.Name = "cloneFromEchoesFileToolStripMenuItem";
            this.cloneFromEchoesFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cloneFromEchoesFileToolStripMenuItem.Text = "Clone from echoes file";
            this.cloneFromEchoesFileToolStripMenuItem.Click += new System.EventHandler(this.ImportEchoes_Click);
            // 
            // cloneFromAnotherSaveToolStripMenuItem
            // 
            this.cloneFromAnotherSaveToolStripMenuItem.Name = "cloneFromAnotherSaveToolStripMenuItem";
            this.cloneFromAnotherSaveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cloneFromAnotherSaveToolStripMenuItem.Text = "Clone from another save";
            this.cloneFromAnotherSaveToolStripMenuItem.Click += new System.EventHandler(this.CloneFromSaveEchoes_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.toolStripMenuItem1.Text = "Add an echo list";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.AddListEchoes_Click);
            // 
            // removeAnEchoListToolStripMenuItem
            // 
            this.removeAnEchoListToolStripMenuItem.Name = "removeAnEchoListToolStripMenuItem";
            this.removeAnEchoListToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeAnEchoListToolStripMenuItem.Text = "Remove an echo list";
            this.removeAnEchoListToolStripMenuItem.Click += new System.EventHandler(this.RemoveListEchoes_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(192, 6);
            // 
            // eraseAllEchoesToolStripMenuItem
            // 
            this.eraseAllEchoesToolStripMenuItem.Name = "eraseAllEchoesToolStripMenuItem";
            this.eraseAllEchoesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.eraseAllEchoesToolStripMenuItem.Text = "Erase all echoes";
            this.eraseAllEchoesToolStripMenuItem.Click += new System.EventHandler(this.ClearEchoes_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(12, 20);
            // 
            // EchoTree
            // 
            this.EchoTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.EchoTree.AllowDrop = true;
            this.EchoTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.EchoTree.DefaultToolTipProvider = null;
            this.EchoTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.EchoTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.EchoTree.Location = new System.Drawing.Point(6, 47);
            this.EchoTree.Name = "EchoTree";
            this.EchoTree.SelectedNode = null;
            this.EchoTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.MultiSameParent;
            this.EchoTree.Size = new System.Drawing.Size(261, 532);
            this.EchoTree.TabIndex = 20;
            this.EchoTree.Text = "advTree1";
            this.EchoTree.SelectionChanged += new System.EventHandler(this.EchoTree_SelectionChanged);
            this.EchoTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EchoTree_KeyDown);
            // 
            // groupPanel16
            // 
            this.groupPanel16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel16.Controls.Add(this.labelEchoString);
            this.groupPanel16.Controls.Add(this.EchoString);
            this.groupPanel16.Controls.Add(this.EchoDLCValue2);
            this.groupPanel16.Controls.Add(this.EchoDLCValue1);
            this.groupPanel16.Controls.Add(this.labeEchoDLCValue2);
            this.groupPanel16.Controls.Add(this.labelEchoDLCValue1);
            this.groupPanel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel16.Location = new System.Drawing.Point(282, 3);
            this.groupPanel16.Name = "groupPanel16";
            this.groupPanel16.Padding = new System.Windows.Forms.Padding(6);
            this.groupPanel16.Size = new System.Drawing.Size(671, 160);
            this.groupPanel16.TabIndex = 26;
            this.groupPanel16.TabStop = false;
            this.groupPanel16.Text = "Selected Echo Log";
            // 
            // labelEchoString
            // 
            this.labelEchoString.AutoSize = true;
            this.labelEchoString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEchoString.Location = new System.Drawing.Point(3, 109);
            this.labelEchoString.Name = "labelEchoString";
            this.labelEchoString.Size = new System.Drawing.Size(62, 13);
            this.labelEchoString.TabIndex = 13;
            this.labelEchoString.Text = "Echo String";
            // 
            // EchoString
            // 
            this.EchoString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoString.Location = new System.Drawing.Point(6, 125);
            this.EchoString.Name = "EchoString";
            this.EchoString.Size = new System.Drawing.Size(443, 20);
            this.EchoString.TabIndex = 12;
            this.EchoString.TextChanged += new System.EventHandler(this.EchoString_TextChanged);
            // 
            // EchoDLCValue2
            // 
            this.EchoDLCValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoDLCValue2.Location = new System.Drawing.Point(6, 81);
            this.EchoDLCValue2.Name = "EchoDLCValue2";
            this.EchoDLCValue2.Size = new System.Drawing.Size(120, 20);
            this.EchoDLCValue2.TabIndex = 3;
            this.EchoDLCValue2.ValueChanged += new System.EventHandler(this.EchoDLCValue2_ValueChanged);
            // 
            // EchoDLCValue1
            // 
            this.EchoDLCValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EchoDLCValue1.Location = new System.Drawing.Point(6, 37);
            this.EchoDLCValue1.Name = "EchoDLCValue1";
            this.EchoDLCValue1.Size = new System.Drawing.Size(120, 20);
            this.EchoDLCValue1.TabIndex = 2;
            this.EchoDLCValue1.ValueChanged += new System.EventHandler(this.EchoDLCValue1_ValueChanged);
            // 
            // labeEchoDLCValue2
            // 
            this.labeEchoDLCValue2.AutoSize = true;
            this.labeEchoDLCValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeEchoDLCValue2.Location = new System.Drawing.Point(3, 65);
            this.labeEchoDLCValue2.Name = "labeEchoDLCValue2";
            this.labeEchoDLCValue2.Size = new System.Drawing.Size(67, 13);
            this.labeEchoDLCValue2.TabIndex = 1;
            this.labeEchoDLCValue2.Text = "DLC Value 2";
            // 
            // labelEchoDLCValue1
            // 
            this.labelEchoDLCValue1.AutoSize = true;
            this.labelEchoDLCValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEchoDLCValue1.Location = new System.Drawing.Point(3, 21);
            this.labelEchoDLCValue1.Name = "labelEchoDLCValue1";
            this.labelEchoDLCValue1.Size = new System.Drawing.Size(67, 13);
            this.labelEchoDLCValue1.TabIndex = 0;
            this.labelEchoDLCValue1.Text = "DLC Value 1";
            // 
            // ucEchoes
            // 
            this.Controls.Add(this.EchoesTab);
            this.Name = "ucEchoes";
            this.Size = new System.Drawing.Size(956, 591);
            this.EchoesTab.ResumeLayout(false);
            this.EchoesTab.PerformLayout();
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.MenuEcho.ResumeLayout(false);
            this.MenuEcho.PerformLayout();
            this.groupPanel16.ResumeLayout(false);
            this.groupPanel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EchoDLCValue2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EchoDLCValue1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel EchoesTab;
        private CustomControls.WTGroupBox groupPanel4;
        private CustomControls.WTMenuStrip MenuEcho;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripComboBox EchoList;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private CustomControls.WTTreeView EchoTree;
        private CustomControls.WTGroupBox groupPanel16;
        private CustomControls.WTNumericUpDown EchoDLCValue2;
        private CustomControls.WTNumericUpDown EchoDLCValue1;
        private CustomControls.WTLabel labeEchoDLCValue2;
        private CustomControls.WTLabel labelEchoDLCValue1;
        private System.Windows.Forms.ToolStripMenuItem actionsStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportAllToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectionToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mergeFromEchoesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeFromAnotherSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cloneFromEchoesFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneFromAnotherSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem eraseAllEchoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem removeAnEchoListToolStripMenuItem;
        private CustomControls.WTLabel labelEchoString;
        private CustomControls.WTTextBox EchoString;
    }
}
