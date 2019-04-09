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
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using WillowTree.Inventory;

namespace WillowTree.Plugins
{
    public partial class ucDebug : UserControl, IPlugin
    {
        WillowSaveGame CurrentWSG;

        public ucDebug()
        {
            InitializeComponent();
        }

        public void InitializePlugin(PluginComponentManager pm)
        {
            PluginEvents events = new PluginEvents();
            events.GameLoaded = OnGameLoaded;
            pm.RegisterPlugin(this, events);
        }

        public void ReleasePlugin()
        {
            CurrentWSG = null;
        }

        public void OnGameLoaded(object sender, PluginEventArgs e)
        {
            this.CurrentWSG = e.WTM.SaveData;
        }

        private void Breakpoint_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            DebugText1.Text = DoProfiling();
        }

//This is some left over code that I used to profile some BuildName methods.
//It doesn't work because I subsequently removed the additional methods, but 
//I'll leave it here in case there is another use for it later.  Its easy enough
//to substitute other methods in to profile.

//        private string DoProfiling()
//        {
//            string itemText =
//@"gd_itemgrades.Weapons.ItemGrade_Weapon_SniperRifle
//gd_manufacturers.Manufacturers.Maliwan
//gd_weap_sniper_rifle.A_Weapon.WeaponType_sniper_rifle
//gd_weap_sniper_rifle.Body.body1
//gd_weap_sniper_rifle.Grip.grip3
//gd_weap_sniper_rifle.mag.mag1
//gd_weap_sniper_rifle.Barrel.barrel1
//gd_weap_sniper_rifle.Sight.sight1
//gd_weap_sniper_rifle.Stock.stock1
//None
//gd_weap_sniper_rifle.acc.acc2_Incendiary
//gd_weap_shared_materialparts.ManufacturerMaterials.Material_Maliwan_1
//gd_weap_names_shared.Prefix_Tech.Prefix_DTIncendiary2_Incendiary
//gd_weap_sniper_rifle.Title.Title__SniperRifle
//6
//0
//0
//0
//";
//            InventoryEntry baseEntry = InventoryEntry.ImportFromText(itemText, InventoryType.Weapon);
//            List<InventoryEntry> entries = new List<InventoryEntry>();

//            for (int i = 0; i < 1000; i++)
//            {
//                entries.Add(new InventoryEntry(baseEntry));
//            }

//            int loopCount = 100;
//            long totalTicksMethod1 = 0;
//            long totalTicksMethod2 = 0;
//            long totalTicksMethod3 = 0;
//            long totalTicksMethod4 = 0;
//            long totalTicksMethod5 = 0;
//            long totalTicksMethod6 = 0;

//            Stopwatch timer = new Stopwatch();


//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod1 += ticks;
//            }

//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName2();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod2 += ticks;
//            }

//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName3();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod3 += ticks;
//            }

//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod4 += ticks;
//            }

//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName2();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod5 += ticks;
//            }

//            for (int i = 0; i <= loopCount; i++)
//            {
//                timer.Reset();
//                timer.Start();
//                foreach (InventoryEntry entry in entries)
//                    entry.BuildName3();
//                timer.Stop();

//                long ticks = timer.ElapsedTicks;
//                if (i != 0)
//                    totalTicksMethod6 += ticks;
//            }

//            return "BuildName (try 1): " + totalTicksMethod1 + " ticks\r\n" +
//                "BuildName2 (try 1): " + totalTicksMethod2 + " ticks\r\n" +
//                "BuildName3 (try 1): " + totalTicksMethod3 + " ticks\r\n" +
//                "BuildName (try 2): " + totalTicksMethod4 + " ticks\r\n" +
//                "BuildName2 (try 2): " + totalTicksMethod5 + " ticks\r\n" +
//                "BuildName3 (try 2): " + totalTicksMethod6 + " ticks\r\n";
//        }
    }
}
