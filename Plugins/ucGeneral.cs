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
using System.Xml;
using Aga.Controls.Tree;

namespace WillowTree.Plugins
{
    public partial class ucGeneral : UserControl, IPlugin
    {
        WillowSaveGame CurrentWSG;
        XmlFile LocationsXml;

        public ucGeneral()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            events.GameSaving = OnGameSaving;
            pm.RegisterPlugin(this, events);

            this.Enabled = false;
            LocationsXml = db.LocationsXml;
            db.setXPchart();
            DoLocationsList();
            Cash.Maximum = GlobalSettings.MaxCash;
            Experience.Maximum = GlobalSettings.MaxExperience;
            Level.Maximum = GlobalSettings.MaxLevel;
            BankSpace.Maximum = GlobalSettings.MaxBankSlots;
            BackpackSpace.Maximum = GlobalSettings.MaxBackpackSlots;
            SkillPoints.Maximum = GlobalSettings.MaxSkillPoints;
        }

        public void ReleasePlugin()
        {
            CurrentWSG = null;
            LocationsXml = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            // Warning: Setting numeric up/down controls to values that are outside their min/max
            // range will cause an exception and crash the program.  It is safest to use
            // Util.SetNumericUpDown() to set them since it will adjust the value to a valid value
            // if it is too high or low.
            CurrentWSG = e.WTM.SaveData;

            CharacterName.Text = CurrentWSG.CharacterName;
            Util.SetNumericUpDown(Level, CurrentWSG.Level);
            if (Level.Value != CurrentWSG.Level)
                MessageBox.Show("The character's level was outside the acceptable range.  It has been adjusted.\n\nOld: " + CurrentWSG.Level + "\nNew: " + (int)Level.Value);

            Util.SetNumericUpDown(Experience, CurrentWSG.Experience);
            if (Experience.Value != CurrentWSG.Experience)
                MessageBox.Show("The character's experience was outside the acceptable range.  It has been adjusted.\n\nOld: " + CurrentWSG.Experience + "\nNew: " + (int)Experience.Value);

            Util.SetNumericUpDown(SkillPoints, CurrentWSG.SkillPoints);
            if (SkillPoints.Value != CurrentWSG.SkillPoints)
                MessageBox.Show("The character's skill point count was outside the acceptable range.  It has been adjusted.\n\nOld: " + CurrentWSG.SkillPoints + "\nNew: " + (int)SkillPoints.Value);

            if (CurrentWSG.FinishedPlaythrough1 == 0)
                PT2Unlocked.SelectedIndex = 0;
            else
                PT2Unlocked.SelectedIndex = 1;

            // No message when cash is adjusted because it will likely have to be changed on
            // every load for people who exceed the limit.  The spam would be annoying.
            if (CurrentWSG.Cash < 0)
                Util.SetNumericUpDown(Cash, int.MaxValue);
            else
                Util.SetNumericUpDown(Cash, CurrentWSG.Cash);
          
            Util.SetNumericUpDown(BackpackSpace, CurrentWSG.BackpackSize);
            if (BackpackSpace.Value != CurrentWSG.BackpackSize)
                MessageBox.Show("The character's backpack capacity was outside the acceptable range.  It has been adjusted.\n\nOld: " + CurrentWSG.BackpackSize + "\nNew: " + (int)BackpackSpace.Value);

            Util.SetNumericUpDown(EquipSlots, CurrentWSG.EquipSlots);
            Util.SetNumericUpDown(SaveNumber, CurrentWSG.SaveNumber);

            UI_UpdateCurrentLocationComboBox(CurrentWSG.CurrentLocation);

            if (CurrentWSG.Class == "gd_Roland.Character.CharacterClass_Roland") Class.SelectedIndex = 0;
            else if (CurrentWSG.Class == "gd_lilith.Character.CharacterClass_Lilith") Class.SelectedIndex = 1;
            else if (CurrentWSG.Class == "gd_mordecai.Character.CharacterClass_Mordecai") Class.SelectedIndex = 2;
            else if (CurrentWSG.Class == "gd_Brick.Character.CharacterClass_Brick") Class.SelectedIndex = 3;

            // If DLC section 1 is not present then the bank does not exist, so disable the
            // control to prevent the user from editing its size.
            labelGeneralBankSpace.Enabled = CurrentWSG.DLC.HasSection1;
            BankSpace.Enabled = CurrentWSG.DLC.HasSection1;
            if (CurrentWSG.DLC.HasSection1)
            {
                Util.SetNumericUpDown(BankSpace, CurrentWSG.DLC.BankSize);
                if (BankSpace.Value != CurrentWSG.DLC.BankSize)
                    MessageBox.Show("The character's bank capacity was outside the acceptable range.  It has been adjusted.\n\nOld: " + CurrentWSG.BackpackSize + "\nNew: " + (int)BackpackSpace.Value);
            }
            else
                Util.SetNumericUpDown(BankSpace, 0);

            DoWindowTitle();
            Application.DoEvents();
            DoLocationTree();
            this.Enabled = true;
        }

        public void OnGameSaving(object sender, PluginEventArgs e)
        {
            if (BankSpace.Enabled)
                CurrentWSG.DLC.BankSize = (int)BankSpace.Value;

            // TODO: Most of these values that are being set in GameSaving should
            // be set right away with events when the values change in order to
            // play nicely with other plugins.  There is the potential for this
            // plugin to change a value and another plugin may not be aware of the
            // change because it only gets applied at save time the way it works now.
            CurrentWSG.CharacterName = CharacterName.Text;
            CurrentWSG.Level = (int)Level.Value;
            CurrentWSG.Experience = (int)Experience.Value;
            CurrentWSG.SkillPoints = (int)SkillPoints.Value;
            CurrentWSG.FinishedPlaythrough1 = PT2Unlocked.SelectedIndex;
            CurrentWSG.Cash = (int)Cash.Value;
            CurrentWSG.BackpackSize = (int)BackpackSpace.Value;
            CurrentWSG.EquipSlots = (int)EquipSlots.Value;
            CurrentWSG.SaveNumber = (int)SaveNumber.Value;

            // Try to look up the outpost name from the text that is displayed in the combo box.
            string loc = LocationsXml.XmlReadAssociatedValue("OutpostName", "OutpostDisplayName", (string)CurrentLocation.SelectedItem);
    
            // If the outpost name is not found then this location is not in the data file
            // so the string stored in CurrentLocation is already the outpost name.
            if (loc == "")
                loc = (string)CurrentLocation.SelectedItem;

            CurrentWSG.CurrentLocation = loc;
        }

        private void UpdateClass()
        {
            if (Class.SelectedIndex == 0) CurrentWSG.Class = "gd_Roland.Character.CharacterClass_Roland";
            else if (Class.SelectedIndex == 1) CurrentWSG.Class = "gd_lilith.Character.CharacterClass_Lilith";
            else if (Class.SelectedIndex == 2) CurrentWSG.Class = "gd_mordecai.Character.CharacterClass_Mordecai";
            else if (Class.SelectedIndex == 3) CurrentWSG.Class = "gd_Brick.Character.CharacterClass_Brick";
        }

        public void UI_UpdateCurrentLocationComboBox(string locationToSelect)
        {
            CurrentLocation.Items.Clear();
            CurrentLocation.Items.Add("None");

            // See if the selected location can be found in the WT# data file.
            string loc = LocationsXml.XmlReadAssociatedValue("OutpostDisplayName", "OutpostName", locationToSelect);
            if (loc == "")
            {
                // Not in the data file, so an entry must be added or the combo
                // box won't even be able to display the selected location.
                if (locationToSelect != "None")
                    CurrentLocation.Items.Add(locationToSelect);
                loc = locationToSelect;
            }
     
            // Add all the location entries that were in the WT# location file
            foreach (string location in LocationsList.Items)
                CurrentLocation.Items.Add(location);

            CurrentLocation.SelectedItem = loc;
        }

        public void DoLocationsList()
        {
            LocationsList.Items.Clear();

            foreach (string section in LocationsXml.stListSectionNames())
            {
                string outpostname = LocationsXml.XmlReadValue(section, "OutpostDisplayName");
                if (outpostname == "")
                    outpostname = LocationsXml.XmlReadValue(section, "OutpostName");
                LocationsList.Items.Add(outpostname);
            }
        }

        public void DoLocationTree()
        {
            // Clear the tree
            TreeModel model = new TreeModel();
            LocationTree.Model = model;

            LocationTree.BeginUpdate();
            for (int build = 0; build < CurrentWSG.TotalLocations; build++)
            {
                string key = CurrentWSG.LocationStrings[build];
                string name = LocationsXml.XmlReadAssociatedValue("OutpostDisplayName", "OutpostName", key);
                if (name == "")
                    name = key;

                ColoredTextNode node = new ColoredTextNode(name);
                node.Tag = key;

                model.Nodes.Add(node);
            }
            //LocationTree.Update();
            LocationTree.EndUpdate();
        }

        public void DoWindowTitle()
        {
            this.ParentForm.Text = "WillowTree# - " + CharacterName.Text + "  Level " + Level.Value + " " + Class.Text + " (" + CurrentWSG.Platform + ")";
        }

        public List<string> ReadFromXmlLocations(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNodeList locationnodes = doc.SelectNodes("WT/Locations/Location");

            if (locationnodes == null)
            {
                // Give an exception if there is no location section
                if (doc.SelectSingleNode("WT/Locations") == null)
                    throw new ApplicationException("NoLocations");
                // or an empty list if there was a location section but it has no location entries
                return new List<string>();
            }

            List<string> locations = new List<string>();
            foreach (string location in locationnodes)
                locations.Add(location);

            return locations;
        }
        public List<string> ReadFromSaveLocations(string filename)
        {
            WillowSaveGame OtherSave = new WillowSaveGame();

            try
            {
                OtherSave.LoadWSG(filename);
            }
            catch { throw new ApplicationException("LoadFailed"); }

            List<string> locations = new List<string>(OtherSave.LocationStrings);
            return locations;
        }

        private void DeleteAllLocations()
        {
            CurrentWSG.TotalLocations = 0;
            CurrentWSG.LocationStrings = new string[0];
            DoLocationTree();
        }
        public void MergeFromSaveLocations(string filename)
        {
            WillowSaveGame OtherSave = new WillowSaveGame();
            OtherSave.LoadWSG(filename);

            if (OtherSave.LocationStrings.Count() == 0)
                return;

            // Construct a list structure with the current locations
            List<string> locations = new List<string>(CurrentWSG.LocationStrings);

            // Copy only the locations that are not duplicates from the other save
            foreach (string location in OtherSave.LocationStrings)
            {
                if (locations.Contains(location) == false)
                    locations.Add(location);
            }

            // Update WSG data from the newly constructed list
            CurrentWSG.LocationStrings = locations.ToArray();
            CurrentWSG.TotalLocations = locations.Count();
            DoLocationTree();
        }
        public void MergeAllFromXmlLocations(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("WT/Locations") == null)
                throw new ApplicationException("NoLocations");

            XmlNodeList locationnodes = doc.SelectNodes("WT/Locations/Location");
            if (locationnodes == null)
                return;

            // Construct a list structure with the current locations
            List<string> locations = new List<string>(CurrentWSG.LocationStrings);

            // Copy only the locations that are not duplicates from the XML file
            foreach (XmlNode node in locationnodes)
            {
                string location = node.InnerText;
                if (locations.Contains(location) == false)
                    locations.Add(node.InnerText);
            }

            // Update WSG data from the newly constructed list
            CurrentWSG.LocationStrings = locations.ToArray();
            CurrentWSG.TotalLocations = locations.Count();
            DoLocationTree();
        }
        public void LoadLocations(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("WT/Locations") == null)
                throw new ApplicationException("NoLocations");

            XmlNodeList locationnodes = doc.SelectNodes("WT/Locations/Location");

            int locationcount = locationnodes.Count;
            string[] location = new string[locationcount];
            for (int i = 0; i < locationcount; i++)
                location[i] = locationnodes[i].InnerText;

            CurrentWSG.LocationStrings = location;
            CurrentWSG.TotalLocations = locationcount;
            DoLocationTree();
        }
        private void SaveToXmlLocations(string filename)
        {
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Location File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Locations");
            for (int i = 0; i < CurrentWSG.TotalLocations; i++)
                writer.WriteElementString("Location", CurrentWSG.LocationStrings[i]);
            writer.WriteEndDocument();
            writer.Close();
        }
        private void SaveSelectedToXmlLocations(string filename)
        {
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Location File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Locations");

            foreach (TreeNodeAdv nodeAdv in LocationTree.SelectedNodes)
            {
                string name = nodeAdv.GetKey();
                if (!string.IsNullOrEmpty(name))
                    writer.WriteElementString("Location", name);
            }
   
            writer.WriteEndDocument();
            writer.Close();
        }

        private void CharacterName_TextChanged(object sender, EventArgs e)
        {
            DoWindowTitle();
        }

        private void Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateClass();
            DoWindowTitle();
        }

        private void DeleteAllLocations_Click(object sender, EventArgs e)
        {
            DeleteAllLocations();
        }

        private void DeleteLocation_Click(object sender, EventArgs e)
        {
            TreeNodeAdv NextSelection = null;
            while (LocationTree.SelectedNode != null)
            {
                int Selected = LocationTree.SelectedNode.Index;

                CurrentWSG.TotalLocations = CurrentWSG.TotalLocations - 1;
                for (int Position = Selected; Position < CurrentWSG.TotalLocations; Position++)
                {
                    CurrentWSG.LocationStrings[Position] = CurrentWSG.LocationStrings[Position + 1];
                }
                Util.ResizeArraySmaller(ref CurrentWSG.LocationStrings, CurrentWSG.TotalLocations);

                NextSelection = LocationTree.SelectedNode.NextVisibleNode;
                LocationTree.SelectedNode.Remove();
            }
            if (NextSelection != null)
                LocationTree.SelectedNode = NextSelection;

            DoLocationTree();
        }

        private void ExportAllToFileLocations_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog tempSave = new Util.WTSaveFileDialog("locations", "Default.locations");
            try
            {
                if (tempSave.ShowDialog() == DialogResult.OK)
                {
                    SaveToXmlLocations(tempSave.FileName());
                    MessageBox.Show("Locations saved to " + tempSave.FileName());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while trying to save locations: " + ex.ToString());
                return;
            }
        }

        private void ExportSelectedToFileLocations_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog tempSave = new Util.WTSaveFileDialog("locations", "Default.locations");

            try
            {
                if (tempSave.ShowDialog() == DialogResult.OK)
                {
                    SaveSelectedToXmlLocations(tempSave.FileName());
                    MessageBox.Show("Locations saved to " + tempSave.FileName());
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error occurred while trying to save locations: " + ex.ToString()); 
                return; 
            }
        }

        private void ImportAllFromFileLocations_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("locations", "Default.locations");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadLocations(tempOpen.FileName());
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoLocations")
                        MessageBox.Show("Couldn't find a location section in the file.  Action aborted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while trying to load: " + ex.ToString());
                    return;
                }
            }
        }

        private void MergeAllFromFileLocations_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("locations", "Default.locations");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MergeAllFromXmlLocations(tempOpen.FileName());
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoLocations")
                        MessageBox.Show("Couldn't find a location section in the file.  Action aborted.");
                }
                catch { MessageBox.Show("Couldn't load the file.  Action aborted."); }
            }
        }

        private void MergeFromSaveLocations_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MergeFromSaveLocations(tempOpen.FileName());
                }
                catch { MessageBox.Show("Couldn't open the other save file."); return; }

                DoLocationTree();
            }
        }

        private void Level_ValueChanged(object sender, EventArgs e)
        {
            if (Level.Value > 0 && Level.Value < 70)
            {
                Experience.Minimum = db.XPChart[(int)Level.Value];

            }
            else
            {
                Experience.Minimum = 0;
            }
            DoWindowTitle();
        }

        private void ImportAllFromSaveLocations_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                WillowSaveGame OtherSave = new WillowSaveGame();

                try
                {
                    OtherSave.LoadWSG(tempOpen.FileName());
                }
                catch { MessageBox.Show("Couldn't open the other save file."); return; }

                CurrentWSG.TotalLocations = OtherSave.TotalLocations;
                CurrentWSG.LocationStrings = OtherSave.LocationStrings;
                DoLocationTree();
            }
        }

        private void LocationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelectedItem = LocationsList.SelectedIndex;
                CurrentWSG.TotalLocations = CurrentWSG.TotalLocations + 1;
                Util.ResizeArrayLarger(ref CurrentWSG.LocationStrings, CurrentWSG.TotalLocations);
                CurrentWSG.LocationStrings[CurrentWSG.TotalLocations - 1] = LocationsXml.stListSectionNames()[SelectedItem];
                DoLocationTree();
            }
            catch { }
        }

        private void LocationTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteLocation_Click(this, EventArgs.Empty);
        }
    }
}
