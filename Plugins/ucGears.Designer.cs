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
    partial class ucGears
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
            this.GearsTab = new WillowTree.CustomControls.CCPanel();
            this.gbGear = new WillowTree.CustomControls.WTGroupBox();
            this.btnGearSearch = new WillowTree.CustomControls.WTButton();
            this.GearSearch = new WillowTree.CustomControls.WTTextBox();
            this.MenuGears = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromMultipleFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAllFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorGear = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAllLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAllQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GearTree = new WillowTree.CustomControls.WTTreeView();
            this.GearPartsGroup = new WillowTree.CustomControls.WTGroupBox();
            this.txtGearInformation = new WillowTree.CustomControls.WTTextBox();
            this.LevelIndexGear = new WillowTree.CustomControls.WTSlideSelector();
            this.QualityGear = new WillowTree.CustomControls.WTSlideSelector();
            this.MenuGearParts = new WillowTree.CustomControls.WTMenuStrip();
            this.saveChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportGearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PartsGear = new WillowTree.CustomControls.WTListBox();
            this.EquippedSlotGear = new WillowTree.CustomControls.WTComboBox();
            this.labelGearQuantity = new WillowTree.CustomControls.WTLabel();
            this.labelGearEquipped = new WillowTree.CustomControls.WTLabel();
            this.QuantityGear = new WillowTree.CustomControls.WTNumericUpDown();
            this.gbPartSelectorGear = new WillowTree.CustomControls.WTGroupBox();
            this.PartInfoGear = new WillowTree.CustomControls.WTTextBox();
            this.PartSelectorGear = new WillowTree.CustomControls.WTTreeView();
            this.copyToBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GearsTab.SuspendLayout();
            this.gbGear.SuspendLayout();
            this.MenuGears.SuspendLayout();
            this.GearPartsGroup.SuspendLayout();
            this.MenuGearParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantityGear)).BeginInit();
            this.gbPartSelectorGear.SuspendLayout();
            this.SuspendLayout();
            // 
            // GearsTab
            // 
            this.GearsTab.Controls.Add(this.gbGear);
            this.GearsTab.Controls.Add(this.GearPartsGroup);
            this.GearsTab.Controls.Add(this.gbPartSelectorGear);
            this.GearsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GearsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GearsTab.ForeColor = System.Drawing.Color.White;
            this.GearsTab.Location = new System.Drawing.Point(0, 0);
            this.GearsTab.Name = "GearsTab";
            this.GearsTab.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.GearsTab.Size = new System.Drawing.Size(956, 591);
            this.GearsTab.TabIndex = 1;
            this.GearsTab.Text = "Gears";
            // 
            // gbGear
            // 
            this.gbGear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGear.Controls.Add(this.btnGearSearch);
            this.gbGear.Controls.Add(this.GearSearch);
            this.gbGear.Controls.Add(this.MenuGears);
            this.gbGear.Controls.Add(this.GearTree);
            this.gbGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGear.Location = new System.Drawing.Point(3, 3);
            this.gbGear.Name = "gbGear";
            this.gbGear.Padding = new System.Windows.Forms.Padding(3);
            this.gbGear.Size = new System.Drawing.Size(273, 585);
            this.gbGear.TabIndex = 21;
            this.gbGear.TabStop = false;
            this.gbGear.Text = "Gear Backpack";
            // 
            // btnGearSearch
            // 
            this.btnGearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGearSearch.Location = new System.Drawing.Point(215, 556);
            this.btnGearSearch.Name = "btnGearSearch";
            this.btnGearSearch.Size = new System.Drawing.Size(52, 23);
            this.btnGearSearch.TabIndex = 44;
            this.btnGearSearch.Text = "Search";
            this.btnGearSearch.UseVisualStyleBackColor = false;
            this.btnGearSearch.Click += new System.EventHandler(this.btnGearSearch_Click);
            // 
            // GearSearch
            // 
            this.GearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GearSearch.Location = new System.Drawing.Point(6, 559);
            this.GearSearch.Name = "GearSearch";
            this.GearSearch.Size = new System.Drawing.Size(200, 20);
            this.GearSearch.TabIndex = 45;
            this.GearSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GearSearch_KeyDown);
            // 
            // MenuGears
            // 
            this.MenuGears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuGears.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.importToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.editAllToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.MenuGears.Location = new System.Drawing.Point(6, 23);
            this.MenuGears.Name = "MenuGears";
            this.MenuGears.Size = new System.Drawing.Size(261, 24);
            this.MenuGears.TabIndex = 21;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.newToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewGear_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromMultipleFilesToolStripMenuItem,
            this.fromClipboardToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.importToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromMultipleFilesToolStripMenuItem
            // 
            this.fromMultipleFilesToolStripMenuItem.Name = "fromMultipleFilesToolStripMenuItem";
            this.fromMultipleFilesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromMultipleFilesToolStripMenuItem.Text = "from File(s)";
            this.fromMultipleFilesToolStripMenuItem.Click += new System.EventHandler(this.ImportFromFilesGears_Click);
            // 
            // fromClipboardToolStripMenuItem
            // 
            this.fromClipboardToolStripMenuItem.Name = "fromClipboardToolStripMenuItem";
            this.fromClipboardToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromClipboardToolStripMenuItem.Text = "from Clipboard";
            this.fromClipboardToolStripMenuItem.Click += new System.EventHandler(this.ImportFromClipboardGear_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllToXMLToolStripMenuItem,
            this.importAllFromXMLToolStripMenuItem,
            this.toolStripSeparatorGear,
            this.clearAllToolStripMenuItem,
            this.copyToBackpackToolStripMenuItem,
            this.copyToBankToolStripMenuItem,
            this.copyToLockerToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.moveToLockerToolStripMenuItem,
            this.purgeDuplicatesToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // exportAllToXMLToolStripMenuItem
            // 
            this.exportAllToXMLToolStripMenuItem.Name = "exportAllToXMLToolStripMenuItem";
            this.exportAllToXMLToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exportAllToXMLToolStripMenuItem.Text = "Export All to XML";
            this.exportAllToXMLToolStripMenuItem.Click += new System.EventHandler(this.ExportToXmlGears_Click);
            // 
            // importAllFromXMLToolStripMenuItem
            // 
            this.importAllFromXMLToolStripMenuItem.Name = "importAllFromXMLToolStripMenuItem";
            this.importAllFromXMLToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.importAllFromXMLToolStripMenuItem.Text = "Import All from XML";
            this.importAllFromXMLToolStripMenuItem.Click += new System.EventHandler(this.ImportAllFromXmlGears_Click);
            // 
            // toolStripSeparatorGear
            // 
            this.toolStripSeparatorGear.Name = "toolStripSeparatorGear";
            this.toolStripSeparatorGear.Size = new System.Drawing.Size(197, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.ClearAllGear_Click);
            // 
            // copyToLockerToolStripMenuItem
            // 
            this.copyToLockerToolStripMenuItem.Name = "copyToLockerToolStripMenuItem";
            this.copyToLockerToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.copyToLockerToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyToLockerToolStripMenuItem.Text = "Copy to Locker";
            this.copyToLockerToolStripMenuItem.Click += new System.EventHandler(this.CopyLocker_Click);
            // 
            // copyToBankToolStripMenuItem
            // 
            this.copyToBankToolStripMenuItem.Name = "copyToBankToolStripMenuItem";
            this.copyToBankToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.copyToBankToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyToBankToolStripMenuItem.Text = "Copy to Bank";
            this.copyToBankToolStripMenuItem.Click += new System.EventHandler(this.CopyBank_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.ShortcutKeyDisplayString = "Ins";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.DuplicateGear_Click);
            // 
            // moveToLockerToolStripMenuItem
            // 
            this.moveToLockerToolStripMenuItem.Name = "moveToLockerToolStripMenuItem";
            this.moveToLockerToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.moveToLockerToolStripMenuItem.Text = "Move to Locker";
            this.moveToLockerToolStripMenuItem.Click += new System.EventHandler(this.MoveGear_Click);
            // 
            // purgeDuplicatesToolStripMenuItem
            // 
            this.purgeDuplicatesToolStripMenuItem.Name = "purgeDuplicatesToolStripMenuItem";
            this.purgeDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.purgeDuplicatesToolStripMenuItem.Text = "Purge Duplicates";
            this.purgeDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.PurgeDuplicatesGear_Click);
            // 
            // editAllToolStripMenuItem
            // 
            this.editAllToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAllLevelToolStripMenuItem,
            this.editAllQualityToolStripMenuItem});
            this.editAllToolStripMenuItem.Name = "editAllToolStripMenuItem";
            this.editAllToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.editAllToolStripMenuItem.Text = "Edit All";
            // 
            // editAllLevelToolStripMenuItem
            // 
            this.editAllLevelToolStripMenuItem.Name = "editAllLevelToolStripMenuItem";
            this.editAllLevelToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.editAllLevelToolStripMenuItem.Text = "Level";
            this.editAllLevelToolStripMenuItem.Click += new System.EventHandler(this.EditLevelAllGears_Click);
            // 
            // editAllQualityToolStripMenuItem
            // 
            this.editAllQualityToolStripMenuItem.Name = "editAllQualityToolStripMenuItem";
            this.editAllQualityToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.editAllQualityToolStripMenuItem.Text = "Quality";
            this.editAllQualityToolStripMenuItem.Click += new System.EventHandler(this.EditQualityAllGears_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteGear_Click);
            // 
            // GearTree
            // 
            this.GearTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.GearTree.AllowDrop = true;
            this.GearTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GearTree.DefaultToolTipProvider = null;
            this.GearTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.GearTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.GearTree.Indent = 15;
            this.GearTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.GearTree.Location = new System.Drawing.Point(6, 47);
            this.GearTree.Name = "GearTree";
            this.GearTree.SelectedNode = null;
            this.GearTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.GearTree.Size = new System.Drawing.Size(261, 506);
            this.GearTree.TabIndex = 20;
            this.GearTree.Text = "advTree1";
            this.GearTree.SelectionChanged += new System.EventHandler(this.GearTree_SelectionChanged);
            this.GearTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GearTree_KeyDown);
            // 
            // GearPartsGroup
            // 
            this.GearPartsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GearPartsGroup.Controls.Add(this.txtGearInformation);
            this.GearPartsGroup.Controls.Add(this.LevelIndexGear);
            this.GearPartsGroup.Controls.Add(this.QualityGear);
            this.GearPartsGroup.Controls.Add(this.MenuGearParts);
            this.GearPartsGroup.Controls.Add(this.PartsGear);
            this.GearPartsGroup.Controls.Add(this.EquippedSlotGear);
            this.GearPartsGroup.Controls.Add(this.labelGearQuantity);
            this.GearPartsGroup.Controls.Add(this.labelGearEquipped);
            this.GearPartsGroup.Controls.Add(this.QuantityGear);
            this.GearPartsGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GearPartsGroup.Location = new System.Drawing.Point(282, 3);
            this.GearPartsGroup.Name = "GearPartsGroup";
            this.GearPartsGroup.Padding = new System.Windows.Forms.Padding(3);
            this.GearPartsGroup.Size = new System.Drawing.Size(671, 293);
            this.GearPartsGroup.TabIndex = 36;
            this.GearPartsGroup.TabStop = false;
            this.GearPartsGroup.Text = "Gear Parts";
            // 
            // txtGearInformation
            // 
            this.txtGearInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGearInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGearInformation.Location = new System.Drawing.Point(403, 47);
            this.txtGearInformation.Multiline = true;
            this.txtGearInformation.Name = "txtGearInformation";
            this.txtGearInformation.ReadOnly = true;
            this.txtGearInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGearInformation.Size = new System.Drawing.Size(262, 186);
            this.txtGearInformation.TabIndex = 31;
            this.txtGearInformation.WordWrap = false;
            // 
            // LevelIndexGear
            // 
            this.LevelIndexGear.Caption = "Level";
            this.LevelIndexGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelIndexGear.ForeColor = System.Drawing.Color.Black;
            this.LevelIndexGear.Location = new System.Drawing.Point(403, 237);
            this.LevelIndexGear.Maximum = 71;
            this.LevelIndexGear.MaximumAdvanced = 2147483647;
            this.LevelIndexGear.Minimum = 0;
            this.LevelIndexGear.MinimumAdvanced = -2147483648;
            this.LevelIndexGear.Name = "LevelIndexGear";
            this.LevelIndexGear.Size = new System.Drawing.Size(262, 46);
            this.LevelIndexGear.TabIndex = 47;
            this.LevelIndexGear.UpDownBackColor = System.Drawing.Color.White;
            this.LevelIndexGear.Value = 0;
            // 
            // QualityGear
            // 
            this.QualityGear.Caption = "Quality";
            this.QualityGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QualityGear.ForeColor = System.Drawing.Color.Black;
            this.QualityGear.Location = new System.Drawing.Point(119, 237);
            this.QualityGear.Maximum = 5;
            this.QualityGear.MaximumAdvanced = 32767;
            this.QualityGear.Minimum = 0;
            this.QualityGear.MinimumAdvanced = -32768;
            this.QualityGear.Name = "QualityGear";
            this.QualityGear.Size = new System.Drawing.Size(180, 46);
            this.QualityGear.TabIndex = 51;
            this.QualityGear.UpDownBackColor = System.Drawing.Color.White;
            this.QualityGear.Value = 0;
            // 
            // MenuGearParts
            // 
            this.MenuGearParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuGearParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveChangesToolStripMenuItem,
            this.deletePartToolStripMenuItem,
            this.exportGearToolStripMenuItem});
            this.MenuGearParts.Location = new System.Drawing.Point(6, 23);
            this.MenuGearParts.Name = "MenuGearParts";
            this.MenuGearParts.Size = new System.Drawing.Size(659, 24);
            this.MenuGearParts.TabIndex = 40;
            // 
            // saveChangesToolStripMenuItem
            // 
            this.saveChangesToolStripMenuItem.Name = "saveChangesToolStripMenuItem";
            this.saveChangesToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.saveChangesToolStripMenuItem.Text = "Save Changes";
            this.saveChangesToolStripMenuItem.Click += new System.EventHandler(this.SaveChangesGear_Click);
            // 
            // deletePartToolStripMenuItem
            // 
            this.deletePartToolStripMenuItem.Name = "deletePartToolStripMenuItem";
            this.deletePartToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.deletePartToolStripMenuItem.Text = "Delete Part";
            this.deletePartToolStripMenuItem.Click += new System.EventHandler(this.DeletePartGear_Click);
            // 
            // exportGearToolStripMenuItem
            // 
            this.exportGearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toFileToolStripMenuItem,
            this.toClipboardToolStripMenuItem});
            this.exportGearToolStripMenuItem.Name = "exportGearToolStripMenuItem";
            this.exportGearToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.exportGearToolStripMenuItem.Text = "Export Gear";
            // 
            // toFileToolStripMenuItem
            // 
            this.toFileToolStripMenuItem.Name = "toFileToolStripMenuItem";
            this.toFileToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.toFileToolStripMenuItem.Text = "to File";
            this.toFileToolStripMenuItem.Click += new System.EventHandler(this.ExportToFileGear_Click);
            // 
            // toClipboardToolStripMenuItem
            // 
            this.toClipboardToolStripMenuItem.Name = "toClipboardToolStripMenuItem";
            this.toClipboardToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.toClipboardToolStripMenuItem.Text = "to Clipboard";
            this.toClipboardToolStripMenuItem.Click += new System.EventHandler(this.ExportToClipboardGear_Click);
            // 
            // PartsGear
            // 
            this.PartsGear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.PartsGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartsGear.FormattingEnabled = true;
            this.PartsGear.Location = new System.Drawing.Point(6, 47);
            this.PartsGear.Name = "PartsGear";
            this.PartsGear.Size = new System.Drawing.Size(391, 186);
            this.PartsGear.TabIndex = 21;
            this.PartsGear.SelectedIndexChanged += new System.EventHandler(this.PartsGear_SelectedIndexChanged);
            this.PartsGear.SelectedValueChanged += new System.EventHandler(this.PartsGear_SelectedValueChanged);
            this.PartsGear.DoubleClick += new System.EventHandler(this.PartsGear_DoubleClick);
            this.PartsGear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PartsGear_KeyDown);
            // 
            // EquippedSlotGear
            // 
            this.EquippedSlotGear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EquippedSlotGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquippedSlotGear.FormattingEnabled = true;
            this.EquippedSlotGear.Location = new System.Drawing.Point(305, 255);
            this.EquippedSlotGear.Name = "EquippedSlotGear";
            this.EquippedSlotGear.Size = new System.Drawing.Size(92, 21);
            this.EquippedSlotGear.TabIndex = 28;
            // 
            // labelGearQuantity
            // 
            this.labelGearQuantity.AutoSize = true;
            this.labelGearQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGearQuantity.Location = new System.Drawing.Point(6, 239);
            this.labelGearQuantity.Name = "labelGearQuantity";
            this.labelGearQuantity.Size = new System.Drawing.Size(46, 13);
            this.labelGearQuantity.TabIndex = 25;
            this.labelGearQuantity.Text = "Quantity";
            // 
            // labelGearEquipped
            // 
            this.labelGearEquipped.AutoSize = true;
            this.labelGearEquipped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGearEquipped.Location = new System.Drawing.Point(305, 239);
            this.labelGearEquipped.Name = "labelGearEquipped";
            this.labelGearEquipped.Size = new System.Drawing.Size(52, 13);
            this.labelGearEquipped.TabIndex = 27;
            this.labelGearEquipped.Text = "Equipped";
            // 
            // QuantityGear
            // 
            this.QuantityGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityGear.Location = new System.Drawing.Point(6, 255);
            this.QuantityGear.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.QuantityGear.Name = "QuantityGear";
            this.QuantityGear.Size = new System.Drawing.Size(107, 20);
            this.QuantityGear.TabIndex = 22;
            // 
            // gbPartSelectorGear
            // 
            this.gbPartSelectorGear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPartSelectorGear.Controls.Add(this.PartInfoGear);
            this.gbPartSelectorGear.Controls.Add(this.PartSelectorGear);
            this.gbPartSelectorGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPartSelectorGear.Location = new System.Drawing.Point(282, 302);
            this.gbPartSelectorGear.Name = "gbPartSelectorGear";
            this.gbPartSelectorGear.Size = new System.Drawing.Size(671, 286);
            this.gbPartSelectorGear.TabIndex = 35;
            this.gbPartSelectorGear.TabStop = false;
            this.gbPartSelectorGear.Text = "Parts Selector";
            // 
            // PartInfoGear
            // 
            this.PartInfoGear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PartInfoGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartInfoGear.Location = new System.Drawing.Point(422, 23);
            this.PartInfoGear.Multiline = true;
            this.PartInfoGear.Name = "PartInfoGear";
            this.PartInfoGear.ReadOnly = true;
            this.PartInfoGear.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PartInfoGear.Size = new System.Drawing.Size(243, 257);
            this.PartInfoGear.TabIndex = 30;
            this.PartInfoGear.WordWrap = false;
            // 
            // PartSelectorGear
            // 
            this.PartSelectorGear.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.PartSelectorGear.AllowDrop = true;
            this.PartSelectorGear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.PartSelectorGear.DefaultToolTipProvider = null;
            this.PartSelectorGear.DragDropMarkColor = System.Drawing.Color.Black;
            this.PartSelectorGear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartSelectorGear.LineColor = System.Drawing.SystemColors.ControlDark;
            this.PartSelectorGear.Location = new System.Drawing.Point(6, 23);
            this.PartSelectorGear.Name = "PartSelectorGear";
            this.PartSelectorGear.SelectedNode = null;
            this.PartSelectorGear.Size = new System.Drawing.Size(411, 257);
            this.PartSelectorGear.TabIndex = 29;
            this.PartSelectorGear.Text = "from Clipboard";
            this.PartSelectorGear.SelectionChanged += new System.EventHandler(this.PartSelectorGear_SelectionChanged);
            this.PartSelectorGear.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PartSelectorGear_NodeDoubleClick);
            // 
            // copyToBackpackToolStripMenuItem
            // 
            this.copyToBackpackToolStripMenuItem.Name = "copyToBackpackToolStripMenuItem";
            this.copyToBackpackToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+B";
            this.copyToBackpackToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyToBackpackToolStripMenuItem.Text = "Copy to Backpack";
            this.copyToBackpackToolStripMenuItem.Click += new System.EventHandler(this.CopyBackpack_Click);
            // 
            // ucGears
            // 
            this.Controls.Add(this.GearsTab);
            this.Name = "ucGears";
            this.Size = new System.Drawing.Size(956, 591);
            this.GearsTab.ResumeLayout(false);
            this.gbGear.ResumeLayout(false);
            this.gbGear.PerformLayout();
            this.MenuGears.ResumeLayout(false);
            this.MenuGears.PerformLayout();
            this.GearPartsGroup.ResumeLayout(false);
            this.GearPartsGroup.PerformLayout();
            this.MenuGearParts.ResumeLayout(false);
            this.MenuGearParts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QuantityGear)).EndInit();
            this.gbPartSelectorGear.ResumeLayout(false);
            this.gbPartSelectorGear.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel GearsTab;
        private CustomControls.WTGroupBox gbGear;
        private CustomControls.WTMenuStrip MenuGears;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromMultipleFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllToXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAllFromXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorGear;
        private System.Windows.Forms.ToolStripMenuItem editAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private CustomControls.WTTreeView GearTree;
        private CustomControls.WTGroupBox GearPartsGroup;
        private CustomControls.WTSlideSelector LevelIndexGear;
        private CustomControls.WTSlideSelector QualityGear;
        private CustomControls.WTMenuStrip MenuGearParts;
        private System.Windows.Forms.ToolStripMenuItem saveChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportGearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toClipboardToolStripMenuItem;
        private CustomControls.WTListBox PartsGear;
        private CustomControls.WTComboBox EquippedSlotGear;
        private CustomControls.WTLabel labelGearQuantity;
        private CustomControls.WTLabel labelGearEquipped;
        private CustomControls.WTNumericUpDown QuantityGear;
        private CustomControls.WTGroupBox gbPartSelectorGear;
        private CustomControls.WTTextBox PartInfoGear;
        private CustomControls.WTTreeView PartSelectorGear;
        private System.Windows.Forms.ToolStripMenuItem moveToLockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToLockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purgeDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editAllLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editAllQualityToolStripMenuItem;
        private CustomControls.WTButton btnGearSearch;
        private CustomControls.WTTextBox GearSearch;
        private CustomControls.WTTextBox txtGearInformation;
        private System.Windows.Forms.ToolStripMenuItem copyToBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToBackpackToolStripMenuItem;
    }
}
