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
//using System.Drawing;
//using System.Linq;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace WillowTree.Plugins
{
    public partial class ucAmmo : UserControl, IPlugin
    {
        WillowSaveGame CurrentWSG;

        public ucAmmo()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            pm.RegisterPlugin(this, events);

            this.Enabled = false;
        }

        public void ReleasePlugin() 
        {
            CurrentWSG = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            CurrentWSG = e.WTM.SaveData;
            DoAmmoTree();
            this.Enabled = true;
        }

        public void DoAmmoTree()
        {
            AmmoTree.BeginUpdate();
            TreeModel model = new TreeModel();
            AmmoTree.Model = model;

            for (int build = 0; build < CurrentWSG.NumberOfPools; build++)
            {
                string ammoName = GetAmmoName(CurrentWSG.ResourcePools[build]);
                ColoredTextNode node = new ColoredTextNode(ammoName);
                model.Nodes.Add(node);
            }
            AmmoTree.EndUpdate();
        }

        public string GetAmmoName(string d_resources)
        {
            if (d_resources == "d_resources.AmmoResources.Ammo_Sniper_Rifle")
                return "Sniper Rifle";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Repeater_Pistol")
                return "Repeater Pistol";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Grenade_Protean")
                return "Protean Grenades";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Patrol_SMG")
                return "Patrol SMG";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Combat_Shotgun")
                return "Combat Shotgun";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Combat_Rifle")
                return "Combat Rifle";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Revolver_Pistol")
                return "Revolver Pistol";
            else if (d_resources == "d_resources.AmmoResources.Ammo_Rocket_Launcher")
                return "Rocket Launcher";
            else
                return d_resources;
        }

        private void AmmoTree_SelectionChanged(object sender, EventArgs e)
        {
            TreeNodeAdv selectedNode = AmmoTree.SelectedNode;
            if (selectedNode != null)
            {
                Util.SetNumericUpDown(AmmoPoolRemaining, (decimal)CurrentWSG.RemainingPools[selectedNode.Index]);
                Util.SetNumericUpDown(AmmoSDULevel, CurrentWSG.PoolLevels[selectedNode.Index]);
            }              
        }

        private void AmmoPoolRemaining_ValueChanged(object sender, EventArgs e)
        {
            TreeNodeAdv selectedNode = AmmoTree.SelectedNode;
            if (selectedNode != null)
                CurrentWSG.RemainingPools[selectedNode.Index] = (float)AmmoPoolRemaining.Value;
        }

        private void AmmoSDULevel_ValueChanged(object sender, EventArgs e)
        {
            TreeNodeAdv selectedNode = AmmoTree.SelectedNode;
            if (selectedNode != null)
                CurrentWSG.PoolLevels[selectedNode.Index] = (int)AmmoSDULevel.Value;
        }

        private void DeleteAmmo_Click(object sender, EventArgs e)
        {
            if (AmmoTree.SelectedNode != null)
            {
                CurrentWSG.NumberOfPools = CurrentWSG.NumberOfPools - 1;
                Util.ResizeArraySmaller(ref CurrentWSG.AmmoPools, CurrentWSG.NumberOfPools);
                Util.ResizeArraySmaller(ref CurrentWSG.ResourcePools, CurrentWSG.NumberOfPools);
                Util.ResizeArraySmaller(ref CurrentWSG.RemainingPools, CurrentWSG.NumberOfPools);
                Util.ResizeArraySmaller(ref CurrentWSG.PoolLevels, CurrentWSG.NumberOfPools);
                DoAmmoTree();
            }
        }

        private void NewAmmo_Click(object sender, EventArgs e)
        {
            try
            {
                string New_d_resources = Microsoft.VisualBasic.Interaction.InputBox("Enter the 'd_resources' for the new Ammo Pool", "New Ammo Pool", "", 10, 10);
                string New_d_resourcepools = Microsoft.VisualBasic.Interaction.InputBox("Enter the 'd_resourcepools' for the new Ammo Pool", "New Ammo Pool", "", 10, 10);
                if (New_d_resourcepools != "" && New_d_resources != "")
                {
                    CurrentWSG.NumberOfPools = CurrentWSG.NumberOfPools + 1;
                    Util.ResizeArrayLarger(ref CurrentWSG.AmmoPools, CurrentWSG.NumberOfPools);
                    Util.ResizeArrayLarger(ref CurrentWSG.ResourcePools, CurrentWSG.NumberOfPools);
                    Util.ResizeArrayLarger(ref CurrentWSG.RemainingPools, CurrentWSG.NumberOfPools);
                    Util.ResizeArrayLarger(ref CurrentWSG.PoolLevels, CurrentWSG.NumberOfPools);
                    CurrentWSG.AmmoPools[CurrentWSG.NumberOfPools - 1] = New_d_resourcepools;
                    CurrentWSG.ResourcePools[CurrentWSG.NumberOfPools - 1] = New_d_resources;
                    DoAmmoTree();
                }
            }
            catch { MessageBox.Show("Couldn't add new ammo pool."); }
        }

        private void AmmoTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteAmmo_Click(this, EventArgs.Empty);
        }
    }
}
