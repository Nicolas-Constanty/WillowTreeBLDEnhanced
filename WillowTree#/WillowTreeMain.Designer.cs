/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@usWTers.sf.net>
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

namespace WillowTree
{
    partial class WillowTreeMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WillowTreeMain));
            this.MainMenu = new WillowTree.CustomControls.WTMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.PCFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.PS3Format = new System.Windows.Forms.ToolStripMenuItem();
            this.Xbox360Format = new System.Windows.Forms.ToolStripMenuItem();
            this.xbox360JPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelQualityEditingModesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useSlidersdecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useNumericBoxdecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useNumericBoxhexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemPartSelectorTracking = new System.Windows.Forms.ToolStripMenuItem();
            this.colorizeListsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showManufacturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRarityValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEffectiveLevelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.sortModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseNavigationLayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel3 = new WillowTree.CustomControls.WTFlowLayoutPanel();
            this.tabControl1 = new WillowTree.CustomControls.WTTabControl();
            this.appThemes1 = new WillowTree.AppThemes();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem3});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(956, 24);
            this.MainMenu.TabIndex = 21;
            this.MainMenu.Text = "menuStrip3";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Save,
            this.SaveAs,
            this.SelectFormat,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.Open.Size = new System.Drawing.Size(174, 22);
            this.Open.Text = "Open";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Save.Size = new System.Drawing.Size(174, 22);
            this.Save.Text = "Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // SaveAs
            // 
            this.SaveAs.Name = "SaveAs";
            this.SaveAs.Size = new System.Drawing.Size(174, 22);
            this.SaveAs.Text = "Save As...";
            this.SaveAs.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // SelectFormat
            // 
            this.SelectFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PCFormat,
            this.PS3Format,
            this.Xbox360Format,
            this.xbox360JPToolStripMenuItem});
            this.SelectFormat.Name = "SelectFormat";
            this.SelectFormat.Size = new System.Drawing.Size(174, 22);
            this.SelectFormat.Text = "Change Save Format";
            // 
            // PCFormat
            // 
            this.PCFormat.Name = "PCFormat";
            this.PCFormat.Size = new System.Drawing.Size(152, 22);
            this.PCFormat.Text = "PC";
            this.PCFormat.Click += new System.EventHandler(this.PCFormat_Click);
            // 
            // PS3Format
            // 
            this.PS3Format.Name = "PS3Format";
            this.PS3Format.Size = new System.Drawing.Size(152, 22);
            this.PS3Format.Text = "PS3";
            this.PS3Format.Click += new System.EventHandler(this.PS3Format_Click);
            // 
            // Xbox360Format
            // 
            this.Xbox360Format.Name = "Xbox360Format";
            this.Xbox360Format.Size = new System.Drawing.Size(152, 22);
            this.Xbox360Format.Text = "Xbox 360";
            this.Xbox360Format.Click += new System.EventHandler(this.XBoxFormat_Click);
            // 
            // xbox360JPToolStripMenuItem
            // 
            this.xbox360JPToolStripMenuItem.Name = "xbox360JPToolStripMenuItem";
            this.xbox360JPToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xbox360JPToolStripMenuItem.Text = "Xbox 360 (JP)";
            this.xbox360JPToolStripMenuItem.Click += new System.EventHandler(this.XBoxJPFormat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(174, 22);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ExitWT_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.CheckOnClick = true;
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelQualityEditingModesToolStripMenuItem,
            this.toolStripSeparator6,
            this.MenuItemPartSelectorTracking,
            this.colorizeListsToolStripMenuItem,
            this.showManufacturerToolStripMenuItem,
            this.showRarityValueToolStripMenuItem,
            this.showEffectiveLevelsToolStripMenuItem,
            this.toolStripSeparator2,
            this.SaveOptions});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.optionsToolStripMenuItem_DropDownOpening);
            // 
            // levelQualityEditingModesToolStripMenuItem
            // 
            this.levelQualityEditingModesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useSlidersdecimalToolStripMenuItem,
            this.useNumericBoxdecimalToolStripMenuItem,
            this.useNumericBoxhexadecimalToolStripMenuItem});
            this.levelQualityEditingModesToolStripMenuItem.Name = "levelQualityEditingModesToolStripMenuItem";
            this.levelQualityEditingModesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.levelQualityEditingModesToolStripMenuItem.Text = "Level/Quality Editing Mode";
            // 
            // useSlidersdecimalToolStripMenuItem
            // 
            this.useSlidersdecimalToolStripMenuItem.Name = "useSlidersdecimalToolStripMenuItem";
            this.useSlidersdecimalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.useSlidersdecimalToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.useSlidersdecimalToolStripMenuItem.Text = "Use Sliders (decimal)";
            this.useSlidersdecimalToolStripMenuItem.Click += new System.EventHandler(this.StandardInput_Click);
            // 
            // useNumericBoxdecimalToolStripMenuItem
            // 
            this.useNumericBoxdecimalToolStripMenuItem.Name = "useNumericBoxdecimalToolStripMenuItem";
            this.useNumericBoxdecimalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.useNumericBoxdecimalToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.useNumericBoxdecimalToolStripMenuItem.Text = "Use Numeric Box (decimal)";
            this.useNumericBoxdecimalToolStripMenuItem.Click += new System.EventHandler(this.AdvancedInputDecimal_Click);
            // 
            // useNumericBoxhexadecimalToolStripMenuItem
            // 
            this.useNumericBoxhexadecimalToolStripMenuItem.Name = "useNumericBoxhexadecimalToolStripMenuItem";
            this.useNumericBoxhexadecimalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.useNumericBoxhexadecimalToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.useNumericBoxhexadecimalToolStripMenuItem.Text = "Use Numeric Box (hexadecimal)";
            this.useNumericBoxhexadecimalToolStripMenuItem.Click += new System.EventHandler(this.AdvancedInputHexadecimal_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(212, 6);
            // 
            // MenuItemPartSelectorTracking
            // 
            this.MenuItemPartSelectorTracking.CheckOnClick = true;
            this.MenuItemPartSelectorTracking.Name = "MenuItemPartSelectorTracking";
            this.MenuItemPartSelectorTracking.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.MenuItemPartSelectorTracking.Size = new System.Drawing.Size(215, 22);
            this.MenuItemPartSelectorTracking.Text = "Part Selector Tracking";
            this.MenuItemPartSelectorTracking.Click += new System.EventHandler(this.MenuItemPartSelectorTracking_Click);
            // 
            // colorizeListsToolStripMenuItem
            // 
            this.colorizeListsToolStripMenuItem.CheckOnClick = true;
            this.colorizeListsToolStripMenuItem.Name = "colorizeListsToolStripMenuItem";
            this.colorizeListsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.colorizeListsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.colorizeListsToolStripMenuItem.Text = "Colorize Lists";
            this.colorizeListsToolStripMenuItem.Click += new System.EventHandler(this.colorizeListsToolStripMenuItem_Click);
            // 
            // showManufacturerToolStripMenuItem
            // 
            this.showManufacturerToolStripMenuItem.CheckOnClick = true;
            this.showManufacturerToolStripMenuItem.Name = "showManufacturerToolStripMenuItem";
            this.showManufacturerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.showManufacturerToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showManufacturerToolStripMenuItem.Text = "Show Manufacturer";
            this.showManufacturerToolStripMenuItem.Click += new System.EventHandler(this.showManufacturerToolStripMenuItem_Click);
            // 
            // showRarityValueToolStripMenuItem
            // 
            this.showRarityValueToolStripMenuItem.CheckOnClick = true;
            this.showRarityValueToolStripMenuItem.Name = "showRarityValueToolStripMenuItem";
            this.showRarityValueToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.showRarityValueToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showRarityValueToolStripMenuItem.Text = "Show Rarity Values";
            this.showRarityValueToolStripMenuItem.Click += new System.EventHandler(this.showRarityValueToolStripMenuItem_Click);
            // 
            // showEffectiveLevelsToolStripMenuItem
            // 
            this.showEffectiveLevelsToolStripMenuItem.CheckOnClick = true;
            this.showEffectiveLevelsToolStripMenuItem.Name = "showEffectiveLevelsToolStripMenuItem";
            this.showEffectiveLevelsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.showEffectiveLevelsToolStripMenuItem.Text = "Show Effective Levels";
            this.showEffectiveLevelsToolStripMenuItem.Click += new System.EventHandler(this.showEffectiveLevelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // SaveOptions
            // 
            this.SaveOptions.Name = "SaveOptions";
            this.SaveOptions.Size = new System.Drawing.Size(215, 22);
            this.SaveOptions.Text = "Save Options as Default";
            this.SaveOptions.Click += new System.EventHandler(this.SaveOptions_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortModeToolStripMenuItem,
            this.increaseNavigationLayersToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItem3.Text = "HotKeys";
            this.toolStripMenuItem3.Visible = false;
            // 
            // sortModeToolStripMenuItem
            // 
            this.sortModeToolStripMenuItem.Name = "sortModeToolStripMenuItem";
            this.sortModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.sortModeToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.sortModeToolStripMenuItem.Text = "SortMode";
            this.sortModeToolStripMenuItem.Click += new System.EventHandler(this.NextSort_Click);
            // 
            // increaseNavigationLayersToolStripMenuItem
            // 
            this.increaseNavigationLayersToolStripMenuItem.Name = "increaseNavigationLayersToolStripMenuItem";
            this.increaseNavigationLayersToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.increaseNavigationLayersToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.increaseNavigationLayersToolStripMenuItem.Text = "IncreaseNavigationLayers";
            this.increaseNavigationLayersToolStripMenuItem.Click += new System.EventHandler(this.IncreaseNavigationLayers_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(209, 584);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel3.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = -1;
            this.tabControl1.Size = new System.Drawing.Size(956, 619);
            this.tabControl1.TabIndex = 19;
            this.tabControl1.Text = "tabControl1";
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // WillowTreeMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 643);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "WillowTreeMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = " WillowTree#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WillowTreeMain_FormClosing);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.WTTabControl tabControl1;
        private CustomControls.WTMenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.ToolStripMenuItem SaveAs;
        private System.Windows.Forms.ToolStripMenuItem SelectFormat;
        private System.Windows.Forms.ToolStripMenuItem PCFormat;
        private System.Windows.Forms.ToolStripMenuItem PS3Format;
        private System.Windows.Forms.ToolStripMenuItem Xbox360Format;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelQualityEditingModesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useSlidersdecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useNumericBoxdecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useNumericBoxhexadecimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPartSelectorTracking;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem SaveOptions;
        private System.Windows.Forms.ToolStripMenuItem colorizeListsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRarityValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showManufacturerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem sortModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEffectiveLevelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseNavigationLayersToolStripMenuItem;
        private CustomControls.WTFlowLayoutPanel flowLayoutPanel3;
        private AppThemes appThemes1;
        private System.Windows.Forms.ToolStripMenuItem xbox360JPToolStripMenuItem;
    }
}
