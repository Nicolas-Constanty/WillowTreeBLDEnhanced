/*  This file is part of WillowTree#
 * 
 *  Copyright (C) 2011 Matthew Carter <matt911@users.sf.net>
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
using System.ComponentModel;
//using System.Drawing;
//using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using WillowTree.CustomControls;

namespace WillowTree.Plugins
{
    // This plugin is a custom UserControl that supports the IPlugin interface.
    // I created a new UserControl with Project->Add New Item->User Control then
    // opened the source code view and added ", IPlugin" after the base class
    // (UserControl) in the class definition to specify that it must support the
    // IPlugin interface.  Supporting that interface requires two methods to be
    // implemented in the control: InitializePlugin and ReleasePlugin.

    // I am not going to use the designer to create and add this to a tab page
    // so I hide it from the toolbox with [DesignTimeVisible(false)].  This
    // plugin object is instanced and placed on a tab page by CreatePluginAsTab 
    // in WillowTreeMain.
    [DesignTimeVisible(false)]
    public partial class SamplePlugin : UserControl, IPlugin
    {
        /// <summary>
        /// The plugin manager or application will call this to let the
        /// plugin know that the application is ready for the plugin to
        /// request registration for any plugin events it requires.
        /// </summary>
        // Required method to implement the IPlugin interface
        public void InitializePlugin(PluginComponentManager pm)
        {
            // Register for plugin events.
            // Leave any that the plugin doesn't need null
            PluginEvents events = new PluginEvents();
            events.GameLoaded = GameLoaded;
            events.GameLoading = GameLoading;
            events.GameSaving = GameSaving;
            events.GameSaved = GameSaved;
            events.PluginSelected = PluginSelected;
            events.PluginUnselected = PluginUnselected;
            pm.RegisterPlugin(this, events);

            // If the plugin needs any other initialization
            // it should be done here.
        }

        // Required method to implement the IPlugin interface
        public void ReleasePlugin()
        {
            // Release any resources acquired by the plugin here.
        }

        public SamplePlugin()
        {
            InitializeComponent();
        }

        public void GameLoading(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("Loading \"" + e.Filename + "\"\r\n");
        }

        public void GameLoaded(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("Character " + e.WTM.SaveData.CharacterName + " loaded\r\n");
        }

        public void GameSaving(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("Saving to \"" + e.Filename + "\"\r\n");
        }

        public void GameSaved(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("File saved.\r\n");
        }

        public void PluginSelected(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("Plugin Selected\r\n");
        }

        public void PluginUnselected(object sender, PluginEventArgs e)
        {
            wtTextBox1.AppendText("Plugin Unselected\r\n");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wtTextBox1.Clear();
        }

        public bool TypeIsOfBaseType(FieldInfo type, Type basetype)
        {
            return false;
        }            
        public List<string> GetThemeInfo()
        {
            List<string> ThemeNames = new List<string>();

            Type ThemeType = WillowTree.Services.AppThemes.GetType();
            string TypeName = ThemeType.FullName;
            FieldInfo[] finfos = ThemeType.GetFields();
            foreach (FieldInfo finfo in finfos)
            {
                ThemeNames.Add(TypeName + "." + finfo.Name);
            }
            return ThemeNames;
        }

        private void wtButton1_Click(object sender, EventArgs e)
        {
            Type MyType = WillowTree.Services.AppThemes.GetType();
            MemberInfo[] Mymemberinfoarray = MyType.GetMembers();
            // Gets and displays the DeclaringType method.
            wtTextBox1.AppendText("There are " +  Mymemberinfoarray.Length + " members in " + MyType.FullName + ".\r\n");
            if (MyType.IsPublic)
            {
                wtTextBox1.AppendText(MyType.FullName + " is public.");
            }
            foreach (MemberInfo info in Mymemberinfoarray)
            {
                wtTextBox1.AppendText(info.Name + ":" + info.MemberType.ToString() + "\r\n");
            }
        }
    }
}
