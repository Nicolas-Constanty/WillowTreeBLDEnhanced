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
    partial class ucQuests
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
            this.NewQuestList = new System.Windows.Forms.ToolStripMenuItem();
            this.QuestsTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel3 = new WillowTree.CustomControls.WTGroupBox();
            this.MenuQuests = new WillowTree.CustomControls.WTMenuStrip();
            this.NewQuest = new System.Windows.Forms.ToolStripMenuItem();
            this.QuestList = new System.Windows.Forms.ToolStripComboBox();
            this.deletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.setAsActiveQuestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuestTree = new WillowTree.CustomControls.WTTreeView();
            this.ActivePT1QuestGroup = new WillowTree.CustomControls.WTGroupBox();
            this.ActiveQuest = new WillowTree.CustomControls.WTTextBox();
            this.SelectedQuestGroup = new WillowTree.CustomControls.WTGroupBox();
            this.QuestDLCValue2 = new WillowTree.CustomControls.WTNumericUpDown();
            this.QuestDLCValue1 = new WillowTree.CustomControls.WTNumericUpDown();
            this.labeQuestDLCValue2 = new WillowTree.CustomControls.WTLabel();
            this.labelQuestDLCValue1 = new WillowTree.CustomControls.WTLabel();
            this.QuestDescription = new WillowTree.CustomControls.WTTextBox();
            this.QuestSummary = new WillowTree.CustomControls.WTTextBox();
            this.labelQuestDescription = new WillowTree.CustomControls.WTLabel();
            this.labelQuestSummary = new WillowTree.CustomControls.WTLabel();
            this.labelQuestObjectiveValue = new WillowTree.CustomControls.WTLabel();
            this.NumberOfObjectives = new WillowTree.CustomControls.WTNumericUpDown();
            this.ObjectiveValue = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelQuestObjectives = new WillowTree.CustomControls.WTLabel();
            this.Objectives = new WillowTree.CustomControls.WTComboBox();
            this.labelQuestProgress = new WillowTree.CustomControls.WTLabel();
            this.QuestProgress = new WillowTree.CustomControls.WTComboBox();
            this.labelQuestString = new WillowTree.CustomControls.WTLabel();
            this.QuestString = new WillowTree.CustomControls.WTTextBox();
            this.QuestsTab.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.MenuQuests.SuspendLayout();
            this.ActivePT1QuestGroup.SuspendLayout();
            this.SelectedQuestGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestDLCValue2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestDLCValue1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfObjectives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectiveValue)).BeginInit();
            this.SuspendLayout();
            // 
            // NewQuestList
            // 
            this.NewQuestList.Name = "NewQuestList";
            this.NewQuestList.Size = new System.Drawing.Size(157, 22);
            this.NewQuestList.Text = "New Quest List";
            // 
            // QuestsTab
            // 
            this.QuestsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestsTab.Controls.Add(this.groupPanel3);
            this.QuestsTab.Controls.Add(this.ActivePT1QuestGroup);
            this.QuestsTab.Controls.Add(this.SelectedQuestGroup);
            this.QuestsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestsTab.ForeColor = System.Drawing.Color.White;
            this.QuestsTab.Location = new System.Drawing.Point(0, 0);
            this.QuestsTab.Name = "QuestsTab";
            this.QuestsTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.QuestsTab.Size = new System.Drawing.Size(956, 591);
            this.QuestsTab.TabIndex = 4;
            this.QuestsTab.Text = "Quests";
            // 
            // groupPanel3
            // 
            this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel3.Controls.Add(this.MenuQuests);
            this.groupPanel3.Controls.Add(this.QuestTree);
            this.groupPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel3.Location = new System.Drawing.Point(3, 3);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel3.Size = new System.Drawing.Size(273, 585);
            this.groupPanel3.TabIndex = 22;
            this.groupPanel3.TabStop = false;
            this.groupPanel3.Text = "Current Quests";
            // 
            // MenuQuests
            // 
            this.MenuQuests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuQuests.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewQuest,
            this.deletToolStripMenuItem,
            this.actionsStripMenuItem1});
            this.MenuQuests.Location = new System.Drawing.Point(6, 23);
            this.MenuQuests.Name = "MenuQuests";
            this.MenuQuests.Size = new System.Drawing.Size(261, 24);
            this.MenuQuests.TabIndex = 21;
            this.MenuQuests.Text = "menuStrip1";
            // 
            // NewQuest
            // 
            this.NewQuest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QuestList});
            this.NewQuest.Name = "NewQuest";
            this.NewQuest.Size = new System.Drawing.Size(41, 20);
            this.NewQuest.Text = "New";
            // 
            // QuestList
            // 
            this.QuestList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.QuestList.MaxDropDownItems = 30;
            this.QuestList.Name = "QuestList";
            this.QuestList.Size = new System.Drawing.Size(230, 21);
            this.QuestList.SelectedIndexChanged += new System.EventHandler(this.QuestList_SelectedIndexChanged);
            // 
            // deletToolStripMenuItem
            // 
            this.deletToolStripMenuItem.Name = "deletToolStripMenuItem";
            this.deletToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.deletToolStripMenuItem.Text = "Delete";
            this.deletToolStripMenuItem.Click += new System.EventHandler(this.DeleteQuest_Click);
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
            this.eraseAllEchoesToolStripMenuItem,
            this.setAsActiveQuestToolStripMenuItem});
            this.actionsStripMenuItem1.Name = "actionsStripMenuItem1";
            this.actionsStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.actionsStripMenuItem1.Text = "Actions";
            // 
            // exportAllToFileToolStripMenuItem
            // 
            this.exportAllToFileToolStripMenuItem.Name = "exportAllToFileToolStripMenuItem";
            this.exportAllToFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exportAllToFileToolStripMenuItem.Text = "Export all to file";
            this.exportAllToFileToolStripMenuItem.Click += new System.EventHandler(this.ExportToFileQuests_Click);
            // 
            // exportSelectionToFileToolStripMenuItem
            // 
            this.exportSelectionToFileToolStripMenuItem.Name = "exportSelectionToFileToolStripMenuItem";
            this.exportSelectionToFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exportSelectionToFileToolStripMenuItem.Text = "Export selection to file";
            this.exportSelectionToFileToolStripMenuItem.Click += new System.EventHandler(this.ExportSelectedQuests_Click);
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
            this.mergeFromEchoesFileToolStripMenuItem.Text = "Merge from quests file";
            this.mergeFromEchoesFileToolStripMenuItem.Click += new System.EventHandler(this.MergeAllFromFileQuests_Click);
            // 
            // mergeFromAnotherSaveToolStripMenuItem
            // 
            this.mergeFromAnotherSaveToolStripMenuItem.Name = "mergeFromAnotherSaveToolStripMenuItem";
            this.mergeFromAnotherSaveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.mergeFromAnotherSaveToolStripMenuItem.Text = "Merge from another save";
            this.mergeFromAnotherSaveToolStripMenuItem.Click += new System.EventHandler(this.MergeFromSaveQuests_Click);
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
            this.cloneFromEchoesFileToolStripMenuItem.Text = "Clone from quests file";
            this.cloneFromEchoesFileToolStripMenuItem.Click += new System.EventHandler(this.ImportFromFileQuests_Click);
            // 
            // cloneFromAnotherSaveToolStripMenuItem
            // 
            this.cloneFromAnotherSaveToolStripMenuItem.Name = "cloneFromAnotherSaveToolStripMenuItem";
            this.cloneFromAnotherSaveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cloneFromAnotherSaveToolStripMenuItem.Text = "Clone from another save";
            this.cloneFromAnotherSaveToolStripMenuItem.Click += new System.EventHandler(this.ImportFromSaveQuests_Click);
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
            this.toolStripMenuItem1.Text = "Add quest list";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.AddListQuests_Click);
            // 
            // removeAnEchoListToolStripMenuItem
            // 
            this.removeAnEchoListToolStripMenuItem.Name = "removeAnEchoListToolStripMenuItem";
            this.removeAnEchoListToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.removeAnEchoListToolStripMenuItem.Text = "Remove quest list";
            this.removeAnEchoListToolStripMenuItem.Click += new System.EventHandler(this.RemoveListQuests_Click);
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
            this.eraseAllEchoesToolStripMenuItem.Text = "Clear quest list";
            this.eraseAllEchoesToolStripMenuItem.Click += new System.EventHandler(this.ClearQuests_Click);
            // 
            // setAsActiveQuestToolStripMenuItem
            // 
            this.setAsActiveQuestToolStripMenuItem.Name = "setAsActiveQuestToolStripMenuItem";
            this.setAsActiveQuestToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.setAsActiveQuestToolStripMenuItem.Text = "Set as Active Quest";
            this.setAsActiveQuestToolStripMenuItem.Click += new System.EventHandler(this.SetActiveQuest_Click);
            // 
            // QuestTree
            // 
            this.QuestTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.QuestTree.AllowDrop = true;
            this.QuestTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.QuestTree.DefaultToolTipProvider = null;
            this.QuestTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.QuestTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.QuestTree.Location = new System.Drawing.Point(6, 47);
            this.QuestTree.Name = "QuestTree";
            this.QuestTree.SelectedNode = null;
            this.QuestTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.MultiSameParent;
            this.QuestTree.Size = new System.Drawing.Size(261, 532);
            this.QuestTree.TabIndex = 20;
            this.QuestTree.Text = "advTree1";
            this.QuestTree.SelectionChanged += new System.EventHandler(this.QuestTree_SelectionChanged);
            this.QuestTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuestTree_KeyDown);
            // 
            // ActivePT1QuestGroup
            // 
            this.ActivePT1QuestGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivePT1QuestGroup.Controls.Add(this.ActiveQuest);
            this.ActivePT1QuestGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivePT1QuestGroup.Location = new System.Drawing.Point(282, 3);
            this.ActivePT1QuestGroup.Name = "ActivePT1QuestGroup";
            this.ActivePT1QuestGroup.Padding = new System.Windows.Forms.Padding(3);
            this.ActivePT1QuestGroup.Size = new System.Drawing.Size(671, 62);
            this.ActivePT1QuestGroup.TabIndex = 25;
            this.ActivePT1QuestGroup.TabStop = false;
            this.ActivePT1QuestGroup.Text = "Active Quest";
            // 
            // ActiveQuest
            // 
            this.ActiveQuest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActiveQuest.Location = new System.Drawing.Point(6, 26);
            this.ActiveQuest.Name = "ActiveQuest";
            this.ActiveQuest.Size = new System.Drawing.Size(659, 20);
            this.ActiveQuest.TabIndex = 24;
            this.ActiveQuest.TextChanged += new System.EventHandler(this.ActiveQuest_TextChanged);
            // 
            // SelectedQuestGroup
            // 
            this.SelectedQuestGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedQuestGroup.Controls.Add(this.QuestDLCValue2);
            this.SelectedQuestGroup.Controls.Add(this.QuestDLCValue1);
            this.SelectedQuestGroup.Controls.Add(this.labeQuestDLCValue2);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestDLCValue1);
            this.SelectedQuestGroup.Controls.Add(this.QuestDescription);
            this.SelectedQuestGroup.Controls.Add(this.QuestSummary);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestDescription);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestSummary);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestObjectiveValue);
            this.SelectedQuestGroup.Controls.Add(this.NumberOfObjectives);
            this.SelectedQuestGroup.Controls.Add(this.ObjectiveValue);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestObjectives);
            this.SelectedQuestGroup.Controls.Add(this.Objectives);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestProgress);
            this.SelectedQuestGroup.Controls.Add(this.QuestProgress);
            this.SelectedQuestGroup.Controls.Add(this.labelQuestString);
            this.SelectedQuestGroup.Controls.Add(this.QuestString);
            this.SelectedQuestGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedQuestGroup.Location = new System.Drawing.Point(282, 71);
            this.SelectedQuestGroup.Name = "SelectedQuestGroup";
            this.SelectedQuestGroup.Size = new System.Drawing.Size(671, 517);
            this.SelectedQuestGroup.TabIndex = 23;
            this.SelectedQuestGroup.TabStop = false;
            this.SelectedQuestGroup.Text = "Selected Quest";
            // 
            // QuestDLCValue2
            // 
            this.QuestDLCValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestDLCValue2.Location = new System.Drawing.Point(422, 45);
            this.QuestDLCValue2.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.QuestDLCValue2.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.QuestDLCValue2.Name = "QuestDLCValue2";
            this.QuestDLCValue2.Size = new System.Drawing.Size(120, 20);
            this.QuestDLCValue2.TabIndex = 27;
            this.QuestDLCValue2.ValueChanged += new System.EventHandler(this.QuestDLCValue2_ValueChanged);
            // 
            // QuestDLCValue1
            // 
            this.QuestDLCValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestDLCValue1.Location = new System.Drawing.Point(296, 45);
            this.QuestDLCValue1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.QuestDLCValue1.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.QuestDLCValue1.Name = "QuestDLCValue1";
            this.QuestDLCValue1.Size = new System.Drawing.Size(120, 20);
            this.QuestDLCValue1.TabIndex = 26;
            this.QuestDLCValue1.ValueChanged += new System.EventHandler(this.QuestDLCValue1_ValueChanged);
            // 
            // labeQuestDLCValue2
            // 
            this.labeQuestDLCValue2.AutoSize = true;
            this.labeQuestDLCValue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeQuestDLCValue2.Location = new System.Drawing.Point(419, 29);
            this.labeQuestDLCValue2.Name = "labeQuestDLCValue2";
            this.labeQuestDLCValue2.Size = new System.Drawing.Size(67, 13);
            this.labeQuestDLCValue2.TabIndex = 25;
            this.labeQuestDLCValue2.Text = "DLC Value 2";
            // 
            // labelQuestDLCValue1
            // 
            this.labelQuestDLCValue1.AutoSize = true;
            this.labelQuestDLCValue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestDLCValue1.Location = new System.Drawing.Point(293, 29);
            this.labelQuestDLCValue1.Name = "labelQuestDLCValue1";
            this.labelQuestDLCValue1.Size = new System.Drawing.Size(67, 13);
            this.labelQuestDLCValue1.TabIndex = 24;
            this.labelQuestDLCValue1.Text = "DLC Value 1";
            // 
            // QuestDescription
            // 
            this.QuestDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestDescription.Location = new System.Drawing.Point(6, 242);
            this.QuestDescription.Multiline = true;
            this.QuestDescription.Name = "QuestDescription";
            this.QuestDescription.ReadOnly = true;
            this.QuestDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.QuestDescription.Size = new System.Drawing.Size(659, 269);
            this.QuestDescription.TabIndex = 23;
            // 
            // QuestSummary
            // 
            this.QuestSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.QuestSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestSummary.Location = new System.Drawing.Point(6, 198);
            this.QuestSummary.Name = "QuestSummary";
            this.QuestSummary.ReadOnly = true;
            this.QuestSummary.Size = new System.Drawing.Size(588, 20);
            this.QuestSummary.TabIndex = 22;
            // 
            // labelQuestDescription
            // 
            this.labelQuestDescription.AutoSize = true;
            this.labelQuestDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestDescription.Location = new System.Drawing.Point(3, 226);
            this.labelQuestDescription.Name = "labelQuestDescription";
            this.labelQuestDescription.Size = new System.Drawing.Size(91, 13);
            this.labelQuestDescription.TabIndex = 21;
            this.labelQuestDescription.Text = "Quest Description";
            // 
            // labelQuestSummary
            // 
            this.labelQuestSummary.AutoSize = true;
            this.labelQuestSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestSummary.Location = new System.Drawing.Point(3, 182);
            this.labelQuestSummary.Name = "labelQuestSummary";
            this.labelQuestSummary.Size = new System.Drawing.Size(81, 13);
            this.labelQuestSummary.TabIndex = 20;
            this.labelQuestSummary.Text = "Quest Summary";
            // 
            // labelQuestObjectiveValue
            // 
            this.labelQuestObjectiveValue.AutoSize = true;
            this.labelQuestObjectiveValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestObjectiveValue.Location = new System.Drawing.Point(373, 85);
            this.labelQuestObjectiveValue.Name = "labelQuestObjectiveValue";
            this.labelQuestObjectiveValue.Size = new System.Drawing.Size(82, 13);
            this.labelQuestObjectiveValue.TabIndex = 19;
            this.labelQuestObjectiveValue.Text = "Objective Value";
            // 
            // NumberOfObjectives
            // 
            this.NumberOfObjectives.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfObjectives.Location = new System.Drawing.Point(166, 138);
            this.NumberOfObjectives.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumberOfObjectives.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumberOfObjectives.Name = "NumberOfObjectives";
            this.NumberOfObjectives.Size = new System.Drawing.Size(106, 20);
            this.NumberOfObjectives.TabIndex = 14;
            this.NumberOfObjectives.TabStop = false;
            this.NumberOfObjectives.Visible = false;
            // 
            // ObjectiveValue
            // 
            this.ObjectiveValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectiveValue.Location = new System.Drawing.Point(376, 102);
            this.ObjectiveValue.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.ObjectiveValue.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.ObjectiveValue.Name = "ObjectiveValue";
            this.ObjectiveValue.Size = new System.Drawing.Size(84, 20);
            this.ObjectiveValue.TabIndex = 18;
            this.ObjectiveValue.ValueChanged += new System.EventHandler(this.ObjectiveValue_ValueChanged);
            // 
            // labelQuestObjectives
            // 
            this.labelQuestObjectives.AutoSize = true;
            this.labelQuestObjectives.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestObjectives.Location = new System.Drawing.Point(163, 85);
            this.labelQuestObjectives.Name = "labelQuestObjectives";
            this.labelQuestObjectives.Size = new System.Drawing.Size(57, 13);
            this.labelQuestObjectives.TabIndex = 17;
            this.labelQuestObjectives.Text = "Objectives";
            // 
            // Objectives
            // 
            this.Objectives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Objectives.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Objectives.FormattingEnabled = true;
            this.Objectives.Location = new System.Drawing.Point(166, 101);
            this.Objectives.Name = "Objectives";
            this.Objectives.Size = new System.Drawing.Size(204, 21);
            this.Objectives.TabIndex = 16;
            this.Objectives.SelectedIndexChanged += new System.EventHandler(this.Objectives_SelectedIndexChanged);
            this.Objectives.Click += new System.EventHandler(this.QuestProgress_Click);
            // 
            // labelQuestProgress
            // 
            this.labelQuestProgress.AutoSize = true;
            this.labelQuestProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestProgress.Location = new System.Drawing.Point(3, 85);
            this.labelQuestProgress.Name = "labelQuestProgress";
            this.labelQuestProgress.Size = new System.Drawing.Size(48, 13);
            this.labelQuestProgress.TabIndex = 13;
            this.labelQuestProgress.Text = "Progress";
            // 
            // QuestProgress
            // 
            this.QuestProgress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuestProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestProgress.FormattingEnabled = true;
            this.QuestProgress.Items.AddRange(new object[] {
            "Not started",
            "Started",
            "Ready to turn in",
            "Finished"});
            this.QuestProgress.Location = new System.Drawing.Point(6, 101);
            this.QuestProgress.Name = "QuestProgress";
            this.QuestProgress.Size = new System.Drawing.Size(154, 21);
            this.QuestProgress.TabIndex = 12;
            this.QuestProgress.SelectedIndexChanged += new System.EventHandler(this.QuestProgress_SelectedIndexChanged);
            this.QuestProgress.Click += new System.EventHandler(this.QuestProgress_Click);
            // 
            // labelQuestString
            // 
            this.labelQuestString.AutoSize = true;
            this.labelQuestString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuestString.Location = new System.Drawing.Point(6, 29);
            this.labelQuestString.Name = "labelQuestString";
            this.labelQuestString.Size = new System.Drawing.Size(65, 13);
            this.labelQuestString.TabIndex = 11;
            this.labelQuestString.Text = "Quest String";
            // 
            // QuestString
            // 
            this.QuestString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestString.Location = new System.Drawing.Point(6, 45);
            this.QuestString.Name = "QuestString";
            this.QuestString.ReadOnly = true;
            this.QuestString.Size = new System.Drawing.Size(271, 20);
            this.QuestString.TabIndex = 10;
            // 
            // ucQuests
            // 
            this.Controls.Add(this.QuestsTab);
            this.Name = "ucQuests";
            this.Size = new System.Drawing.Size(956, 591);
            this.QuestsTab.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.MenuQuests.ResumeLayout(false);
            this.MenuQuests.PerformLayout();
            this.ActivePT1QuestGroup.ResumeLayout(false);
            this.ActivePT1QuestGroup.PerformLayout();
            this.SelectedQuestGroup.ResumeLayout(false);
            this.SelectedQuestGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuestDLCValue2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuestDLCValue1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfObjectives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectiveValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel QuestsTab;
        private CustomControls.WTGroupBox groupPanel3;
        private CustomControls.WTMenuStrip MenuQuests;
        private System.Windows.Forms.ToolStripMenuItem NewQuest;
        private System.Windows.Forms.ToolStripComboBox QuestList;
        private System.Windows.Forms.ToolStripMenuItem deletToolStripMenuItem;
        private CustomControls.WTTreeView QuestTree;
        private System.Windows.Forms.ToolStripMenuItem NewQuestList;
        private CustomControls.WTGroupBox ActivePT1QuestGroup;
        private CustomControls.WTTextBox ActiveQuest;
        private CustomControls.WTGroupBox SelectedQuestGroup;
        private CustomControls.WTTextBox QuestDescription;
        private CustomControls.WTTextBox QuestSummary;
        private CustomControls.WTLabel labelQuestDescription;
        private CustomControls.WTLabel labelQuestSummary;
        private CustomControls.WTLabel labelQuestObjectiveValue;
        private CustomControls.WTNumericUpDown NumberOfObjectives;
        private CustomControls.WTNumericUpDown ObjectiveValue;
        private CustomControls.WTLabel labelQuestObjectives;
        private CustomControls.WTComboBox Objectives;
        private CustomControls.WTLabel labelQuestProgress;
        private CustomControls.WTComboBox QuestProgress;
        private CustomControls.WTLabel labelQuestString;
        private CustomControls.WTTextBox QuestString;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeAnEchoListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem eraseAllEchoesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsActiveQuestToolStripMenuItem;
        private CustomControls.WTNumericUpDown QuestDLCValue2;
        private CustomControls.WTNumericUpDown QuestDLCValue1;
        private CustomControls.WTLabel labeQuestDLCValue2;
        private CustomControls.WTLabel labelQuestDLCValue1;
    }
}
