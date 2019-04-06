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
using WillowTree.CustomControls;

namespace WillowTree.Plugins
{
    public partial class ucSkills : UserControl, IPlugin
    {
        PluginComponentManager pluginManager;
        WillowSaveGame CurrentWSG;
        XmlFile SkillsAllXml;
        XmlFile SkillsCommonXml;
        XmlFile SkillsBerserkerXml;
        XmlFile SkillsSoldierXml;
        XmlFile SkillsSirenXml;
        XmlFile SkillsHunterXml;
        string lastClass;

        public ucSkills()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            pm.RegisterPlugin(this, events);

            pluginManager = pm;
            SkillsAllXml = db.SkillsAllXml;
            SkillsCommonXml = db.SkillsCommonXml;
            SkillsBerserkerXml = db.SkillsBerserkerXml;
            SkillsSoldierXml = db.SkillsSoldierXml;
            SkillsSirenXml = db.SkillsSirenXml;
            SkillsHunterXml = db.SkillsHunterXml;

            this.Enabled = false;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            CurrentWSG = e.WTM.SaveData;
            DoSkillList();
            this.Enabled = true;
            DoSkillTree();
        }

        public void ReleasePlugin()
        {
            pluginManager = null;
            CurrentWSG = null;
            SkillsAllXml = null;
            SkillsCommonXml = null;
            SkillsBerserkerXml = null;
            SkillsSoldierXml = null;
            SkillsSirenXml = null;
            SkillsHunterXml = null;
        }

        private XmlFile GetClassSkillXml(string classString)
        {
            XmlFile xml;
            switch (classString)
            {
                case "gd_Roland.Character.CharacterClass_Roland":
                    xml = SkillsSoldierXml;
                    break;
                case "gd_lilith.Character.CharacterClass_Lilith":
                    xml = SkillsSirenXml;
                    break;
                case "gd_mordecai.Character.CharacterClass_Mordecai":
                    xml = SkillsHunterXml;
                    break;
                case "gd_Brick.Character.CharacterClass_Brick":
                    xml = SkillsBerserkerXml;
                    break;
                default:
                    xml = SkillsCommonXml;
                    break;
            }
            return xml;
        }

        public void DoSkillList()
        {
            XmlFile xml;

            SkillList.Items.Clear();

            lastClass = CurrentWSG.Class;
            xml = GetClassSkillXml(lastClass);
            foreach (string section in xml.stListSectionNames())
                SkillList.Items.Add((string)xml.XmlReadValue(section, "SkillName"));

            xml = SkillsCommonXml;
            foreach (string section in xml.stListSectionNames())
                SkillList.Items.Add((string)xml.XmlReadValue(section, "SkillName"));
        }

        public void DoSkillTree()
        {
            // Skill tree
            //     Key = name of the skill as stored in CurrentWSG.SkillNames
            //     Text = human readable display name of the skill
            SkillTree.BeginUpdate();
            TreeModel model = new TreeModel();
            SkillTree.Model = model;

            Util.SetNumericUpDown(SkillLevel, 0);
            Util.SetNumericUpDown(SkillExp, 0);
            SkillActive.SelectedItem = "No";
            for (int build = 0; build < CurrentWSG.NumberOfSkills; build++)
            {
                ColoredTextNode node = new ColoredTextNode();

                string key = CurrentWSG.SkillNames[build];
                node.Key = key;

                string name = SkillsAllXml.XmlReadValue(key, "SkillName");
                if (name != "")
                    node.Text = name;
                else
                    node.Text = CurrentWSG.SkillNames[build];

                model.Nodes.Add(node);
            }
            SkillTree.EndUpdate();
        }

        private void SkillLevel_ValueChanged(object sender, EventArgs e)
        {
            if (SkillTree.SelectedNode == null)
                return;

            int index = CurrentWSG.SkillNames.IndexOf((SkillTree.SelectedNode.Tag as ColoredTextNode).Key);
            if (index != -1)
                CurrentWSG.LevelOfSkills[index] = (int)SkillLevel.Value;
        }

        private void SkillExp_ValueChanged(object sender, EventArgs e)
        {
            if (SkillTree.SelectedNode == null)
                return;

            int index = CurrentWSG.SkillNames.IndexOf((SkillTree.SelectedNode.Tag as ColoredTextNode).Key);
            if (index != -1)
                CurrentWSG.ExpOfSkills[index] = (int)SkillExp.Value;
        }

        private void SkillActive_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SkillTree.SelectedNode == null)
                return;

            int index = CurrentWSG.SkillNames.IndexOf((SkillTree.SelectedNode.Tag as ColoredTextNode).Key);
            if (index != -1)
            {
                if (SkillActive.SelectedIndex == 1)
                    CurrentWSG.InUse[index] = 1;
                else
                    CurrentWSG.InUse[index] = -1;
            }
        }

        private void DeleteSkill_Click(object sender, EventArgs e)
        {
            TreeNodeAdv[] selectedNodes = SkillTree.SelectedNodes.ToArray();

            int count = selectedNodes.Length;
            for (int i = 0; i < count; i++)
            {
                string skillName = selectedNodes[i].GetKey();

                // Remove the skill from the WillowSaveGame data
                int Selected = CurrentWSG.SkillNames.IndexOf(skillName);
                if (Selected != -1)
                {
                    for (int Position = Selected; Position < CurrentWSG.NumberOfSkills - 1; Position++)
                    {
                        CurrentWSG.SkillNames[Position] = CurrentWSG.SkillNames[Position + 1];
                        CurrentWSG.InUse[Position] = CurrentWSG.InUse[Position + 1];
                        CurrentWSG.ExpOfSkills[Position] = CurrentWSG.ExpOfSkills[Position + 1];
                        CurrentWSG.LevelOfSkills[Position] = CurrentWSG.LevelOfSkills[Position + 1];

                    }
                    Util.ResizeArraySmaller(ref CurrentWSG.SkillNames, CurrentWSG.NumberOfSkills);
                    Util.ResizeArraySmaller(ref CurrentWSG.InUse, CurrentWSG.NumberOfSkills);
                    Util.ResizeArraySmaller(ref CurrentWSG.ExpOfSkills, CurrentWSG.NumberOfSkills);
                    Util.ResizeArraySmaller(ref CurrentWSG.LevelOfSkills, CurrentWSG.NumberOfSkills);

                    CurrentWSG.NumberOfSkills--;
                }

                // Remove the skill from the skill tree
                if (selectedNodes[i] == SkillTree.SelectedNode)
                    SkillTree.SelectedNode = SkillTree.SelectedNode.NextVisibleNode;
                selectedNodes[i].Remove();
            }

            if (SkillTree.SelectedNode == null)
            {
                if (SkillTree.Root.Children.Count > 0)
                    SkillTree.SelectedNode = SkillTree.Root.Children.Last();
            }
        }

        private void ExportToFileSkills_Click(object sender, EventArgs e)
        {
            Util.WTSaveFileDialog tempExport = new Util.WTSaveFileDialog("skills", CurrentWSG.CharacterName + ".skills");

            if (tempExport.ShowDialog() == DialogResult.OK)
            {
                // Create empty xml file
                XmlTextWriter writer = new XmlTextWriter(tempExport.FileName(), new System.Text.ASCIIEncoding());
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement("INI");
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

                XmlFile Skills = new XmlFile(tempExport.FileName());
                List<string> subsectionnames = new List<string>();
                List<string> subsectionvalues = new List<string>();

                for (int Progress = 0; Progress < CurrentWSG.NumberOfSkills; Progress++)
                {
                    subsectionnames.Clear();
                    subsectionvalues.Clear();

                    subsectionnames.Add("Level");
                    subsectionnames.Add("Experience");
                    subsectionnames.Add("InUse");
                    subsectionvalues.Add(CurrentWSG.LevelOfSkills[Progress].ToString());
                    subsectionvalues.Add(CurrentWSG.ExpOfSkills[Progress].ToString());
                    subsectionvalues.Add(CurrentWSG.InUse[Progress].ToString());

                    Skills.AddSection(CurrentWSG.SkillNames[Progress], subsectionnames, subsectionvalues);
                }
            }
        }

        private void ImportFromFileSkills_Click(object sender, EventArgs e)
        {
            Util.WTOpenFileDialog tempImport = new Util.WTOpenFileDialog("skills", CurrentWSG.CharacterName + ".skills");

            if (tempImport.ShowDialog() == DialogResult.OK)
            {
                XmlFile ImportSkills = new XmlFile(tempImport.FileName());
                List<string> sectionNames = ImportSkills.stListSectionNames();

                int sectionCount = sectionNames.Count;

                string[] TempSkillNames = new string[sectionCount];
                int[] TempSkillLevels = new int[sectionCount];
                int[] TempSkillExp = new int[sectionCount];
                int[] TempSkillInUse = new int[sectionCount];
                for (int Progress = 0; Progress < sectionCount; Progress++)
                {
                    string name = sectionNames[Progress];

                    TempSkillNames[Progress] = name;
                    TempSkillLevels[Progress] = Parse.AsInt(ImportSkills.XmlReadValue(name, "Level"));
                    TempSkillExp[Progress] = Parse.AsInt(ImportSkills.XmlReadValue(name, "Experience"));
                    TempSkillInUse[Progress] = Parse.AsInt(ImportSkills.XmlReadValue(name, "InUse"));
                }
                CurrentWSG.SkillNames = TempSkillNames;
                CurrentWSG.LevelOfSkills = TempSkillLevels;
                CurrentWSG.ExpOfSkills = TempSkillExp;
                CurrentWSG.InUse = TempSkillInUse;
                CurrentWSG.NumberOfSkills = sectionCount;
                DoSkillTree();
            }
        }

        private void SkillList_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem4.HideDropDown();
            try
            {
                // Make sure that if the class was changed, the new skill list is loaded
                if (lastClass != CurrentWSG.Class)
                    DoSkillList();

                // Look up the name of the selected skill from its display text
                string skillName = SkillsAllXml.XmlReadAssociatedValue("Name", "SkillName", (string)SkillList.SelectedItem);
                if (skillName == "")
                    skillName = (string)SkillList.SelectedItem;

                // If the skill is already in the tree, just select it and do nothing
                TreeNodeAdv skillNode = SkillTree.FindFirstNodeByTag(skillName, false);
                if (skillNode != null)
                {
                    SkillTree.SelectedNode = skillNode;
                    return;
                }

                // Add enough room for the new skill in each of the skill arrays
                CurrentWSG.NumberOfSkills = CurrentWSG.NumberOfSkills + 1;
                Util.ResizeArrayLarger(ref CurrentWSG.SkillNames, CurrentWSG.NumberOfSkills);
                Util.ResizeArrayLarger(ref CurrentWSG.LevelOfSkills, CurrentWSG.NumberOfSkills);
                Util.ResizeArrayLarger(ref CurrentWSG.ExpOfSkills, CurrentWSG.NumberOfSkills);
                Util.ResizeArrayLarger(ref CurrentWSG.InUse, CurrentWSG.NumberOfSkills);

                // Set the data for the new skill.
                int index = CurrentWSG.NumberOfSkills - 1;
                CurrentWSG.InUse[index] = -1;
                CurrentWSG.LevelOfSkills[index] = 0;
                CurrentWSG.ExpOfSkills[index] = 01;
                CurrentWSG.SkillNames[CurrentWSG.NumberOfSkills - 1] = skillName;
                DoSkillTree();
            }
            catch { MessageBox.Show("Could not add new Skill."); }
        }

        private void SkillTree_SelectionChanged(object sender, EventArgs e)
        {
            if (SkillTree.SelectedNode == null)
            {
                SkillName.Text = "";
                Util.SetNumericUpDown(SkillLevel, 0);
                Util.SetNumericUpDown(SkillExp, 0);
                return;
            }

            int index = CurrentWSG.SkillNames.IndexOf((SkillTree.SelectedNode.Tag as ColoredTextNode).Key);
            if (index == -1)
            {
                SkillName.Text = "";
                Util.SetNumericUpDown(SkillLevel, 0);
                Util.SetNumericUpDown(SkillExp, 0);
            }
            else
            {
                SkillName.Text = CurrentWSG.SkillNames[index];
                Util.SetNumericUpDown(SkillLevel, CurrentWSG.LevelOfSkills[index]);
                Util.SetNumericUpDown(SkillExp, CurrentWSG.ExpOfSkills[index]);
                if (CurrentWSG.InUse[index] == -1)
                    SkillActive.SelectedItem = "No";
                else
                    SkillActive.SelectedItem = "Yes";
            }
        }

        private void SkillTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSkill_Click(this, EventArgs.Empty);
        }
    }
}
