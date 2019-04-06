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
using System.Xml;

namespace WillowTree.Plugins
{
    public partial class ucQuests : UserControl, IPlugin
    {
        WillowSaveGame CurrentWSG;
        XmlFile QuestsXml;

        public bool Clicked = false; //Goes with the quest stuff. Really...ineffective.

        public ucQuests()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            pm.RegisterPlugin(this, events);

            QuestsXml = db.QuestsXml;

            this.Enabled = false;
            DoQuestList();
        }

        public void ReleasePlugin()
        {
            CurrentWSG = null;
            QuestsXml = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            CurrentWSG = e.WTM.SaveData;
            DoQuestTree();

            int index = GetSelectedQuestList();
            if (index != -1)
                ActiveQuest.Text = CurrentWSG.QuestLists[index].CurrentQuest;

            this.Enabled = true;
        }

        public void DoQuestList()
        {
            List<string> sectionNames = QuestsXml.stListSectionNames();

            foreach (string section in sectionNames)
                QuestList.Items.Add(QuestsXml.XmlReadValue(section, "MissionName"));
        }

        public void DoQuestTree()
        {
            QuestTree.BeginUpdate();

            // Make a new quest tree or clear the old one
            if (QuestTree.Model == null)
                QuestTree.Model = new TreeModel();
            else
                QuestTree.Clear();

            TreeModel model = QuestTree.Model as TreeModel;

            for (int listIndex = 0; listIndex < CurrentWSG.NumberOfQuestLists; listIndex++)
            {
                // Create the category node for this playthrough
                // Quest tree category nodes:
                //     Text = human readable quest category ("Playthrough 1 Quests", etc)
                //     Tag = quest list index as string (0 based)
                ColoredTextNode parent = new ColoredTextNode();
                parent.Text = "Playthrough " + (listIndex + 1) + " Quests";
                parent.Tag = listIndex.ToString();
                model.Nodes.Add(parent);

                WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[listIndex];
                // Create all the actual quest nodes for this playthrough
                //     Text = human readable quest name
                //     Tag = internal quest name
                for (int questIndex = 0; questIndex < qt.TotalQuests; questIndex++)
                {
                    string nodeName = qt.Quests[questIndex].Name;

                    ColoredTextNode node = new ColoredTextNode();
                    node.Tag = nodeName;
                    node.Text = QuestsXml.XmlReadValue(nodeName, "MissionName");
                    if (node.Text == "")
                        node.Text = "(" + nodeName + ")";
                    parent.Nodes.Add(node);
                }
            }
            QuestTree.EndUpdate();
        }

        public static string QuestSearchKey;
        public bool QuestSearchByName(WillowSaveGame.QuestEntry qe)
        {
            return qe.Name == QuestSearchKey;
        }

        public void DeleteAllQuests(int index)
        {
            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];
            qt.Quests.Clear();
            qt.TotalQuests = 0;

            QuestTree.BeginUpdate();
            TreeNodeAdv[] children = QuestTree.Root.Children[index].Children.ToArray();
            foreach (TreeNodeAdv child in children)
                child.Remove();

            string startQuest = "Z0_Missions.Missions.M_IntroStateSaver";
            AddQuestByName(startQuest, index);
            qt.CurrentQuest = startQuest;

            QuestTree.EndUpdate();
        }
        private int GetSelectedQuestList()
        {
            int index = -1;

            if (QuestTree.SelectedNode == null)
            {
                // Do nothing, fall through to return -1 for failure.
            }
            else if (QuestTree.SelectedNode.Parent != QuestTree.Root)
                index = Parse.AsInt(QuestTree.SelectedNode.Parent.GetKey(), -1);
            else
            {
                // This is a category node not a quest.  If there is exactly one
                // selected then choose it as the location for import, otherwise 
                // fall through and return -1 for failure.
                if (QuestTree.SelectedNodes.Count == 1)
                    index = Parse.AsInt(QuestTree.SelectedNode.GetKey());
            }
            return index;
        }
        public void LoadQuests(string filename, int index)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("WT/Quests") == null)
                throw new ApplicationException("NoQuests");

            XmlNodeList questnodes = doc.SelectNodes("WT/Quests/Quest");

            int count = questnodes.Count;

            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];
            qt.Quests.Clear();
            qt.TotalQuests = count;

            QuestTree.BeginUpdate();

            TreeNodeAdv parent = QuestTree.Root.Children[index];

            // Remove the old entries from the tree view
            TreeNodeAdv[] children = parent.Children.ToArray();
            foreach (TreeNodeAdv child in children)
                child.Remove();

            for (int nodeIndex = 0; nodeIndex < count; nodeIndex++)
            {
                XmlNode node = questnodes[nodeIndex];
                WillowSaveGame.QuestEntry qe = new WillowSaveGame.QuestEntry();
                qe.Name = node.GetElement("Name", "");
                qe.Progress = node.GetElementAsInt("Progress", 0);
                qe.DLCValue1 = node.GetElementAsInt("DLCValue1", 0);
                qe.DLCValue2 = node.GetElementAsInt("DLCValue2", 0);
                    
                int objectiveCount = node.GetElementAsInt("Objectives", 0);
                qe.NumberOfObjectives = objectiveCount;
                qe.Objectives = new WillowSaveGame.QuestObjective[objectiveCount];

                for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                {
                    qe.Objectives[objectiveIndex].Description = node.GetElement("FolderName" + objectiveIndex, "");
                    qe.Objectives[objectiveIndex].Progress = node.GetElementAsInt("FolderValue" + objectiveIndex, 0);
                }
                qt.Quests.Add(qe);

                // Add the quest to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = qe.Name;
                treeNode.Text = QuestsXml.XmlReadValue(qe.Name, "MissionName");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + treeNode.Tag as string + ")";
                QuestTree.Root.Children[index].AddNode(treeNode);
            }

            // TODO: The current quest is not currently stored in a quest file.
            // It should be stored when the entire list is stored and restored
            // when the list is loaded here.
            qt.CurrentQuest = "";

            QuestTree.EndUpdate();
        }
        public void MergeFromSaveQuests(string filename, int index)
        {
            WillowSaveGame OtherSave = new WillowSaveGame();
            OtherSave.LoadWSG(filename);

            if (OtherSave.NumberOfQuestLists - 1 < index)
                return;

            WillowSaveGame.QuestTable qtOther = OtherSave.QuestLists[index];
            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

            QuestTree.BeginUpdate();
            foreach (WillowSaveGame.QuestEntry qe in OtherSave.QuestLists[index].Quests)
            {
                string name = qe.Name;
                int progress = qe.Progress;

                // Check to see if the quest is already in the list
                QuestSearchKey = name;
                int prevIndex = qt.Quests.FindIndex(QuestSearchByName);
                if (prevIndex != -1)
                {
                    // This quest entry exists in both lists.  If the progress is 
                    // not greater then don't do anything with it.
                    WillowSaveGame.QuestEntry old = qt.Quests[prevIndex];
                    if (progress < old.Progress)
                        continue;

                    if (progress == old.Progress)
                    {
                        // If the progress of the quest is the same, there may be
                        // individual objectives that don't have the same progress
                        // so check and update them.
                        int objectiveCount = qe.NumberOfObjectives;

                        // The number of objectives should be the same with the same
                        // level of progress.  If they aren't then there's something 
                        // wrong so just ignore the new quest and keep the old.
                        if (objectiveCount !=  old.NumberOfObjectives)
                            continue;

                        for (int i = 0; i < objectiveCount; i++)
                        {
                            int objectiveProgress = old.Objectives[i].Progress;
                            if (qe.Objectives[i].Progress < objectiveProgress)
                                qe.Objectives[i].Progress = objectiveProgress;
                        }
                    }

                    // This quest progress is further advanced than the existing one
                    // so replace the existing one in the list.
                    qt.Quests[prevIndex] = qe;

                    // The quest doesn't need to be added to the quest list since we
                    // modified an existing entry.  The tree view doesn't need to be 
                    // changed because the name and text should still be the same. 
                    continue;
                }

                // Add the quest entry to the quest list
                qt.Quests.Add(qe);
                qt.TotalQuests++;

                // Add the quest to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = qe.Name;
                treeNode.Text = QuestsXml.XmlReadValue(qe.Name, "MissionName");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + treeNode.Tag as string + ")";
                QuestTree.Root.Children[index].AddNode(treeNode);
            }
            QuestTree.EndUpdate();

            // In case the operation modified the currently selected quest, refresh
            // the quest group panel by signalizing the selection changed event.
            QuestTree_SelectionChanged(null, null);
        }
        public void MergeAllFromXmlQuests(string filename, int index)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.SelectSingleNode("/WT/Quests") == null)
                throw new ApplicationException("NoQuests");

            XmlNodeList questnodes = doc.SelectNodes("/WT/Quests/Quest");
            if (questnodes == null)
                return;

            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

            QuestTree.BeginUpdate();
            // Copy only the quests that are not duplicates from the XML file
            foreach (XmlNode node in questnodes)
            {
                string name = node.GetElement("Name", "");
                int progress = node.GetElementAsInt("Progress", 0);
                
                // Check to see if the quest is already in the list
                QuestSearchKey = name;
                int prevIndex = qt.Quests.FindIndex(QuestSearchByName);
                if (prevIndex != -1)
                {
                    // This quest entry exists in both lists.  If the progress is 
                    // not greater then don't do anything with it.
                    WillowSaveGame.QuestEntry old = qt.Quests[prevIndex];
                    if (progress <= old.Progress)
                        continue;

                    // This quest progress is further advanced than the existing one
                    // so copy all its values.
                    old.Progress = progress;
                    old.DLCValue1 = node.GetElementAsInt("DLCValue1", 0);
                    old.DLCValue2 = node.GetElementAsInt("DLCValue2", 0);

                    int newObjectiveCount = node.GetElementAsInt("Objectives", 0);
                    old.NumberOfObjectives = newObjectiveCount;
                    old.Objectives = new WillowSaveGame.QuestObjective[newObjectiveCount];

                    for (int objectiveIndex = 0; objectiveIndex < newObjectiveCount; objectiveIndex++)
                    {
                        old.Objectives[objectiveIndex].Description = node.GetElement("FolderName" + objectiveIndex, "");
                        old.Objectives[objectiveIndex].Progress = node.GetElementAsInt("FolderValue" + objectiveIndex, 0);
                    }

                    // The quest doesn't need to be added to the quest list since we
                    // modified an existing entry.  The tree view doesn't need to be 
                    // changed because the name and text should still be the same. 
                    continue;
                }

                // Create a new quest entry from the quest's xml node data
                WillowSaveGame.QuestEntry qe = new WillowSaveGame.QuestEntry();
                qe.Name = name;
                qe.Progress = progress;
                qe.DLCValue1 = node.GetElementAsInt("DLCValue1", 0);
                qe.DLCValue2 = node.GetElementAsInt("DLCValue2", 0);

                int objectiveCount = node.GetElementAsInt("Objectives", 0);
                qe.NumberOfObjectives = objectiveCount;
                qe.Objectives = new WillowSaveGame.QuestObjective[objectiveCount];

                for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                {
                    qe.Objectives[objectiveIndex].Description = node.GetElement("FolderName" + objectiveIndex, "");
                    qe.Objectives[objectiveIndex].Progress = node.GetElementAsInt("FolderValue" + objectiveIndex, 0);
                }

                // Add the quest entry to the quest list
                qt.Quests.Add(qe);
                qt.TotalQuests++;

                // Add the quest to the tree view
                ColoredTextNode treeNode = new ColoredTextNode();
                treeNode.Tag = name;
                treeNode.Text = QuestsXml.XmlReadValue(name, "MissionName");
                if (treeNode.Text == "")
                    treeNode.Text = "(" + name + ")";
                QuestTree.Root.Children[index].AddNode(treeNode);
            }
            QuestTree.EndUpdate();

            // In case the operation modified the currently selected quest, refresh
            // the quest group panel by signalizing the selection changed event.
            QuestTree_SelectionChanged(null, null);
        }
        private void SaveToXmlQuests(string filename, int index)
        {
            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Quest File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Quests");

            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

            int count = CurrentWSG.QuestLists[index].TotalQuests;
            for (int i = 0; i < count; i++)
            {
                WillowSaveGame.QuestEntry qe = qt.Quests[i];
                writer.WriteStartElement("Quest");
                writer.WriteElementString("Name", qe.Name);
                //                writer.WriteString(et.QuestStrings[i]);
                writer.WriteElementString("Progress", qe.Progress.ToString());
                writer.WriteElementString("DLCValue1", qe.DLCValue1.ToString());
                writer.WriteElementString("DLCValue2", qe.DLCValue2.ToString());
                writer.WriteElementString("Objectives", qe.NumberOfObjectives.ToString());

                int objectiveCount = qe.NumberOfObjectives;
                for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                {
                    writer.WriteElementString("FolderName" + objectiveIndex, qe.Objectives[objectiveIndex].Description);
                    writer.WriteElementString("FolderValue" + objectiveIndex, qe.Objectives[objectiveIndex].Progress.ToString());
                }
                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }
        private void SaveSelectedToXmlQuests(string filename, int index)
        {
            TreeNodeAdv[] selected;

            // There are two valid ways a user can select nodes to save to xml.
            // He can choose exactly one category node or he can choose multiple
            // quest nodes.  Figure out which and create an array of the nodes.
            if (QuestTree.SelectedNode.Parent == QuestTree.Root && QuestTree.SelectedNodes.Count == 1)
                selected = QuestTree.Root.Children[index].Children.ToArray();
            else
                selected = QuestTree.SelectedNodes.ToArray();

            XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartDocument();
            writer.WriteComment("WillowTree Quest File");
            writer.WriteComment("Note: the XML tags are case sensitive");
            writer.WriteStartElement("WT");
            writer.WriteStartElement("Quests");

            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

            foreach (TreeNodeAdv nodeAdv in selected)
            {
                string key = nodeAdv.GetKey();
                QuestSearchKey = nodeAdv.GetKey();

                int i = qt.Quests.FindIndex(QuestSearchByName);
                if (i == -1)
                    continue;

                WillowSaveGame.QuestEntry qe = qt.Quests[i];
                writer.WriteStartElement("Quest");
                writer.WriteElementString("Name", qe.Name);
                writer.WriteElementString("Progress", qe.Progress.ToString());
                //                writer.WriteString(et.QuestStrings[i]);
                writer.WriteElementString("DLCValue1", qe.DLCValue1.ToString());
                writer.WriteElementString("DLCValue2", qe.DLCValue2.ToString());
                writer.WriteElementString("Objectives", qe.NumberOfObjectives.ToString());

                int objectiveCount = qe.NumberOfObjectives;
                for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                {
                    writer.WriteElementString("FolderName" + objectiveIndex, qe.Objectives[objectiveIndex].Description);
                    writer.WriteElementString("FolderValue" + objectiveIndex, qe.Objectives[objectiveIndex].Progress.ToString());
                }
                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }

        public bool MultipleIntroStateSaver(int playthroughIndex)
        {
            int TotalFound = 0;
            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[playthroughIndex];
            int questCount = qt.TotalQuests;

            for (int questIndex = 0; questIndex < questCount; questIndex++)
            {
                if (qt.Quests[questIndex].Name == "Z0_Missions.Missions.M_IntroStateSaver")
                    TotalFound = TotalFound + 1;
            }

            if (TotalFound > 1)
                return true;
            else
                return false;
        }

        private void AddQuestByName(string name, int index)
        {
            WillowSaveGame.QuestEntry qe = new WillowSaveGame.QuestEntry();
            qe.Name = name;
            // TODO: These should not always be zero.  They are non-zero for
            // missions that are from the DLCs.  The data files dont
            // contain the values they should be yet, so once that data
            // is added to the data files these should be changed.
            qe.DLCValue1 = 0;
            qe.DLCValue2 = 0;

            List<WillowSaveGame.QuestObjective> objectives = new List<WillowSaveGame.QuestObjective>();

            int objectiveCount;

            for (objectiveCount = 0; ; objectiveCount++)
            {
                WillowSaveGame.QuestObjective objective;
                string desc = QuestsXml.XmlReadValue(qe.Name, "Objectives" + objectiveCount);
                if (desc == "")
                    break;

                objective.Description = desc;
                objective.Progress = 0;
                objectives.Add(objective);
            }

            qe.NumberOfObjectives = objectiveCount;
            qe.Objectives = objectives.ToArray();
            if (objectiveCount > 0)
                qe.Progress = 1;
            else
                qe.Progress = 2;

            // Add the quest entry to the quest list
            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];
            qt.Quests.Add(qe);
            qt.TotalQuests++;

            // Add the quest entry to the tree view
            ColoredTextNode treeNode = new ColoredTextNode();
            treeNode.Tag = name;
            treeNode.Text = QuestsXml.XmlReadValue(name, "MissionName");
            if (treeNode.Text == "")
                treeNode.Text = "(" + name + ")";
            QuestTree.Root.Children[index].AddNode(treeNode);
        }

        private void AddListQuests_Click(object sender, EventArgs e)
        {
            int index = CurrentWSG.NumberOfQuestLists;

            // Create an empty quest table
            WillowSaveGame.QuestTable qt = new WillowSaveGame.QuestTable();
            qt.Index = index;
            qt.TotalQuests = 0;
            qt.Quests = new List<WillowSaveGame.QuestEntry>();

            // Add the new table to the list
            CurrentWSG.QuestLists.Add(qt);
            CurrentWSG.NumberOfQuestLists++;

            QuestTree.BeginUpdate();

            //Add the new table to the tree view
            ColoredTextNode categoryNode = new ColoredTextNode();
            categoryNode.Text = "Playthrough " + (index + 1) + " Quests";
            categoryNode.Tag = index.ToString();
            (QuestTree.Model as TreeModel).Nodes.Add(categoryNode);

            // Add Fresh Off the Bus (the first quest) to the table
            string startQuest = "Z0_Missions.Missions.M_IntroStateSaver";
            AddQuestByName(startQuest, index);
            qt.CurrentQuest = startQuest;
            QuestTree.EndUpdate();
        }

        private void ClearQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a quest list first.");
                return;
            }

            DeleteAllQuests(index);
        }

        private void Objectives_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();

            // Do nothing if the quest list index is invalid or it is a
            // category node not a quest node.
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
                return;

            if (Clicked == true)
                Util.SetNumericUpDown(ObjectiveValue, 
                    CurrentWSG.QuestLists[index].Quests[QuestTree.SelectedNode.Index].Objectives[Objectives.SelectedIndex].Progress);
        }

        private void ObjectiveValue_ValueChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();

            // Do nothing if the quest list index is invalid or it is a
            // category node not a quest node.
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
                return;

            if (Objectives.Items.Count > 0)
            {
                if (Clicked == true)
                    CurrentWSG.QuestLists[index].Quests[QuestTree.SelectedNode.Index].Objectives[Objectives.SelectedIndex].Progress = (int)ObjectiveValue.Value;
            }
        }

        private void QuestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to add to first.");
                return;
            }

            int SelectedItem = QuestList.SelectedIndex;
            NewQuest.HideDropDown();
            try
            {
                if (SelectedItem != -1)
                {
                    WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

                    List<string> sectionNames = QuestsXml.stListSectionNames();
                       
                    WillowSaveGame.QuestEntry qe = new WillowSaveGame.QuestEntry();
                    string name = sectionNames[SelectedItem];
                    qe.Name = name;
                    qe.Progress = 1;
                    // TODO: These should not always be zero.  They are non-zero for
                    // missions that are from the DLCs.  The data files dont
                    // contain the values they should be yet, so once that data
                    // is added to the data files these should be changed.
                    qe.DLCValue1 = 0;
                    qe.DLCValue2 = 0;

                    List<WillowSaveGame.QuestObjective> objectives = new List<WillowSaveGame.QuestObjective>();

                    int objectiveCount;

                    XmlNode questXmlNode = QuestsXml.XmlReadNode(name);
                    System.Diagnostics.Debug.Assert(questXmlNode != null);

                    for (objectiveCount = 0; ; objectiveCount++)
                    {
                        WillowSaveGame.QuestObjective objective;

                        string desc = questXmlNode.GetElement("Objectives" + objectiveCount, "");
                        if (desc == "")
                            break;
                        
                        objective.Description = desc;
                        objective.Progress = 0;
                        objectives.Add(objective);
                    }
                    
                    qe.NumberOfObjectives = objectiveCount;
                    qe.Objectives = objectives.ToArray();

                    qt.Quests.Add(qe);
                    qt.TotalQuests++;

                    ColoredTextNode treeNode = new ColoredTextNode();
                    treeNode.Tag = name;
                    treeNode.Text = questXmlNode.GetElement("MissionName", "");

                    if (treeNode.Text == "")
                        treeNode.Text = "(" + name + ")";

                    TreeNodeAdv parent = QuestTree.Root.Children[index];
                    parent.AddNode(treeNode);
                    QuestTree.SelectedNode = parent.Children[parent.Children.Count - 1];
                }
            }
            catch { }
        }

        private void QuestProgress_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
                return;
            
            try
            {
                WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];
                if (Clicked == true)
                {
                    WillowSaveGame.QuestEntry qe = qt.Quests[QuestTree.SelectedNode.Index];

                    if (qe.Progress == 4 && QuestProgress.SelectedIndex < 3)
                    {
                        // The quest was marked as turned in before and now will not
                        // be complete.  The objective list has to be re-added since it is
                        // removed when the quest is turned in.
                        Objectives.Items.Clear();

                        List<WillowSaveGame.QuestObjective> objectives = new List<WillowSaveGame.QuestObjective>();

                        XmlNode questXmlNode = QuestsXml.XmlReadNode(qe.Name);
 
                        int objectiveCount;
                        for (objectiveCount = 0; ; objectiveCount++)
                        {
                            WillowSaveGame.QuestObjective objective;
                            string desc = questXmlNode.GetElement("Objectives" + objectiveCount, "");
                            if (desc == "")
                                break;

                            objective.Description = desc;
                            objective.Progress = 0;
                            objectives.Add(objective);
                        }

                        qe.NumberOfObjectives = objectiveCount;
                        qe.Objectives = objectives.ToArray();
                        qe.Progress = QuestProgress.SelectedIndex;

                        if (objectiveCount > 0)
                        {
                            for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                                Objectives.Items.Add(qe.Objectives[objectiveIndex].Description);
                        }

                        // Update the UI elements
                        Util.SetNumericUpDown(NumberOfObjectives, objectiveCount);
                        Util.SetNumericUpDown(ObjectiveValue, 0);
                    }
                    else if (qe.Progress != 4 && QuestProgress.SelectedIndex == 3)
                    {
                        // The quest was not marked as turned in but now it will be.  Clear the
                        // objective list since turned in quests no longer should have one.
                        Objectives.Items.Clear();
                        qe.Objectives = new WillowSaveGame.QuestObjective[0];
                        qe.Progress = 4;
                        qe.NumberOfObjectives = 0;
                    }
                    else
                        qe.Progress = QuestProgress.SelectedIndex;
                }
            }
            catch { }
        }

        private void QuestProgress_Click(object sender, EventArgs e)
        {
            Clicked = true;
        }

        private void UIClearQuestPanel()
        {
            QuestString.Text = "";
            Objectives.Items.Clear();
            Util.SetNumericUpDown(NumberOfObjectives, 0);
            Util.SetNumericUpDown(ObjectiveValue, 0);
            QuestProgress.SelectedIndex = 0;
            QuestDescription.Text = "";
            QuestSummary.Text = "";
            SelectedQuestGroup.Text = "No Quest Selected";
            Util.SetNumericUpDown(QuestDLCValue1, 0);
            Util.SetNumericUpDown(QuestDLCValue2, 0);
        }

        private void UIClearPlaythrough()
        {
            ActivePT1QuestGroup.Text = "No Playthrough Selected";
            ActiveQuest.Text = "";
        }

        private void UISetPlaythrough(int index)
        {
            ActivePT1QuestGroup.Text = "Active Playthrough " + (index + 1) + " Quest";
            ActiveQuest.Text = CurrentWSG.QuestLists[index].CurrentQuest;
        }

        private void UpdateActiveQuestList(int index)
        {
            if (index == -1)
            {
                ActivePT1QuestGroup.Text = "No Playthrough Selected";
                ActiveQuest.Text = "";
            }
            else
            {
                ActivePT1QuestGroup.Text = "Active Playthrough " + (index + 1) + " Quest";
                ActiveQuest.Text = CurrentWSG.QuestLists[index].CurrentQuest;
            }
        }

        private void QuestTree_SelectionChanged(object sender, EventArgs e)
        {
            Clicked = false;

            int index = GetSelectedQuestList();
            UpdateActiveQuestList(index);

            // If a quest node is not selected reset the UI elements and exit
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
            {
                UIClearQuestPanel();
                return;
            }

            try
            {
                WillowSaveGame.QuestEntry qe = CurrentWSG.QuestLists[index].Quests[QuestTree.SelectedNode.Index];
 
                SelectedQuestGroup.Text = QuestTree.SelectedNode.GetText();
                string key = QuestTree.SelectedNode.GetKey();
                QuestString.Text = key;

                if (qe.Progress > 2)
                    QuestProgress.SelectedIndex = 3;
                else 
                    QuestProgress.SelectedIndex = qe.Progress;

                int objectiveCount = qe.NumberOfObjectives;
                Util.SetNumericUpDown(NumberOfObjectives, objectiveCount);

                XmlNode questData = QuestsXml.XmlReadNode(key);
                Objectives.Items.Clear();
                for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                    Objectives.Items.Add(questData.GetElement("Objectives" + objectiveIndex, ""));
                Util.SetNumericUpDown(ObjectiveValue, 0);
                QuestSummary.Text = questData.GetElement("MissionSummary", "");
                QuestDescription.Text = questData.GetElement("MissionDescription", "");
                Util.SetNumericUpDown(QuestDLCValue1, qe.DLCValue1);
                Util.SetNumericUpDown(QuestDLCValue2, qe.DLCValue2);
            }
            catch 
            { 
                // Blank out all the user elements if there is any kind of exception
                // while trying to set them.
                UIClearQuestPanel();
            }
        }

        private void DeleteQuestEntry(int listIndex, int entryIndex)
        {
            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[listIndex];
            qt.TotalQuests--;
            qt.Quests.RemoveAt(entryIndex);
        }

        private void DeleteQuest_Click(object sender, EventArgs e)
        {
            TreeNodeAdv NextSelection = null;

            int index = GetSelectedQuestList();

            // Get out if it is a category node or doesn't have a valid quest list index.
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
            {
                MessageBox.Show("Select one or more quests to delete first.");
                return;
            }

            // If there's only one node selected, it is the first quest,
            // and there's no other copy of the first quest in that quest
            // list then give the user a message letting him know he can't
            // remove it.  If he selects it in a group of quests then it will
            // just be silently ignored in the removal loop.
            if (QuestTree.SelectedNodes.Count == 1)
            {
                if (QuestTree.SelectedNode.GetText() == "Fresh Off The Bus" && MultipleIntroStateSaver(index) == false)
                {
                    MessageBox.Show("You must have the default quest.");
                    return;
                }
            }

            TreeNodeAdv[] selected = QuestTree.SelectedNodes.ToArray();

            foreach (TreeNodeAdv nodeAdv in selected)
            {
                if (nodeAdv.GetText() == "Fresh Off The Bus" && MultipleIntroStateSaver(index) != true)
                {
                    nodeAdv.IsSelected = false;
                    NextSelection = nodeAdv;
                    continue;
                }

                NextSelection = nodeAdv.NextVisibleNode;
                DeleteQuestEntry(index, nodeAdv.Index);
                nodeAdv.Remove();
            }
            if (NextSelection != null)
                QuestTree.SelectedNode = NextSelection;
        }

        private void ExportToFileQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to export first.");
                return;
            }            

            try
            {
                Util.WTSaveFileDialog tempExport = new Util.WTSaveFileDialog("quests", CurrentWSG.CharacterName + ".PT" + (index + 1) + ".quests");

                if (tempExport.ShowDialog() == DialogResult.OK)
                {
                    SaveToXmlQuests(tempExport.FileName(), index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed:\r\n" + ex.ToString());
            }
        }

        private void ExportSelectedQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a single quest list or select the quests to export first.");
                return;
            }

            Util.WTSaveFileDialog tempSave = new Util.WTSaveFileDialog("quests", CurrentWSG.CharacterName + ".PT" + (index + 1) + ".quests");

            try
            {
                if (tempSave.ShowDialog() == DialogResult.OK)
                {
                    SaveSelectedToXmlQuests(tempSave.FileName(), index);
                    MessageBox.Show("Quests saved to " + tempSave.FileName());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while trying to save locations: " + ex.ToString());
                return;
            }
        }

        private void ImportFromFileQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to import to first.");
                return;
            }

            Util.WTOpenFileDialog tempImport = new Util.WTOpenFileDialog("quests", CurrentWSG.CharacterName + ".PT" + (index + 1) + ".quests");

            if (tempImport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadQuests(tempImport.FileName(), index);
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoQuests")
                        MessageBox.Show("Couldn't find a quests section in the file.  Action aborted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while trying to load: " + ex.ToString());
                    return;
                }
            }
        }

        private void ImportFromSaveQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a playthrough to import to first.");
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
                catch { return; }

                if (OtherSave.NumberOfQuestLists - 1 < index)
                {
                    MessageBox.Show("This quest list does not exist in the other savegame file.");
                    return;
                }

                // Note that when you set lists equal to one another like this it doesn't copy
                // the elements, only the pointer to the list.  This is only safe here because
                // OtherSave will be disposed of right away and not modify the values.  If OtherSave
                // was being used actively then a new copy of all the elements in the quest list
                // would have to be made or else changes to one would affect the quest
                // list of the other.

                // Replace the old entries in the quest table with the new ones
                CurrentWSG.QuestLists[index] = OtherSave.QuestLists[index];

                WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];

                QuestTree.BeginUpdate();
                TreeNodeAdv parent = QuestTree.Root.Children[index];

                // Remove the old entries from the tree view
                TreeNodeAdv[] children = parent.Children.ToArray();
                foreach (TreeNodeAdv child in children)
                    child.Remove();

                // Add the new entries to the tree view
                foreach (WillowSaveGame.QuestEntry qe in qt.Quests)
                {
                    string nodeName = qe.Name;

                    ColoredTextNode node = new ColoredTextNode();
                    node.Tag = nodeName;
                    node.Text = QuestsXml.XmlReadValue(nodeName, "MissionName");
                    if (node.Text == "")
                        node.Text = "(" + node.Tag as string + ")";
                    parent.AddNode(node);
                }
                QuestTree.EndUpdate();
            }
        }

        private void MergeAllFromFileQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a single quest list to import to first.");
                return;
            }
            
            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("quests", "Default.quests");
            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MergeAllFromXmlQuests(tempOpen.FileName(), index);
                }
                catch (ApplicationException ex)
                {
                    if (ex.Message == "NoQuests")
                        MessageBox.Show("Couldn't find a quests section in the file.  Action aborted.");
                }
                catch { MessageBox.Show("Couldn't load the file.  Action aborted."); }
            }
        }

        private void MergeFromSaveQuests_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1)
            {
                MessageBox.Show("Select a single quest list to import to first.");
                return;
            }

            Util.WTOpenFileDialog tempOpen = new Util.WTOpenFileDialog("sav", "");

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                WillowSaveGame OtherSave = new WillowSaveGame();

                try
                {
                    MergeFromSaveQuests(tempOpen.FileName(), index);
                }
                catch { MessageBox.Show("Couldn't open the other save file."); return; }
            }
        }

        private void QuestTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteQuest_Click(this, EventArgs.Empty);
        }

        private void RemoveListQuests_Click(object sender, EventArgs e)
        {
            TreeNodeAdv[] selection = QuestTree.SelectedNodes.ToArray();

            QuestTree.BeginUpdate();
            foreach (TreeNodeAdv nodeAdv in selection)
            {
                if (nodeAdv.Parent == QuestTree.Root)
                {
                    CurrentWSG.NumberOfQuestLists--;
                    CurrentWSG.QuestLists.RemoveAt(nodeAdv.Index);
                    QuestTree.Root.Children[nodeAdv.Index].Remove();
                }
            }

            // The indexes will be messed up if a list that is not the last one is
            // removed, so update the tree text, tree indexes, and quest list indices
            int count = CurrentWSG.NumberOfQuestLists;
            for (int index = 0; index < count; index++)
            {
                TreeNodeAdv nodeAdv = QuestTree.Root.Children[index];

                // Adjust the category node's text and tag to reflect its new position
                ColoredTextNode parent = nodeAdv.Data();
                parent.Text = "Playthrough " + (index + 1) + " Quests";
                parent.Tag = index.ToString();

                // Adjust the quest list index to reflect its new position 
                CurrentWSG.QuestLists[index].Index = index;
            }
            QuestTree.EndUpdate();
        }

        private void SetActiveQuest_Click(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1 || QuestTree.SelectedNodes.Count != 1 || 
                QuestTree.SelectedNode.Parent == QuestTree.Root)
            {
                MessageBox.Show("Select a single quest from the quest list first.");
                return;
            }

            WillowSaveGame.QuestTable qt = CurrentWSG.QuestLists[index];
            string currentQuest = QuestTree.SelectedNode.GetKey();
            qt.CurrentQuest = currentQuest;

            ActiveQuest.Text = currentQuest;
        }

        private void ActiveQuest_TextChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index != -1)
                CurrentWSG.QuestLists[index].CurrentQuest = ActiveQuest.Text;
        }

        private void QuestDLCValue1_ValueChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
                return;

            CurrentWSG.QuestLists[index].Quests[QuestTree.SelectedNode.Index].DLCValue1 = (int)QuestDLCValue1.Value;
        }

        private void QuestDLCValue2_ValueChanged(object sender, EventArgs e)
        {
            int index = GetSelectedQuestList();
            if (index == -1 || QuestTree.SelectedNode.Parent == QuestTree.Root)
                return;

            CurrentWSG.QuestLists[index].Quests[QuestTree.SelectedNode.Index].DLCValue2 = (int)QuestDLCValue2.Value;
        }
    }
}
