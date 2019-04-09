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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Aga.Controls.Tree;
using WillowTree.CustomControls;
using WillowTree.Inventory;

namespace WillowTree.Plugins
{
    public partial class ucGears : UserControl, IPlugin
    {
        #region Members
        PluginComponentManager pluginManager;
        Font HighlightFont;
        WillowSaveGame CurrentWSG;

        public InventoryTreeList GearTL;

        String gearTextName;
        String gearFileName;
        int gearVisibleLine;
        #endregion
        
        public ucGears()
        {
            InitializeComponent();
        }
        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            events.PluginCommand = OnPluginCommand;
            pm.RegisterPlugin(this, events);

            pluginManager = pm;

            switch (this.Text)
            {
                case "Weapons":
                    GearTL = new InventoryTreeList(GearTree, db.WeaponList);
                    this.gbGear.Text = "Weapon Backpack";
                    this.copyToBackpackToolStripMenuItem.Visible = false;
                    break;
                case "Items":
                    GearTL = new InventoryTreeList(GearTree, db.ItemList);
                    this.gbGear.Text = "Item Backpack";
                    this.copyToBackpackToolStripMenuItem.Visible = false;
                    break;
                case "Bank":
                    GearTL = new InventoryTreeList(GearTree, db.BankList);
                    this.gbGear.Text = "Bank";
                    this.copyToBankToolStripMenuItem.Visible = false;
                    break;
            }

            Init();

            // The index translators control the caption that goes over the top of each
            // level or quality SlideSelector.  Attach each translator then signal the
            // value changed event to cause the translator to update the caption.
            this.LevelIndexGear.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.QualityGear.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);
            this.LevelIndexGear.OnValueChanged(EventArgs.Empty);
            this.QualityGear.OnValueChanged(EventArgs.Empty);
            
            HighlightFont = new Font(GearTree.Font, FontStyle.Italic | FontStyle.Bold);

            this.Enabled = false;
        }
        private void Init()
        {
            string tabName = this.Text;

            //Section for Bank to change interface
            if (GearTree.SelectedNode != null)
                if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Weapon)
                    tabName = "Weapons";
                else if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Item)
                    tabName = "Items";

            //Config interface for Weapon, Item, Bank
            this.EquippedSlotGear.Items.Clear();
            switch (tabName)
            {
                case "Weapons":
                    DoGearTabs("weapon_tabs.txt");//TODO RSM Push both Weapon/Item in array (db.WpnFile) so we don't have to reload
                    gearTextName = "Weapon";
                    gearVisibleLine = 15;

                    //Change control label and text
                    this.exportGearToolStripMenuItem.Text = "Export Weapon";
                    this.GearPartsGroup.Text = "Weapon Parts";
                    this.labelGearEquipped.Text = "Equipped Slot";
                    this.labelGearQuantity.Text = "Remaining Ammo";

                    //Change control others property
                    //this.QualityGear.Value = 5;

                    this.EquippedSlotGear.Items.AddRange(new object[] {
                        "Unequipped",
                        "Slot 1 (Up)",
                        "Slot 2 (Down)",
                        "Slot 3 (Left)",
                        "Slot 4 (Right)"});

                    //this.QuantityGear.Maximum = new decimal(new int[] {
                    //    500,
                    //    0,
                    //    0,
                    //    0});
                    break;

                case "Items":
                    DoGearTabs("item_tabs.txt");
                    gearTextName = "Item";
                    gearVisibleLine = 17;

                    //Change control label and text
                    this.exportGearToolStripMenuItem.Text = "Export Item";
                    this.GearPartsGroup.Text = "Item Parts";
                    this.labelGearEquipped.Text = "Equipped";
                    this.labelGearQuantity.Text = "Quantity";

                    //Change control others property
                    //this.QualityGear.Value = 0;

                    this.EquippedSlotGear.Items.AddRange(new object[] {
                        "No",
                        "Yes"});

                    //this.QuantityGear.Maximum = new decimal(new int[] {
                    //    16777215,
                    //    0,
                    //    0,
                    //    0});
                    break;
                default:
                    break;
            }
        }

        public void ReleasePlugin()
        {
            this.LevelIndexGear.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.QualityGear.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);

            pluginManager = null;
            HighlightFont = null;
            CurrentWSG = null;

            GearTL = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            this.CurrentWSG = e.WTM.SaveData;
            GearTL.UpdateTree();

            //Config interface with WTM for Weapon, Item
            switch (this.Text)
            {
                case "Weapons":
                case "Weapons Bank":
                    gearFileName = CurrentWSG.CharacterName + "'s " + gearTextName + "s";
                    break;
                case "Items":
                case "Items Bank":
                    gearFileName = CurrentWSG.CharacterName + "'s " + gearTextName + "s";
                    break;
                default:
                    break;
            }

            this.Enabled = true;
        }

        public void OnPluginCommand(object sender, PluginCommandEventArgs e)
        {
            if (e.Command == PluginCommand.IncreaseNavigationDepth)
                GearTL.IncreaseNavigationDepth();
            else if (e.Command == PluginCommand.ChangeSortMode)
                GearTL.NextSort();
        }

        private void DoGearTabs(String textFile)
        {
            PartSelectorGear.Clear();

            string TabsLine = System.IO.File.ReadAllText(db.DataPath + textFile);
            string[] TabsList = TabsLine.Split(new char[] { (char)';' });
            for (int Progress = 0; Progress < TabsList.Length; Progress++)
                Util.DoPartsCategory(TabsList[Progress], PartSelectorGear);
        }
        
        private void NewGear_Click(object sender, EventArgs e)
        {
            GearTL.AddNew(GearTL.Unsorted.invType);//Bank AddNewWeapon/Item
            GearTL.AdjustSelectionAfterAdd();
            GearTree.EnsureVisible(GearTree.SelectedNode);
        }
        private void DeletePartGear_Click(object sender, EventArgs e)
        {
            if (PartsGear.SelectedIndex != -1)
                PartsGear.Items[PartsGear.SelectedIndex] = "None";
        }
        private void DeleteGear_Click(object sender, EventArgs e)
        {
            GearTL.DeleteSelected();
        }
        private void DuplicateGear_Click(object sender, EventArgs e)
        {
            GearTL.DuplicateSelected();
        }
        
        private void MoveGear_Click(object sender, EventArgs e)
        {
            GearTL.CopySelected(db.LockerList, true);
        }
        private void CopyLocker_Click(object sender, EventArgs e)
        {
            GearTL.CopySelected(db.LockerList, false);
        }
        private void CopyBackpack_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeAdv node in GearTree.SelectedNodes)
                if (node.Children.Count == 0)
                {
                    InventoryEntry entry = node.GetEntry();
                    if (entry.Type == InventoryType.Weapon)
                        db.WeaponList.Duplicate(entry); //LockerTL.CopySelected(db.WeaponList, false);
                    else if (entry.Type == InventoryType.Item)
                        db.ItemList.Duplicate(entry); //LockerTL.CopySelected(db.ItemList, false);
                }
        }
        private void CopyBank_Click(object sender, EventArgs e)
        {
            GearTL.CopySelected(db.BankList, false);
        }
        private void ClearAllGear_Click(object sender, EventArgs e)
        {
            GearTL.Clear();
        }
        private void PurgeDuplicatesGear_Click(object sender, EventArgs e)
        {
            GearTL.PurgeDuplicates();
        }

        private string ExportToTextGear()
        {
            List<string> InOutParts = new List<string>();

            for (int Progress = 0; Progress < PartsGear.Items.Count; Progress++)
                InOutParts.Add((string)PartsGear.Items[Progress]);
            
            List<int> values = InventoryEntry.CalculateValues((int)QuantityGear.Value,
                (int)QualityGear.Value, (int)EquippedSlotGear.SelectedIndex, (int)LevelIndexGear.Value, (int)JunkGear.Value, (int)LockedGear.Value, ((string)PartsGear.Items[0]));

            int valueCount = WillowSaveGame.ExportValuesCount;
            for (int i = 0; i < valueCount; i++)
                InOutParts.Add(values[i].ToString());

            return string.Join("\r\n", InOutParts.ToArray()) + "\r\n";
        }
        private void ExportToClipboardGear_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(ExportToTextGear());
            }
            catch { MessageBox.Show("Export to clipboard failed."); }
        }
        private void ExportToFileGear_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog ToFile = new Util.WTSaveFileDialog("txt", GearPartsGroup.Text);
            if (ToFile.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(ToFile.FileName(), ExportToTextGear());
        }
        private void ExportToXmlGears_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog fileDlg = new Util.WTSaveFileDialog("xml", gearFileName);

            if (fileDlg.ShowDialog() == DialogResult.OK)
                GearTL.SaveToXml(fileDlg.FileName());
        }

        private bool ImportFromTextGear(string text)
        {
            InventoryEntry gear = InventoryEntry.ImportFromText(text, GearTL.Unsorted.invType);

            if (gear == null) return false;

            GearTL.Add(gear);
            return true;
        }
        private void ImportFromClipboardGear_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImportFromTextGear(Clipboard.GetText()))
                    GearTree.SelectedNode = GearTree.AllNodes.Last();
            }
            catch
            {
                MessageBox.Show("Invalid clipboard data, " + gearTextName + " not inserted.");
            }
        }
        private void ImportFromFilesGears_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog FromFile = new Util.WTOpenFileDialog("txt", gearFileName);
            FromFile.Multiselect(true);

            if (FromFile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in FromFile.FileNames())
                {
                    try
                    {
                        if (ImportFromTextGear(System.IO.File.ReadAllText(file)))
                            GearTree.SelectedNode = GearTree.AllNodes.Last();
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to read file \"" + file + "\".");
                        continue;
                    }
                }
            }
        }
        private void ImportAllFromXmlGears_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog fileDlg = new Util.WTOpenFileDialog("xml", gearFileName);

            if (fileDlg.ShowDialog() == DialogResult.OK)
                GearTL.ImportFromXml(fileDlg.FileName(), GearTL.Unsorted.invType);
        }
        
        private void GearTree_SelectionChanged(object sender, EventArgs e)
        {
            int OldPartIndex = PartsGear.SelectedIndex;

            PartsGear.Items.Clear();

            // If the node has children it not an item. It is a category label.
            if (GearTree.SelectedNode == null || GearTree.SelectedNode.Children.Count > 0)
            {
                GearPartsGroup.Text = "No " + gearTextName + " Selected";
                return;
            }

            InventoryEntry gear = GearTree.SelectedNode.GetEntry();
            GearPartsGroup.Text = gear.Name;

            Init();
            for (int i = 0; i < gear.Parts.Count; i++)
                PartsGear.Items.Add(gear.Parts[i]);
            
            WTSlideSelector.MinMaxAdvanced(gear.UsesBigLevel, ref LevelIndexGear);

            Util.SetNumericUpDown(QuantityGear, gear.Quantity);
            QualityGear.Value = gear.QualityIndex;
            EquippedSlotGear.SelectedIndex = gear.EquippedSlot;
            LevelIndexGear.Value = gear.LevelIndex;
            JunkGear.Value = gear.IsJunk;
            LockedGear.Value = gear.IsLocked;

            if (PartsGear.Items.Count > OldPartIndex)
                PartsGear.SelectedIndex = OldPartIndex;

            GearInformationUpdate();
        }

        private void SaveChangesGear_Click(object sender, EventArgs e)
        {
            if (GearTree.SelectedNode == null)
                return;

            // Do nothing if it is a category not an item.
            if (GearTree.SelectedNode.Children.Count > 0)
                return;

            InventoryEntry gear = GearTree.SelectedNode.GetEntry();

            for (int Progress = 0; Progress < PartsGear.Items.Count; Progress++)
                gear.Parts[Progress] = (string)PartsGear.Items[Progress];

            gear.UsesBigLevel = InventoryEntry.ItemgradePartUsesBigLevel((string)PartsGear.Items[0]);
            gear.Quantity = (int)QuantityGear.Value;
            gear.QualityIndex = QualityGear.Value;
            gear.EquippedSlot = EquippedSlotGear.SelectedIndex;
            gear.LevelIndex = LevelIndexGear.Value;
            gear.IsJunk = (int)JunkGear.Value;
            gear.IsLocked = (int)LockedGear.Value;

            // Recalculate the gear stats
            if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Weapon)
                gear.RecalculateDataWeapon();
            else if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Item)
                gear.RecalculateDataItem();

            gear.BuildName();

            // When the item changes, it may not belong in the same location in
            // in the sorted tree because the name, level, or other sort key 
            // has changed.  Remove the node then place it back into the tree to 
            // make sure it is relocated to the proper location, then select the 
            // node and make sure it is visible so the user is focused on the new 
            // location after the changes.
            GearTL.RemoveFromTreeView(GearTree.SelectedNode, false);
            GearTL.AddToTreeView(gear);
            GearTL.AdjustSelectionAfterAdd();
            GearTree.EnsureVisible(GearTree.SelectedNode);

            // Set the parts group page header to display the new name
            GearPartsGroup.Text = gear.Name;
        }

        //TODO Should be less dependant from "Colorize Lists" menu
        private void btnGearSearch_Click(object sender, EventArgs e)
        {
            string searchText = GearSearch.Text.ToUpper();
            string text = "";

            foreach (TreeNodeAdv node in GearTL.Tree.AllNodes)
            {
                if (node.Children.Count == 0)
                {
                    text = node.GetEntry().ToXmlText().ToUpper();

                    if (searchText != "" && text.Contains(searchText))
                        (node.Tag as ColoredTextNode).Font = HighlightFont;
                    else
                        (node.Tag as ColoredTextNode).Font = GearTree.Font;
                }
            }
            this.Refresh(); //LockerTree_SelectionChanged is not needed for visual update
        }
        private void GearSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGearSearch_Click(this, KeyEventArgs.Empty);
            }
        }
        
        private void EditQualityAllGears_Click(object sender, EventArgs e)
        {
            int quality;
            string qualityText = Microsoft.VisualBasic.Interaction.InputBox("All of the " + gearTextName + "s in your backpack will be adjusted to the following level:", "Edit All Qualitys", "", 10, 10);

            try
            {
                quality = Parse.AsInt(qualityText);
            }
            catch (FormatException) { return; }

            foreach (InventoryEntry gear in GearTL.Sorted)
                gear.QualityIndex = quality;

            QualityGear.Value = quality;
        }
        private void EditLevelAllGears_Click(object sender, EventArgs e)
        {
            int level;
            int levelindex;

            string levelText = Microsoft.VisualBasic.Interaction.InputBox("All of the " + gearTextName + "s in your backpack will be adjusted to the following level:", "Edit All Levels", "", 10, 10);
            try
            {
                level = Parse.AsInt(levelText);
                levelindex = level + 2;
            }
            catch (FormatException) { return; }

            foreach (InventoryEntry gear in GearTL.Sorted)
            {
                gear.EffectiveLevel = level;
                gear.NameParts[5] = "(L" + level + ")";
                gear.LevelIndex = levelindex;
            }

            GearTL.UpdateNames();

            LevelIndexGear.Value = levelindex;
        }

        private void PartSelectorGear_SelectionChanged(object sender, EventArgs e)
        {
            PartInfoGear.Clear();
            try
            {
                // Read ALL subsections of a given XML section
                XmlFile Category = XmlFile.XmlFileFromCache(db.DataPath + PartSelectorGear.SelectedNode.Parent.GetKey() + ".txt");

                // XML Section: PartCategories.SelectedNode.Text
                List<string> xmlSection = Category.XmlReadSection(PartSelectorGear.SelectedNode.GetText());

                PartInfoGear.Lines = xmlSection.ToArray();
            }
            catch { }
        }
        private void PartSelectorGear_NodeDoubleClick(object sender, MouseEventArgs e)
        {
            if (PartsGear.SelectedItem != null && PartSelectorGear.SelectedNode.Children.Count == 0)
            {
                // If part selector tracking is active it'll reposition the node 
                // when the selected item is changed, so temporarily disable it.  
                bool tracking = GlobalSettings.PartSelectorTracking;
                GlobalSettings.PartSelectorTracking = false;
                PartsGear.Items[PartsGear.SelectedIndex] = PartSelectorGear.SelectedNode.Parent.GetKey() + "." + PartSelectorGear.SelectedNode.GetText();
                GlobalSettings.PartSelectorTracking = true;
            }
        }

        private void PartsGear_DoubleClick(object sender, EventArgs e)
        {
            string tempManualPart = Microsoft.VisualBasic.Interaction.InputBox("Enter a new part", "Manual Edit", (string)PartsGear.SelectedItem, 10, 10);
            if (tempManualPart != "")
                PartsGear.Items[PartsGear.SelectedIndex] = tempManualPart;
        }
        private void PartsGear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PartsGear.SelectedIndex == -1)
                return;

            string part = PartsGear.SelectedItem.ToString();

            if (GlobalSettings.PartSelectorTracking == true)
            {
                PartSelectorGear.BeginUpdate();
                TreeNodeAdv categoryNode = PartSelectorGear.FindFirstNodeByTag(part.Before('.'), false);
                if (categoryNode != null)
                {
                    TreeNodeAdv partNode = categoryNode.FindFirstByTag(part.After('.'), false);
                    PartSelectorGear.CollapseAll();
                    PartSelectorGear.SelectedNode = partNode;
                    if (partNode != null)
                        Util.CenterNode(PartSelectorGear.SelectedNode, gearVisibleLine);
                }
                PartSelectorGear.EndUpdate();
            }
            else
            {
                PartInfoGear.Clear();

                List<string> xmlSection = db.GetPartSection(part);

                if (xmlSection != null)
                    PartInfoGear.Lines = xmlSection.ToArray();
            }
        }
        private void PartsGear_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PartsGear.SelectedIndex == 0)
            {
                if (InventoryEntry.ItemgradePartUsesBigLevel((string)PartsGear.Items[0]))
                {
                    LevelIndexGear.MaximumAdvanced = int.MaxValue;
                    LevelIndexGear.MinimumAdvanced = int.MinValue;
                }
                else
                {
                    LevelIndexGear.MaximumAdvanced = short.MaxValue;
                    LevelIndexGear.MinimumAdvanced = short.MinValue;
                }            
            }
        }

        private void GearTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteGear_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Insert)
                DuplicateGear_Click(this, EventArgs.Empty);
            else if (e.KeyData == (Keys.Control | Keys.B))
                CopyBackpack_Click(this, EventArgs.Empty);
            else if (e.KeyData == (Keys.Control | Keys.N))
                CopyBank_Click(this, EventArgs.Empty);
            else if (e.KeyData == (Keys.Control | Keys.L))
                CopyLocker_Click(this, EventArgs.Empty);
        }
        private void PartsGear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeletePartGear_Click(this, EventArgs.Empty);
        }

        private void GearInformationUpdate()
        {
            txtGearInformation.Clear();

            if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Weapon)
                if (GearTree.SelectedNode.GetEntry() != null)
                    txtGearInformation.Text = db.WeaponInfo(GearTree.SelectedNode.GetEntry());
            //TODO RSM make like WeaponInfo
            //else if (GearTree.SelectedNode.GetEntry().Type == InventoryType.Item)
            //    if (GearTree.SelectedNode.GetEntry() != null)
            //        txtGearInformation.Text = db.ItemInfo(GearTree.SelectedNode.GetEntry());
        }
    }
}
