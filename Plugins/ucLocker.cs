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
    public partial class ucLocker : UserControl, IPlugin
    {
        #region Members
        PluginComponentManager pluginManager;
        Font HighlightFont;

        public InventoryTreeList LockerTL;
        #endregion

        public ucLocker()
        {
            InitializeComponent();
        }
        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            events.GameSaving = OnGameSaving;
            events.PluginCommand = OnPluginCommand;
            pm.RegisterPlugin(this, events);

            pluginManager = pm;

            // The index translators control the caption that goes over the top of each
            // level or quality SlideSelector.  Attach each translator then signal the
            // value changed event to cause the translator to update the caption.
            this.QualityLocker.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);
            this.LevelIndexLocker.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.LevelIndexOverride.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.QualityOverride.IndexTranslator += new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);
            this.LevelIndexLocker.OnValueChanged(EventArgs.Empty);
            this.QualityLocker.OnValueChanged(EventArgs.Empty);
            this.LevelIndexOverride.OnValueChanged(EventArgs.Empty);
            this.QualityOverride.OnValueChanged(EventArgs.Empty);

            HighlightFont = new Font(LockerTree.Font, FontStyle.Italic | FontStyle.Bold);

            ImportAllFromItems.Enabled = false;
            ImportAllFromWeapons.Enabled = false;

            LockerTL = new InventoryTreeList(LockerTree, db.LockerList);

            string lockerFilename = db.OpenedLockerFilename();
            if (!System.IO.File.Exists(lockerFilename))
                db.OpenedLockerFilename(db.DataPath + "default.xml");

            try
            {
                LoadLocker(db.OpenedLockerFilename());
                LockerTL.UpdateTree();
            }
            catch (ApplicationException)
            {
                MessageBox.Show("The locker file \"" + db.OpenedLockerFilename() + " could not be loaded.  It may be corrupt.  If you delete or rename it the program will make a new one and you may be able to start the program successfully.  Shutting down now.");
                Application.Exit();
                return;
            }
        }

        public void ReleasePlugin()
        {
            this.QualityLocker.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);
            this.LevelIndexLocker.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.LevelIndexOverride.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.LevelTranslator);
            this.QualityOverride.IndexTranslator -= new WillowTree.CustomControls.WTSlideSelector.SlideIndexTranslator(Util.QualityTranslator);

            pluginManager = null;
            HighlightFont = null;

            LockerTL = null;
            db.OpenedLockerFilename(null);
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            ImportAllFromItems.Enabled = true;
            ImportAllFromWeapons.Enabled = true;
            LockerTL.UpdateTree();
        }

        public void OnGameSaving(object sender, PluginEventArgs e)
        {
            LockerTL.SaveToXml(db.OpenedLockerFilename());
        }

        public void OnPluginCommand(object sender, PluginCommandEventArgs e)
        {
            if (e.Command == PluginCommand.IncreaseNavigationDepth)
                LockerTL.IncreaseNavigationDepth();
            else if (e.Command == PluginCommand.ChangeSortMode)
                LockerTL.NextSort();
        }

        public void LoadLocker(string InputFile)
        {
            LockerTL.Tree.BeginUpdate();
            LockerTL.Clear();
            LockerTL.ImportFromXml(InputFile, InventoryType.Any);
            LockerTL.Tree.EndUpdate();
        }

        private void NewWeaponLocker_Click(object sender, EventArgs e)
        {
            LockerTL.AddNew(InventoryType.Weapon);
            LockerTL.AdjustSelectionAfterAdd();
            LockerTree.EnsureVisible(LockerTree.SelectedNode);
        }
        private void NewItemLocker_Click(object sender, EventArgs e)
        {
            LockerTL.AddNew(InventoryType.Item);
            LockerTL.AdjustSelectionAfterAdd();
            LockerTree.EnsureVisible(LockerTree.SelectedNode);
        }
        private void DeleteLocker_Click(object sender, EventArgs e)
        {
            LockerTL.DeleteSelected();
        }
        private void DuplicateLocker_Click(object sender, EventArgs e)
        {
            LockerTL.DuplicateSelected();
        }

        private void MoveLocker_Click(object sender, EventArgs e)
        {
            // Copy the selection because it will change as nodes are removed.
            TreeNodeAdv[] nodes = LockerTree.SelectedNodes.ToArray();

            foreach (TreeNodeAdv node in nodes)
                if (node.Children.Count == 0)
                {
                    InventoryEntry entry = node.GetEntry();
                    if (entry.Type == InventoryType.Weapon)
                        db.WeaponList.Add(entry);
                    else if (entry.Type == InventoryType.Item)
                        db.ItemList.Add(entry);

                    LockerTL.Remove(node, false);
                }
        }
        private void CopyBackpack_Click(object sender, EventArgs e)
        {
            foreach (TreeNodeAdv node in LockerTree.SelectedNodes)
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
            LockerTL.CopySelected(db.BankList, false);
        }
        private void ClearAllLocker_Click(object sender, EventArgs e)
        {
            LockerTL.Clear();
        }
        private void PurgeDuplicatesLocker_Click(object sender, EventArgs e)
        {
            LockerTL.PurgeDuplicates();
        }

        private string ExportToTextLocker()
        {
            List<string> InOutParts = new List<string>();

            for (int Progress = 0; Progress < PartsLocker.Items.Count; Progress++)
                InOutParts.Add((string)PartsLocker.Items[Progress]);

            List<int> values = null;

            if (OverrideExportSettings.Checked == true)
                values = InventoryEntry.CalculateValues((int)RemAmmoOverride.Value,
                    (int)QualityOverride.Value, 0, (int)LevelIndexOverride.Value, ((string)PartsLocker.Items[0]));
            else
                values = InventoryEntry.CalculateValues((int)RemAmmoLocker.Value,
                    (int)QualityLocker.Value, 0, (int)LevelIndexLocker.Value, ((string)PartsLocker.Items[0]));

            for (int i = 0; i < 4; i++)
                InOutParts.Add(values[i].ToString());

            return string.Join("\r\n", InOutParts.ToArray()) + "\r\n";
        }
        private void ExportToClipboardLocker_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(ExportToTextLocker());
            }
            catch { MessageBox.Show("Export to clipboard failed."); }
        }
        private void ExportToFileLocker_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog ToFile = new Util.WTSaveFileDialog("txt", LockerPartsGroup.Text);
            if (ToFile.ShowDialog() == DialogResult.OK)
                System.IO.File.WriteAllText(ToFile.FileName(), ExportToTextLocker());
        }
        private void ExportToXmlLocker_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog fileDlg = new Util.WTSaveFileDialog("xml", db.OpenedLockerFilename());

            if (fileDlg.ShowDialog() == DialogResult.OK)
                LockerTL.SaveToXml(fileDlg.FileName());
        }

        private bool ImportFromTextLocker(string text)
        {
            InventoryEntry gear = InventoryEntry.ImportFromText(text, InventoryType.Unknown);

            if (gear == null) return false;

            LockerTL.Add(gear);
            return true;
        }
        private void ImportFromClipboardLocker_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImportFromTextLocker(Clipboard.GetText()))
                    LockerTree.SelectedNode = LockerTree.AllNodes.Last();
            }
            catch
            {
                MessageBox.Show("Invalid clipboard data.  Import failed.");
            }
        }
        private void ImportFromFilesLocker_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog FromFile = new Util.WTOpenFileDialog("txt", "");
            FromFile.Multiselect(true);

            if (FromFile.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in FromFile.FileNames())
                {
                    try
                    {
                        if (ImportFromTextLocker(System.IO.File.ReadAllText(file)))
                            LockerTree.SelectedNode = LockerTree.AllNodes.Last();
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to read file \"" + file + "\".");
                        continue;
                    }
                }
            }
        }
        private void ImportAllFromXmlLocker_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog fileDlg = new Util.WTOpenFileDialog("xml", "");
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                LockerTL.ImportFromXml(fileDlg.FileName(), InventoryType.Any);
                LockerTL.SaveToXml(fileDlg.FileName());
            }
        }
        private void ImportAllFromItemsLocker_Click(object sender, EventArgs e)
        {
            LockerTree.BeginUpdate();
            foreach (InventoryEntry item in db.ItemList.Items.Values)
                LockerTL.Duplicate(item);
            LockerTree.EndUpdate();
            LockerTL.SaveToXml(db.OpenedLockerFilename());
        }
        private void ImportAllFromWeaponsLocker_Click(object sender, EventArgs e)
        {
            LockerTree.BeginUpdate();
            foreach (InventoryEntry weapon in db.WeaponList.Items.Values)
                LockerTL.Duplicate(weapon);
            LockerTree.EndUpdate();
            LockerTL.SaveToXml(db.OpenedLockerFilename());
        }
        
        private void LockerTree_SelectionChanged(object sender, EventArgs e)
        {
            PartsLocker.Items.Clear();

            if (LockerTree.SelectedNode == null || LockerTree.SelectedNode.Children.Count > 0)
                return;

            InventoryEntry entry = LockerTree.SelectedNode.GetEntry();

            if (LockerTree.SelectedNode.Children.Count == 0)
            {   // Tree nodes with no children are items or weapons.  Entries with children would be section headers.
                string SelectedItem = LockerTree.SelectedNode.Data().Text;

                LockerPartsGroup.Text = entry.Name;
                RatingLocker.Text = entry.Rating;
                DescriptionLocker.Text = entry.Description.Replace("$LINE$", "\r\n");


                int partcount = entry.GetPartCount();

                for (int Progress = 0; Progress < partcount; Progress++)
                    PartsLocker.Items.Add(entry.Parts[Progress]);
                
                WTSlideSelector.MinMaxAdvanced(entry.UsesBigLevel, ref LevelIndexLocker);

                Util.SetNumericUpDown(RemAmmoLocker, entry.Quantity);
                QualityLocker.Value = entry.QualityIndex;
                LevelIndexLocker.Value = entry.LevelIndex;
            }
        }
        
        private void SaveChangesLocker_Click(object sender, EventArgs e)
        {
            if (LockerTree.SelectedNode == null)
                return;

            if (LockerTree.SelectedNode.Children.Count > 0)
                return;

            InventoryEntry entry = LockerTree.SelectedNode.GetEntry();

            int partcount = entry.GetPartCount();

            for (int Progress = 0; Progress < partcount; Progress++)
                entry.Parts[Progress] = (string)PartsLocker.Items[Progress];

            entry.UsesBigLevel = InventoryEntry.ItemgradePartUsesBigLevel((string)PartsLocker.Items[0]);

            entry.Quantity = (int)RemAmmoLocker.Value;
            entry.QualityIndex = QualityLocker.Value;
            entry.EquippedSlot = 0;
            entry.LevelIndex = LevelIndexLocker.Value;

            if (entry.Type == InventoryType.Weapon)
            {
                entry.RecalculateDataWeapon();
                entry.BuildName();
            }
            else if (entry.Type == InventoryType.Item)
            {
                entry.RecalculateDataItem();
                entry.BuildName();
            }
            else
            {
                System.Diagnostics.Debug.Assert(true, "Invalid item type in locker");
                entry.Name = "Invalid ItemType (" + entry.Type + ")";
            }

            // When the item changes, it may not belong in the same location in
            // in the sorted tree because the name, level, or other sort key 
            // has changed.  Remove the node then place it back into the tree to 
            // make sure it is relocated to the proper location, then select the 
            // node and make sure it is visible so the user is focused on the new 
            // location after the changes.
            LockerTL.RemoveFromTreeView(LockerTree.SelectedNode, false);
            LockerTL.AddToTreeView(entry);
            LockerTL.AdjustSelectionAfterAdd();
            LockerTree.EnsureVisible(LockerTree.SelectedNode);

            LockerPartsGroup.Text = entry.Name;
            LockerTree.Focus();
        }

        //TODO Should be less dependant from "Colorize Lists" menu
        private void btnlockerSearch_Click(object sender, EventArgs e)
        {
            string searchText = lockerSearch.Text.ToUpper();
            string text = "";

            foreach (TreeNodeAdv node in LockerTL.Tree.AllNodes)
            {
                if (node.Children.Count == 0)
                {
                    text = node.GetEntry().ToXmlText().ToUpper();

                    if (searchText != "" && text.Contains(searchText))
                        (node.Tag as ColoredTextNode).Font = HighlightFont;
                    else
                        (node.Tag as ColoredTextNode).Font = LockerTree.Font;
                }
            }
            this.Refresh(); //LockerTree_SelectionChanged is not needed for visual update
        }
        private void lockerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnlockerSearch_Click(this, KeyEventArgs.Empty);
            }
        }
        
        private void OpenLocker_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog FromFile = new Util.WTOpenFileDialog("xml", db.OpenedLockerFilename());

            try
            {
                if (FromFile.ShowDialog() == DialogResult.OK)
                {
                    LoadLocker(FromFile.FileName());
                    db.OpenedLockerFilename(FromFile.FileName()); 
                }
            }
            catch { MessageBox.Show("Could not load the selected WillowTree Locker."); }
        }
        
        private void LockerTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteLocker_Click(this, EventArgs.Empty);
            else if (e.KeyCode == Keys.Insert)
                DuplicateLocker_Click(this, EventArgs.Empty);
            else if (e.KeyData == (Keys.Control | Keys.B))
                CopyBackpack_Click(this, EventArgs.Empty);
            else if (e.KeyData == (Keys.Control | Keys.N))
                CopyBank_Click(this, EventArgs.Empty);
        }
    }
}
