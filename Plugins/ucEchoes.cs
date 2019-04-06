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
using WillowTree;
using System.Xml;

namespace WillowTree.Plugins
{
    public partial class ucEchoes : UserControl, IPlugin
    {
        WillowSaveGame CurrentWSG;
        XmlFile EchoesXml;

        public ucEchoes()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            pm.RegisterPlugin(this, events);

            EchoesXml = db.EchoesXml;
            DoEchoList();
            this.Enabled = false;
        }

        public void ReleasePlugin()
        {
            CurrentWSG = null;
            EchoesXml = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            CurrentWSG = e.WTM.SaveData;
            DoEchoTree();
            this.Enabled = true;
        }

        public void DoEchoList()
        {
            foreach (string section in EchoesXml.stListSectionNames())
            {
                string name = EchoesXml.XmlReadValue(section, "Subject");
                if (name != "")
                    EchoList.Items.Add(name);
                else
                    EchoList.Items.Add(section);
            }
        }

        public void DoEchoTree()
        {
            EchoTree.BeginUpdate();
            TreeModel model = new TreeModel();
            EchoTree.Model = model;

            for (int i = 0; i < CurrentWSG.NumberOfEchoLists; i++)
            {
                // Category nodes
                //      Text = human readable category heading
                //      Tag = echo list index stored as a string (0 based)
                ColoredTextNode parent = new ColoredTextNode();
                parent.Tag = i.ToString();
                parent.Text = "Playthrough " + (CurrentWSG.EchoLists[i].Index + 1) + " Echo Logs";
                model.Nodes.Add(parent);

                for (int build = 0; build < CurrentWSG.EchoLists[i].TotalEchoes; build++)
                {
                    string name = CurrentWSG.EchoLists[i].Echoes[build].Name;

                    // Echo nodes
                    //      Text = human readable echo name
                    //      Tag = internal echo name
                    ColoredTextNode node = new ColoredTextNode();
                    node.Tag = name;
                    node.Text = EchoesXml.XmlReadValue(name, "Subject");
                    if (node.Text == "")
                        node.Text = "(" + name + ")";
                    parent.Nodes.Add(node);
                }
            }
            EchoTree.EndUpdate();
        }

        public static string EchoSearchKey;
        public bool EchoSearchByName(WillowSaveGame.EchoEntry ee)
        {
            return ee.Name == EchoSearchKey;
        }

        public void DeleteAllEchoes(int index)
        {
            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];
            et.Echoes.Clear();
            et.TotalEchoes = 0;

            TreeNodeAdv[] children = EchoTree.Root.Children[index].Children.ToArray();
            foreach (TreeNodeAdv child in children)
                child.Remove();
        }
        private int GetSelectedEchoList()
        {
            int index = -1;

            if (EchoTree.SelectedNode == null)
            {
                // Do nothing, fall through to the feedback message below
            }
            else if (EchoTree.SelectedNode.Parent != EchoTree.Root)
                index = Parse.AsInt(EchoTree.SelectedNode.Parent.GetKey(), -1);
            else
            {
                // This is a category node not an echo.  If there is exactly one
                // selected then choose it as the location for import, otherwise 
                // do nothing and let the feedback message below take effect.
                if (EchoTree.SelectedNodes.Count == 1)
                    index = Parse.AsInt(EchoTree.SelectedNode.GetKey());
            }
            return index;
        }
        public void LoadEchoes(string filename, int index)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("WT/Echoes") == null)
                throw new ApplicationException("NoEchoes");

            XmlNodeList echonodes = doc.SelectNodes("WT/Echoes/Echo");

            int count = echonodes.Count;

            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];
            et.Echoes.Clear();
            et.TotalEchoes = 0;

            EchoTree.BeginUpdate();

            for (int i = 0; i < count; i++)
            {
                // Create a new echo entry and populate it from the xml node
                WillowSaveGame.EchoEntry ee = new WillowSaveGame.EchoEntry();
                XmlNode node = echonodes[i];
                string name = node.GetElement("Name", "");
                ee.Name = name;
                ee.DLCValue1 = node.GetElementAsInt("DLCValue1", 0);
                ee.DLCValue2 = node.GetElementAsInt("DLCValue2", 0);

                // Add the echo to the list
                et.Echoes.Add(ee);
                et.TotalEchoes++;

                // Add the echo to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = name;
                treeNode.Text = EchoesXml.XmlReadValue(name, "Subject");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + name + ")";
                EchoTree.Root.Children[index].AddNode(treeNode);
            }

            EchoTree.EndUpdate();
        }
        public void MergeFromSaveEchoes(string filename, int index)
        {
            WillowSaveGame OtherSave = new WillowSaveGame();
            OtherSave.LoadWSG(filename);

            if (OtherSave.NumberOfEchoLists - 1 < index)
                return;

            WillowSaveGame.EchoTable etOther = OtherSave.EchoLists[index];
            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

            EchoTree.BeginUpdate();

            // Copy only the locations that are not duplicates from the other save
            foreach (WillowSaveGame.EchoEntry ee in CurrentWSG.EchoLists[index].Echoes)
            {
                string name = ee.Name;

                // Make sure the echo is not already in the list
                EchoSearchKey = name;
                if (et.Echoes.FindIndex(EchoSearchByName) != -1)
                    continue;

                // Add the echo entry to the echo list
                et.Echoes.Add(ee);
                et.TotalEchoes++;

                // Add the echo to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = name;
                treeNode.Text = EchoesXml.XmlReadValue(name, "Subject");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + name + ")";
                EchoTree.Root.Children[index].AddNode(treeNode);
            }
            EchoTree.EndUpdate();
        }
        public void MergeAllFromXmlEchoes(string filename, int index)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("WT/Echoes") == null)
                throw new ApplicationException("NoEchoes");

            XmlNodeList echonodes = doc.SelectNodes("WT/Echoes/Echo");
            if (echonodes == null)
                return;

            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

            EchoTree.BeginUpdate();

            // Copy only the echos that are not duplicates from the XML file
            foreach (XmlNode node in echonodes)
            {
                string name = node.GetElement("Name", "");

                // Make sure the echo is not already in the list
                EchoSearchKey = name;
                if (et.Echoes.FindIndex(EchoSearchByName) != -1)
                    continue;

                // Create a new echo entry an populate it from the node
                WillowSaveGame.EchoEntry ee = new WillowSaveGame.EchoEntry();
                ee.Name = name;
                ee.DLCValue1 = node.GetElementAsInt("DLCValue1", 0);
                ee.DLCValue2 = node.GetElementAsInt("DLCValue2", 0);

                // Add the echo entry to the echo list
                et.Echoes.Add(ee);
                et.TotalEchoes++;

                // Add the echo to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = name;
                treeNode.Text = EchoesXml.XmlReadValue(name, "Subject");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + name + ")";
                EchoTree.Root.Children[index].AddNode(treeNode);
            }
            EchoTree.EndUpdate();
        }
        private void SaveToXmlEchoes(string filename, int index)
        {
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Echo File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Echoes");

            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

            int count = CurrentWSG.EchoLists[index].TotalEchoes;
            for (int i = 0; i < count; i++)
            {
                WillowSaveGame.EchoEntry ee = et.Echoes[i];
                writer.WriteStartElement("Echo");
                writer.WriteElementString("Name", ee.Name);
                writer.WriteElementString("DLCValue1", ee.DLCValue1.ToString());
                writer.WriteElementString("DLCValue2", ee.DLCValue2.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }
        private void SaveSelectedToXmlEchoes(string filename, int index)
        {
            TreeNodeAdv[] selected;

            // There are two valid ways a user can select nodes to save to xml.
            // He can choose exactly one category node or he can choose multiple
            // echo nodes.  Figure out which and create an array of the nodes.
            if (EchoTree.SelectedNode.Parent == EchoTree.Root && EchoTree.SelectedNodes.Count == 1)
                selected = EchoTree.Root.Children[index].Children.ToArray();
            else
                selected = EchoTree.SelectedNodes.ToArray();
            
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Echo File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Echoes");

            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

            foreach (TreeNodeAdv nodeAdv in selected)
            {
                string key = nodeAdv.GetKey();
                EchoSearchKey = nodeAdv.GetKey();

                int i = et.Echoes.FindIndex(EchoSearchByName);
                if (i == -1)
                    continue;

                WillowSaveGame.EchoEntry ee = et.Echoes[i];
                writer.WriteStartElement("Echo");
                writer.WriteElementString("Name", ee.Name);
                writer.WriteElementString("DLCValue1", ee.DLCValue1.ToString());
                writer.WriteElementString("DLCValue2", ee.DLCValue2.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }

        private void AddListEchoes_Click(object sender, EventArgs e)
        {
            int index = CurrentWSG.NumberOfEchoLists;

            // Create an empty echo table
            WillowSaveGame.EchoTable et = new WillowSaveGame.EchoTable();
            et.Index = CurrentWSG.NumberOfEchoLists;
            et.Echoes = new List<WillowSaveGame.EchoEntry>();
            et.TotalEchoes = 0;

            // Add the new table to the list
            CurrentWSG.EchoLists.Add(et);
            CurrentWSG.NumberOfEchoLists++;

            EchoTree.BeginUpdate();

            //Add the new table to the tree view
            ColoredTextNode categoryNode = new ColoredTextNode();
            categoryNode.Tag = index.ToString();
            categoryNode.Text = "Playthrough " + (CurrentWSG.EchoLists[index].Index + 1) + " Echo Logs";
            (EchoTree.Model as TreeModel).Nodes.Add(categoryNode);

            EchoTree.EndUpdate();
        }

        private void ClearEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select an echo list first.");
                return;
            }

            DeleteAllEchoes(index);
        }

        private void CloneFromSaveEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a single echo list to import to first.");
                return;
            }
            
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                WillowSaveGame OtherSave = new WillowSaveGame();

                try
                {
                    OtherSave.LoadWSG(tempOpen.FileName());
                }
                catch { MessageBox.Show("Couldn't open the other save file."); return; }

                if (OtherSave.NumberOfEchoLists - 1 < index)
                {
                    MessageBox.Show("The echo list does not exist in the other savegame file.");
                    return;
                }

                // Replace the old entries in the echo table with the new ones
                CurrentWSG.EchoLists[index] = OtherSave.EchoLists[index];

                WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

                EchoTree.BeginUpdate();
                TreeNodeAdv parent = EchoTree.Root.Children[index];

                // Remove the old entries from the tree view
                TreeNodeAdv[] children = parent.Children.ToArray();
                foreach (TreeNodeAdv child in children)
                    child.Remove();

                // Add the new entries to the tree view
                foreach (WillowSaveGame.EchoEntry ee in et.Echoes)
                {
                    string name = ee.Name;

                    ColoredTextNode node = new ColoredTextNode();
                    node.Tag = name;
                    node.Text = EchoesXml.XmlReadValue(name, "Subject");
                    if (node.Text == "")
                        node.Text = "(" + name + ")";
                    parent.AddNode(node);
                }
                EchoTree.EndUpdate();                
            }
        }

        private void DeleteEcho_Click(object sender, EventArgs e)
        {
            // Get out if no node is selected
            int index = GetSelectedEchoList();
            if (index == -1 || EchoTree.SelectedNode.Parent == EchoTree.Root)
                return;

            TreeNodeAdv NextSelection = null;

            TreeNodeAdv[] selected = EchoTree.SelectedNodes.ToArray();
            foreach (TreeNodeAdv nodeAdv in selected)
            {
                // Just remove the node from the selection if it is a
                // category node
                if (nodeAdv.Parent == EchoTree.Root)
                {
                    NextSelection = nodeAdv;
                    nodeAdv.IsSelected = false;
                    continue;
                }

                WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];

                // Remove the echo from the echo list
                et.TotalEchoes--;
                et.Echoes.RemoveAt(nodeAdv.Index);
                
                // Remove the echo from the tree view
                NextSelection = nodeAdv.NextVisibleNode;
                nodeAdv.Remove();
            }

            // Select a new selected node if the selected node was removed
            if (EchoTree.SelectedNode == null)
                EchoTree.SelectedNode = NextSelection;
        }

        private void EchoList_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem1.HideDropDown();

            if (EchoList.SelectedIndex == -1)
                return;

            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select an echo list first.");
                return;
            }

            string name = EchoesXml.stListSectionNames()[EchoList.SelectedIndex];

            // Create a new echo entry and populate it
            WillowSaveGame.EchoEntry ee = new WillowSaveGame.EchoEntry();
            ee.Name = name;
            // TODO: These values shouldn't always be zero, but the data doesn't
            // exist in the data files yet.  When the proper data is in the data
            // file then it needs to be looked up here.
            ee.DLCValue1 = 0;
            ee.DLCValue2 = 0;

            // Add the new echo to the echo list
            WillowSaveGame.EchoTable et = CurrentWSG.EchoLists[index];
            et.Echoes.Add(ee);
            et.TotalEchoes++;

            // Add the new echo to the echo tree view
            ColoredTextNode treeNode = new ColoredTextNode();
            treeNode.Tag = name;
            treeNode.Text = EchoesXml.XmlReadValue(name, "Subject");
            if (treeNode.Text == "")
                treeNode.Text = "(" + treeNode.Tag as string + ")";
            TreeNodeAdv parent = EchoTree.Root.Children[index];
            parent.AddNode(treeNode);

            // Select the newly added node so the user will know it was added
            EchoTree.SelectedNode = parent.Children[parent.Children.Count - 1];
            EchoTree.EnsureVisible(EchoTree.SelectedNode);
        }

        private void ExportEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to export first.");
                return;
            }

            try
            {
                Util.WTSaveFileDialog tempExport = new Util.WTSaveFileDialog("echologs", CurrentWSG.CharacterName + ".PT" + (index + 1) + ".echologs");

                if (tempExport.ShowDialog() == DialogResult.OK)
                {
                    SaveToXmlEchoes(tempExport.FileName(), index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed:\r\n" + ex.ToString());
            }
        }

        private void ExportSelectedEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a single echo list or select the echoes to export first.");
                return;
            }

            Util.WTSaveFileDialog tempSave = new Util.WTSaveFileDialog("echologs", CurrentWSG.CharacterName + ".PT" + (index + 1) + ".echologs");

            try
            {
                if (tempSave.ShowDialog() == DialogResult.OK)
                    SaveSelectedToXmlEchoes(tempSave.FileName(), index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while trying to save locations: " + ex.ToString());
                return;
            }

            MessageBox.Show("Echoes saved to " + tempSave.FileName());
        }

        private void ImportEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to import first.");
                return;
            }

            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("echologs", "Default.echologs");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadEchoes(tempOpen.FileName(), index);
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoEchoes")
                        MessageBox.Show("Couldn't find an echoes section in the file.  Action aborted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while trying to load: " + ex.ToString());
                    return;
                }
            }
        }

        private void EchoDLCValue1_ValueChanged(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1 || EchoTree.SelectedNode.Parent == EchoTree.Root)
                return;

            CurrentWSG.EchoLists[index].Echoes[EchoTree.SelectedNode.Index].DLCValue1 = (int)EchoDLCValue1.Value;
        }

        private void EchoDLCValue2_ValueChanged(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1 || EchoTree.SelectedNode.Parent == EchoTree.Root)
                return;

            CurrentWSG.EchoLists[index].Echoes[EchoTree.SelectedNode.Index].DLCValue2 = (int)EchoDLCValue2.Value;
        }

        private void UIClearEchoPanel()
        {
            Util.SetNumericUpDown(EchoDLCValue1, 0);
            Util.SetNumericUpDown(EchoDLCValue2, 0);
            EchoString.Text = "";
        }

        private void EchoTree_SelectionChanged(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();

            // If a echo node is not selected reset the UI elements and exit
            if (index == -1 || EchoTree.SelectedNode.Parent == EchoTree.Root)
            {
                UIClearEchoPanel();
                return;
            }

            WillowSaveGame.EchoEntry ee = CurrentWSG.EchoLists[index].Echoes[EchoTree.SelectedNode.Index];

            Util.SetNumericUpDown(EchoDLCValue1, ee.DLCValue1);
            Util.SetNumericUpDown(EchoDLCValue2, ee.DLCValue2);
            EchoString.Text = ee.Name;
        }

        private void EchoTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteEcho_Click(this, EventArgs.Empty);
        }

        private void MergeAllFromFileEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a single echo list to import to first.");
                return;
            }

            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("echologs", "Default.echologs");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MergeAllFromXmlEchoes(tempOpen.FileName(), index);
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoEchoes")
                        MessageBox.Show("Couldn't find a location section in the file.  Action aborted.");
                }
                catch { MessageBox.Show("Couldn't load the file.  Action aborted."); }
            }
        }

        private void MergeFromSaveEchoes_Click(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1)
            {
                MessageBox.Show("Select a single echo list to import to first.");
                return;
            }
            
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MergeFromSaveEchoes(tempOpen.FileName(), index);
                }
                catch { MessageBox.Show("Couldn't open the other save file."); return; }
            }
        }
        
        private void RemoveListEchoes_Click(object sender, EventArgs e)
        {
            TreeNodeAdv[] selection = EchoTree.SelectedNodes.ToArray();

            foreach (TreeNodeAdv nodeAdv in selection)
            {
                if (nodeAdv.Parent == EchoTree.Root)
                {
                    CurrentWSG.NumberOfEchoLists--;
                    CurrentWSG.EchoLists.RemoveAt(nodeAdv.Index);
                    EchoTree.Root.Children[nodeAdv.Index].Remove();
                }
            }

            // The indexes will be messed up if a list that is not the last one is
            // removed, so update the tree text, tree indexes, and echo list indices
            int count = CurrentWSG.NumberOfEchoLists;
            for (int index = 0; index < count; index++)
            {
                TreeNodeAdv nodeAdv = EchoTree.Root.Children[index];

                // Adjust the category node's text and tag to reflect its new position
                ColoredTextNode parent = nodeAdv.Data();
                parent.Text = "Playthrough " + (index + 1) + " Echo Logs";
                parent.Tag = index.ToString();

                // Adjust the echo list index to reflect its new position 
                CurrentWSG.EchoLists[index].Index = index;
            }
            EchoTree.EndUpdate();
        }

        private void EchoString_TextChanged(object sender, EventArgs e)
        {
            int index = GetSelectedEchoList();
            if (index == -1 || EchoTree.SelectedNode.Parent == EchoTree.Root)
                return;

            string name = EchoString.Text;
            CurrentWSG.EchoLists[index].Echoes[EchoTree.SelectedNode.Index].Name = name;

            string text = EchoesXml.XmlReadValue(name, "Subject");
            if (text == "")
                text = "(" + name + ")";

            EchoTree.SelectedNode.SetKey(name);
            EchoTree.SelectedNode.SetText(text);
        }
    }
}
