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
using System.Linq;
using System.Drawing;
//using System.Xml;
//using System.Windows.Forms;
using WillowTree.Inventory;

namespace WillowTree
{
    public partial class db
    {
        static Dictionary<string, string> NameLookup;
        public static string AppPath = WillowSaveGame.AppPath;
        public static string DataPath = WillowSaveGame.DataPath;
        public static string XmlPath = DataPath + "Xml" + System.IO.Path.DirectorySeparatorChar;
        private static string OpenedLocker; //Keep tracking of last open locker file

        static List<string> skillfiles = new List<string>()
            {
                DataPath + "gd_skills_common.txt",
                DataPath + "gd_Skills2_Roland.txt",
                DataPath + "gd_Skills2_Lilith.txt",
                DataPath + "gd_skills2_Mordecai.txt",
                DataPath + "gd_Skills2_Brick.txt"
            };

        public static XmlFile EchoesXml = new XmlFile(DataPath + "Echos.ini");
        public static XmlFile LocationsXml = new XmlFile(DataPath + "Locations.ini");
        public static XmlFile QuestsXml = new XmlFile(DataPath + "Quests.ini");
        public static XmlFile SkillsCommonXml = new XmlFile(DataPath + "gd_skills_common.txt");
        public static XmlFile SkillsSoldierXml = new XmlFile(DataPath + "gd_skills2_Roland.txt");
        public static XmlFile SkillsSirenXml = new XmlFile(DataPath + "gd_Skills2_Lilith.txt");
        public static XmlFile SkillsHunterXml = new XmlFile(DataPath + "gd_skills2_Mordecai.txt");
        public static XmlFile SkillsBerserkerXml = new XmlFile(DataPath + "gd_Skills2_Brick.txt");
        public static XmlFile SkillsAllXml = new XmlFile(skillfiles, XmlPath + "gd_skills.xml");
        public static XmlFile PartNamesXml = new XmlFile(DataPath + "partnames.ini");

        public static int[] XPChart = new int[71];

        public static void setXPchart()
        {
            XPChart[0] = 0;
            XPChart[1] = 0;
            XPChart[2] = 358;
            XPChart[3] = 1241;
            XPChart[4] = 2850;
            XPChart[5] = 5376;
            XPChart[6] = 8997;
            XPChart[7] = 13886;
            XPChart[8] = 20208;
            XPChart[9] = 28126;
            XPChart[10] = 37798;
            XPChart[11] = 49377;
            XPChart[12] = 63016;
            XPChart[13] = 78861;
            XPChart[14] = 97061;
            XPChart[15] = 117757;
            XPChart[16] = 141092;
            XPChart[17] = 167207;
            XPChart[18] = 196238;
            XPChart[19] = 228322;
            XPChart[20] = 263595;
            XPChart[21] = 302190;
            XPChart[22] = 344238;
            XPChart[23] = 389873;
            XPChart[24] = 439222;
            XPChart[25] = 492414;
            XPChart[26] = 549578;
            XPChart[27] = 610840;
            XPChart[28] = 676325;
            XPChart[29] = 746158;
            XPChart[30] = 820463;
            XPChart[31] = 899363;
            XPChart[32] = 982980;
            XPChart[33] = 1071436;
            XPChart[34] = 1164850;
            XPChart[35] = 1263343;
            XPChart[36] = 1367034;
            XPChart[37] = 1476041;
            XPChart[38] = 1590483;
            XPChart[39] = 1710476;
            XPChart[40] = 1836137;
            XPChart[41] = 1967582;
            XPChart[42] = 2104926;
            XPChart[43] = 2248285;
            XPChart[44] = 2397772;
            XPChart[45] = 2553561;
            XPChart[46] = 2715586;
            XPChart[47] = 2884139;
            XPChart[48] = 3059273;
            XPChart[49] = 3241098;
            XPChart[50] = 3429728;
            // lvl 50 says 3625271
            XPChart[51] = 3628272;
            XPChart[52] = 3827841;
            XPChart[53] = 4037544;
            XPChart[54] = 4254492;
            XPChart[55] = 4478793;
            XPChart[56] = 4710557;
            XPChart[57] = 4949891;
            XPChart[58] = 5196904;
            XPChart[59] = 5451702;
            XPChart[60] = 5714395;
            XPChart[61] = 5985086;
            //Knoxx-only
            XPChart[62] = 6263885;
            XPChart[63] = 6550897;
            XPChart[64] = 6846227;
            XPChart[65] = 7149982;
            XPChart[66] = 7462266;
            XPChart[67] = 7783184;
            XPChart[68] = 8112840;
            XPChart[69] = 8451340;

            XPChart[70] = 2147483647;
        }

        public static InventoryList WeaponList = new InventoryList(InventoryType.Weapon);
        public static InventoryList ItemList = new InventoryList(InventoryType.Item);
        public static InventoryList BankList = new InventoryList(InventoryType.Any);
        public static InventoryList LockerList = new InventoryList(InventoryType.Any);

        private static int _keyIndex;

        public static string CreateUniqueKey()
        {
            return _keyIndex++.ToString();
        }

        public static string OpenedLockerFilename()
        {
            if (string.IsNullOrEmpty(OpenedLocker))
                return db.DataPath + "default.xml";
            else
                return OpenedLocker;
        }

        public static void OpenedLockerFilename(string sOpenedLocker)
        {
            OpenedLocker = sOpenedLocker;
        }

        // 0 references - Build composite string by combining the attribute from each part in a thing with a space between
        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetInventoryAttributeComposite(XmlFile xml, string[,] InventoryArray, int InventoryIndex, int InventoryPartCount, string AttributeName)
        {
            string Name = "";
            for (int build = 0; build < InventoryPartCount; build++)
            {

                string readValue = xml.XmlReadValue(InventoryArray[InventoryIndex, build], AttributeName);

                if (Name == "" && readValue != null)
                    Name = readValue;
                else if (readValue != null && readValue != "")
                    Name = (Name + " " + readValue);
            }

            return Name;
        }

        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetInventoryNameSlow(List<string> parts, int inventoryType)
        {
            if (inventoryType == InventoryType.Weapon)
                return GetWeaponNameSlow(parts);
            else if (inventoryType == InventoryType.Item)
                return GetItemNameSlow(parts);
            else
                throw new ArgumentException("Unknown inventory type specified in GetInventoryName");
        }

        public static string GetName(string part)
        {
            // This method fetches the name of a part from the NameLookup dictionary
            // which only contains name data extracted from each part.  The name 
            // could be fetched from the individual part data by using 
            // GetPartName(string part) or GetPartAttribute(string part, "PartName"),
            // but since those have to search through lots of Xml nodes to find the 
            // data this should be faster.
            string value;
            if (NameLookup.TryGetValue(part, out value) == false)
                return "";
            return value;
        }

        // 1 references - Compose the full name of a item from its parts (list version)
        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetWeaponNameFast(List<string> parts)
        {
            // This version uses the higher performance GetName function to fetch 
            // part names from the NameLookup dictionary.
            string Name = GetName(parts[13]);
            string Prefix = GetName(parts[12]);

            string MfgText;
            if (GlobalSettings.ShowManufacturer)
            {
                MfgText = GetName(parts[1]); // Mfg Name
                if (MfgText == "")
                    MfgText = "Generic";
            }
            else
                MfgText = "";

            string BodyText = GetName(parts[3]); // Body text
            string MaterialText = GetName(parts[11]); // Material text

            int Model = Parse.AsInt(GetName(parts[8]), 0);        // Number from stock
            Model += Parse.AsInt(GetName(parts[5]), 0);           // Number from mag
            //            if ((GetPartRarity(parts[8]) == 0) || (GetPartRarity(parts[5]) == 0))
            //                Model = Model / 10;
            if ((GetPartRarity(parts[8]) != 0) && (GetPartRarity(parts[5]) != 0))
                Model = Model * 10;
            string ModelText = (Model != 0 ? Model.ToString() : "");

            string ModelName = BodyText + ModelText + MaterialText;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (MfgText != "")
            {
                sb.Append(MfgText);
                sb.Append(' ');
            }

            if (ModelName != "")
            {
                sb.Append(ModelName);
                sb.Append(' ');
            }

            if (Prefix != "")
            {
                sb.Append(Prefix);
                sb.Append(' ');
            }

            sb.Append(Name);

            return sb.ToString();
        }

        // 0 references - Compose the full name of a item from its parts (list version)
        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetWeaponNameSlow(List<string> parts)
        {
            // This version uses the lower performance GetPartName function to fetch 
            // part names directly from the Xml part data files rather than using
            // the NameLookup dictionary.
            string Name = GetPartName(parts[13]);
            string Prefix = GetPartName(parts[12]);

            string MfgText;
            if (GlobalSettings.ShowManufacturer)
            {
                MfgText = GetPartAttribute(parts[1], "Manufacturer"); // Mfg Name
                if (MfgText == "")
                    MfgText = "Generic";
            }
            else
                MfgText = "";

            string BodyText = GetPartName(parts[3]); // Body text
            string MaterialText = GetPartName(parts[11]); // Material text

            int Model = GetPartNumber(parts[8]);        // Number from stock
            Model += GetPartNumber(parts[5]);           // Number from mag
            //            if ((GetPartRarity(parts[8]) == 0) || (GetPartRarity(parts[5]) == 0))
            //                Model = Model / 10;
            if ((GetPartRarity(parts[8]) != 0) && (GetPartRarity(parts[5]) != 0))
                Model = Model * 10;
            string ModelText = (Model != 0 ? Model.ToString() : "");

            string ModelName = BodyText + ModelText + MaterialText;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (MfgText != "")
            {
                sb.Append(MfgText);
                sb.Append(' ');
            }

            if (ModelName != "")
            {
                sb.Append(ModelName);
                sb.Append(' ');
            }

            if (Prefix != "")
            {
                sb.Append(Prefix);
                sb.Append(' ');
            }

            sb.Append(Name);

            return sb.ToString();
        }

        // 0 references - Compose the full name of an item from its parts (list vesion)
        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetItemNameFast(List<string> parts)
        {
            // This version uses the higher performance GetName function to fetch 
            // part names from the NameLookup dictionary.
            string Name = GetName(parts[8]);
            string Prefix = GetName(parts[7]);
            if ((Name == "") && (Prefix == ""))
            {
                Name = GetName(parts[1]);
                if (Name == "")
                    return "Unknown Item";
                return Name;
            }

            string MfgText;
            if (GlobalSettings.ShowManufacturer)
            {
                MfgText = GetName(parts[6]); // Mfg Name
                if (MfgText == "")
                    MfgText = GetPartAttribute(parts[1], "NoManufacturerName");
            }
            else
                MfgText = "";

            string BodyText = GetName(parts[5]); // Right text
            string MaterialText = GetName(parts[2]); // Material text

            int Model = Parse.AsInt(GetName(parts[3]));        // ModelNumber text
            Model += Parse.AsInt(GetName(parts[4]));           //
            //            if ((GetPartRarity(parts[3]) == 0) || (GetPartRarity(parts[4]) == 0))
            //                Model = Model / 10;
            if ((GetPartRarity(parts[3]) != 0) && (GetPartRarity(parts[4]) != 0))
                Model = Model * 10;
            string ModelText = (Model != 0 ? Model.ToString() : "");

            string ModelName = BodyText + ModelText + MaterialText;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (MfgText != "")
            {
                sb.Append(MfgText);
                sb.Append(' ');
            }

            if (ModelName != "")
            {
                sb.Append(ModelName);
                sb.Append(' ');
            }

            if (Prefix != "")
            {
                sb.Append(Prefix);
                sb.Append(' ');
            }

            sb.Append(Name);

            return sb.ToString();
        }

        // 1 references - Compose the full name of an item from its parts (list vesion)
        // OBSOLETE - Nothing uses this.  It couid be removed.
        public static string GetItemNameSlow(List<string> parts)
        {
            string Name = GetPartName(parts[8]);
            string Prefix = GetPartName(parts[7]);
            if ((Name == "") && (Prefix == ""))
            {
                Name = GetPartAttribute(parts[1], "ItemName");
                if (Name == "")
                    return "Unknown Item";
                return Name;
            }

            string MfgText;
            if (GlobalSettings.ShowManufacturer)
            {
                MfgText = GetPartAttribute(parts[6], "Manufacturer"); // Mfg Name
                if (MfgText == "")
                    MfgText = GetPartAttribute(parts[1], "NoManufacturerName");
            }
            else
                MfgText = "";

            string BodyText = GetPartName(parts[5]); // Right text
            string MaterialText = GetPartName(parts[2]); // Material text

            int Model = GetPartNumber(parts[3]);        // ModelNumber text
            Model += GetPartNumber(parts[4]);           //
            if ((GetPartRarity(parts[3]) == 0) || (GetPartRarity(parts[4]) == 0))
                Model = Model / 10;
            string ModelText = (Model != 0 ? Model.ToString() : "");

            string ModelName = BodyText + ModelText + MaterialText;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (MfgText != "")
            {
                sb.Append(MfgText);
                sb.Append(' ');
            }

            if (ModelName != "")
            {
                sb.Append(ModelName);
                sb.Append(' ');
            }

            if (Prefix != "")
            {
                sb.Append(Prefix);
                sb.Append(' ');
            }

            sb.Append(Name);

            return sb.ToString();
        }

        // 27 references - Fetch a given attribute from a part
        public static string GetPartAttribute(string part, string AttributeName)
        {
            string Database = part.Before('.');
            if (Database == "")
                return "";

            string PartName = part.After('.');

            string DbFileName = DataPath + Database + ".txt";
            if (!System.IO.File.Exists(DbFileName))
                return "";

            XmlFile DataFile = XmlFile.XmlFileFromCache(DbFileName);

            string ComponentText = DataFile.XmlReadValue(PartName, AttributeName);
            return ComponentText;
        }

        // 2 references
        public static List<string> GetPartSection(string part)
        {
            string Database = part.Before('.');
            if (Database == "")
                return null;

            string PartName = part.After('.');

            string DbFileName = XmlPath + Database + ".xml";
            if (!System.IO.File.Exists(DbFileName))
                return null;

            XmlFile DataFile = XmlFile.XmlFileFromCache(DbFileName);

            List<string> section = DataFile.XmlReadSection(PartName);
            return section;
        }

        // 9 references - Fetch the PartName attribute of a part
        public static string GetPartName(string part)
        {
            // This is the slow way to get part names directly from the part Xml
            // files.  GetName(string part) is a higher performance method that
            // fetches parts from the NameLookup dictionary which contains only
            // part names and no other data.
            return GetPartAttribute(part, "PartName");
        }

        // 5 references - Fetch the PartNumberAddend attribute of a part
        public static int GetPartNumber(string part)
        {
            string ComponentText = GetPartAttribute(part, "PartNumberAddend");
            return Parse.AsInt(ComponentText, null);
        }

        public static int GetPartRarity(string part)
        {
            string ComponentText = GetPartAttribute(part, "Rarity");
            return Parse.AsInt(ComponentText, null);
        }
        //// 5 references - Translate the Rarity attribute of a part and return its value
        //public static int GetPartRarity(string part)
        //{
        //    string RarityText = GetPartAttribute(part, "Rarity");
        //    switch (RarityText)
        //    {   // The rarity values are extracted directly from code contributed by CK Y
        //        // I assume they are correct, but I haven't tested them independently. (matt911)
        //        case (""):
        //        case ("WeaponPartRarity1_Common"):
        //            return 0;
        //        case ("WeaponPartRarity2_Uncommon"):
        //            return 1;
        //        case ("WeaponPartRarity3_Uncommoner"):
        //            return 3;
        //        case ("WeaponPartRarity4_Rare"):
        //            return 5;
        //        case ("WeaponPartRarity5_VeryRare"):
        //            return 8;
        //        case ("WeaponPartRarity6_Legendary"):
        //            return 50;
        //        case ("ArtifactPartRarity1_Common"):
        //            return 0;
        //        case ("ArtifactPartRarity2_Uncommon"):
        //            return 2;
        //        case ("ArtifactPartRarity3_Uncommoner"):
        //            return 4;
        //        case ("ArtifactPartRarity4_Rare"):
        //            return 6;
        //        case ("ArtifactPartRarity5_VeryRare"):
        //            return 8;
        //        case ("ArtifactPartRarity6_Legendary"):
        //            return 10;
        //        case ("GrenadeModPartRarity1_Common"):
        //            return 0;
        //        case ("GrenadeModPartRarity2_Uncommon"):
        //            return 2;
        //        case ("GrenadeModPartRarity3_Uncommoner"):
        //            return 4;
        //        case ("GrenadeModPartRarity4_Rare"):
        //            return 6;
        //        case ("GrenadeModPartRarity5_VeryRare"):
        //            return 8;
        //        case ("GrenadeModPartRarity6_Legendary"):
        //            return 10;
        //        case ("ShieldPartRarity1_Common"):
        //            return 0;
        //        case ("ShieldPartRarity2_Uncommon"):
        //            return 2;
        //        case ("ShieldPartRarity3_Uncommoner"):
        //            return 4;
        //        case ("ShieldPartRarity4_Rare"):
        //            return 6;
        //        case ("ShieldPartRarity5_VeryRare"):
        //            return 8;
        //        case ("ShieldPartRarity6_Legendary"):
        //            return 10;
        //        case ("ClassModPartRarity1_Common"):
        //            return 0;
        //        case ("ClassModPartRarity2_Uncommon"):
        //            return 2;
        //        case ("ClassModPartRarity3_Uncommoner"):
        //            return 4;
        //        case ("ClassModPartRarity4_Rare"):
        //            return 6;
        //        case ("ClassModPartRarity5_VeryRare"):
        //            return 8;
        //        case ("ClassModPartRarity6_Legendary"):
        //            return 10;        
        //        default:
        //            int val;
        //            if (int.TryParse(RarityText, out val))
        //                return val;
        //            MessageBox.Show("Unrecognized rarity string in " + part);
        //            return 0;
        //    }
        //}

        public static Color RarityToColorItem(int rarity)
        {
            Color color;

            if (rarity <= 4) { color = GlobalSettings.RarityColor[0]; }
            else if (rarity <= 9) { color = GlobalSettings.RarityColor[1]; }
            else if (rarity <= 13) { color = GlobalSettings.RarityColor[2]; }
            else if (rarity <= 49) { color = GlobalSettings.RarityColor[3]; }
            else if (rarity <= 60) { color = GlobalSettings.RarityColor[4]; }
            else if (rarity <= 65) { color = GlobalSettings.RarityColor[5]; }
            else if (rarity <= 100) { color = GlobalSettings.RarityColor[6]; }
            else if (rarity <= 169) { color = GlobalSettings.RarityColor[7]; }
            else if (rarity <= 170) { color = GlobalSettings.RarityColor[8]; }
            else if (rarity <= 171) { color = GlobalSettings.RarityColor[9]; }
            else if (rarity <= 179) { color = GlobalSettings.RarityColor[10]; }
            else color = GlobalSettings.RarityColor[11];
            return color;
        }

        public static Color RarityToColorWeapon(int rarity)
        {
            Color color;

            if (rarity <= 4) { color = GlobalSettings.RarityColor[0]; }
            else if (rarity <= 10) { color = GlobalSettings.RarityColor[1]; }
            else if (rarity <= 15) { color = GlobalSettings.RarityColor[2]; }
            else if (rarity <= 49) { color = GlobalSettings.RarityColor[3]; }
            else if (rarity <= 60) { color = GlobalSettings.RarityColor[4]; }
            else if (rarity <= 65) { color = GlobalSettings.RarityColor[5]; }
            else if (rarity <= 100) { color = GlobalSettings.RarityColor[6]; }
            else if (rarity <= 169) { color = GlobalSettings.RarityColor[7]; }
            else if (rarity <= 170) { color = GlobalSettings.RarityColor[8]; }
            else if (rarity <= 171) { color = GlobalSettings.RarityColor[9]; }
            else if (rarity <= 179) { color = GlobalSettings.RarityColor[10]; }
            else color = GlobalSettings.RarityColor[11];
            return color;
        }

        private static double GetExtraStats(string[] WeaponParts, string StatName)
        {
            double bonus = 0;
            double penalty = 0;
            double value;
            try
            {
                double ExtraDamage = 0;
                for (int i = 3; i < 14; i++)
                {
                    string valuestring = GetPartAttribute(WeaponParts[i], StatName);
                    if (valuestring.Contains(','))
                    {
                        // TODO: I think there are some entries that have two numbers
                        // with a comma between them.  Need to figure out how to properly
                        // handle them.  This breakpoint will catch it when debugging so
                        // so I can figure it out.
                        //Debugger.Break();
                        value = Parse.AsDouble(GetPartAttribute(WeaponParts[i], StatName), 0);
                    }
                    else
                        value = Parse.AsDouble(GetPartAttribute(WeaponParts[i], StatName), 0);

                    if (value >= 0)
                        bonus += value;
                    else
                        penalty -= value;
                }
                ExtraDamage = ((1 + bonus) / (1 + penalty)) - 1;
                return ExtraDamage;
            }
            catch
            {
                return -1;
            }
        }

        public static int GetEffectiveLevelItem(string[] ItemParts, int Quality, int LevelIndex)
        {
            if (LevelIndex != 0)
                return LevelIndex - 2;

            string Manufacturer = ItemParts[6].After("gd_manufacturers.Manufacturers.");
            //            string Manufacturer = GetPartAttribute(WeaponParts[6], "Manufacturer").TrimEnd();
            string LevelIndexText = GetPartAttribute(ItemParts[0], Manufacturer + Quality);
            return Parse.AsInt(LevelIndexText, 2) - 2;
        }

        //public class InventoryComparer : Comparer<InventoryEntry>
        //{
        //    public int[] comparisons;

        //    public InventoryComparer(int[] comparisonarray)
        //    {
        //        comparisons = comparisonarray;
        //    }

        //    public override int Compare(InventoryEntry x, InventoryEntry y)
        //    {
        //        int result = 0;
        //        foreach (int comparison in comparisons)
        //        {
        //            switch (comparison)
        //            {
        //                case 0: result = string.Compare(x.Name, y.Name); break;
        //                case 1:
        //                    if (x.Rarity > y.Rarity)
        //                        result = -1;
        //                    else if (x.Rarity < y.Rarity)
        //                        result = 1;
        //                    else
        //                        result = 0;
        //                    break;
        //                case 2: result = string.Compare(x.Category, y.Category); break;
        //                case 3: result = string.Compare(x.NameParts[3], y.NameParts[3]); break;
        //                case 4: result = string.Compare(x.NameParts[2], y.NameParts[2]); break;
        //                case 5: result = string.Compare(x.NameParts[1], y.NameParts[1]); break;
        //                case 6: result = string.Compare(x.NameParts[0], y.NameParts[0]); break;
        //                case 7:
        //                    if (x.Level > y.Level)
        //                        result = -1;
        //                    else if (x.Level < y.Level)
        //                        result = 1;
        //                    else
        //                        result = 0;
        //                    break;
        //                case 8:
        //                    int xkeyval = int.Parse(x.Key);
        //                    int ykeyval = int.Parse(y.Key);
        //                    if (xkeyval > ykeyval)
        //                        result = -1;
        //                    else if (xkeyval < ykeyval)
        //                        result = 1;
        //                    else
        //                        result = 0;
        //                    break;
        //            }
        //            if (result != 0)
        //                return result;
        //        }
        //        return result;
        //    }
        //}
        //InventoryComparer CategoryNameComparison = new InventoryComparer(new int[] { 2, 0, 8 });
        //InventoryComparer CategoryRarityLevelComparison = new InventoryComparer(new int[] { 2, 1, 7, 8 });
        //InventoryComparer CategoryTitlePrefixModelComparison = new InventoryComparer(new int[] { 2, 3, 4, 5, 8 });
        //InventoryComparer CategoryLevelNameComparison = new InventoryComparer(new int[] { 2, 7, 0, 8 });
        //InventoryComparer NameComparison = new InventoryComparer(new int[] { 0, 8 });
        //InventoryComparer RarityLevelComparison = new InventoryComparer(new int[] { 1, 7, 8 });
        //InventoryComparer TitlePrefixModelComparison = new InventoryComparer(new int[] { 2, 3, 4, 5, 8 });
        //InventoryComparer LevelNameComparison = new InventoryComparer(new int[] { 7, 0, 8 });

        //public class InventoryComparisonIterator
        //{
        //    public int ComparerIndex;
        //    public InventoryComparer[] Comparers;
        //    public InventoryComparisonIterator(InventoryComparer[] comparers)
        //    {
        //        Comparers = comparers;
        //    }
        //    public void NextComparer()
        //    {
        //        ComparerIndex++;
        //        if (ComparerIndex >= Comparers.Length)
        //            ComparerIndex = 0;
        //    }
        //    public void PreviousComparer()
        //    {
        //        ComparerIndex++;
        //        if (ComparerIndex >= Comparers.Length)
        //            ComparerIndex = 0;
        //    }
        //    public InventoryComparer CurrentComparer()
        //    {
        //        return Comparers[ComparerIndex];
        //    }
        //}

        public static int GetEffectiveLevelWeapon(string[] WeaponParts, int Quality, int LevelIndex)
        {
            if (LevelIndex != 0)
                return LevelIndex - 2;

            //            string Manufacturer = GetPartAttribute(WeaponParts[1], "Manufacturer").TrimEnd();
            // There may be a case below where the manufacturer is invalid or blank
            string Manufacturer = WeaponParts[1].After("gd_manufacturers.Manufacturers.");
            string LevelIndexText = GetPartAttribute(WeaponParts[0], Manufacturer + Quality);
            return Parse.AsInt(LevelIndexText, 2) - 2;
        }

        private static int GetWeaponDamage(string[] WeaponParts, int Quality, int LevelIndex)
        {
            try
            {
                double PenaltyDamage = 0;
                double BonusDamage = 0;
                double Multiplier;
                if (WeaponParts[2] == "gd_weap_repeater_pistol.A_Weapon.WeaponType_repeater_pistol")
                    Multiplier = 1;
                else Multiplier = Parse.AsDouble(GetPartAttribute(WeaponParts[2], "WeaponDamageFormulaMultiplier"), 1);
                int Level = GetEffectiveLevelWeapon(WeaponParts, Quality, LevelIndex);
                double Power = 1.3;
                double Offset = 9;
                for (int i = 3; i < 14; i++)
                {
                    if (WeaponParts[i].Contains("."))
                    {
                        double PartDamage = Parse.AsDouble(GetPartAttribute(WeaponParts[i], "WeaponDamage"), 0);
                        if (PartDamage < 0)
                            PenaltyDamage -= PartDamage;
                        else
                            BonusDamage += PartDamage;
                    }
                }

                double DmgScaler = (1 + BonusDamage) / (1 + PenaltyDamage);
                double BaseDamage = 0.8 * Multiplier * (Math.Pow(Level + 2, Power) + Offset);
                double ScaledDamage = BaseDamage * DmgScaler;
                return (int)Math.Truncate(ScaledDamage + 1);
            }
            catch
            {
                return -1;
            }
        }

        public static string WeaponInfo(InventoryEntry invEntry)
        {
            string WeaponInfo;
            string[] parts = invEntry.Parts.ToArray<string>();

            int Damage = db.GetWeaponDamage(parts, invEntry.QualityIndex, invEntry.LevelIndex);
            WeaponInfo = "Expected Damage: " + Damage;

            double statvalue = db.GetExtraStats(parts, "TechLevelIncrease");
            if (statvalue != 0)
                WeaponInfo += "\r\nElemental Tech Level: " + statvalue;

            statvalue = db.GetExtraStats(parts, "WeaponDamage");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Damage";

            statvalue = db.GetExtraStats(parts, "WeaponFireRate");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Rate of Fire";

            statvalue = db.GetExtraStats(parts, "WeaponCritBonus");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Critical Damage";

            statvalue = db.GetExtraStats(parts, "WeaponReloadSpeed");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Reload Speed";

            statvalue = db.GetExtraStats(parts, "WeaponSpread");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Spread";

            statvalue = db.GetExtraStats(parts, "MaxAccuracy");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Max Accuracy";

            statvalue = db.GetExtraStats(parts, "MinAccuracy");
            if (statvalue != 0)
                WeaponInfo += "\r\n" + statvalue.ToString("P") + " Min Accuracy";

            return WeaponInfo;
        }

        public static void InitializeNameLookup()
        {
            NameLookup = new Dictionary<string, string>();
            {
                XmlFile names = PartNamesXml;

                foreach (string section in names.stListSectionNames())
                {
                    List<string> entries = names.XmlReadSection(section);
                    foreach (string entry in entries)
                    {
                        int index = entry.IndexOf(':');
                        string part = entry.Substring(0, index);
                        string name = entry.Substring(index + 1);
                        NameLookup.Add(part, name);
                    }
                }
            }
        }
    }
}
