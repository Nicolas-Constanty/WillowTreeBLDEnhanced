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
    partial class ucSkills
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
            this.SkillsTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel11 = new WillowTree.CustomControls.WTGroupBox();
            this.SkillTree = new WillowTree.CustomControls.WTTreeView();
            this.MenuSkills = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.SkillList = new System.Windows.Forms.ToolStripComboBox();
            this.deleteToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupPanel12 = new WillowTree.CustomControls.WTGroupBox();
            this.skillLevelLabel = new WillowTree.CustomControls.WTLabel();
            this.SkillLevel = new WillowTree.CustomControls.WTNumericUpDown();
            this.skillStringLabel = new WillowTree.CustomControls.WTLabel();
            this.SkillExp = new WillowTree.CustomControls.WTNumericUpDown();
            this.SkillName = new WillowTree.CustomControls.WTTextBox();
            this.skillExperienceLabel = new WillowTree.CustomControls.WTLabel();
            this.SkillActive = new WillowTree.CustomControls.WTComboBox();
            this.skillActiveLabel = new WillowTree.CustomControls.WTLabel();
            this.SkillsTab.SuspendLayout();
            this.groupPanel11.SuspendLayout();
            this.MenuSkills.SuspendLayout();
            this.groupPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkillLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillExp)).BeginInit();
            this.SuspendLayout();
            // 
            // SkillsTab
            // 
            this.SkillsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SkillsTab.Controls.Add(this.groupPanel11);
            this.SkillsTab.Controls.Add(this.groupPanel12);
            this.SkillsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillsTab.ForeColor = System.Drawing.Color.White;
            this.SkillsTab.Location = new System.Drawing.Point(0, 0);
            this.SkillsTab.Name = "SkillsTab";
            this.SkillsTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.SkillsTab.Size = new System.Drawing.Size(956, 591);
            this.SkillsTab.TabIndex = 3;
            this.SkillsTab.Text = "Skills";
            // 
            // groupPanel11
            // 
            this.groupPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel11.Controls.Add(this.SkillTree);
            this.groupPanel11.Controls.Add(this.MenuSkills);
            this.groupPanel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel11.Location = new System.Drawing.Point(3, 3);
            this.groupPanel11.Name = "groupPanel11";
            this.groupPanel11.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel11.Size = new System.Drawing.Size(273, 585);
            this.groupPanel11.TabIndex = 24;
            this.groupPanel11.TabStop = false;
            this.groupPanel11.Text = "Current Skills";
            // 
            // SkillTree
            // 
            this.SkillTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.SkillTree.AllowDrop = true;
            this.SkillTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.SkillTree.DefaultToolTipProvider = null;
            this.SkillTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.SkillTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.SkillTree.Location = new System.Drawing.Point(6, 47);
            this.SkillTree.Name = "SkillTree";
            this.SkillTree.SelectedNode = null;
            this.SkillTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.SkillTree.Size = new System.Drawing.Size(261, 532);
            this.SkillTree.TabIndex = 22;
            this.SkillTree.Text = "advTree1";
            this.SkillTree.SelectionChanged += new System.EventHandler(this.SkillTree_SelectionChanged);
            this.SkillTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SkillTree_KeyDown);
            // 
            // MenuSkills
            // 
            this.MenuSkills.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuSkills.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem4,
            this.deleteToolStripMenuItem3,
            this.exportToolStripMenuItem3,
            this.importToolStripMenuItem3});
            this.MenuSkills.Location = new System.Drawing.Point(6, 23);
            this.MenuSkills.Name = "MenuSkills";
            this.MenuSkills.Size = new System.Drawing.Size(261, 24);
            this.MenuSkills.TabIndex = 21;
            this.MenuSkills.Text = "menuStrip2";
            // 
            // newToolStripMenuItem4
            // 
            this.newToolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SkillList});
            this.newToolStripMenuItem4.Name = "newToolStripMenuItem4";
            this.newToolStripMenuItem4.Size = new System.Drawing.Size(41, 20);
            this.newToolStripMenuItem4.Text = "New";
            // 
            // SkillList
            // 
            this.SkillList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.SkillList.MaxDropDownItems = 100;
            this.SkillList.Name = "SkillList";
            this.SkillList.Size = new System.Drawing.Size(230, 21);
            this.SkillList.SelectedIndexChanged += new System.EventHandler(this.SkillList_Click);
            // 
            // deleteToolStripMenuItem3
            // 
            this.deleteToolStripMenuItem3.Name = "deleteToolStripMenuItem3";
            this.deleteToolStripMenuItem3.Size = new System.Drawing.Size(50, 20);
            this.deleteToolStripMenuItem3.Text = "Delete";
            this.deleteToolStripMenuItem3.Click += new System.EventHandler(this.DeleteSkill_Click);
            // 
            // exportToolStripMenuItem3
            // 
            this.exportToolStripMenuItem3.Name = "exportToolStripMenuItem3";
            this.exportToolStripMenuItem3.Size = new System.Drawing.Size(49, 20);
            this.exportToolStripMenuItem3.Text = "Export";
            this.exportToolStripMenuItem3.Click += new System.EventHandler(this.ExportToFileSkills_Click);
            // 
            // importToolStripMenuItem3
            // 
            this.importToolStripMenuItem3.Name = "importToolStripMenuItem3";
            this.importToolStripMenuItem3.Size = new System.Drawing.Size(48, 20);
            this.importToolStripMenuItem3.Text = "Import";
            this.importToolStripMenuItem3.Click += new System.EventHandler(this.ImportFromFileSkills_Click);
            // 
            // groupPanel12
            // 
            this.groupPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel12.Controls.Add(this.skillLevelLabel);
            this.groupPanel12.Controls.Add(this.SkillLevel);
            this.groupPanel12.Controls.Add(this.skillStringLabel);
            this.groupPanel12.Controls.Add(this.SkillExp);
            this.groupPanel12.Controls.Add(this.SkillName);
            this.groupPanel12.Controls.Add(this.skillExperienceLabel);
            this.groupPanel12.Controls.Add(this.SkillActive);
            this.groupPanel12.Controls.Add(this.skillActiveLabel);
            this.groupPanel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel12.Location = new System.Drawing.Point(282, 3);
            this.groupPanel12.Name = "groupPanel12";
            this.groupPanel12.Size = new System.Drawing.Size(671, 192);
            this.groupPanel12.TabIndex = 30;
            this.groupPanel12.TabStop = false;
            this.groupPanel12.Text = "Selected Skill";
            // 
            // skillLevelLabel
            // 
            this.skillLevelLabel.AutoSize = true;
            this.skillLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillLevelLabel.Location = new System.Drawing.Point(3, 21);
            this.skillLevelLabel.Name = "skillLevelLabel";
            this.skillLevelLabel.Size = new System.Drawing.Size(33, 13);
            this.skillLevelLabel.TabIndex = 24;
            this.skillLevelLabel.Text = "Level";
            // 
            // SkillLevel
            // 
            this.SkillLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillLevel.Location = new System.Drawing.Point(6, 37);
            this.SkillLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.SkillLevel.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SkillLevel.Name = "SkillLevel";
            this.SkillLevel.Size = new System.Drawing.Size(120, 20);
            this.SkillLevel.TabIndex = 21;
            this.SkillLevel.ValueChanged += new System.EventHandler(this.SkillLevel_ValueChanged);
            // 
            // skillStringLabel
            // 
            this.skillStringLabel.AutoSize = true;
            this.skillStringLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillStringLabel.Location = new System.Drawing.Point(3, 153);
            this.skillStringLabel.Name = "skillStringLabel";
            this.skillStringLabel.Size = new System.Drawing.Size(56, 13);
            this.skillStringLabel.TabIndex = 29;
            this.skillStringLabel.Text = "Skill String";
            // 
            // SkillExp
            // 
            this.SkillExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillExp.Location = new System.Drawing.Point(6, 81);
            this.SkillExp.Maximum = new decimal(new int[] {
            16777215,
            0,
            0,
            0});
            this.SkillExp.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SkillExp.Name = "SkillExp";
            this.SkillExp.Size = new System.Drawing.Size(120, 20);
            this.SkillExp.TabIndex = 22;
            this.SkillExp.ValueChanged += new System.EventHandler(this.SkillExp_ValueChanged);
            // 
            // SkillName
            // 
            this.SkillName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillName.Location = new System.Drawing.Point(6, 166);
            this.SkillName.Name = "SkillName";
            this.SkillName.ReadOnly = true;
            this.SkillName.Size = new System.Drawing.Size(325, 20);
            this.SkillName.TabIndex = 28;
            // 
            // skillExperienceLabel
            // 
            this.skillExperienceLabel.AutoSize = true;
            this.skillExperienceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillExperienceLabel.Location = new System.Drawing.Point(3, 65);
            this.skillExperienceLabel.Name = "skillExperienceLabel";
            this.skillExperienceLabel.Size = new System.Drawing.Size(60, 13);
            this.skillExperienceLabel.TabIndex = 25;
            this.skillExperienceLabel.Text = "Experience";
            // 
            // SkillActive
            // 
            this.SkillActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SkillActive.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SkillActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillActive.FormattingEnabled = true;
            this.SkillActive.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.SkillActive.Location = new System.Drawing.Point(6, 125);
            this.SkillActive.Name = "SkillActive";
            this.SkillActive.Size = new System.Drawing.Size(121, 21);
            this.SkillActive.TabIndex = 27;
            this.SkillActive.SelectionChangeCommitted += new System.EventHandler(this.SkillActive_SelectionChangeCommitted);
            // 
            // skillActiveLabel
            // 
            this.skillActiveLabel.AutoSize = true;
            this.skillActiveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillActiveLabel.Location = new System.Drawing.Point(3, 109);
            this.skillActiveLabel.Name = "skillActiveLabel";
            this.skillActiveLabel.Size = new System.Drawing.Size(37, 13);
            this.skillActiveLabel.TabIndex = 26;
            this.skillActiveLabel.Text = "Active";
            // 
            // ucSkills
            // 
            this.Controls.Add(this.SkillsTab);
            this.Name = "ucSkills";
            this.Size = new System.Drawing.Size(956, 591);
            this.SkillsTab.ResumeLayout(false);
            this.groupPanel11.ResumeLayout(false);
            this.groupPanel11.PerformLayout();
            this.MenuSkills.ResumeLayout(false);
            this.MenuSkills.PerformLayout();
            this.groupPanel12.ResumeLayout(false);
            this.groupPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkillLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillExp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel SkillsTab;
        private CustomControls.WTGroupBox groupPanel11;
        private CustomControls.WTTreeView SkillTree;
        private CustomControls.WTMenuStrip MenuSkills;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem4;
        private System.Windows.Forms.ToolStripComboBox SkillList;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem3;
        private CustomControls.WTGroupBox groupPanel12;
        private CustomControls.WTLabel skillLevelLabel;
        private CustomControls.WTNumericUpDown SkillLevel;
        private CustomControls.WTLabel skillStringLabel;
        private CustomControls.WTNumericUpDown SkillExp;
        private CustomControls.WTTextBox SkillName;
        private CustomControls.WTLabel skillExperienceLabel;
        private CustomControls.WTComboBox SkillActive;
        private CustomControls.WTLabel skillActiveLabel;
    }
}
