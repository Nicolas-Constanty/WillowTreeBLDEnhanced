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
    partial class ucGeneral
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
            this.GeneralTab = new WillowTree.CustomControls.CCPanel();
            this.groupPanel8 = new WillowTree.CustomControls.WTGroupBox();
            this.labelGeneralBankSpace = new WillowTree.CustomControls.WTLabel();
            this.BankSpace = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelGeneralCurrentLocation = new WillowTree.CustomControls.WTLabel();
            this.CurrentLocation = new WillowTree.CustomControls.WTComboBox();
            this.Level = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelGeneralEquipSlots = new WillowTree.CustomControls.WTLabel();
            this.Experience = new WillowTree.CustomControls.WTNumericUpDown();
            this.EquipSlots = new WillowTree.CustomControls.WTNumericUpDown();
            this.Cash = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelGeneralBackpackSpace = new WillowTree.CustomControls.WTLabel();
            this.SkillPoints = new WillowTree.CustomControls.WTNumericUpDown();
            this.BackpackSpace = new WillowTree.CustomControls.WTNumericUpDown();
            this.SaveNumber = new WillowTree.CustomControls.WTNumericUpDown();
            this.labelGeneralPT2Unlocked = new WillowTree.CustomControls.WTLabel();
            this.labelGeneralLevel = new WillowTree.CustomControls.WTLabel();
            this.PT2Unlocked = new WillowTree.CustomControls.WTComboBox();
            this.labelGeneralExperience = new WillowTree.CustomControls.WTLabel();
            this.Class = new WillowTree.CustomControls.WTComboBox();
            this.labelGeneralCash = new WillowTree.CustomControls.WTLabel();
            this.labelGeneralClass = new WillowTree.CustomControls.WTLabel();
            this.labelGeneralSkillPoints = new WillowTree.CustomControls.WTLabel();
            this.labelGeneralCharacterName = new WillowTree.CustomControls.WTLabel();
            this.labelGeneralSaveNumber = new WillowTree.CustomControls.WTLabel();
            this.CharacterName = new WillowTree.CustomControls.WTTextBox();
            this.groupPanel10 = new WillowTree.CustomControls.WTGroupBox();
            this.MenuLocations = new WillowTree.CustomControls.WTMenuStrip();
            this.newToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.LocationsList = new System.Windows.Forms.ToolStripComboBox();
            this.deleteToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllToXMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mergeFromLocationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeFromAnotherSaveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.importAllFromXMLToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFromAnotherSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.eraseAllLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LocationTree = new WillowTree.CustomControls.WTTreeView();
            this.GeneralTab.SuspendLayout();
            this.groupPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BankSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Level)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Experience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipSlots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackpackSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveNumber)).BeginInit();
            this.groupPanel10.SuspendLayout();
            this.MenuLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.groupPanel8);
            this.GeneralTab.Controls.Add(this.groupPanel10);
            this.GeneralTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GeneralTab.ForeColor = System.Drawing.Color.White;
            this.GeneralTab.Location = new System.Drawing.Point(0, 0);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.GeneralTab.Size = new System.Drawing.Size(956, 591);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "General";
            // 
            // groupPanel8
            // 
            this.groupPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel8.Controls.Add(this.labelGeneralBankSpace);
            this.groupPanel8.Controls.Add(this.BankSpace);
            this.groupPanel8.Controls.Add(this.labelGeneralCurrentLocation);
            this.groupPanel8.Controls.Add(this.CurrentLocation);
            this.groupPanel8.Controls.Add(this.Level);
            this.groupPanel8.Controls.Add(this.labelGeneralEquipSlots);
            this.groupPanel8.Controls.Add(this.Experience);
            this.groupPanel8.Controls.Add(this.EquipSlots);
            this.groupPanel8.Controls.Add(this.Cash);
            this.groupPanel8.Controls.Add(this.labelGeneralBackpackSpace);
            this.groupPanel8.Controls.Add(this.SkillPoints);
            this.groupPanel8.Controls.Add(this.BackpackSpace);
            this.groupPanel8.Controls.Add(this.SaveNumber);
            this.groupPanel8.Controls.Add(this.labelGeneralPT2Unlocked);
            this.groupPanel8.Controls.Add(this.labelGeneralLevel);
            this.groupPanel8.Controls.Add(this.PT2Unlocked);
            this.groupPanel8.Controls.Add(this.labelGeneralExperience);
            this.groupPanel8.Controls.Add(this.Class);
            this.groupPanel8.Controls.Add(this.labelGeneralCash);
            this.groupPanel8.Controls.Add(this.labelGeneralClass);
            this.groupPanel8.Controls.Add(this.labelGeneralSkillPoints);
            this.groupPanel8.Controls.Add(this.labelGeneralCharacterName);
            this.groupPanel8.Controls.Add(this.labelGeneralSaveNumber);
            this.groupPanel8.Controls.Add(this.CharacterName);
            this.groupPanel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel8.Location = new System.Drawing.Point(3, 3);
            this.groupPanel8.Name = "groupPanel8";
            this.groupPanel8.Size = new System.Drawing.Size(321, 585);
            this.groupPanel8.TabIndex = 32;
            this.groupPanel8.TabStop = false;
            this.groupPanel8.Text = "General Information";
            // 
            // labelGeneralBankSpace
            // 
            this.labelGeneralBankSpace.AutoSize = true;
            this.labelGeneralBankSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralBankSpace.Location = new System.Drawing.Point(195, 145);
            this.labelGeneralBankSpace.Name = "labelGeneralBankSpace";
            this.labelGeneralBankSpace.Size = new System.Drawing.Size(76, 13);
            this.labelGeneralBankSpace.TabIndex = 35;
            this.labelGeneralBankSpace.Text = "Bank Capacity";
            // 
            // BankSpace
            // 
            this.BankSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BankSpace.Location = new System.Drawing.Point(195, 161);
            this.BankSpace.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BankSpace.Name = "BankSpace";
            this.BankSpace.Size = new System.Drawing.Size(120, 20);
            this.BankSpace.TabIndex = 7;
            // 
            // labelGeneralCurrentLocation
            // 
            this.labelGeneralCurrentLocation.AutoSize = true;
            this.labelGeneralCurrentLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralCurrentLocation.Location = new System.Drawing.Point(6, 365);
            this.labelGeneralCurrentLocation.Name = "labelGeneralCurrentLocation";
            this.labelGeneralCurrentLocation.Size = new System.Drawing.Size(85, 13);
            this.labelGeneralCurrentLocation.TabIndex = 33;
            this.labelGeneralCurrentLocation.Text = "Current Location";
            // 
            // CurrentLocation
            // 
            this.CurrentLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrentLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLocation.FormattingEnabled = true;
            this.CurrentLocation.Location = new System.Drawing.Point(6, 381);
            this.CurrentLocation.Name = "CurrentLocation";
            this.CurrentLocation.Size = new System.Drawing.Size(309, 21);
            this.CurrentLocation.TabIndex = 11;
            // 
            // Level
            // 
            this.Level.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Level.Location = new System.Drawing.Point(6, 161);
            this.Level.Maximum = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.Level.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(120, 20);
            this.Level.TabIndex = 2;
            this.Level.ValueChanged += new System.EventHandler(this.Level_ValueChanged);
            // 
            // labelGeneralEquipSlots
            // 
            this.labelGeneralEquipSlots.AutoSize = true;
            this.labelGeneralEquipSlots.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralEquipSlots.Location = new System.Drawing.Point(195, 200);
            this.labelGeneralEquipSlots.Name = "labelGeneralEquipSlots";
            this.labelGeneralEquipSlots.Size = new System.Drawing.Size(104, 13);
            this.labelGeneralEquipSlots.TabIndex = 31;
            this.labelGeneralEquipSlots.Text = "Weapon Equip Slots";
            // 
            // Experience
            // 
            this.Experience.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Experience.Location = new System.Drawing.Point(6, 216);
            this.Experience.Maximum = new decimal(new int[] {
            8451341,
            0,
            0,
            0});
            this.Experience.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Experience.Name = "Experience";
            this.Experience.Size = new System.Drawing.Size(120, 20);
            this.Experience.TabIndex = 3;
            // 
            // EquipSlots
            // 
            this.EquipSlots.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EquipSlots.Location = new System.Drawing.Point(195, 216);
            this.EquipSlots.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.EquipSlots.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.EquipSlots.Name = "EquipSlots";
            this.EquipSlots.Size = new System.Drawing.Size(120, 20);
            this.EquipSlots.TabIndex = 8;
            // 
            // Cash
            // 
            this.Cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cash.Location = new System.Drawing.Point(6, 326);
            this.Cash.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Cash.Name = "Cash";
            this.Cash.Size = new System.Drawing.Size(120, 20);
            this.Cash.TabIndex = 5;
            // 
            // labelGeneralBackpackSpace
            // 
            this.labelGeneralBackpackSpace.AutoSize = true;
            this.labelGeneralBackpackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralBackpackSpace.Location = new System.Drawing.Point(195, 89);
            this.labelGeneralBackpackSpace.Name = "labelGeneralBackpackSpace";
            this.labelGeneralBackpackSpace.Size = new System.Drawing.Size(100, 13);
            this.labelGeneralBackpackSpace.TabIndex = 29;
            this.labelGeneralBackpackSpace.Text = "Backpack Capacity";
            // 
            // SkillPoints
            // 
            this.SkillPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkillPoints.Location = new System.Drawing.Point(6, 271);
            this.SkillPoints.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SkillPoints.Name = "SkillPoints";
            this.SkillPoints.Size = new System.Drawing.Size(120, 20);
            this.SkillPoints.TabIndex = 4;
            // 
            // BackpackSpace
            // 
            this.BackpackSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackpackSpace.Location = new System.Drawing.Point(195, 105);
            this.BackpackSpace.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BackpackSpace.Name = "BackpackSpace";
            this.BackpackSpace.Size = new System.Drawing.Size(120, 20);
            this.BackpackSpace.TabIndex = 6;
            // 
            // SaveNumber
            // 
            this.SaveNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveNumber.Location = new System.Drawing.Point(195, 326);
            this.SaveNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.SaveNumber.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SaveNumber.Name = "SaveNumber";
            this.SaveNumber.Size = new System.Drawing.Size(120, 20);
            this.SaveNumber.TabIndex = 10;
            // 
            // labelGeneralPT2Unlocked
            // 
            this.labelGeneralPT2Unlocked.AutoSize = true;
            this.labelGeneralPT2Unlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralPT2Unlocked.Location = new System.Drawing.Point(195, 255);
            this.labelGeneralPT2Unlocked.Name = "labelGeneralPT2Unlocked";
            this.labelGeneralPT2Unlocked.Size = new System.Drawing.Size(121, 13);
            this.labelGeneralPT2Unlocked.TabIndex = 27;
            this.labelGeneralPT2Unlocked.Text = "Playthrough 2 Unlocked";
            // 
            // labelGeneralLevel
            // 
            this.labelGeneralLevel.AutoSize = true;
            this.labelGeneralLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralLevel.Location = new System.Drawing.Point(6, 145);
            this.labelGeneralLevel.Name = "labelGeneralLevel";
            this.labelGeneralLevel.Size = new System.Drawing.Size(33, 13);
            this.labelGeneralLevel.TabIndex = 17;
            this.labelGeneralLevel.Text = "Level";
            // 
            // PT2Unlocked
            // 
            this.PT2Unlocked.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PT2Unlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PT2Unlocked.FormattingEnabled = true;
            this.PT2Unlocked.Items.AddRange(new object[] {
            "No",
            "Yes",
            "2"});
            this.PT2Unlocked.Location = new System.Drawing.Point(195, 271);
            this.PT2Unlocked.Name = "PT2Unlocked";
            this.PT2Unlocked.Size = new System.Drawing.Size(120, 21);
            this.PT2Unlocked.TabIndex = 9;
            // 
            // labelGeneralExperience
            // 
            this.labelGeneralExperience.AutoSize = true;
            this.labelGeneralExperience.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralExperience.Location = new System.Drawing.Point(6, 200);
            this.labelGeneralExperience.Name = "labelGeneralExperience";
            this.labelGeneralExperience.Size = new System.Drawing.Size(60, 13);
            this.labelGeneralExperience.TabIndex = 18;
            this.labelGeneralExperience.Text = "Experience";
            // 
            // Class
            // 
            this.Class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Class.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Class.FormattingEnabled = true;
            this.Class.Items.AddRange(new object[] {
            "Soldier",
            "Siren",
            "Hunter",
            "Berserker"});
            this.Class.Location = new System.Drawing.Point(6, 105);
            this.Class.Name = "Class";
            this.Class.Size = new System.Drawing.Size(120, 21);
            this.Class.TabIndex = 1;
            this.Class.SelectedIndexChanged += new System.EventHandler(this.Class_SelectedIndexChanged);
            // 
            // labelGeneralCash
            // 
            this.labelGeneralCash.AutoSize = true;
            this.labelGeneralCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralCash.Location = new System.Drawing.Point(6, 310);
            this.labelGeneralCash.Name = "labelGeneralCash";
            this.labelGeneralCash.Size = new System.Drawing.Size(31, 13);
            this.labelGeneralCash.TabIndex = 19;
            this.labelGeneralCash.Text = "Cash";
            // 
            // labelGeneralClass
            // 
            this.labelGeneralClass.AutoSize = true;
            this.labelGeneralClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralClass.Location = new System.Drawing.Point(6, 89);
            this.labelGeneralClass.Name = "labelGeneralClass";
            this.labelGeneralClass.Size = new System.Drawing.Size(32, 13);
            this.labelGeneralClass.TabIndex = 24;
            this.labelGeneralClass.Text = "Class";
            // 
            // labelGeneralSkillPoints
            // 
            this.labelGeneralSkillPoints.AutoSize = true;
            this.labelGeneralSkillPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralSkillPoints.Location = new System.Drawing.Point(6, 255);
            this.labelGeneralSkillPoints.Name = "labelGeneralSkillPoints";
            this.labelGeneralSkillPoints.Size = new System.Drawing.Size(58, 13);
            this.labelGeneralSkillPoints.TabIndex = 20;
            this.labelGeneralSkillPoints.Text = "Skill Points";
            // 
            // labelGeneralCharacterName
            // 
            this.labelGeneralCharacterName.AutoSize = true;
            this.labelGeneralCharacterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralCharacterName.Location = new System.Drawing.Point(6, 34);
            this.labelGeneralCharacterName.Name = "labelGeneralCharacterName";
            this.labelGeneralCharacterName.Size = new System.Drawing.Size(84, 13);
            this.labelGeneralCharacterName.TabIndex = 23;
            this.labelGeneralCharacterName.Text = "Character Name";
            // 
            // labelGeneralSaveNumber
            // 
            this.labelGeneralSaveNumber.AutoSize = true;
            this.labelGeneralSaveNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralSaveNumber.Location = new System.Drawing.Point(195, 310);
            this.labelGeneralSaveNumber.Name = "labelGeneralSaveNumber";
            this.labelGeneralSaveNumber.Size = new System.Drawing.Size(93, 13);
            this.labelGeneralSaveNumber.TabIndex = 21;
            this.labelGeneralSaveNumber.Text = "Save Slot Number";
            // 
            // CharacterName
            // 
            this.CharacterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CharacterName.Location = new System.Drawing.Point(6, 50);
            this.CharacterName.Name = "CharacterName";
            this.CharacterName.Size = new System.Drawing.Size(309, 20);
            this.CharacterName.TabIndex = 0;
            this.CharacterName.TextChanged += new System.EventHandler(this.CharacterName_TextChanged);
            // 
            // groupPanel10
            // 
            this.groupPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupPanel10.Controls.Add(this.MenuLocations);
            this.groupPanel10.Controls.Add(this.LocationTree);
            this.groupPanel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel10.Location = new System.Drawing.Point(330, 3);
            this.groupPanel10.Name = "groupPanel10";
            this.groupPanel10.Padding = new System.Windows.Forms.Padding(3);
            this.groupPanel10.Size = new System.Drawing.Size(342, 585);
            this.groupPanel10.TabIndex = 33;
            this.groupPanel10.TabStop = false;
            this.groupPanel10.Text = "Visited Locations";
            // 
            // MenuLocations
            // 
            this.MenuLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLocations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem6,
            this.deleteToolStripMenuItem8,
            this.actionsToolStripMenuItem2});
            this.MenuLocations.Location = new System.Drawing.Point(6, 23);
            this.MenuLocations.Name = "MenuLocations";
            this.MenuLocations.Size = new System.Drawing.Size(330, 24);
            this.MenuLocations.TabIndex = 40;
            this.MenuLocations.Text = "menuStrip2";
            // 
            // newToolStripMenuItem6
            // 
            this.newToolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LocationsList});
            this.newToolStripMenuItem6.Name = "newToolStripMenuItem6";
            this.newToolStripMenuItem6.Size = new System.Drawing.Size(41, 20);
            this.newToolStripMenuItem6.Text = "New";
            // 
            // LocationsList
            // 
            this.LocationsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocationsList.MaxDropDownItems = 30;
            this.LocationsList.Name = "LocationsList";
            this.LocationsList.Size = new System.Drawing.Size(300, 21);
            this.LocationsList.SelectedIndexChanged += new System.EventHandler(this.LocationsList_SelectedIndexChanged);
            // 
            // deleteToolStripMenuItem8
            // 
            this.deleteToolStripMenuItem8.Name = "deleteToolStripMenuItem8";
            this.deleteToolStripMenuItem8.Size = new System.Drawing.Size(50, 20);
            this.deleteToolStripMenuItem8.Text = "Delete";
            this.deleteToolStripMenuItem8.Click += new System.EventHandler(this.DeleteLocation_Click);
            // 
            // actionsToolStripMenuItem2
            // 
            this.actionsToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllToXMLToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.toolStripSeparator3,
            this.mergeFromLocationFileToolStripMenuItem,
            this.mergeFromAnotherSaveFileToolStripMenuItem,
            this.toolStripSeparator5,
            this.importAllFromXMLToolStripMenuItem1,
            this.replaceFromAnotherSaveToolStripMenuItem,
            this.toolStripSeparator4,
            this.eraseAllLocationsToolStripMenuItem});
            this.actionsToolStripMenuItem2.Name = "actionsToolStripMenuItem2";
            this.actionsToolStripMenuItem2.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem2.Text = "Actions";
            // 
            // exportAllToXMLToolStripMenuItem1
            // 
            this.exportAllToXMLToolStripMenuItem1.Name = "exportAllToXMLToolStripMenuItem1";
            this.exportAllToXMLToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.exportAllToXMLToolStripMenuItem1.Text = "Export all to file";
            this.exportAllToXMLToolStripMenuItem1.Click += new System.EventHandler(this.ExportAllToFileLocations_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.toolStripMenuItem1.Text = "Export selection to file";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ExportSelectedToFileLocations_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
            // 
            // mergeFromLocationFileToolStripMenuItem
            // 
            this.mergeFromLocationFileToolStripMenuItem.Name = "mergeFromLocationFileToolStripMenuItem";
            this.mergeFromLocationFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.mergeFromLocationFileToolStripMenuItem.Text = "Merge from location file";
            this.mergeFromLocationFileToolStripMenuItem.Click += new System.EventHandler(this.MergeAllFromFileLocations_Click);
            // 
            // mergeFromAnotherSaveFileToolStripMenuItem
            // 
            this.mergeFromAnotherSaveFileToolStripMenuItem.Name = "mergeFromAnotherSaveFileToolStripMenuItem";
            this.mergeFromAnotherSaveFileToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.mergeFromAnotherSaveFileToolStripMenuItem.Text = "Merge from another save";
            this.mergeFromAnotherSaveFileToolStripMenuItem.Click += new System.EventHandler(this.MergeFromSaveLocations_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(192, 6);
            // 
            // importAllFromXMLToolStripMenuItem1
            // 
            this.importAllFromXMLToolStripMenuItem1.Name = "importAllFromXMLToolStripMenuItem1";
            this.importAllFromXMLToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.importAllFromXMLToolStripMenuItem1.Text = "Clone from location file";
            this.importAllFromXMLToolStripMenuItem1.Click += new System.EventHandler(this.ImportAllFromFileLocations_Click);
            // 
            // replaceFromAnotherSaveToolStripMenuItem
            // 
            this.replaceFromAnotherSaveToolStripMenuItem.Name = "replaceFromAnotherSaveToolStripMenuItem";
            this.replaceFromAnotherSaveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.replaceFromAnotherSaveToolStripMenuItem.Text = "Clone from another save";
            this.replaceFromAnotherSaveToolStripMenuItem.Click += new System.EventHandler(this.ImportAllFromSaveLocations_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(192, 6);
            // 
            // eraseAllLocationsToolStripMenuItem
            // 
            this.eraseAllLocationsToolStripMenuItem.Name = "eraseAllLocationsToolStripMenuItem";
            this.eraseAllLocationsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.eraseAllLocationsToolStripMenuItem.Text = "Erase all locations";
            this.eraseAllLocationsToolStripMenuItem.Click += new System.EventHandler(this.DeleteAllLocations_Click);
            // 
            // LocationTree
            // 
            this.LocationTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.LocationTree.AllowDrop = true;
            this.LocationTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.LocationTree.DefaultToolTipProvider = null;
            this.LocationTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.LocationTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.LocationTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.LocationTree.Location = new System.Drawing.Point(6, 47);
            this.LocationTree.Name = "LocationTree";
            this.LocationTree.SelectedNode = null;
            this.LocationTree.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.MultiSameParent;
            this.LocationTree.Size = new System.Drawing.Size(330, 532);
            this.LocationTree.TabIndex = 20;
            this.LocationTree.Text = "advTree1";
            this.LocationTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LocationTree_KeyDown);
            // 
            // ucGeneral
            // 
            this.Controls.Add(this.GeneralTab);
            this.Name = "ucGeneral";
            this.Size = new System.Drawing.Size(956, 591);
            this.GeneralTab.ResumeLayout(false);
            this.groupPanel8.ResumeLayout(false);
            this.groupPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BankSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Level)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Experience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EquipSlots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkillPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BackpackSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveNumber)).EndInit();
            this.groupPanel10.ResumeLayout(false);
            this.groupPanel10.PerformLayout();
            this.MenuLocations.ResumeLayout(false);
            this.MenuLocations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.CCPanel GeneralTab;
        private CustomControls.WTGroupBox groupPanel8;
        private CustomControls.WTLabel labelGeneralBankSpace;
        private CustomControls.WTNumericUpDown BankSpace;
        private CustomControls.WTLabel labelGeneralCurrentLocation;
        private CustomControls.WTComboBox CurrentLocation;
        private CustomControls.WTNumericUpDown Level;
        private CustomControls.WTLabel labelGeneralEquipSlots;
        private CustomControls.WTNumericUpDown Experience;
        private CustomControls.WTNumericUpDown EquipSlots;
        private CustomControls.WTNumericUpDown Cash;
        private CustomControls.WTLabel labelGeneralBackpackSpace;
        private CustomControls.WTNumericUpDown SkillPoints;
        private CustomControls.WTNumericUpDown BackpackSpace;
        private CustomControls.WTNumericUpDown SaveNumber;
        private CustomControls.WTLabel labelGeneralPT2Unlocked;
        private CustomControls.WTLabel labelGeneralLevel;
        private CustomControls.WTComboBox PT2Unlocked;
        private CustomControls.WTLabel labelGeneralExperience;
        public CustomControls.WTComboBox Class;
        private CustomControls.WTLabel labelGeneralCash;
        private CustomControls.WTLabel labelGeneralClass;
        private CustomControls.WTLabel labelGeneralSkillPoints;
        private CustomControls.WTLabel labelGeneralCharacterName;
        private CustomControls.WTLabel labelGeneralSaveNumber;
        private CustomControls.WTTextBox CharacterName;
        private CustomControls.WTGroupBox groupPanel10;
        private CustomControls.WTMenuStrip MenuLocations;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem6;
        private System.Windows.Forms.ToolStripComboBox LocationsList;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exportAllToXMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mergeFromLocationFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeFromAnotherSaveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem importAllFromXMLToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem replaceFromAnotherSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem eraseAllLocationsToolStripMenuItem;
        private CustomControls.WTTreeView LocationTree;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
