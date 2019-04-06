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
    partial class ucLocker
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
            this.LockerTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel13 = new WillowTree.CustomControls.WTGroupBox();
            this.MenuLocker = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weaponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.openLockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLockerAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBackpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportAllFromWeapons = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportAllFromItems = new System.Windows.Forms.ToolStripMenuItem();
            this.fromXMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnlockerSearch = new WillowTree.CustomControls.WTButton();
            this.lockerSearch = new WillowTree.CustomControls.WTTextBox();
            this.LockerTree = new WillowTree.CustomControls.WTTreeView();
            this.LockerPartsGroup = new WillowTree.CustomControls.WTGroupBox();
            this.QualityLocker = new WillowTree.CustomControls.WTSlideSelector();
            this.LevelIndexLocker = new WillowTree.CustomControls.WTSlideSelector();
            this.MenuLockerSelected = new WillowTree.CustomControls.WTMenuStrip();
            this.saveChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromMultipleFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupPanel1 = new WillowTree.CustomControls.WTGroupBox();
            this.LevelIndexOverride = new WillowTree.CustomControls.WTSlideSelector();
            this.QualityOverride = new WillowTree.CustomControls.WTSlideSelector();
            this.labelOverrideRemAmmo = new WillowTree.CustomControls.WTLabel();
            this.RemAmmoOverride = new WillowTree.CustomControls.WTNumericUpDown();
            this.OverrideExportSettings = new WillowTree.CustomControls.WTCheckBox();
            this.LockerRemAmmoLabel = new WillowTree.CustomControls.WTLabel();
            this.RemAmmoLocker = new WillowTree.CustomControls.WTNumericUpDown();
            this.PartsLocker = new WillowTree.CustomControls.WTListBox();
            this.PartsLockerLabel = new WillowTree.CustomControls.WTLabel();
            this.DescriptionLockerLabel = new WillowTree.CustomControls.WTLabel();
            this.DescriptionLocker = new WillowTree.CustomControls.WTTextBox();
            this.RatingLocker = new WillowTree.CustomControls.WTTextBox();
            this.copyToBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LockerTab.SuspendLayout();
            this.groupPanel13.SuspendLayout();
            this.MenuLocker.SuspendLayout();
            this.LockerPartsGroup.SuspendLayout();
            this.MenuLockerSelected.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemAmmoOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemAmmoLocker)).BeginInit();
            this.SuspendLayout();
            // 
            // LockerTab
            // 
            this.LockerTab.Controls.Add(this.groupPanel13);
            this.LockerTab.Controls.Add(this.LockerPartsGroup);
            this.LockerTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LockerTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerTab.ForeColor = System.Drawing.Color.White;
            this.LockerTab.Location = new System.Drawing.Point(0, 0);
            this.LockerTab.Name = "LockerTab";
            this.LockerTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.LockerTab.Size = new System.Drawing.Size(956, 591);
            this.LockerTab.TabIndex = 7;
            this.LockerTab.Text = "Locker";
            // 
            // groupPanel13
            // 
            this.groupPanel13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel13.Controls.Add(this.MenuLocker);
            this.groupPanel13.Controls.Add(this.btnlockerSearch);
            this.groupPanel13.Controls.Add(this.lockerSearch);
            this.groupPanel13.Controls.Add(this.LockerTree);
            this.groupPanel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel13.Location = new System.Drawing.Point(3, 3);
            this.groupPanel13.Name = "groupPanel13";
            this.groupPanel13.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel13.Size = new System.Drawing.Size(273, 585);
            this.groupPanel13.TabIndex = 24;
            this.groupPanel13.TabStop = false;
            this.groupPanel13.Text = "Locker";
            // 
            // MenuLocker
            // 
            this.MenuLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLocker.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem5,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem6});
            this.MenuLocker.Location = new System.Drawing.Point(6, 23);
            this.MenuLocker.Name = "MenuLocker";
            this.MenuLocker.Size = new System.Drawing.Size(261, 24);
            this.MenuLocker.TabIndex = 44;
            this.MenuLocker.Text = "menuStrip2";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.weaponToolStripMenuItem,
            this.itemToolStripMenuItem1});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.newToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.newToolStripMenuItem.Text = "New";
            // 
            // weaponToolStripMenuItem
            // 
            this.weaponToolStripMenuItem.Name = "weaponToolStripMenuItem";
            this.weaponToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.weaponToolStripMenuItem.Text = "Weapon";
            this.weaponToolStripMenuItem.Click += new System.EventHandler(this.NewWeaponLocker_Click);
            // 
            // itemToolStripMenuItem1
            // 
            this.itemToolStripMenuItem1.Name = "itemToolStripMenuItem1";
            this.itemToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.itemToolStripMenuItem1.Text = "Item";
            this.itemToolStripMenuItem1.Click += new System.EventHandler(this.NewItemLocker_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteLocker_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLockerToolStripMenuItem,
            this.saveLockerAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearAllToolStripMenuItem,
            this.copyToBackpackToolStripMenuItem,
            this.copyToBankToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.moveToBackpackToolStripMenuItem,
            this.purgeDuplicatesToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem5.Text = "Actions";
            // 
            // openLockerToolStripMenuItem
            // 
            this.openLockerToolStripMenuItem.Name = "openLockerToolStripMenuItem";
            this.openLockerToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openLockerToolStripMenuItem.Text = "Open Locker";
            this.openLockerToolStripMenuItem.Click += new System.EventHandler(this.OpenLocker_Click);
            // 
            // saveLockerAsToolStripMenuItem
            // 
            this.saveLockerAsToolStripMenuItem.Name = "saveLockerAsToolStripMenuItem";
            this.saveLockerAsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveLockerAsToolStripMenuItem.Text = "Save Locker As";
            this.saveLockerAsToolStripMenuItem.Click += new System.EventHandler(this.ExportToXmlLocker_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.ClearAllLocker_Click);
            // 
            // copyToBackpackToolStripMenuItem
            // 
            this.copyToBackpackToolStripMenuItem.Name = "copyToBackpackToolStripMenuItem";
            this.copyToBackpackToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+B";
            this.copyToBackpackToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyToBackpackToolStripMenuItem.Text = "Copy to Backpack";
            this.copyToBackpackToolStripMenuItem.Click += new System.EventHandler(this.CopyBackpack_Click);
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.ShortcutKeyDisplayString = "Ins";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.duplicateToolStripMenuItem.Text = "Duplicate";
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.DuplicateLocker_Click);
            // 
            // moveToBackpackToolStripMenuItem
            // 
            this.moveToBackpackToolStripMenuItem.Name = "moveToBackpackToolStripMenuItem";
            this.moveToBackpackToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.moveToBackpackToolStripMenuItem.Text = "Move to Backpack";
            this.moveToBackpackToolStripMenuItem.Click += new System.EventHandler(this.MoveLocker_Click);
            // 
            // purgeDuplicatesToolStripMenuItem
            // 
            this.purgeDuplicatesToolStripMenuItem.Name = "purgeDuplicatesToolStripMenuItem";
            this.purgeDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.purgeDuplicatesToolStripMenuItem.Text = "Purge Duplicates";
            this.purgeDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.PurgeDuplicatesLocker_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.ExportToXmlLocker_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportAllFromWeapons,
            this.ImportAllFromItems,
            this.fromXMLFileToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(58, 20);
            this.toolStripMenuItem1.Text = "Import All";
            // 
            // ImportAllFromWeapons
            // 
            this.ImportAllFromWeapons.Name = "ImportAllFromWeapons";
            this.ImportAllFromWeapons.Size = new System.Drawing.Size(146, 22);
            this.ImportAllFromWeapons.Text = "from Weapons";
            this.ImportAllFromWeapons.Click += new System.EventHandler(this.ImportAllFromWeaponsLocker_Click);
            // 
            // ImportAllFromItems
            // 
            this.ImportAllFromItems.Name = "ImportAllFromItems";
            this.ImportAllFromItems.Size = new System.Drawing.Size(146, 22);
            this.ImportAllFromItems.Text = "from Items";
            this.ImportAllFromItems.Click += new System.EventHandler(this.ImportAllFromItemsLocker_Click);
            // 
            // fromXMLFileToolStripMenuItem
            // 
            this.fromXMLFileToolStripMenuItem.Name = "fromXMLFileToolStripMenuItem";
            this.fromXMLFileToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.fromXMLFileToolStripMenuItem.Text = "from XML File";
            this.fromXMLFileToolStripMenuItem.Click += new System.EventHandler(this.ImportAllFromXmlLocker_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.OpenLocker_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(12, 20);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(12, 20);
            // 
            // btnlockerSearch
            // 
            this.btnlockerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnlockerSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlockerSearch.Location = new System.Drawing.Point(215, 556);
            this.btnlockerSearch.Name = "btnlockerSearch";
            this.btnlockerSearch.Size = new System.Drawing.Size(52, 23);
            this.btnlockerSearch.TabIndex = 12;
            this.btnlockerSearch.Text = "Search";
            this.btnlockerSearch.UseVisualStyleBackColor = false;
            this.btnlockerSearch.Click += new System.EventHandler(this.btnlockerSearch_Click);
            // 
            // lockerSearch
            // 
            this.lockerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lockerSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lockerSearch.Location = new System.Drawing.Point(6, 559);
            this.lockerSearch.Name = "lockerSearch";
            this.lockerSearch.Size = new System.Drawing.Size(200, 20);
            this.lockerSearch.TabIndex = 43;
            this.lockerSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lockerSearch_KeyDown);
            // 
            // LockerTree
            // 
            this.LockerTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.LockerTree.AllowDrop = true;
            this.LockerTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.LockerTree.DefaultToolTipProvider = null;
            this.LockerTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.LockerTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerTree.Indent = 15;
            this.LockerTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.LockerTree.Location = new System.Drawing.Point(6, 47);
            this.LockerTree.Name = "LockerTree";
            this.LockerTree.SelectedNode = null;
            this.LockerTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.LockerTree.Size = new System.Drawing.Size(261, 503);
            this.LockerTree.TabIndex = 20;
            this.LockerTree.Text = "advTree1";
            this.LockerTree.SelectionChanged += new System.EventHandler(this.LockerTree_SelectionChanged);
            this.LockerTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LockerTree_KeyDown);
            // 
            // LockerPartsGroup
            // 
            this.LockerPartsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LockerPartsGroup.Controls.Add(this.QualityLocker);
            this.LockerPartsGroup.Controls.Add(this.LevelIndexLocker);
            this.LockerPartsGroup.Controls.Add(this.MenuLockerSelected);
            this.LockerPartsGroup.Controls.Add(this.groupPanel1);
            this.LockerPartsGroup.Controls.Add(this.LockerRemAmmoLabel);
            this.LockerPartsGroup.Controls.Add(this.RemAmmoLocker);
            this.LockerPartsGroup.Controls.Add(this.PartsLocker);
            this.LockerPartsGroup.Controls.Add(this.PartsLockerLabel);
            this.LockerPartsGroup.Controls.Add(this.DescriptionLockerLabel);
            this.LockerPartsGroup.Controls.Add(this.DescriptionLocker);
            this.LockerPartsGroup.Controls.Add(this.RatingLocker);
            this.LockerPartsGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerPartsGroup.Location = new System.Drawing.Point(282, 3);
            this.LockerPartsGroup.Name = "LockerPartsGroup";
            this.LockerPartsGroup.Padding = new System.Windows.Forms.Padding(3);
            this.LockerPartsGroup.Size = new System.Drawing.Size(671, 585);
            this.LockerPartsGroup.TabIndex = 25;
            this.LockerPartsGroup.TabStop = false;
            this.LockerPartsGroup.Text = "Selected equipment";
            // 
            // QualityLocker
            // 
            this.QualityLocker.Caption = "Quality";
            this.QualityLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QualityLocker.ForeColor = System.Drawing.Color.Black;
            this.QualityLocker.Location = new System.Drawing.Point(232, 309);
            this.QualityLocker.Maximum = 5;
            this.QualityLocker.MaximumAdvanced = 32767;
            this.QualityLocker.Minimum = 0;
            this.QualityLocker.MinimumAdvanced = -32768;
            this.QualityLocker.Name = "QualityLocker";
            this.QualityLocker.Size = new System.Drawing.Size(154, 46);
            this.QualityLocker.TabIndex = 52;
            this.QualityLocker.UpDownBackColor = System.Drawing.Color.White;
            this.QualityLocker.Value = 0;
            // 
            // LevelIndexLocker
            // 
            this.LevelIndexLocker.Caption = "Level";
            this.LevelIndexLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelIndexLocker.ForeColor = System.Drawing.Color.Black;
            this.LevelIndexLocker.Location = new System.Drawing.Point(418, 309);
            this.LevelIndexLocker.Maximum = 71;
            this.LevelIndexLocker.MaximumAdvanced = 2147483647;
            this.LevelIndexLocker.Minimum = 0;
            this.LevelIndexLocker.MinimumAdvanced = -2147483648;
            this.LevelIndexLocker.Name = "LevelIndexLocker";
            this.LevelIndexLocker.Size = new System.Drawing.Size(232, 46);
            this.LevelIndexLocker.TabIndex = 51;
            this.LevelIndexLocker.UpDownBackColor = System.Drawing.Color.White;
            this.LevelIndexLocker.Value = 0;
            // 
            // MenuLockerSelected
            // 
            this.MenuLockerSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLockerSelected.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveChangesToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.MenuLockerSelected.Location = new System.Drawing.Point(6, 23);
            this.MenuLockerSelected.Name = "MenuLockerSelected";
            this.MenuLockerSelected.Size = new System.Drawing.Size(659, 24);
            this.MenuLockerSelected.TabIndex = 49;
            this.MenuLockerSelected.Text = "menuStrip1";
            // 
            // saveChangesToolStripMenuItem
            // 
            this.saveChangesToolStripMenuItem.Name = "saveChangesToolStripMenuItem";
            this.saveChangesToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.saveChangesToolStripMenuItem.Text = "Save Changes";
            this.saveChangesToolStripMenuItem.Click += new System.EventHandler(this.SaveChangesLocker_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromMultipleFilesToolStripMenuItem,
            this.fromClipboardToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromMultipleFilesToolStripMenuItem
            // 
            this.fromMultipleFilesToolStripMenuItem.Name = "fromMultipleFilesToolStripMenuItem";
            this.fromMultipleFilesToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromMultipleFilesToolStripMenuItem.Text = "from File(s)";
            this.fromMultipleFilesToolStripMenuItem.Click += new System.EventHandler(this.ImportFromFilesLocker_Click);
            // 
            // fromClipboardToolStripMenuItem
            // 
            this.fromClipboardToolStripMenuItem.Name = "fromClipboardToolStripMenuItem";
            this.fromClipboardToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromClipboardToolStripMenuItem.Text = "from Clipboard";
            this.fromClipboardToolStripMenuItem.Click += new System.EventHandler(this.ImportFromClipboardLocker_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toFileToolStripMenuItem,
            this.toClipboardToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toFileToolStripMenuItem
            // 
            this.toFileToolStripMenuItem.Name = "toFileToolStripMenuItem";
            this.toFileToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.toFileToolStripMenuItem.Text = "to File";
            this.toFileToolStripMenuItem.Click += new System.EventHandler(this.ExportToFileLocker_Click);
            // 
            // toClipboardToolStripMenuItem
            // 
            this.toClipboardToolStripMenuItem.Name = "toClipboardToolStripMenuItem";
            this.toClipboardToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.toClipboardToolStripMenuItem.Text = "to Clipboard";
            this.toClipboardToolStripMenuItem.Click += new System.EventHandler(this.ExportToClipboardLocker_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.Controls.Add(this.LevelIndexOverride);
            this.groupPanel1.Controls.Add(this.QualityOverride);
            this.groupPanel1.Controls.Add(this.labelOverrideRemAmmo);
            this.groupPanel1.Controls.Add(this.RemAmmoOverride);
            this.groupPanel1.Controls.Add(this.OverrideExportSettings);
            this.groupPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(6, 50);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(658, 70);
            this.groupPanel1.TabIndex = 36;
            this.groupPanel1.TabStop = false;
            this.groupPanel1.Text = "Export Override Settings";
            // 
            // LevelIndexOverride
            // 
            this.LevelIndexOverride.Caption = "Level";
            this.LevelIndexOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelIndexOverride.ForeColor = System.Drawing.Color.Black;
            this.LevelIndexOverride.Location = new System.Drawing.Point(412, 21);
            this.LevelIndexOverride.Maximum = 71;
            this.LevelIndexOverride.MaximumAdvanced = 65535;
            this.LevelIndexOverride.Minimum = 0;
            this.LevelIndexOverride.MinimumAdvanced = 0;
            this.LevelIndexOverride.Name = "LevelIndexOverride";
            this.LevelIndexOverride.Size = new System.Drawing.Size(232, 46);
            this.LevelIndexOverride.TabIndex = 45;
            this.LevelIndexOverride.UpDownBackColor = System.Drawing.Color.White;
            this.LevelIndexOverride.Value = 0;
            // 
            // QualityOverride
            // 
            this.QualityOverride.Caption = "Quality";
            this.QualityOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QualityOverride.ForeColor = System.Drawing.Color.Black;
            this.QualityOverride.Location = new System.Drawing.Point(226, 21);
            this.QualityOverride.Maximum = 5;
            this.QualityOverride.MaximumAdvanced = 65535;
            this.QualityOverride.Minimum = 0;
            this.QualityOverride.MinimumAdvanced = 0;
            this.QualityOverride.Name = "QualityOverride";
            this.QualityOverride.Size = new System.Drawing.Size(154, 46);
            this.QualityOverride.TabIndex = 44;
            this.QualityOverride.UpDownBackColor = System.Drawing.Color.White;
            this.QualityOverride.Value = 0;
            // 
            // labelOverrideRemAmmo
            // 
            this.labelOverrideRemAmmo.AutoSize = true;
            this.labelOverrideRemAmmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOverrideRemAmmo.Location = new System.Drawing.Point(106, 23);
            this.labelOverrideRemAmmo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.labelOverrideRemAmmo.Name = "labelOverrideRemAmmo";
            this.labelOverrideRemAmmo.Size = new System.Drawing.Size(80, 13);
            this.labelOverrideRemAmmo.TabIndex = 27;
            this.labelOverrideRemAmmo.Text = "Ammo/Quantity";
            // 
            // RemAmmoOverride
            // 
            this.RemAmmoOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemAmmoOverride.Location = new System.Drawing.Point(106, 39);
            this.RemAmmoOverride.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RemAmmoOverride.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RemAmmoOverride.Name = "RemAmmoOverride";
            this.RemAmmoOverride.Size = new System.Drawing.Size(89, 20);
            this.RemAmmoOverride.TabIndex = 26;
            // 
            // OverrideExportSettings
            // 
            this.OverrideExportSettings.AutoSize = true;
            this.OverrideExportSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverrideExportSettings.Location = new System.Drawing.Point(6, 22);
            this.OverrideExportSettings.Name = "OverrideExportSettings";
            this.OverrideExportSettings.Size = new System.Drawing.Size(88, 17);
            this.OverrideExportSettings.TabIndex = 43;
            this.OverrideExportSettings.Text = "Use Override";
            this.OverrideExportSettings.UseVisualStyleBackColor = false;
            // 
            // LockerRemAmmoLabel
            // 
            this.LockerRemAmmoLabel.AutoSize = true;
            this.LockerRemAmmoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LockerRemAmmoLabel.Location = new System.Drawing.Point(112, 311);
            this.LockerRemAmmoLabel.Name = "LockerRemAmmoLabel";
            this.LockerRemAmmoLabel.Size = new System.Drawing.Size(89, 13);
            this.LockerRemAmmoLabel.TabIndex = 45;
            this.LockerRemAmmoLabel.Text = "Remaining Ammo";
            // 
            // RemAmmoLocker
            // 
            this.RemAmmoLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemAmmoLocker.Location = new System.Drawing.Point(112, 327);
            this.RemAmmoLocker.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RemAmmoLocker.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RemAmmoLocker.Name = "RemAmmoLocker";
            this.RemAmmoLocker.Size = new System.Drawing.Size(89, 20);
            this.RemAmmoLocker.TabIndex = 43;
            // 
            // PartsLocker
            // 
            this.PartsLocker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PartsLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartsLocker.FormattingEnabled = true;
            this.PartsLocker.Items.AddRange(new object[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""});
            this.PartsLocker.Location = new System.Drawing.Point(6, 364);
            this.PartsLocker.Name = "PartsLocker";
            this.PartsLocker.Size = new System.Drawing.Size(658, 212);
            this.PartsLocker.TabIndex = 9;
            // 
            // PartsLockerLabel
            // 
            this.PartsLockerLabel.AutoSize = true;
            this.PartsLockerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartsLockerLabel.Location = new System.Drawing.Point(6, 348);
            this.PartsLockerLabel.Name = "PartsLockerLabel";
            this.PartsLockerLabel.Size = new System.Drawing.Size(31, 13);
            this.PartsLockerLabel.TabIndex = 8;
            this.PartsLockerLabel.Text = "Parts";
            // 
            // DescriptionLockerLabel
            // 
            this.DescriptionLockerLabel.AutoSize = true;
            this.DescriptionLockerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLockerLabel.Location = new System.Drawing.Point(6, 168);
            this.DescriptionLockerLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.DescriptionLockerLabel.Name = "DescriptionLockerLabel";
            this.DescriptionLockerLabel.Size = new System.Drawing.Size(60, 13);
            this.DescriptionLockerLabel.TabIndex = 7;
            this.DescriptionLockerLabel.Text = "Description";
            // 
            // DescriptionLocker
            // 
            this.DescriptionLocker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescriptionLocker.Location = new System.Drawing.Point(6, 184);
            this.DescriptionLocker.Multiline = true;
            this.DescriptionLocker.Name = "DescriptionLocker";
            this.DescriptionLocker.Size = new System.Drawing.Size(658, 118);
            this.DescriptionLocker.TabIndex = 6;
            // 
            // RatingLocker
            // 
            this.RatingLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RatingLocker.Location = new System.Drawing.Point(425, 142);
            this.RatingLocker.Name = "RatingLocker";
            this.RatingLocker.Size = new System.Drawing.Size(106, 20);
            this.RatingLocker.TabIndex = 2;
            this.RatingLocker.Text = "Rating";
            // 
            // 
            // 
            // copyToBankToolStripMenuItem
            // 
            this.copyToBankToolStripMenuItem.Name = "copyToBankToolStripMenuItem";
            this.copyToBankToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.copyToBankToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyToBankToolStripMenuItem.Text = "Copy to Bank";
            this.copyToBankToolStripMenuItem.Click += new System.EventHandler(this.CopyBank_Click);
            // 
            // ucLocker
            // 
            this.Controls.Add(this.LockerTab);
            this.Name = "ucLocker";
            this.Size = new System.Drawing.Size(956, 591);
            this.LockerTab.ResumeLayout(false);
            this.groupPanel13.ResumeLayout(false);
            this.groupPanel13.PerformLayout();
            this.MenuLocker.ResumeLayout(false);
            this.MenuLocker.PerformLayout();
            this.LockerPartsGroup.ResumeLayout(false);
            this.LockerPartsGroup.PerformLayout();
            this.MenuLockerSelected.ResumeLayout(false);
            this.MenuLockerSelected.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemAmmoOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemAmmoLocker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel LockerTab;
        private CustomControls.WTGroupBox groupPanel13;
        private CustomControls.WTMenuStrip MenuLocker;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weaponToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem openLockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLockerAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ImportAllFromWeapons;
        private System.Windows.Forms.ToolStripMenuItem ImportAllFromItems;
        private System.Windows.Forms.ToolStripMenuItem fromXMLFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private CustomControls.WTButton btnlockerSearch;
        private CustomControls.WTTextBox lockerSearch;
        private CustomControls.WTTreeView LockerTree;
        private CustomControls.WTGroupBox LockerPartsGroup;
        private CustomControls.WTSlideSelector QualityLocker;
        private CustomControls.WTSlideSelector LevelIndexLocker;
        private CustomControls.WTMenuStrip MenuLockerSelected;
        private System.Windows.Forms.ToolStripMenuItem saveChangesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromMultipleFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toClipboardToolStripMenuItem;
        private CustomControls.WTGroupBox groupPanel1;
        private CustomControls.WTSlideSelector LevelIndexOverride;
        private CustomControls.WTSlideSelector QualityOverride;
        private CustomControls.WTLabel labelOverrideRemAmmo;
        private CustomControls.WTNumericUpDown RemAmmoOverride;
        private CustomControls.WTCheckBox OverrideExportSettings;
        private CustomControls.WTLabel LockerRemAmmoLabel;
        private CustomControls.WTNumericUpDown RemAmmoLocker;
        private CustomControls.WTListBox PartsLocker;
        private CustomControls.WTLabel PartsLockerLabel;
        private CustomControls.WTLabel DescriptionLockerLabel;
        private CustomControls.WTTextBox DescriptionLocker;
        private CustomControls.WTTextBox RatingLocker;
        private System.Windows.Forms.ToolStripMenuItem purgeDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBackpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToBackpackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyToBankToolStripMenuItem;

    }
}
