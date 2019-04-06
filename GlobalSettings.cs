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
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;

namespace WillowTree
{
    public enum InputMode { Standard, Advanced, UseGlobalSetting};

    public class GlobalSettings
    {
        // GlobalSettings provides a place to collect all the settings that
        // I want to be able to be loaded and saved to the user option
        // file options.xml.  
        public delegate void InputMethodChangedEventHandler(WillowTree.InputMode method, bool UseHexadecimal);
        static public event InputMethodChangedEventHandler InputMethodChanged;

        static private WillowTree.InputMode _InputMode = WillowTree.InputMode.Standard;
        static public WillowTree.InputMode InputMode
        {
            get { return _InputMode; }
            set
            {
                _InputMode = value;
                if (InputMethodChanged != null)
                    InputMethodChanged(InputMode, UseHexInAdvancedMode);
            }
        }

        static public bool UseHexInAdvancedMode = false;
        static public bool PartSelectorTracking = true;
        static public bool ShowManufacturer = false;
        static public bool UseColor = false;
        static public bool ShowRarity = false;
        static public bool ShowLevel = true;
        static public string lastLockerFile = string.Empty;
        static public Color BackgroundColor = Color.FromArgb(48, 48, 48);

        // ------- MAX VALUES ---------
        // All values that exceed these sanity limits will be adjusted by the UI and give the 
        // user a warning except for cash.  Cash increases too often automatically through 
        // gameplay so it will be adjusted silently to prevent receiving a warning every time
        // the savegame is opened.
        //
        // All max values can be set to any number up to 2147483647 (int.MaxValue) by editing
        // the limits in 'options.xml'.  These values are the defaults if 'options.xml' has
        // not yet been created or its values are missing or corrupt.
        // ----------------------------
        
        // Borderlands 1.4.2.1 (Steam PC) has a bug that allows the cash to go to an extreme 
        // negative number if you exceed int.MaxValue when picking up a money bag off the ground.
        // You cannot buy anything in that state until you sell an item to the shop, which will 
        // set your money back to int.MaxValue again and repeat the cycle when you again pick
        // up money off the ground.  The default for MaxCash is set to 1 billion to stay clear 
        // of int.MaxValue and avoid that problem.  Bank and Backpack limits exist to keep the
        // savegame file from becoming excessively large which slows down loading.  Borderlands 
        // can support much higher values but these are safe and sane.
        static public int MaxCash = 1000000000;
        static public int MaxExperience = 8451341;
        static public int MaxLevel = 69;
        static public int MaxBackpackSlots = 1000;
        static public int MaxBankSlots = 1000;
        static public int MaxSkillPoints = 500;

        static public Color[] RarityColor =
            {
                Color.White,
                Color.FromArgb(0x3d, 0xe6, 0x0b),
                Color.FromArgb(0x2f, 0x78, 0xff), 
//                Color.FromArgb(3111167),
                Color.FromArgb(185, 64, 255),
                Color.FromArgb(255, 220 ,53),
                Color.FromArgb(0xff,0x96, 0x00),
                Color.DarkOrange,
                Color.Cyan,
                Color.FromArgb(0x3d, 0xe6, 0x0b),
                Color.Red,
                Color.GhostWhite,
                Color.Yellow,
            };

        static public void Save()
        {
            string filename = db.XmlPath + "options.xml";

            using (XmlTextWriter gs = new XmlTextWriter(filename, Encoding.UTF8))
            {
                gs.WriteStartDocument();
                gs.Formatting = Formatting.Indented;
                gs.WriteStartElement("Settings");

                gs.WriteElementString("InputMode", InputMode.ToString());
                gs.WriteElementString("UseHexInAdvancedMode", UseHexInAdvancedMode.ToString());
                gs.WriteElementString("PartSelectorTracking", PartSelectorTracking.ToString());
                gs.WriteElementString("ShowManufacturer", ShowManufacturer.ToString());
                gs.WriteElementString("UseColor", UseColor.ToString());
                gs.WriteElementString("ShowRarity", ShowRarity.ToString());
                gs.WriteElementString("ShowLevel", ShowLevel.ToString());
                gs.WriteElementString("lastLockerFile", db.OpenedLockerFilename());

                gs.WriteElementString("MaxCash", MaxCash.ToString());
                gs.WriteElementString("MaxLevel", MaxLevel.ToString());
                gs.WriteElementString("MaxExperience", MaxExperience.ToString());
                gs.WriteElementString("MaxInventorySlots", MaxBackpackSlots.ToString());
                gs.WriteElementString("MaxBankSlots", MaxBankSlots.ToString());
                gs.WriteElementString("MaxSkillPoints", MaxSkillPoints.ToString());

                gs.WriteElementString("RarityColor0", RarityColor[0].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor1", RarityColor[1].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor2", RarityColor[2].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor3", RarityColor[3].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor4", RarityColor[4].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor5", RarityColor[5].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor6", RarityColor[6].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor7", RarityColor[7].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor8", RarityColor[8].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor9", RarityColor[9].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor10", RarityColor[10].ToArgb().ToString("X"));
                gs.WriteElementString("RarityColor11", RarityColor[11].ToArgb().ToString("X"));

                gs.WriteEndElement();
                gs.WriteEndDocument();
            }
        }

        static private bool XmlReadBool(XmlTextReader gs, ref bool var)
        {
            bool value;
            if (bool.TryParse(gs.ReadElementContentAsString(), out value))
            {
                var = value;
                return true;
            }
            return false;
        }

        static private bool XmlReadInt(XmlTextReader gs, ref int var)
        {
            int value;
            if (int.TryParse(gs.ReadElementContentAsString(), out value))
            {
                var = value;
                return true;
            }
            return false;
        }

        static public void Load()
        {
            string filename = db.XmlPath + "options.xml";

            if (!System.IO.File.Exists(filename))
                return;
            using (XmlTextReader gs = new XmlTextReader(filename))
            {
                gs.XmlResolver = null;
                while (gs.Read())
                {
                    if (gs.NodeType == XmlNodeType.Element)
                    {
                        switch (gs.Name)
                        {
                            case "InputMode":
                                string InputModeText = gs.ReadElementContentAsString();
                                if (InputModeText == "Advanced")
                                    InputMode = InputMode.Advanced;
                                else if (InputModeText == "Standard")
                                    InputMode = InputMode.Standard;
                                break;
                            case "UseHexInAdvancedMode": XmlReadBool(gs, ref UseHexInAdvancedMode); break;
                            case "PartSelectorTracking": XmlReadBool(gs, ref PartSelectorTracking); break;
                            case "ShowManufacturer": XmlReadBool(gs, ref ShowManufacturer); break;
                            case "ShowRarity": XmlReadBool(gs, ref ShowRarity); break;
                            case "ShowLevel": XmlReadBool(gs, ref ShowLevel); break;
                            case "UseColor": XmlReadBool(gs, ref UseColor); break;
                            case "lastLockerFile":
                                {
                                    db.OpenedLockerFilename(gs.ReadElementContentAsString());
                                }
                                break;

                            case "MaxCash": XmlReadInt(gs, ref MaxCash); break;
                            case "MaxLevel": XmlReadInt(gs, ref MaxLevel); break;
                            case "MaxExperience": XmlReadInt(gs, ref MaxExperience); break;
                            case "MaxInventorySlots": XmlReadInt(gs, ref MaxBackpackSlots); break;
                            case "MaxBankSlots": XmlReadInt(gs, ref MaxBankSlots); break;
                            case "MaxSkillPoints": XmlReadInt(gs, ref MaxSkillPoints); break;

                            case "RarityColor0":
                            case "RarityColor1":
                            case "RarityColor2":
                            case "RarityColor3":
                            case "RarityColor4":
                            case "RarityColor5":
                            case "RarityColor6":
                            case "RarityColor7":
                            case "RarityColor8":
                            case "RarityColor9":
                            case "RarityColor10":
                            case "RarityColor11":
                                try
                                {
                                    int index = Parse.AsInt(gs.Name.After("RarityColor"));
                                    uint colorval;
                                    string text = gs.ReadElementContentAsString();
                                    if (uint.TryParse(text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out colorval))
                                        GlobalSettings.RarityColor[index] = Color.FromArgb((int)colorval);
                                } catch { }
                                break;
                        }
                    }
                }
            }
        }
    }
}
