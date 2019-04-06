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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using System.Xml;
//using X360.IO;
//using X360.STFS;
//Yeah, I don't need most of these. So sue me.
//using Microsoft.VisualBasic;
//using System.Net;
//using System.Threading;
using System.IO;
//using System.Reflection;
//using System.Diagnostics;
using System.Drawing;
using WillowTree.CustomControls;
using Aga.Controls;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using WillowTree.Inventory;
using WillowTree.Plugins;

namespace WillowTree
{
    public partial class WillowTreeMain : Form
    {
        PluginComponentManager PluginManager = Services.PluginManager;
        ThemeManager ThemeManager = Services.ThemeManager;

        WillowSaveGame CurrentWSG;

        public WillowSaveGame SaveData
        {
            get { return CurrentWSG; }
        }

        private void SetUITreeStyles(bool UseColor)
        {
            TreeViewTheme theme;

            if (UseColor)
                theme = Services.AppThemes.treeViewTheme1;
            else
                theme = null;

            db.ItemList.OnTreeThemeChanged(theme);
            db.WeaponList.OnTreeThemeChanged(theme);
            db.BankList.OnTreeThemeChanged(theme);
            db.LockerList.OnTreeThemeChanged(theme);
        }

        public WillowTreeMain()
        {
            GlobalSettings.Load();

            if (System.IO.Directory.Exists(db.DataPath) == false)
            {
                MessageBox.Show("Couldn't find the 'Data' folder! Please make sure that WillowTree# and its data folder are in the same directory.");
                Application.Exit();
                return;
            }

            if (System.IO.File.Exists(db.DataPath + "default.xml") == false)
                System.IO.File.WriteAllText(db.DataPath + "default.xml", "<?xml version=\"1.0\" encoding=\"us-ascii\"?>\r\n<INI></INI>\r\n");

            InitializeComponent();

            db.InitializeNameLookup();

            Save.Enabled = false;
            SaveAs.Enabled = false;
            SelectFormat.Enabled = false;

            CreatePluginAsTab("General", new ucGeneral());
            CreatePluginAsTab("Weapons", new ucGears());
            CreatePluginAsTab("Items", new ucGears());
            CreatePluginAsTab("Skills", new ucSkills());
            CreatePluginAsTab("Quest", new ucQuests());
            CreatePluginAsTab("Ammo Pools", new ucAmmo());
            CreatePluginAsTab("Echo Logs", new ucEchoes());
            CreatePluginAsTab("Bank", new ucGears());
            CreatePluginAsTab("Locker", new ucLocker());
#if DEBUG
            CreatePluginAsTab("Debug", new ucDebug());
#endif
            CreatePluginAsTab("About", new ucAbout());

            try
            {
                tabControl1.SelectTab("ucAbout");
            }
            catch { }

            SetUITreeStyles(GlobalSettings.UseColor);
        }

        public void CreatePluginAsTab(string tabTitle, Control plugin)
        {
            if (plugin is IPlugin)
            {
                plugin.Text = tabTitle;
                plugin.BackColor = Color.Transparent;
                tabControl1.Controls.Add(plugin);
                PluginManager.InitializePlugin(plugin as IPlugin);
            }
        }

        private void ConvertListForEditing<T>(InventoryList itmList, ref List<T> objs) where T : WillowSaveGame.Object
        {
            // Populate itmList with items created from the WillowSaveGame data lists
            itmList.ClearSilent();
            foreach (var obj in objs)
                itmList.AddSilent(new InventoryEntry(itmList.invType, obj.Strings, obj.Values, obj.Paddings));
            itmList.OnListReload();

            // Release the WillowSaveGame data lists now that the data is converted
            // to the format that the WillowTree UI uses.
            foreach (var obj in objs)
            {
                obj.Strings = null;
                obj.Values = null;
            }
        }

        private void ConvertListForEditing(InventoryList itmList, ref List<List<string>> itmStrings, ref List<List<int>> itmValues)
        {
            // Populate itmList with items created from the WillowSaveGame data lists
            itmList.ClearSilent();
            for (int i = 0; i < itmStrings.Count; i++)
                itmList.AddSilent(new InventoryEntry(itmList.invType, itmStrings[i], itmValues[i]));
            itmList.OnListReload();

            // Release the WillowSaveGame data lists now that the data is converted
            // to the format that the WillowTree UI uses.
            itmStrings = null;
            itmValues = null;
        }
        private void ConvertListForEditing(InventoryList itmList, ref List<WillowSaveGame.BankEntry> itmBank)
        {
            // Populate itmList with items created from the WillowSaveGame data lists
            itmList.ClearSilent();
            for (int i = 0; i < itmBank.Count; i++)
            {
                List<int> itmBankValues = new List<int>() { itmBank[i].Quantity, itmBank[i].Quality, itmBank[i].Equipped, itmBank[i].Level };

                // Store a reference to the parts list
                List<string> parts = itmBank[i].Parts;

                // Detach the parts list from the bank entry.
                itmBank[i].Parts = null;
 
                // Items have a different part order in the bank and in the backpack
                // Part                Index      Index
                //                   Inventory    Bank
                // Item Grade            0          1
                // Item Type             1          0
                // Body                  2          3
                // Left                  3          4
                // Right                 4          5
                // Material              5          6
                // Manufacturer          6          2
                // Prefix                7          7
                // Title                 8          8

                int itmType = itmBank[i].TypeId - 1;

                // Convert all items into the backpack part order.  Weapons use
                // the same format for both backpack and bank.
      
                if (itmType == InventoryType.Item)
                {
                    string temp = parts[1];
                    parts[1] = parts[0];
                    parts[0] = temp;
                    temp = parts[2];
                    parts[2] = parts[3];
                    parts[3] = parts[4];
                    parts[4] = parts[5];
                    parts[5] = parts[6];
                    parts[6] = temp;
                }
                
                
                // Create an inventory entry with the re-ordered parts list and add it
                itmList.AddSilent(new InventoryEntry((byte)(itmBank[i].TypeId - 1), parts, itmBankValues));
                //Item/Weapon in bank have their type increase by 1, we reduce TypeId by 1 to manipulate them like other list
            }
            itmList.OnListReload();

            // Release the WillowSaveGame bank data now that the data is converted
            // to the format that the WillowTree UI uses.  It gets recreated at save time.
            itmBank = null;
        }
        
        private void RepopulateListForSaving(InventoryList itmList, ref List<List<string>> itmStrings, ref List<List<int>> itmValues)
        {
            itmStrings = new List<List<string>>();
            itmValues = new List<List<int>>();

            // Build the lists of string and value data needed by WillowSaveGame to store the 
            // inventory from the data that is in itmList.
            foreach (InventoryEntry item in itmList.Items.Values)
            {
                itmStrings.Add(item.Parts);
                List<int> values = item.GetValues();
                itmValues.Add(values);
            }

            // itm may represent: item, weapon, bank
            // Note that the string lists that contain the parts are shared
            // between itmList and itmStrings after this method runs, so 
            // cross-contamination can occur if they are modified.  They should 
            // only be held in this state long enough to save, which does not modify
            // values, then itmStrings/itmValues should be released by setting them
            // to null since they will not be used again until the next save when they
            // will have to be recreated.
        }
        private void RepopulateListForSaving<T>(InventoryList itmList, ref List<T> objs) where T : WillowSaveGame.Object, new()
        {
            // Build the lists of string and value data needed by WillowSaveGame to store the 
            // inventory from the data that is in itmList.
            foreach (InventoryEntry item in itmList.Items.Values)
            {
                var obj = new T();
                foreach (var part in item.Parts)
                {
                    obj.Strings.Add(part);
                }
                foreach (var value in item.GetValues())
                {
                    obj.Values.Add(value);
                }
                if (item.padding != null)
                {
                    obj.Paddings = new byte[item.padding.Length];
                    Array.Copy(item.padding, obj.Paddings, item.padding.Length);
                    objs.Add(obj);
                }
            }

            // itm may represent: item, weapon, bank
            // Note that the string lists that contain the parts are shared
            // between itmList and itmStrings after this method runs, so 
            // cross-contamination can occur if they are modified.  They should 
            // only be held in this state long enough to save, which does not modify
            // values, then itmStrings/itmValues should be released by setting them
            // to null since they will not be used again until the next save when they
            // will have to be recreated.
        }
        private void RepopulateListForSaving(InventoryList itmList, ref List<WillowSaveGame.BankEntry> itmBank)
        {
            itmBank = new List<WillowSaveGame.BankEntry>();
            WillowSaveGame.BankEntry itm;

            // Build the lists of string and value data needed by WillowSaveGame to store the 
            // inventory from the data that is in itmList.
            foreach (InventoryEntry item in itmList.Items.Values)
            {
                itm = new WillowSaveGame.BankEntry();

                if (item.Type == InventoryType.Item)
                {
                    // Items must have their parts reordered because they are different in the bank.
                    List<string> oldParts = item.Parts;
                    itm.Parts = new List<string>() { oldParts[1], oldParts[0], oldParts[6], oldParts[2], oldParts[3],
                                                                  oldParts[4], oldParts[5], oldParts[7], oldParts[8] };     
                }
                else
                    itm.Parts = new List<string>(item.Parts);

                //Item/Weapon in bank have their type increase by 1, we  increase TypeId by 1 to restore them to their natural value
                itm.TypeId = (byte)(item.Type + 1);

                List<int> values = item.GetValues();

                //if (Convert.ToBoolean(values[4])) //TODO RSM UsesBigLevel
                itm.Quantity = values[0];//Quantity;
                itm.Quality = (short)values[1];//QualityIndex;
                itm.Equipped = (byte)values[2];//Equipped;
                itm.Level = (short)values[3];//LevelIndex;

                itmBank.Add(itm);
            }

            // The string lists stored in the bank entries are not not shared 
            // after this method runs like they are for the backpack inventory.
            // Still they should be released after saving since this is duplicate
            // data that will be rebuilt on the next save attempt.
        }

        private void DoWindowTitle()
        {
            ucGeneral eGeneral = PluginManager.GetPlugin(typeof(ucGeneral)) as ucGeneral;
            if (eGeneral != null)
                eGeneral.DoWindowTitle();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            string fileName = (CurrentWSG != null) ? CurrentWSG.OpenedWSG : "";

            Util.WTOpenFileDialog openDlg = new Util.WTOpenFileDialog("sav", fileName);
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                fileName = openDlg.FileName();

                PluginManager.OnGameLoading(new PluginEventArgs(this, fileName));
                Application.DoEvents();
                CurrentWSG = new WillowSaveGame();
                CurrentWSG.AutoRepair = true;
                CurrentWSG.LoadWSG(fileName);

                if (CurrentWSG.RequiredRepair == true)
                {
                    DialogResult result = MessageBox.Show(
                        "Your savegame contains corrupted data so it cannot be loaded.  " +
                        "It is possible to discard the invalid data to repair your savegame " +
                        "so that it can be opened.  Repairing WILL CAUSE SOME DATA LOSS but " +
                        "should bring your savegame back to a working state.\r\n\r\nDo you want to " +
                        "repair the savegame?",
                        "Recoverable Corruption Detected",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (result == DialogResult.No)
                        throw new FileFormatException("Savegame file is corrupt.");
                }

                ConvertListForEditing(db.WeaponList, ref CurrentWSG.Weapons);
                ConvertListForEditing(db.ItemList, ref CurrentWSG.Items);
                ConvertListForEditing(db.BankList, ref CurrentWSG.DLC.BankInventory);

                PluginManager.OnGameLoaded(new PluginEventArgs(this, fileName));

                Save.Enabled = true;
                SaveAs.Enabled = true;
                SelectFormat.Enabled = true;
            }
            else
                fileName = "";
        }
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(CurrentWSG.OpenedWSG, CurrentWSG.OpenedWSG + ".bak0", true);
                if (File.Exists(CurrentWSG.OpenedWSG + ".bak10") == true)
                    File.Delete(CurrentWSG.OpenedWSG + ".bak10");
                for (int i = 9; i >= 0; i--)
                {
                    if (File.Exists(CurrentWSG.OpenedWSG + ".bak" + i) == true)
                        File.Move(CurrentWSG.OpenedWSG + ".bak" + i, CurrentWSG.OpenedWSG + ".bak" + (i + 1));
                }
            }
            catch { }

//            try
            {
                SaveToFile(CurrentWSG.OpenedWSG);
                MessageBox.Show("Saved WSG to: " + CurrentWSG.OpenedWSG);
            }
//            catch { MessageBox.Show("Couldn't save WSG"); }
        }
        private void SaveAs_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog tempSave = new Util.WTSaveFileDialog("sav", CurrentWSG.OpenedWSG);

            if (tempSave.ShowDialog() == DialogResult.OK)
            {
                    SaveToFile(tempSave.FileName());
                    MessageBox.Show("Saved WSG to: " + CurrentWSG.OpenedWSG);
                    Save.Enabled = true;
            }
        }

        // Checks to make sure a savegame contains no raw data.  If it does it asks if it is ok
        // to remove it and performs removal of the raw data.
        // Return value is true if the result is a savegame with no raw data or false if the
        // savegame still contains raw data.
        private bool UIAction_RemoveRawData()
        {
            DialogResult result = MessageBox.Show("This savegame contains some unexpected or possibly corrupt DLC data that WillowTree# does not know how to parse, so it cannot be rewritten in a different machine byte order.  The extra data can be discarded to allow you to convert the savegame, but this will cause DLC data loss if the unknown DLC data is actually used by Borderlands.  It is typical for Steam PC savegames to have a data section like this and it must be removed to convert to Xbox 360 or PS3 format.  The data probably serves no gameplay purpose because it doesn't exist in the PC DVD version.\r\n\r\nDo you want to discard the raw data to allow the conversion?", "Unexpected Raw Data", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return false;

            CurrentWSG.DiscardRawData();
            return true;
        }

        private void PCFormat_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "PC")
                return;

            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.LittleEndian))
            {
                if (UIAction_RemoveRawData() == false)
                    return;
            }

            CurrentWSG.Platform = "PC";
            CurrentWSG.EndianWSG = ByteOrder.LittleEndian;
            DoWindowTitle();
            CurrentWSG.OpenedWSG = "";
            Save.Enabled = false;
        }
        private void PS3Format_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "PS3")
                return;

            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.BigEndian))
            {
                if (UIAction_RemoveRawData() == false)
                    return;
            }

            CurrentWSG.Platform = "PS3";
            CurrentWSG.EndianWSG = ByteOrder.BigEndian;
            DoWindowTitle();
            MessageBox.Show("This save data will be stored in the PS3 format. Please note that you will require \r\nproper SFO, PNG, and PFD files to be transfered back to the \r\nPS3. These can be acquired from another Borderlands save \r\nfor the same profile.");
            CurrentWSG.OpenedWSG = "";
            Save.Enabled = false;
        }
        private void XBoxFormat_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "X360")
                return;

            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.BigEndian))
            {
                if (UIAction_RemoveRawData() == false)
                    return;
            }

            if (CurrentWSG.DeviceID == null)
            {
                XBoxIDDialog dlgXBoxID = new XBoxIDDialog();
                if (dlgXBoxID.ShowDialog() == DialogResult.OK)
                {
                    CurrentWSG.ProfileID = dlgXBoxID.ID.ProfileID;
                    int DeviceIDLength = dlgXBoxID.ID.DeviceID.Count();
                    CurrentWSG.DeviceID = new byte[DeviceIDLength];
                    Array.Copy(dlgXBoxID.ID.DeviceID, CurrentWSG.DeviceID, DeviceIDLength);
                }
                else
                    return;
            }
            CurrentWSG.Platform = "X360";
            CurrentWSG.EndianWSG = ByteOrder.BigEndian;
            DoWindowTitle();
            CurrentWSG.OpenedWSG = "";
            Save.Enabled = false;
        }
        private void XBoxJPFormat_Click(object sender, EventArgs e)
        {
            if (CurrentWSG.Platform == "X360JP")
                return;

            if ((CurrentWSG.ContainsRawData == true) && (CurrentWSG.EndianWSG != ByteOrder.BigEndian))
            {
                if (UIAction_RemoveRawData() == false)
                    return;
            }

            if (CurrentWSG.DeviceID == null)
            {
                XBoxIDDialog dlgXBoxID = new XBoxIDDialog();
                if (dlgXBoxID.ShowDialog() == DialogResult.OK)
                {
                    CurrentWSG.ProfileID = dlgXBoxID.ID.ProfileID;
                    int DeviceIDLength = dlgXBoxID.ID.DeviceID.Count();
                    CurrentWSG.DeviceID = new byte[DeviceIDLength];
                    Array.Copy(dlgXBoxID.ID.DeviceID, CurrentWSG.DeviceID, DeviceIDLength);
                }
                else
                    return;
            }
            CurrentWSG.Platform = "X360JP";
            CurrentWSG.EndianWSG = ByteOrder.BigEndian;
            DoWindowTitle();
            CurrentWSG.OpenedWSG = "";
            Save.Enabled = false;
        }
        
        private void ExitWT_Click(object sender, EventArgs e)
        {
            GlobalSettings.Save();
            Application.Exit();
        }
        private void WillowTreeMain_FormClosing(object sender, EventArgs e)
        {
            GlobalSettings.Save();
        }

        private void StandardInput_Click(object sender, EventArgs e)
        {
            GlobalSettings.InputMode = InputMode.Standard;
        }
        private void AdvancedInputDecimal_Click(object sender, EventArgs e)
        {
            GlobalSettings.UseHexInAdvancedMode = false;
            GlobalSettings.InputMode = InputMode.Advanced;
        }
        private void AdvancedInputHexadecimal_Click(object sender, EventArgs e)
        {
            GlobalSettings.UseHexInAdvancedMode = true;
            GlobalSettings.InputMode = InputMode.Advanced;
        }

        private void SaveToFile(string filename)
        {
            PluginManager.OnGameSaving(new PluginEventArgs(this, filename));
			Application.DoEvents();

            // Convert the weapons and items data from WeaponList/ItemList into
            // the format used by WillowSaveGame.
            CurrentWSG.Weapons = new List<WillowSaveGame.Weapon>();
            CurrentWSG.Items = new List<WillowSaveGame.Item>();
            RepopulateListForSaving(db.WeaponList, ref CurrentWSG.Weapons);
            RepopulateListForSaving(db.ItemList, ref CurrentWSG.Items);
            RepopulateListForSaving(db.BankList, ref CurrentWSG.DLC.BankInventory);
            CurrentWSG.SaveWSG(filename);
            CurrentWSG.OpenedWSG = filename;

            // Release the WillowSaveGame inventory data now that saving is complete.  The
            // same data is still contained in db.WeaponList, db.ItemList, and db.BankList
            // in the format used by the WillowTree UI.           
            CurrentWSG.Weapons = null;
            CurrentWSG.Items = null;
            CurrentWSG.DLC.BankInventory = null;

            PluginManager.OnGameSaved(new PluginEventArgs(this, CurrentWSG.OpenedWSG));
        }

        private void NextSort_Click(object sender, EventArgs e)
        {
            IPlugin page = tabControl1.TabPages[tabControl1.SelectedIndex] as IPlugin;
            if (page != null)
                PluginManager.OnPluginCommand(page, new PluginCommandEventArgs(this, PluginCommand.ChangeSortMode));
        }

        private void IncreaseNavigationLayers_Click(object sender, EventArgs e)
        {
            IPlugin page = tabControl1.TabPages[tabControl1.SelectedIndex] as IPlugin;
            if (page != null)
                PluginManager.OnPluginCommand(page, new PluginCommandEventArgs(this, PluginCommand.IncreaseNavigationDepth));
        }

        private void UpdateNames()
        {
            db.WeaponList.OnNameFormatChanged();
            db.ItemList.OnNameFormatChanged();
            db.BankList.OnNameFormatChanged();
            db.LockerList.OnNameFormatChanged();
        }

        // Options Menu
        private void trackCurrentPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.PartSelectorTracking = true;
        }
        private void trackingDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.PartSelectorTracking = false;
        }
        private void showRarityValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.ShowRarity = !GlobalSettings.ShowRarity;
            UpdateNames();
        }
        private void colorizeListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.UseColor = !GlobalSettings.UseColor;
            SetUITreeStyles(GlobalSettings.UseColor);
        }
        private void optionsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            colorizeListsToolStripMenuItem.Checked = GlobalSettings.UseColor;
            showRarityValueToolStripMenuItem.Checked = GlobalSettings.ShowRarity;
            showEffectiveLevelsToolStripMenuItem.Checked = GlobalSettings.ShowLevel;
            showManufacturerToolStripMenuItem.Checked = GlobalSettings.ShowManufacturer;
            MenuItemPartSelectorTracking.Checked = GlobalSettings.PartSelectorTracking;
        }
        private void showManufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.ShowManufacturer = !GlobalSettings.ShowManufacturer;
            UpdateNames();
        }
        private void showEffectiveLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.ShowLevel = !GlobalSettings.ShowLevel;
            UpdateNames();
        }
        private void SaveOptions_Click(object sender, EventArgs e)
        {
            GlobalSettings.Save();
        }

        public Font treeViewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        //private void DumpTreeDebugInfo(TreeViewAdv tree)
        //{
        //    DebugText1.AppendText("TreeViewAdv: " + tree.Name + "\r\n");
        //    if (WeaponTree.SelectedNode == null)
        //        DebugText1.AppendText("    No node selected\r\n");
        //    else
        //    {
        //        ColoredTextNode node = WeaponTree.SelectedNode.Tag as ColoredTextNode;
        //        string tag = node.Tag as string;
        //        DebugText1.AppendText("WeaponTree.SelectedNode.Tag = \"" + node.Tag as string + "\"\r\n");
        //        DebugText1.AppendText("WeaponTree.SelectedNode.Text = \"" + node.Text + "\"\r\n");
        //        DebugText1.AppendText("WeaponTree.SelectedNode.ForeColor = \"" + node.ForeColor + "\"\r\n");
        //    }
        //}

        private Control SelectedTabObject = null;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((SelectedTabObject != null) && (SelectedTabObject is IPlugin))
                PluginManager.OnPluginUnselected(SelectedTabObject as IPlugin, new PluginEventArgs(this, null));
            
            int selected = tabControl1.SelectedIndex;
            if (selected >= 0)
            {
                SelectedTabObject = tabControl1.Controls[selected];
                if ((SelectedTabObject != null) && (SelectedTabObject is IPlugin)) 
                    PluginManager.OnPluginSelected(SelectedTabObject as IPlugin, new PluginEventArgs(this, null));
            }
            else
                SelectedTabObject = null;

            //if (tabControl1.SelectedIndex == tabControl1.GetTabIndex(DebugTab))
            //{
            //    DumpTreeDebugInfo(Weapon.Tree);
            //}
        }

        private void MenuItemPartSelectorTracking_Click(object sender, EventArgs e)
        {
            GlobalSettings.PartSelectorTracking = !GlobalSettings.PartSelectorTracking;
        }
    }

    public class ColoredTextNode : Node
    {
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //    throw new ArgumentNullException();

                base.Text = value;
            }
        }

        //public string _Name;
        public string Key
        {
            get { return Tag as string; }
            set { Tag = value; }
        }

        private Color _ForeColor;
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        private Font _Font;
        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        public ColoredTextNode()
            : base()
        {
            this._Font = null;
            ForeColor = Color.Black;
        }

        /// <summary>
        /// Initializes a new MyNode class with a given Text property.
        /// </summary>
        /// <param name="text">String to set the text property with.</param>
        public ColoredTextNode(string text)
            : base(text)
        {
            this._Font = null;
            ForeColor = Color.Black;
        }
        public ColoredTextNode(string key, string text)
            : base(text)
        {
            this._Font = null;
            Tag = key;
            ForeColor = Color.Black;
        }

        public void Remove()
        {
            Parent = null;
        }
    }

    public static class AgaControlsExtensions
    {
        // Aga.Controls is basically just TreeViewAdv and all the utility stuff
        // needed to make it work.  These are extensions added to try to help do
        // normal things like insert and remove nodes because the way the
        // TreeViewAdv control works is very strange.
        //
        // TreeViewAdv uses two layers of nodes.  One set of nodes (class 
        // TreeNodeAdv) is used to return user selection information and the other 
        // set of nodes (class Node) is used to create the tree and store data in.
        // Both sets confusingly hold navigation information.  The Tag property
        // of each (TreeNodeAdv) points to one (Node) which holds the data, so
        // each time you check the selection of nodes you get a list of 
        // (TreeNodeAdv)s but you can only add (Node)s to the tree.

        // This is not actually an extension method, but it is needed to work with 
        // TreeViewAdv and is not form-specific so I placed it here.
        public static void ColoredTextNode_DrawText(object sender, DrawEventArgs e)
        {
            TreeViewTheme theme = (e.Node.Tree as WTTreeView).Theme;
            // When a text node is drawn, this callback is called to determine
            // the colors to use.  It is not necessary to set or modify any colors
            // in e (DrawEventArgs) unless custom colors are desired.  The color of
            // nodes usually depends on whether they are selected or active.  That
            // information is stored in e.Context.DrawSelectionMode.
            
            // e.Node is a TreeNodeAdv navigation node.  e.Node.Tag points to the 
            // actual data node which must be of type Node or a descendant of 
            // Node.  It is ColoredTextNode in this program.
            ColoredTextNode node = e.Node.Tag as ColoredTextNode;

            // The node is drawn with different colors depending on whether it is 
            // selected and whether or not the control is active.  The state is 
            // provided in e.Context.DrawSelection.
            //if (e.Context.DrawSelection == DrawSelectionMode.None)
            //{
            //    e.TextColor = theme.ForeColor;
            //    e.BackgroundBrush = theme.BackBrush;
            //}
            if (e.Context.DrawSelection == DrawSelectionMode.None)
            {
                e.TextColor = (e.Node.Tag as ColoredTextNode).ForeColor;
                e.BackgroundBrush = theme.BackBrush;
            }
            else if (e.Context.DrawSelection == DrawSelectionMode.Active)
            {
                e.TextColor = theme.HighlightForeColor;
                e.BackgroundBrush = theme.HighlightBackBrush;
            }
            else if (e.Context.DrawSelection == DrawSelectionMode.Inactive)
            {
                e.TextColor = theme.InactiveForeColor;
                e.BackgroundBrush = theme.InactiveBackBrush;
            }
            else if (e.Context.DrawSelection == DrawSelectionMode.FullRowSelect)
            {
                e.TextColor = theme.HighlightForeColor;
                e.BackgroundBrush = theme.BackBrush;
            }

            if (!e.Context.Enabled)
                e.TextColor = theme.DisabledForeColor;

            // Apply a custom font if the node has one
            if (node.Font != null)
                e.Font = node.Font;
        }

        public static TreeNodeAdv FindFirstNode(this WTTreeView tree, string searchText, bool searchChildren)
        {
            TreeNodeAdv root = tree.Root;
            return FindFirstNode(root, searchText, searchChildren);
        }
        public static TreeNodeAdv FindFirstNodeByTag(this WTTreeView tree, object searchTag, bool searchChildren)
        {
            TreeNodeAdv root = tree.Root;
            if (root.Children.Count == 0)
                return null;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Tag.Equals(searchTag))
                    return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static IEnumerable<TreeNodeAdv> FindNodes(this WTTreeView tree, string searchText, bool searchChildren)
        {
            TreeNodeAdv root = tree.Root;
            if (root.Children.Count == 0)
                yield break;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Text == searchText)
                    yield return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }
        public static IEnumerable<TreeNodeAdv> FindNodesByTag(this WTTreeView tree, object searchTag, bool searchChildren)
        {
            TreeNodeAdv root = tree.Root;
            if (root.Children.Count == 0)
                yield break;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Tag.Equals(searchTag)) 
                    yield return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }
        public static void Clear(this WTTreeView tree)
        {
            (tree.Model as TreeModel).Nodes.Clear();
        }

        public static TreeNodeAdv FindFirstNode(this TreeNodeAdv root, string searchText, bool searchChildren)
        {
            if (root.Children.Count == 0)
                return null;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Text == searchText)
                    return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static TreeNodeAdv FindFirstByTag(this TreeNodeAdv root, object searchTag, bool searchChildren)
        {
            if (root.Children.Count == 0)
                return null;

            TreeNodeAdv node = root.Children[0];
            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Tag.Equals(searchTag))
                    return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static TreeNodeAdv FindNodeAdvByTag(this TreeNodeAdv root, object searchTag, bool searchChildren)
        {
            if (root.Children.Count == 0)
                return null;

            TreeNodeAdv node = root.Children[0];
            while (node != null)
            {
                // Check this node
                if (node.Tag.Equals(searchTag))
                    return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static IEnumerable<TreeNodeAdv> FindNodes(this TreeNodeAdv root, string searchText, bool searchChildren)
        {
            if (root.Children.Count == 0)
                yield break;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
                if (data.Text == searchText)
                    yield return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }
        public static IEnumerable<TreeNodeAdv> FindNodesByTag(this TreeNodeAdv root, object searchTag, bool searchChildren)
        {
            if (root.Children.Count == 0)
                yield break;

            TreeNodeAdv node = root.Children[0];

            while (node != null)
            {
                Node data = node.Tag as Node;

                // Check this node
           		if (data.Tag.Equals(searchTag))
                    yield return node;

                // Select the next node
                if ((searchChildren == true) && (node.Children.Count > 0))
                    node = node.Children[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }

        public static Node FindFirstNode(this Node root, string searchText, bool searchNodes)
        {
            if (root.Nodes.Count == 0)
                return null;

            Node node = root.Nodes[0];

            while (node != null)
            {
                // Check this node
                if (node.Text == searchText)
                    return node;

                // Select the next node
                if ((searchNodes == true) && (node.Nodes.Count > 0))
                    node = node.Nodes[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static Node FindFirstByTag(this Node root, string searchTag, bool searchNodes)
        {
            if (root.Nodes.Count == 0)
                return null;

            Node node = root.Nodes[0];

            while (node != null)
            {
                // Check this node
                if (node.Tag as string == searchTag)
                    return node;

                // Select the next node
                if ((searchNodes == true) && (node.Nodes.Count > 0))
                    node = node.Nodes[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            return null;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
            return null;
        }
        public static IEnumerable<Node> FindNodes(this Node root, string searchText, bool searchNodes)
        {
            if (root.Nodes.Count == 0)
                yield break;

            Node node = root.Nodes[0];

            while (node != null)
            {
                // Check this node
                if (node.Text == searchText)
                    yield return node;

                // Select the next node
                if ((searchNodes == true) && (node.Nodes.Count > 0))
                    node = node.Nodes[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }
        public static IEnumerable<Node> FindNodesByTag(this Node root, string searchTag, bool searchNodes)
        {
            if (root.Nodes.Count == 0)
                yield break;

            Node node = root.Nodes[0];

            while (node != null)
            {
                // Check this node
                if (node.Tag as string == searchTag)
                    yield return node;

                // Select the next node
                if ((searchNodes == true) && (node.Nodes.Count > 0))
                    node = node.Nodes[0];        // First child node
                else
                {
                    while (node.NextNode == null)
                    {
                        // No more siblings, so back to the parent
                        node = node.Parent;
                        if (node == root)
                            yield break;
                    }
                    node = node.NextNode;           // Next sibling node
                }
            }
        }

        public static void InsertNode(this TreeNodeAdv parent, int index, Node dataNode)
        {
            (parent.Tag as Node).Nodes.Insert(index, dataNode);
        }
        public static void AddNode(this TreeNodeAdv parent, Node dataNode)
        {
            (parent.Tag as Node).Nodes.Add(dataNode);
        }
        public static void Remove(this TreeNodeAdv node)
        {
            (node.Tag as Node).Parent = null;
        }

        public static InventoryEntry GetEntry(this TreeNodeAdv nodeAdv)
        {
            return (nodeAdv.Tag as Node).Tag as InventoryEntry;
        }

        public static void SetEntry(this TreeNodeAdv nodeAdv, InventoryEntry entry)
        {
            (nodeAdv.Tag as Node).Tag = entry;
        }

        /// <summary>
        /// Assumes that the Tag in the data Node pointed to by this TreeNodeAdv
        /// is a string value and returns it.  Shorthand for (Tag as Node).Tag.
        /// </summary>
        public static string GetKey(this TreeNodeAdv nodeAdv)
        {
            return (nodeAdv.Tag as Node).Tag as string;
        }

        /// <summary>
        /// Sets the Tag in the data Node pointed to by this TreeNodeAdv to a string
        /// value.
        /// </summary>
        public static void SetKey(this TreeNodeAdv nodeAdv, string key)
        {
            (nodeAdv.Tag as Node).Tag = key;
        }

        public static string GetText(this TreeNodeAdv nodeAdv)
        {
            return (nodeAdv.Tag as Node).Text;
        }

        public static void SetText(this TreeNodeAdv nodeAdv, string text)
        {
            (nodeAdv.Tag as Node).Text = text;
        }

        public static ColoredTextNode Data(this TreeNodeAdv node)
        {
            return node.Tag as ColoredTextNode;
        }
        public static string Text(this TreeNodeAdv node)
        {
            return (node.Tag as Node).Text;
        }
    }
}

