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
using System.IO;
using System.Linq;
using System.Text;
using X360.IO;
using X360.STFS;
using System.Reflection;
using System.Windows.Forms;

namespace WillowTree
{
    public enum ByteOrder
    {
        LittleEndian,
        BigEndian
    }

    public class WillowSaveGame
    {
        // Multiple users using Parallels on Mac reported that WT# was crashing at startup.
        // I narrowed it down to this line.  Application.get_ExecutablePath() is giving them 
        // a UriFormatException for some reason.  I'm rewriting this line to try to resolve it.
        // This also removes the dependence of WillowSaveGame on Windows Forms since
        // Application is a part of the System.Windows.Forms namespace.
        //public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
        public static string AppPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;
        public static string DataPath = AppPath + "Data" + Path.DirectorySeparatorChar;

        // Used for all single-byte string encodings.
        private static Encoding _singleByteEncoding; // DO NOT REFERENCE THIS DIRECTLY!
        private static Encoding SingleByteEncoding
        {
            get
            {
                // Not really thread safe, but it doesn't matter (Encoding
                // should be effectively sealed).
                if (_singleByteEncoding == null)
                {
                    // Use ISO 8859-1 (Windows 1252) encoding for all single-
                    // byte strings.
                    _singleByteEncoding = Encoding.GetEncoding(1252);
                    System.Diagnostics.Debug.Assert(_singleByteEncoding != null,
                        "singleByteEncoding != null");
                    System.Diagnostics.Debug.Assert(_singleByteEncoding.IsSingleByte,
                        "Given string encoding is not a single-byte encoding.");
                }

                return _singleByteEncoding;
            }
        }

        private static byte[] ReadBytes(BinaryReader br, int fieldSize, ByteOrder byteOrder)
        {
            byte[] bytes = br.ReadBytes(fieldSize);
            if (bytes.Length != fieldSize)
                throw new EndOfStreamException();

            if (BitConverter.IsLittleEndian)
            {
                if (byteOrder == ByteOrder.BigEndian)
                    Array.Reverse(bytes);
            }
            else
            {
                if (byteOrder == ByteOrder.LittleEndian)
                    Array.Reverse(bytes);
            }

            return bytes;
        }
        private static byte[] ReadBytes(byte[] inBytes, int fieldSize, ByteOrder byteOrder)
        {
            System.Diagnostics.Debug.Assert(inBytes != null, "inBytes != null");
            System.Diagnostics.Debug.Assert(inBytes.Length >= fieldSize, "inBytes.Length >= fieldSize");

            byte[] outBytes = new byte[fieldSize];
            Buffer.BlockCopy(inBytes, 0, outBytes, 0, fieldSize);

            if (BitConverter.IsLittleEndian)
            {
                if (byteOrder == ByteOrder.BigEndian)
                    Array.Reverse(outBytes, 0, fieldSize);
            }
            else
            {
                if (byteOrder == ByteOrder.LittleEndian)
                    Array.Reverse(outBytes, 0, fieldSize);
            }

            return outBytes;
        }

        private static float ReadSingle(BinaryReader reader, ByteOrder Endian)
        {
            return BitConverter.ToSingle(ReadBytes(reader, sizeof(float), Endian), 0);
        }
        private static int ReadInt32(BinaryReader reader, ByteOrder Endian)
        {
            return BitConverter.ToInt32(ReadBytes(reader, sizeof(int), Endian), 0);
        }
        private static short ReadInt16(BinaryReader reader, ByteOrder Endian)
        {
            return BitConverter.ToInt16(ReadBytes(reader, sizeof(short), Endian), 0);
        }
        private static List<int> ReadListInt32(BinaryReader reader, ByteOrder Endian)
        {
            int count = ReadInt32(reader, Endian);
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                int value = ReadInt32(reader, Endian);
                list.Add(value);
            }
            return list;
        }

        private static void Write(BinaryWriter writer, float value, ByteOrder endian)
        {
            writer.Write(BitConverter.ToSingle(ReadBytes(BitConverter.GetBytes(value), sizeof(float), endian), 0));
        }
        private static void Write(BinaryWriter writer, int value, ByteOrder endian)
        {
            writer.Write(BitConverter.ToInt32(ReadBytes(BitConverter.GetBytes(value), sizeof(int), endian), 0));
        }
        private static void Write(BinaryWriter writer, short value, ByteOrder Endian)
        {
            writer.Write(ReadBytes(BitConverter.GetBytes((short)value), sizeof(short), Endian));
        }

        private static byte[] GetBytesFromInt(int value, ByteOrder Endian)
        {
            return ReadBytes(BitConverter.GetBytes(value), sizeof(int), Endian);
        }

        ///<summary>Reads a string in the format used by the WSGs</summary>
        private static string ReadString(BinaryReader reader, ByteOrder endian)
        {
            int tempLengthValue = ReadInt32(reader, endian);
            if (tempLengthValue == 0)
                return string.Empty;

            string value;

            // matt911: Borderlands doesn't ever use unicode strings as far
            // as I can tell.  All text seems to be single-byte encoded with a code
            // page for the current culture so tempLengthValue is always positive.
            //
            // da_fileserver implemented the unicode string reading to agree with the 
            // way that unreal engine 3 uses it I think, but I can't test this code 
            // because I know of no place where Borderlands itself actually uses it.
            //
            // It appears to me that ReadString and WriteString are filled with a lot of
            // unnecessary code to deal with unicode, but since the code is already 
            // implemented and I haven't had any problems I'd rather leave in code that
            // is not necessary than break something that works already
            
            // Read string data (either single-byte or Unicode).
            if (tempLengthValue < 0)
            {
                // Convert the length value into a unicode byte count.
                tempLengthValue = -tempLengthValue * 2;

                // If the string length is over 4K assume that the string is invalid.
                // This prevents an out of memory exception in the case of invalid data.
                if (tempLengthValue > 4096)
                    throw new InvalidDataException("String length was too long.");

                // Read the byte data (and ensure that the number of bytes
                // read matches the number of bytes it was supposed to read--
                // BinaryReader may not return the same number of bytes read).
                byte[] data = reader.ReadBytes(tempLengthValue);
                if (data.Length != tempLengthValue)
                    throw new EndOfStreamException();

                // Convert the byte data into a string.
                value = Encoding.Unicode.GetString(data);
            }
            else
            {
                // If the string length is over 4K assume that the string is invalid.
                // This prevents an out of memory exception in the case of invalid data.
                if (tempLengthValue > 4096)
                    throw new InvalidDataException("String length was too long.");

                // Read the byte data (and ensure that the number of bytes
                // read matches the number of bytes it was supposed to read--
                // BinaryReader may not return the same number of bytes read).
                byte[] data = reader.ReadBytes(tempLengthValue);
                if (data.Length != tempLengthValue)
                    throw new EndOfStreamException();

                // Convert the byte data into a string.
                value = SingleByteEncoding.GetString(data);
            }

            // Look for the null terminator character. If not found then then string is 
            // probably corrupt.  
            int nullTerminatorIndex = value.IndexOf('\0');
            if (nullTerminatorIndex != value.Length - 1)
                throw new InvalidDataException("String was not properly terminated with a null character.");

            // Return the string, excluding the null terminator
            return value.Substring(0, nullTerminatorIndex);
        }
        ///<summary>Reads a string in the format used by the WSGs</summary>
        private static byte[] SearchForString(BinaryReader reader, ByteOrder endian)
        {
            var bytes = new List<byte>();
            long position;
            byte[] data;

            while (true)
            {
                position = reader.BaseStream.Position;
                int tempLengthValue = ReadInt32(reader, endian);
                bool isLess = false;
                string value;
                if (tempLengthValue < 0)
                {
                    tempLengthValue = -tempLengthValue * 2;
                    isLess = true;
                }
                if (tempLengthValue == 0 || tempLengthValue > 4096)
                {
                    bytes.AddRange(GetBytesFromInt(tempLengthValue, endian));
                    continue;
                }
                data = reader.ReadBytes(tempLengthValue);
                if (data.Length != tempLengthValue)
                    throw new EndOfStreamException();
                if (isLess)
                {
                    value = Encoding.Unicode.GetString(data);
                }
                else
                {
                    value = SingleByteEncoding.GetString(data);
                }
                int nullTerminatorIndex = value.IndexOf('\0');
                if (nullTerminatorIndex != value.Length - 1)
                {
                    bytes.AddRange(GetBytesFromInt(tempLengthValue, endian));
                }
                else
                {
                    //Found String put the file cursor just before 
                    reader.BaseStream.Position = position;
                    break;
                }
            }

            // Return the string, excluding the null terminator
            return bytes.ToArray();
        }
        private static void Write(BinaryWriter writer, string value, ByteOrder endian)
        {
            // Null and empty strings are treated the same (with an output
            // length of zero).
            if (string.IsNullOrEmpty(value))
            {
                writer.Write(0);
                return;
            }

            bool requiresUnicode = isUnicode(value);
            // Generate the bytes (either single-byte or Unicode, depending on input).
            if (!requiresUnicode)
            {
                // Write character length (including null terminator).
                Write(writer, value.Length + 1, endian);

                // Write single-byte encoded string.
                writer.Write(SingleByteEncoding.GetBytes(value));

                // Write null terminator.
                writer.Write((byte)0);
            }
            else
            {
                // Write character length (including null terminator).
                Write(writer, -1 - value.Length, endian);

                // Write UTF-16 encoded string.
                writer.Write(Encoding.Unicode.GetBytes(value));

                // Write null terminator.
                writer.Write((short)0);
            }
        }

        private static byte[] GetBytesFromString(string value, ByteOrder endian)
        {
            var bytes = new List<byte>();
            // Null and empty strings are treated the same (with an output
            // length of zero).
            if (string.IsNullOrEmpty(value))
            {
                return bytes.ToArray();
            }

            bool requiresUnicode = isUnicode(value);
            // Generate the bytes (either single-byte or Unicode, depending on input).
            if (!requiresUnicode)
            {
                var lenght = GetBytesFromInt(value.Length + 1, endian);
                foreach (var b in lenght)
                {
                    bytes.Add(b);
                }
                var str = SingleByteEncoding.GetBytes(value);
                foreach (var b in str)
                {
                    bytes.Add(b);
                }
                bytes.Add((byte)0);
            }
            else
            {
                var lenght = GetBytesFromInt(-1 - value.Length, endian);
                foreach (var b in lenght)
                {
                    bytes.Add(b);
                }
                var str = Encoding.Unicode.GetBytes(value);
                foreach (var b in str)
                {
                    bytes.Add(b);
                }
                bytes.Add((byte)0);
                bytes.Add((byte)0);
            }
            return bytes.ToArray();
        }
        /// <summary> Look for any non-ASCII characters in the input.</summary>
        private static bool isUnicode(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (value[i] > 256)
                    return true;

            return false;
        }

        #region Members
        public ByteOrder EndianWSG;

        public string Platform;
        public string OpenedWSG;
        public bool ContainsRawData;
        // Whether WSG should try to automatically repair or discard any invalid data
        // to recover from an invalid state.  This will allow partial data loss but 
        // may allow partial data recovery as well.
        public bool AutoRepair = false;
        public bool RequiredRepair;

        //General Info
        public string MagicHeader;
        public int VersionNumber;
        public string PLYR;
        public int RevisionNumber;
        public string Class;
        public int Level;
        public int Experience;
        public int SkillPoints;
        public int Unknown1;
        public int Cash;
        public int FinishedPlaythrough1;

        //Skill Arrays
        public int NumberOfSkills;
        public string[] SkillNames;
        public int[] LevelOfSkills;
        public int[] ExpOfSkills;
        public int[] InUse;

        //Vehicle Info
        public int Vehi1Color;
        public int Vehi2Color;
        public int Vehi1Type; // 0 = rocket, 1 = machine gun
        public int Vehi2Type;

        //Ammo Pool Arrays
        public int NumberOfPools;
        public string[] ResourcePools;
        public string[] AmmoPools;
        public float[] RemainingPools;
        public int[] PoolLevels;

        //Delegate for read String and Value
        public delegate List<string> ReadStringsFunction(BinaryReader reader, ByteOrder bo);
        public delegate List<int> ReadValuesFunction(BinaryReader reader, ByteOrder bo);

        public class RawDataInfo
        {
            public byte[] data = new byte[0];
            public int nextValue;
        }

        public class Object
        {
            public List<string> Strings = new List<string>();
            public List<int> Values = new List<int>();
            public byte[] Paddings;

            public ReadStringsFunction readStrings = null;
            public ReadValuesFunction readValues = ReadObjectValues;

            public int Quality
            {
                get { return Values[1]; }
                set { Values[1] = value; }
            }
            public int EquipedSlot
            {
                get { return Values[2]; }
                set { Values[2] = value; }
            }
            public int Level
            {
                get { return Values[3]; }
                set { Values[3] = value; }
            }
        }
        public class Item : Object
        {

            public Item()
            {
                readStrings = ReadItemStrings;
            }

            public int Quantity
            {
                get { return Values[0]; }
                set { Values[0] = value; }
            }
        }
        public class Weapon : Object
        {

            public Weapon()
            {
                readStrings = ReadWeaponStrings;
            }

            public int Ammo
            {
                get { return Values[0]; }
                set { Values[0] = value; }
            }
        }

        //Item Arrays
        public List<Item> Items = new List<Item>();
        public List<Weapon> Weapons = new List<Weapon>();

        //Backpack Info
        public int BackpackSize;
        public int EquipSlots;

        //Challenge related data
        public int ChallengeDataBlockLength;
        public int ChallengeDataBlockId;
        public int ChallengeDataLength;
        public Int16 ChallengeDataEntries;
        public struct ChallengeDataEntry
        {
            public Int16 Id;
            public Byte TypeId;
            public Int32 Value;
        }
        List<ChallengeDataEntry> Challenges;

        public byte[] ChallengeData;
        public int TotalLocations;
        public string[] LocationStrings;
        public string CurrentLocation;
        public int[] SaveInfo1to5 = new int[5];
        public int SaveNumber;
        public int[] SaveInfo7to10 = new int[4];

        public struct QuestObjective
        {
            public int Progress;
            public string Description;
        }

        public class QuestEntry
        {
            public string Name;
            public int Progress;
            public int DLCValue1;
            public int DLCValue2;
            public int NumberOfObjectives;
            public QuestObjective[] Objectives;
        }

        public class QuestTable
        { 
            public List<QuestEntry> Quests;
            public int Index;
            public string CurrentQuest;
            public int TotalQuests;
        }

        //Quest Arrays/Info
        //public class QuestTable
        //{
        //    public int Index;
        //    public string CurrentQuest;
        //    public int TotalQuests;
        //    public string[] QuestStrings;
        //    public int[,] QuestValues;
        //    public string[,] QuestSubfolders;
        //};

        public int NumberOfQuestLists;
        public List<QuestTable> QuestLists = new List<QuestTable>();

        //More unknowns and color info.
        public int TotalPlayTime;
        public string LastPlayedDate;
        public string CharacterName;
        public int Color1;
        public int Color2;
        public int Color3;
        public int Head;

        public byte[] Unknown2; //Seam to be a fixed Array of Boolean since i saw only 01 or 00 in it Only on 38 >

        public List<int> PromoCodesUsed;
        public List<int> PromoCodesRequiringNotification;

        //Echo Info
        public int NumberOfEchoLists;
        public class EchoEntry
        {
            public string Name;
            public int DLCValue1;
            public int DLCValue2;
        }
        public class EchoTable
        {
            public int Index;
            public int TotalEchoes;
            public List<EchoEntry> Echoes;
        };
        public List<EchoTable> EchoLists = new List<EchoTable>();

        // Temporary lists used for primary pack data when the inventory is split
        public List<Item> Items1 = new List<Item>();
        public List<Weapon> Weapons1 = new List<Weapon>();
        // Temporary lists used for primary pack data when the inventory is split
        public List<Item> Items2 = new List<Item>();
        public List<Weapon> Weapons2 = new List<Weapon>();

        public byte[] Unknown3; //80 bytes of 00 at the end 

        public class DLC_Data
        {
            public const int Section1Id = 0x43211234;
            public const int Section2Id = 0x02151984;
            public const int Section3Id = 0x32235947;
            public const int Section4Id = 0x234BA901;

            public bool HasSection1;
            public bool HasSection2;
            public bool HasSection3;
            public bool HasSection4;

            public List<DLCSection> DataSections;

            public int DLC_Size;

            // DLC Section 1 Data (bank data)
            public byte DLC_Unknown1;  // Read only flag. Always resets to 1 in ver 1.41.  Probably CanAccessBank.
            public int BankSize;
            public List<BankEntry> BankInventory = new List<BankEntry>();
            // DLC Section 2 Data (don't know)
            public int DLC_Unknown2; // All four of these are boolean flags.
            public int DLC_Unknown3; // If you set them to any value except 0
            public int DLC_Unknown4; // the game will rewrite them as 1.
            public int SkipDLC2Intro; //
            // DLC Section 3 Data (related to the level cap.  removing this section will delevel your character to 50)
            public byte DLC_Unknown5;  // Read only flag. Always resets to 1 in ver 1.41.  Probably CanExceedLevel50
            // DLC Section 4 Data (DLC backpack)
            public byte SecondaryPackEnabled;  // Read only flag. Always resets to 1 in ver 1.41.
            public int NumberOfItems;
            public List<List<string>> ItemStrings = new List<List<string>>();
            public List<List<int>> ItemValues = new List<List<int>>();
            public int NumberOfWeapons;
            public List<List<string>> WeaponStrings = new List<List<string>>();
            public List<List<int>> WeaponValues = new List<List<int>>();
        }

        public DLC_Data DLC = new DLC_Data();

        public class DLCSection
        {
            public int Id;
            public byte[] RawData;
            public byte[] BaseData; // used temporarily in SaveWSG to store the base data for a section as a byte array
        }

        //Xbox 360 only
        public long ProfileID;
        public byte[] DeviceID;
        public byte[] CON_Image;
        public string Title_Display;
        public string Title_Package;
        public uint TitleID = 1414793191;
        #endregion


        public static uint GetXboxTitleID(Stream inputWSG)
        {
            BinaryReader br = new BinaryReader(inputWSG);
            byte[] fileInMemory = br.ReadBytes((int)inputWSG.Length);
            if (fileInMemory.Count() != inputWSG.Length)
                throw new EndOfStreamException();

            try
            {
                STFSPackage CON = new STFSPackage(new DJsIO(fileInMemory, true), new X360.Other.LogRecord());
                return CON.Header.TitleID;
            }
            catch { return 0; }
        }

        ///<summary>Reports back the expected platform this WSG was created on.</summary>
        public static string WSGType(Stream inputWSG)
        {
            BinaryReader saveReader = new BinaryReader(inputWSG);

            byte byte1 = saveReader.ReadByte();
            byte byte2 = saveReader.ReadByte();
            byte byte3 = saveReader.ReadByte();
            if (byte1 == 'C' && byte2 == 'O' && byte3 == 'N')
            {
                byte byte4 = saveReader.ReadByte();
                if (byte4 == ' ')
                {
                    // This is a really lame way to check for the WSG data...
                    saveReader.BaseStream.Seek(53244, SeekOrigin.Current);

                    byte1 = saveReader.ReadByte();
                    byte2 = saveReader.ReadByte();
                    byte3 = saveReader.ReadByte();
                    if (byte1 == 'W' && byte2 == 'S' && byte3 == 'G')
                    {
                        saveReader.BaseStream.Seek(0x360, SeekOrigin.Begin);
                        uint titleID = ((uint)saveReader.ReadByte() << 24) + ((uint)saveReader.ReadByte() << 16) +
                            ((uint)saveReader.ReadByte() << 8) + (uint)saveReader.ReadByte();


//                        uint titleID = GetXboxTitleID(inputWSG);
                        switch (titleID) 
                        {
                            case 1414793191: return "X360";
                            case 1414793318: return "X360JP";
                            default: return "unknown";
                        }
                    }
                }
            }
            else if (byte1 == 'W' && byte2 == 'S' && byte3 == 'G')
            {
                int wsgVersion = saveReader.ReadInt32();

                // BinaryReader.ReadInt32 always uses little-endian byte order.
                bool littleEndian;
                switch (wsgVersion)
                {
                    case 0x02000000: // 33554432 decimal
                        littleEndian = false;
                        break;
                    case 0x00000002:
                        littleEndian = true;
                        break;
                    default:
                        return "unknown";
                }

                if (littleEndian)
                    return "PC";
                else
                    return "PS3";
            }

            return "Not WSG";
        }

        ///<summary>Extracts a WSG from a CON (Xbox 360 Container File).</summary>
        public MemoryStream OpenXboxWSGStream(Stream InputX360File)
        {
            BinaryReader br = new BinaryReader(InputX360File);
            byte[] fileInMemory = br.ReadBytes((int)InputX360File.Length);
            if (fileInMemory.Count() != InputX360File.Length)
                throw new EndOfStreamException();

            try
            {
                STFSPackage CON = new STFSPackage(new DJsIO(fileInMemory, true), new X360.Other.LogRecord());
                //DJsIO Extract = new DJsIO(true);
                //CON.FileDirectory[0].Extract(Extract);
                ProfileID = CON.Header.ProfileID;
                DeviceID = CON.Header.DeviceID;
                
                //DJsIO Save = new DJsIO("C:\\temp.sav", DJFileMode.Create, true);
                //Save.Write(Extract.ReadStream());
                //Save.Close();
                //byte[] nom = CON.GetFile("SaveGame.sav").GetEntryData(); 
                return new MemoryStream(CON.GetFile("SaveGame.sav").GetTempIO(true).ReadStream(), false);
            }
            catch
            {
                try
                {
                    DJsIO Manual = new DJsIO(fileInMemory, true);
                    Manual.ReadBytes(881);
                    ProfileID = Manual.ReadInt64();
                    Manual.ReadBytes(132);
                    DeviceID = Manual.ReadBytes(20);
                    Manual.ReadBytes(48163);
                    int size = Manual.ReadInt32();
                    Manual.ReadBytes(4040);
                    return new MemoryStream(Manual.ReadBytes(size), false);
                }
                catch { return null; }
            }
        }

        ///<summary>Reads savegame data from a file</summary>
        public void LoadWSG(string inputFile)
        {
            using (FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Platform = WSGType(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);

                if (string.Equals(Platform, "X360", StringComparison.Ordinal) ||
                    string.Equals(Platform, "X360JP", StringComparison.Ordinal))
                {
                    using (MemoryStream x360FileStream = OpenXboxWSGStream(fileStream))
                    {
                        ReadWSG(x360FileStream);
                    }
                }
                else if (string.Equals(Platform, "PS3", StringComparison.Ordinal) ||
                    string.Equals(Platform, "PC", StringComparison.Ordinal))
                {
                    ReadWSG(fileStream);
                }
                else
                {
                    throw new FileFormatException("Input file is not a WSG (platform is " + Platform + ").");
                }

                OpenedWSG = inputFile;
            }
        }

        private void BuildXboxPackage(string packageFileName, string saveFileName, int locale)
        {
            CreateSTFS Package = new CreateSTFS();

            Package.STFSType = STFSType.Type1;
            Package.HeaderData.ProfileID = this.ProfileID;
            Package.HeaderData.DeviceID = this.DeviceID;

            Assembly newAssembly = Assembly.GetExecutingAssembly();
            // WARNING: GetManifestResourceStream is case-sensitive.
            Stream WT_Icon = newAssembly.GetManifestResourceStream("WillowTree.Resources.WT_CON.png");
            Package.HeaderData.ContentImage = System.Drawing.Image.FromStream(WT_Icon);
            Package.HeaderData.PackageImage = Package.HeaderData.ContentImage;
            Package.HeaderData.Title_Display = this.CharacterName + " - Level " + this.Level + " - " + CurrentLocation;
            Package.HeaderData.Title_Package = "Borderlands";
            switch (locale)
            {
                case 1: // US or International version
                    Package.HeaderData.Title_Package = "Borderlands";
                    Package.HeaderData.TitleID = 1414793191;
                    break;                   
                case 2: // JP version
                    Package.HeaderData.Title_Package = "Borderlands (JP)";
                    Package.HeaderData.TitleID = 1414793318;
                    break;
            }
            Package.AddFile(saveFileName, "SaveGame.sav");


            STFSPackage CON = new STFSPackage(Package, new RSAParams(DataPath + "KV.bin"), packageFileName, new X360.Other.LogRecord());

            CON.FlushPackage(new RSAParams(DataPath + "KV.bin"));
            CON.CloseIO();
            WT_Icon.Close();
        }

        private enum PADDINGTYPE
        {
            BYTE,
            INT16,
            INT32,
            LONG,
            UNKNOWN
        }

        private PADDINGTYPE GetPaddingTypeFromSize(int size)
        {
            switch (size)
            {
                case 1:
                    return PADDINGTYPE.BYTE;
                case sizeof(Int16):
                    return PADDINGTYPE.INT16;
                case sizeof(Int32):
                    return PADDINGTYPE.INT32;
                case sizeof(long):
                    return PADDINGTYPE.LONG;
                default:
                    return PADDINGTYPE.UNKNOWN;
            }
        }

        private RawDataInfo CheckPadding(BinaryReader reader, int paddingSize)
        {
            Console.WriteLine(paddingSize);
            var paddingType = GetPaddingTypeFromSize(paddingSize); 
            var rd = new RawDataInfo();
            if (paddingType == PADDINGTYPE.UNKNOWN)
                return rd;
            int count = -paddingSize;
            rd.data = ReadBytes(reader, paddingSize > 4 ? 4 : paddingSize, EndianWSG);
            bool findString = false;
            while (!findString && rd.data.Length < sizeof(int) * 2)
            {
                var d = ReadBytes(reader, paddingSize > 4 ? 4 : paddingSize, EndianWSG);

                long extraPaddingData = 0;
                switch (paddingType)
                {
                    case PADDINGTYPE.BYTE:
                        Console.WriteLine("BYTE");
                        //Looking for non null byte
                        extraPaddingData = d[0];
                        rd.nextValue = d[0];
                        if (extraPaddingData != 0)
                        {
                            Console.WriteLine("No padding ->" + extraPaddingData);
                            reader.BaseStream.Position -= paddingSize;
                            findString = true;
                        }
                        else
                        {
                            Console.WriteLine("Padding ->" + extraPaddingData);
                            rd.data = rd.data.Concat(d).ToArray();
                        }
                        break;
                    case PADDINGTYPE.INT16:
                        Console.WriteLine("INT16");
                        extraPaddingData = BitConverter.ToInt16(ReadBytes(d, sizeof(Int16), EndianWSG), 0);
                        break;
                    case PADDINGTYPE.INT32:
                    case PADDINGTYPE.LONG:
                        Console.WriteLine("INT32");
                        //Looking for next string
                        rd.nextValue = BitConverter.ToInt32(ReadBytes(d, sizeof(int), EndianWSG), 0);
                        extraPaddingData = rd.nextValue;
                        //There is not item smaller or langer than this
                        if (extraPaddingData > 10 && extraPaddingData < 128)
                        {
                            Console.WriteLine("No padding ->" + extraPaddingData);
                            reader.BaseStream.Position -= 4;
                            findString = true;
                        }
                        else
                        {
                            Console.WriteLine("Padding ->" + extraPaddingData);
                            rd.data = rd.data.Concat(d).ToArray();
                        }
                        break;
                    default:
                        return rd;
                }
                
            }
            return rd;
        }

        private static List<string> ReadItemStrings(BinaryReader reader, ByteOrder bo)
        {
            List<string> strings = new List<string>();
            for (int TotalStrings = 0; TotalStrings < 9; TotalStrings++)
                strings.Add(ReadString(reader, bo));
            foreach (var item in strings)
            {
                Console.WriteLine(item);
            }
            return strings;
        }

        private static List<string> ReadWeaponStrings(BinaryReader reader, ByteOrder bo)
        {
            List<string> strings = new List<string>();
            for (int TotalStrings = 0; TotalStrings < 14; TotalStrings++)
                strings.Add(ReadString(reader, bo));
            foreach (var item in strings)
            {
                Console.WriteLine(item);
            }
            return strings;
        }

        private static List<int> ReadObjectValues(BinaryReader reader, ByteOrder bo)
        {
            List<int> values = new List<int>();
            Int32 Value1 = ReadInt32(reader, bo);
            UInt32 tempLevelQuality = (UInt32)ReadInt32(reader, bo);
            Int16 Quality = (Int16)(tempLevelQuality % (UInt32)65536);
            Int16 Level = (Int16)(tempLevelQuality / (UInt32)65536);
            Int32 EquippedSlot = ReadInt32(reader, bo);

            values.Add(Value1);
            values.Add(Quality);
            values.Add(EquippedSlot);
            values.Add(Level);
            return values;
        }

        private T ReadObject<T>(BinaryReader reader, ref RawDataInfo rd, int paddingSize, bool isDLC) where T : Object, new()
        {
            var item = new T();
            item.Strings = item.readStrings(reader, EndianWSG);
            item.Values = item.readValues(reader, EndianWSG);
            if (RevisionNumber > 38 && !isDLC)
            {
                item.Paddings = GetBytesFromInt(ReadInt32(reader, EndianWSG), EndianWSG);
                item.Paddings = item.Paddings.Concat(GetBytesFromInt(ReadInt32(reader, EndianWSG), EndianWSG)).ToArray();
            }
            return item;
        }

        private void ReadObjects<T>(BinaryReader reader, ref List<T> Objects, int groupSize, int paddingSize, bool isDLC) where T : Object, new()
        {
            RawDataInfo rd = null;
            for (int Progress = 0; Progress < groupSize; Progress++)
            {
                Console.WriteLine(Progress + "/" + groupSize);
                var item = ReadObject<T>(reader, ref rd, paddingSize, isDLC);
                if (Progress < groupSize - 1)
                {
                    if (item.Paddings == null)
                        item.Paddings = new byte[0];
                    item.Paddings = item.Paddings.Concat(SearchForString(reader, EndianWSG)).ToArray();
                }
                Objects.Add(item);
            }
        }

        public void SaveWSG(string filename)
        {
            if (this.Platform == "PS3" || this.Platform == "PC")
            {
                using (BinaryWriter Save = new BinaryWriter(new FileStream(filename, FileMode.Create)))
                {
                    Save.Write(this.WriteWSG());
                }
            }

            else if (this.Platform == "X360")
            {
                string tempSaveName = filename + ".temp";
                using (BinaryWriter Save = new BinaryWriter(new FileStream(tempSaveName, FileMode.Create)))
                {
                    Save.Write(this.WriteWSG());
                }

                BuildXboxPackage(filename, tempSaveName, 1);
                File.Delete(tempSaveName);
            }

            else if (this.Platform == "X360JP")
            {
                string tempSaveName = filename + ".temp";
                using (BinaryWriter Save = new BinaryWriter(new FileStream(tempSaveName, FileMode.Create)))
                {
                    Save.Write(this.WriteWSG());
                }

                BuildXboxPackage(filename, tempSaveName, 2);
                File.Delete(tempSaveName);
            }

        }

        private static bool EOF(BinaryReader binaryReader)
        {
            var bs = binaryReader.BaseStream;
            return (bs.Position == bs.Length);
        }

        ///<summary>Read savegame data from an open stream</summary>
        public void ReadWSG(Stream fileStream)
        {
            BinaryReader TestReader = new BinaryReader(fileStream, Encoding.ASCII);

            ContainsRawData = false;
            RequiredRepair = false;
            MagicHeader = new string(TestReader.ReadChars(3));
            VersionNumber = TestReader.ReadInt32();

            if (VersionNumber == 2)
                EndianWSG = ByteOrder.LittleEndian;
            else if (VersionNumber == 0x02000000)
            {
                VersionNumber = 2;
                EndianWSG = ByteOrder.BigEndian;
            }
            else
                throw new FileFormatException("WSG version number does match any known version (" + VersionNumber + ").");
         
            PLYR = new string(TestReader.ReadChars(4));
            if (!string.Equals(PLYR, "PLYR", StringComparison.Ordinal))
                throw new FileFormatException("Player header does not match expected value.");

            RevisionNumber = ReadInt32(TestReader, EndianWSG);
            Class = ReadString(TestReader, EndianWSG);
            Level = ReadInt32(TestReader, EndianWSG);
            Experience = ReadInt32(TestReader, EndianWSG);
            SkillPoints = ReadInt32(TestReader, EndianWSG);
            Unknown1 = ReadInt32(TestReader, EndianWSG);
            Cash = ReadInt32(TestReader, EndianWSG);
            FinishedPlaythrough1 = ReadInt32(TestReader, EndianWSG);
            NumberOfSkills = ReadSkills(TestReader, EndianWSG);
            Vehi1Color = ReadInt32(TestReader, EndianWSG);
            Vehi2Color = ReadInt32(TestReader, EndianWSG);
            Vehi1Type = ReadInt32(TestReader, EndianWSG);
            Vehi2Type = ReadInt32(TestReader, EndianWSG);
            NumberOfPools = ReadAmmo(TestReader, EndianWSG);
            ReadObjects(TestReader, ref Items, ReadInt32(TestReader, EndianWSG), sizeof(int) * 2, false);
            BackpackSize = ReadInt32(TestReader, EndianWSG);
            EquipSlots = ReadInt32(TestReader, EndianWSG);
            ReadObjects(TestReader, ref Weapons, ReadInt32(TestReader, EndianWSG), sizeof(int) * 2, false);

            ChallengeDataBlockLength = ReadInt32(TestReader, EndianWSG);
            byte[] challengeDataBlock = TestReader.ReadBytes(ChallengeDataBlockLength);
            if (challengeDataBlock.Length != ChallengeDataBlockLength)
                throw new EndOfStreamException();

            using (BinaryReader challengeReader = new BinaryReader(new MemoryStream(challengeDataBlock, false), Encoding.ASCII))
            {
                ChallengeDataBlockId = ReadInt32(challengeReader, EndianWSG);
                ChallengeDataLength = ReadInt32(challengeReader, EndianWSG);
                ChallengeDataEntries = ReadInt16(challengeReader, EndianWSG);
                Challenges = new List<ChallengeDataEntry>();
                for (int i = 0; i < ChallengeDataEntries; i++)
                {
                    ChallengeDataEntry challenge;
                    challenge.Id = ReadInt16(challengeReader, EndianWSG);
                    challenge.TypeId = challengeReader.ReadByte();
                    challenge.Value = ReadInt32(challengeReader, EndianWSG);
                    Challenges.Add(challenge);
                }
            }

            TotalLocations = ReadLocations(TestReader, EndianWSG);
            CurrentLocation = ReadString(TestReader, EndianWSG);
            SaveInfo1to5[0] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[1] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[2] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[3] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[4] = ReadInt32(TestReader, EndianWSG);
            SaveNumber = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[0] = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[1] = ReadInt32(TestReader, EndianWSG);
            NumberOfQuestLists = ReadQuests(TestReader, EndianWSG);

            TotalPlayTime = ReadInt32(TestReader, EndianWSG);
            LastPlayedDate = ReadString(TestReader, EndianWSG); //YYYYMMDDHHMMSS
            CharacterName = ReadString(TestReader, EndianWSG);
            Color1 = ReadInt32(TestReader, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Color2 = ReadInt32(TestReader, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Color3 = ReadInt32(TestReader, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Head = ReadInt32(TestReader, EndianWSG);

            if (RevisionNumber > 38)
            {
                Unknown2 = ReadBytes(TestReader, 93, EndianWSG);
            }
            else
            {
                PromoCodesUsed = ReadListInt32(TestReader, EndianWSG);
                PromoCodesRequiringNotification = ReadListInt32(TestReader, EndianWSG);
            }
            NumberOfEchoLists = ReadEchoes(TestReader, EndianWSG);

            DLC.DataSections = new List<DLCSection>();
            DLC.DLC_Size = ReadInt32(TestReader, EndianWSG);
            byte[] dlcDataBlock = TestReader.ReadBytes(DLC.DLC_Size);
            if (dlcDataBlock.Length != DLC.DLC_Size)
                throw new EndOfStreamException();

            using (BinaryReader dlcDataReader = new BinaryReader(new MemoryStream(dlcDataBlock, false), Encoding.ASCII))
            {
                int RemainingBytes = DLC.DLC_Size;
                while (RemainingBytes > 0)
                {
                    DLCSection Section = new DLCSection();
                    Section.Id = ReadInt32(dlcDataReader, EndianWSG);
                    int SectionLength = ReadInt32(dlcDataReader, EndianWSG);
                    long SectionStartPos = (int)dlcDataReader.BaseStream.Position;
                    switch (Section.Id)
                    {
                        case DLC_Data.Section1Id: // 0x43211234
                            DLC.HasSection1 = true;
                            DLC.DLC_Unknown1 = dlcDataReader.ReadByte();
                            DLC.BankSize = ReadInt32(dlcDataReader, EndianWSG);
                            int bankEntriesCount = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.BankInventory = new List<BankEntry>();
                            Console.WriteLine("==========ENTER BANK===========");
                            for (int i = 0; i < bankEntriesCount; i++)
                                DLC.BankInventory.Add(CreateBankEntry(dlcDataReader));
                            Console.WriteLine("==========EXIT BANK===========");
                            break;
                        case DLC_Data.Section2Id: // 0x02151984
                            DLC.HasSection2 = true;
                            DLC.DLC_Unknown2 = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.DLC_Unknown3 = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.DLC_Unknown4 = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.SkipDLC2Intro = ReadInt32(dlcDataReader, EndianWSG);
                            break;
                        case DLC_Data.Section3Id: // 0x32235947
                            DLC.HasSection3 = true;
                            DLC.DLC_Unknown5 = dlcDataReader.ReadByte();
                            break;
                        case DLC_Data.Section4Id: // 0x234ba901
                            DLC.HasSection4 = true;
                            DLC.SecondaryPackEnabled = dlcDataReader.ReadByte();
 
                            try
                            {
                                ReadObjects(dlcDataReader, ref Items, ReadInt32(dlcDataReader, EndianWSG), sizeof(int), true);
                            }
                            catch
                            {
                                // The data was invalid so the processing ran into an exception.
                                // See if the user wants to ignore the invalid data and just try
                                // to recover partial data.  If not, just re-throw the exception.
                                if (AutoRepair == false)
                                    throw;

                                // Set the flag to indicate that repair was required to load the savegame
                                RequiredRepair = true;

                                // Make sure there's no half-processed item that only added
                                // to ItemStrings but crashed before adding ItemValues.
                                // Remove the excess if there is.
                                //if (ItemStrings.Count > ItemValues.Count)
                                //    ItemStrings.RemoveAt(ItemStrings.Count - 1);

                                // Figure out how many weapons were successfully read
                                //DLC.NumberOfWeapons = ItemStrings.Count - NumberOfItems;
                                
                                // If the data is invalid here, the whole DLC weapon list is invalid so
                                // set its length to 0 and be done
                                DLC.NumberOfWeapons = 0;

                                // Skip to the end of the section to discard any raw data that is left over
                                dlcDataReader.BaseStream.Position = SectionStartPos + SectionLength;
                            }
                            //NumberOfItems += DLC.NumberOfItems;

                            try
                            {
                                ReadObjects(dlcDataReader, ref Weapons, ReadInt32(dlcDataReader, EndianWSG), sizeof(int), true);
                            }
                            catch
                            {
                                // The data was invalid so the processing ran into an exception.
                                // See if the user wants to ignore the invalid data and just try
                                // to recover partial data.  If not, just re-throw the exception.
                                if (AutoRepair == false)
                                    throw;

                                // Set the flag to indicate that repair was required to load the savegame
                                RequiredRepair = true;

                                // Make sure there's no half-processed weapon that only added
                                // to WeaponStrings but crashed before adding WeaponValues.
                                // Remove the excess if there is.
                                //if (WeaponStrings.Count > WeaponValues.Count)
                                //    WeaponStrings.RemoveAt(WeaponStrings.Count - 1);

                                // Figure out how many weapons were successfully read
                                //DLC.NumberOfWeapons = WeaponStrings.Count - NumberOfWeapons;

                                // Skip to the end of the section to discard any raw data that is left over
                                dlcDataReader.BaseStream.Position = SectionStartPos + SectionLength;
                            }
                            //NumberOfWeapons += DLC.NumberOfWeapons;
                            break;
                        default:
                            break;
                    }

                    // I don't pretend to know if any of the DLC sections will ever expand
                    // and store more data.  RawData stores any extra data at the end of
                    // the known data in any section and stores the entirety of sections 
                    // with unknown ids in a buffer in its raw byte order dependent form.
                    int RawDataCount = SectionLength - (int)(dlcDataReader.BaseStream.Position - SectionStartPos);

                    Section.RawData = dlcDataReader.ReadBytes(RawDataCount);
                    if (RawDataCount > 0)
                        ContainsRawData = true;
                    RemainingBytes -= SectionLength + 8;
                    DLC.DataSections.Add(Section);
                }
                if (RevisionNumber > 38)
                {
                    //Padding at the end of file, don't know exactly why
                    var temp = new List<byte>();
                    while (!EOF(TestReader))
                    {
                        temp.Add(ReadBytes(TestReader, 1, EndianWSG)[0]);
                    }
                    Unknown3 = temp.ToArray();
                }
            }
        }

        private int ReadEchoes(BinaryReader reader, ByteOrder EndianWSG)
        {
            int echoListCount = ReadInt32(reader, EndianWSG);

            EchoLists.Clear();
            for (int i = 0; i < echoListCount; i++)
            {
                EchoTable et = new EchoTable();
                et.Index = ReadInt32(reader, EndianWSG);
                et.TotalEchoes = ReadInt32(reader, EndianWSG);
                et.Echoes = new List<EchoEntry>();

                for (int echoIndex = 0; echoIndex < et.TotalEchoes; echoIndex++)
                {
                    EchoEntry ee = new EchoEntry();
                    ee.Name = ReadString(reader, EndianWSG);
                    ee.DLCValue1 = ReadInt32(reader, EndianWSG);
                    ee.DLCValue2 = ReadInt32(reader, EndianWSG);
                    et.Echoes.Add(ee);
                }
                EchoLists.Add(et);
            }
            return echoListCount;
        }
        
        private int ReadQuests(BinaryReader reader, ByteOrder EndianWSG)
        {
            int NumberOfQuestLists = ReadInt32(reader, EndianWSG);

            QuestLists.Clear();
            for (int listIndex = 0; listIndex < NumberOfQuestLists; listIndex++)
            {
                QuestTable qt = new QuestTable();
                qt.Index = ReadInt32(reader, EndianWSG);
                qt.CurrentQuest = ReadString(reader, EndianWSG);
                qt.TotalQuests = ReadInt32(reader, EndianWSG);
                qt.Quests = new List<QuestEntry>();
                int questCount = qt.TotalQuests;

                for (int questIndex = 0; questIndex < questCount; questIndex++)
                {
                    QuestEntry qe = new QuestEntry();
                    qe.Name = ReadString(reader, EndianWSG);
                    qe.Progress = ReadInt32(reader, EndianWSG);
                    qe.DLCValue1 = ReadInt32(reader, EndianWSG);
                    qe.DLCValue2 = ReadInt32(reader, EndianWSG);

                    int objectiveCount = ReadInt32(reader, EndianWSG);
                    qe.NumberOfObjectives = objectiveCount;
                    qe.Objectives = new QuestObjective[objectiveCount];

                    for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                    {
                        qe.Objectives[objectiveIndex].Description = ReadString(reader, EndianWSG);
                        qe.Objectives[objectiveIndex].Progress = ReadInt32(reader, EndianWSG);
                    }
                    qt.Quests.Add(qe);
                }

                if (qt.CurrentQuest == "None" & qt.Quests.Count > 0) 
                    qt.CurrentQuest = qt.Quests[0].Name;

                QuestLists.Add(qt);
            }
            return NumberOfQuestLists;
        }

        private int ReadSkills(BinaryReader reader, ByteOrder EndianWSG)
        {
            int skillsCount = ReadInt32(reader, EndianWSG);

            string[] TempSkillNames = new string[skillsCount];
            int[] TempLevelOfSkills = new int[skillsCount];
            int[] TempExpOfSkills = new int[skillsCount];
            int[] TempInUse = new int[skillsCount];

            for (int Progress = 0; Progress < skillsCount; Progress++)
            {
                TempSkillNames[Progress] = ReadString(reader, EndianWSG);
                TempLevelOfSkills[Progress] = ReadInt32(reader, EndianWSG);
                TempExpOfSkills[Progress] = ReadInt32(reader, EndianWSG);
                TempInUse[Progress] = ReadInt32(reader, EndianWSG);
            }

            SkillNames = TempSkillNames;
            LevelOfSkills = TempLevelOfSkills;
            ExpOfSkills = TempExpOfSkills;
            InUse = TempInUse;

            return skillsCount;
        }

        private int ReadAmmo(BinaryReader reader, ByteOrder EndianWSG)
        {
            int poolsCount = ReadInt32(reader, EndianWSG);
 
            string[] TempResourcePools = new string[poolsCount];
            string[] TempAmmoPools = new string[poolsCount];
            float[] TempRemainingPools = new float[poolsCount];
            int[] TempPoolLevels = new int[poolsCount];

            for (int Progress = 0; Progress < poolsCount; Progress++)
            {
                TempResourcePools[Progress] = ReadString(reader, EndianWSG);
                TempAmmoPools[Progress] = ReadString(reader, EndianWSG);
                TempRemainingPools[Progress] = ReadSingle(reader, EndianWSG);
                TempPoolLevels[Progress] = ReadInt32(reader, EndianWSG);
            }

            ResourcePools = TempResourcePools;
            AmmoPools = TempAmmoPools;
            RemainingPools = TempRemainingPools;
            PoolLevels = TempPoolLevels;

            return poolsCount;
        }

        //private int ReadItems(BinaryReader reader, ByteOrder EndianWSG)
        //{
        //    int itemCount = ReadInt32(reader, EndianWSG);

        //    for (int Progress = 0; Progress < itemCount; Progress++)
        //    {
        //        List<string> strings = new List<string>();
        //        for (int TotalStrings = 0; TotalStrings < 9; TotalStrings++)
        //            strings.Add(ReadString(reader, EndianWSG));
        //        ItemStrings.Add(strings);

        //        List<int> values = new List<int>();
        //        Int32 Value1 = ReadInt32(reader, EndianWSG);
        //        UInt32 tempLevelQuality = (UInt32)ReadInt32(reader, EndianWSG);
        //        Int16 Quality = (Int16)(tempLevelQuality % (UInt32)65536);
        //        Int16 Level = (Int16)(tempLevelQuality / (UInt32)65536);
        //        Int32 EquippedSlot = ReadInt32(reader, EndianWSG);
        //        values.Add(Value1);
        //        values.Add(Quality);
        //        values.Add(EquippedSlot);
        //        values.Add(Level);
        //        ItemValues.Add(values);
        //    }
        //    return itemCount;
        //}

        //private int ReadWeapons(BinaryReader reader, ByteOrder EndianWSG)
        //{
        //    int weaponCount = ReadInt32(reader, EndianWSG);

        //    for (int Progress = 0; Progress < weaponCount; Progress++)
        //    {
        //        List<string> strings = new List<string>();

        //        for (int TotalStrings = 0; TotalStrings < 14; TotalStrings++)
        //            strings.Add(ReadString(reader, EndianWSG));

        //        Int32 AmmoCount = ReadInt32(reader, EndianWSG);
        //        UInt32 tempLevelQuality = (UInt32)ReadInt32(reader, EndianWSG);
        //        Int16 Level = (Int16)(tempLevelQuality / (UInt32)65536);
        //        Int16 Quality = (Int16)(tempLevelQuality % (UInt32)65536);
        //        Int32 EquippedSlot = ReadInt32(reader, EndianWSG);

        //        List<int> values = new List<int>() {
        //            AmmoCount,
        //            Quality,
        //            EquippedSlot,
        //            Level
        //        };

        //        WeaponStrings.Add(strings);
        //        WeaponValues.Add(values);
        //    }
        //    return weaponCount;
        //}

        private int ReadLocations(BinaryReader reader, ByteOrder EndianWSG)
        {
            int locationCount = ReadInt32(reader, EndianWSG);
            string[] tempLocationStrings = new string[locationCount];

            for (int Progress = 0; Progress < locationCount; Progress++)
                tempLocationStrings[Progress] = ReadString(reader, EndianWSG);

            LocationStrings = tempLocationStrings;
            return locationCount;
        }

        public void DiscardRawData()
        {
            // Make a list of all the known data sections to compare against.
            List<int> KnownSectionIds = new List<int>() {
                DLC_Data.Section1Id,
                DLC_Data.Section2Id,
                DLC_Data.Section3Id,
                DLC_Data.Section4Id,
            };

            // Traverse the list of data sections from end to beginning because when
            // an item gets deleted it does not affect the index of the ones before it,
            // but it does change the index of the ones after it.
            for (int i = this.DLC.DataSections.Count - 1; i >= 0; i--)
            {
                WillowSaveGame.DLCSection section = this.DLC.DataSections[i];

                if (KnownSectionIds.Contains(section.Id))
                {
                    // clear the raw data in this DLC data section
                    section.RawData = new byte[0];
                }
                else
                {
                    // if the section id is not recognized remove it completely
                    section.RawData = null;
                    this.DLC.DataSections.RemoveAt(i);
                }
            }

            // Now that all the raw data has been removed, reset the raw data flag
            this.ContainsRawData = false;
        }

        private static void Write(BinaryWriter writer, byte[] value, ByteOrder endian)
        {
            writer.Write(value);
        }

        private void WriteValues(BinaryWriter Out, List<int> values)
        {
            Write(Out, values[0], EndianWSG);
            UInt32 tempLevelQuality = (UInt16)values[1] + (UInt16)values[3] * (UInt32)65536;
            Write(Out, (Int32)tempLevelQuality, EndianWSG);
            Write(Out, values[2], EndianWSG);
        }

        private void WriteStrings(BinaryWriter Out, List<string> strings)
        {
            foreach (var s in strings)
            {
                Write(Out, s, EndianWSG);
            }
        }

        private void WritePaddings(BinaryWriter Out, byte[] data)
        {
            Write(Out, data, EndianWSG);
        }

        private void WriteObject<T>(BinaryWriter Out, T obj) where T : Object
        {
            WriteStrings(Out, obj.Strings);
            WriteValues(Out, obj.Values);
            if (obj.Paddings != null)
                WritePaddings(Out, obj.Paddings);
        }

        private void WriteObjects<T>(BinaryWriter Out, List<T> objs) where T : Object
        {
            Write(Out, objs.Count, EndianWSG);
            foreach (var obj in objs)
            {
                WriteObject(Out, obj);
            }
        }

        ///<summary>Save the current data to a WSG as a byte[]</summary>
        public byte[] WriteWSG()
        {
            MemoryStream OutStream = new MemoryStream();
            BinaryWriter Out = new BinaryWriter(OutStream);

            SplitInventoryIntoPacks();

            Out.Write(Encoding.ASCII.GetBytes(MagicHeader));
            Write(Out, VersionNumber, EndianWSG);
            Out.Write(Encoding.ASCII.GetBytes(PLYR));
            Write(Out, RevisionNumber, EndianWSG);
            Write(Out, Class, EndianWSG);
            Write(Out, Level, EndianWSG);
            Write(Out, Experience, EndianWSG);
            Write(Out, SkillPoints, EndianWSG);
            Write(Out, Unknown1, EndianWSG);
            Write(Out, Cash, EndianWSG);
            Write(Out, FinishedPlaythrough1, EndianWSG);
            Write(Out, NumberOfSkills, EndianWSG);

            for (int Progress = 0; Progress < NumberOfSkills; Progress++) //Write Skills
            {
                Write(Out, SkillNames[Progress], EndianWSG);
                Write(Out, LevelOfSkills[Progress], EndianWSG);
                Write(Out, ExpOfSkills[Progress], EndianWSG);
                Write(Out, InUse[Progress], EndianWSG);
            }

            Write(Out, Vehi1Color, EndianWSG);
            Write(Out, Vehi2Color, EndianWSG);
            Write(Out, Vehi1Type, EndianWSG);
            Write(Out, Vehi2Type, EndianWSG);
            Write(Out, NumberOfPools, EndianWSG);

            for (int Progress = 0; Progress < NumberOfPools; Progress++) //Write Ammo Pools
            {
                Write(Out, ResourcePools[Progress], EndianWSG);
                Write(Out, AmmoPools[Progress], EndianWSG);
                Write(Out, RemainingPools[Progress], EndianWSG);
                Write(Out, PoolLevels[Progress], EndianWSG);
            }


            WriteObjects(Out, Items1); //Write Items

            Write(Out, BackpackSize, EndianWSG);
            Write(Out, EquipSlots, EndianWSG);

            WriteObjects(Out, Weapons1); //Write Weapons

            Int16 count = (Int16)Challenges.Count();
            Write(Out, count * 7 + 10, EndianWSG);
            Write(Out, ChallengeDataBlockId, EndianWSG);
            Write(Out, count * 7 + 2, EndianWSG);
            Write(Out, count, EndianWSG);
            foreach (ChallengeDataEntry challenge in Challenges)
            {
                Write(Out, challenge.Id, EndianWSG);
                Out.Write(challenge.TypeId);
                Write(Out, challenge.Value, EndianWSG);
            }

            Write(Out, TotalLocations, EndianWSG);

            for (int Progress = 0; Progress < TotalLocations; Progress++) //Write Locations
                Write(Out, LocationStrings[Progress], EndianWSG);

            Write(Out, CurrentLocation, EndianWSG);
            Write(Out, SaveInfo1to5[0], EndianWSG);
            Write(Out, SaveInfo1to5[1], EndianWSG);
            Write(Out, SaveInfo1to5[2], EndianWSG);
            Write(Out, SaveInfo1to5[3], EndianWSG);
            Write(Out, SaveInfo1to5[4], EndianWSG);
            Write(Out, SaveNumber, EndianWSG);
            Write(Out, SaveInfo7to10[0], EndianWSG);
            Write(Out, SaveInfo7to10[1], EndianWSG);
            Write(Out, NumberOfQuestLists, EndianWSG);

            for (int listIndex = 0; listIndex < NumberOfQuestLists; listIndex++)
            {
                QuestTable qt = QuestLists[listIndex];
                Write(Out, qt.Index, EndianWSG);
                Write(Out, qt.CurrentQuest, EndianWSG);
                Write(Out, qt.TotalQuests, EndianWSG);

                int questCount = qt.TotalQuests;
                for (int questIndex = 0; questIndex < questCount; questIndex++)  //Write Playthrough 1 Quests
                {
                    QuestEntry qe = qt.Quests[questIndex];
                    Write(Out, qe.Name, EndianWSG);
                    Write(Out, qe.Progress, EndianWSG);
                    Write(Out, qe.DLCValue1, EndianWSG);
                    Write(Out, qe.DLCValue2, EndianWSG);

                    int objectiveCount = qe.NumberOfObjectives;
                    Write(Out, objectiveCount, EndianWSG);

                    for (int i = 0; i < objectiveCount; i++)
                    {
                        Write(Out, qe.Objectives[i].Description, EndianWSG);
                        Write(Out, qe.Objectives[i].Progress, EndianWSG);
                    }
                }
            }

            Write(Out, TotalPlayTime, EndianWSG);
            Write(Out, LastPlayedDate, EndianWSG);
            Write(Out, CharacterName, EndianWSG);
            Write(Out, Color1, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Color2, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Color3, EndianWSG); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Head, EndianWSG);

            if (RevisionNumber > 38)
            {
                Write(Out, Unknown2, EndianWSG);
            }
            else
            {
                int NumberOfPromoCodesUsed = PromoCodesUsed.Count;
                Write(Out, NumberOfPromoCodesUsed, EndianWSG);
                for (int i = 0; i < NumberOfPromoCodesUsed; i++)
                    Write(Out, PromoCodesUsed[i], EndianWSG);
                int NumberOfPromoCodesRequiringNotification = PromoCodesRequiringNotification.Count;
                Write(Out, NumberOfPromoCodesRequiringNotification, EndianWSG);
                for (int i = 0; i < NumberOfPromoCodesRequiringNotification; i++)
                    Write(Out, PromoCodesRequiringNotification[i], EndianWSG);
            }
 
            Write(Out, NumberOfEchoLists, EndianWSG);
            for (int listIndex = 0; listIndex < NumberOfEchoLists; listIndex++)
            {
                EchoTable et = EchoLists[listIndex];
                Write(Out, et.Index, EndianWSG);
                Write(Out, et.TotalEchoes, EndianWSG);

                for (int echoIndex = 0; echoIndex < et.TotalEchoes; echoIndex++) //Write Locations
                {
                    EchoEntry ee = et.Echoes[echoIndex];
                    Write(Out, ee.Name, EndianWSG);
                    Write(Out, ee.DLCValue1, EndianWSG);
                    Write(Out, ee.DLCValue2, EndianWSG);
                }
            }

            DLC.DLC_Size = 0;
            // This loop writes the base data for each section into byte[]
            // BaseData so its size can be obtained and it can easily be
            // written to the output stream as a single block.  Calculate
            // DLC.DLC_Size as it goes since that has to be written before
            // the blocks are written to the output stream.
            foreach (DLCSection Section in DLC.DataSections)
            {
                MemoryStream tempStream = new MemoryStream();
                BinaryWriter memwriter = new BinaryWriter(tempStream);
                switch (Section.Id)
                {
                    case DLC_Data.Section1Id:
                        memwriter.Write(DLC.DLC_Unknown1);
                        Write(memwriter, DLC.BankSize, EndianWSG);
                        Write(memwriter, DLC.BankInventory.Count, EndianWSG);
                        for (int i = 0; i < DLC.BankInventory.Count; i++)
                            WriteBankEntry(memwriter, DLC.BankInventory[i], EndianWSG, RevisionNumber);
                        break;
                    case DLC_Data.Section2Id:
                        Write(memwriter, DLC.DLC_Unknown2, EndianWSG);
                        Write(memwriter, DLC.DLC_Unknown3, EndianWSG);
                        Write(memwriter, DLC.DLC_Unknown4, EndianWSG);
                        Write(memwriter, DLC.SkipDLC2Intro, EndianWSG);
                        break;
                    case DLC_Data.Section3Id:
                        memwriter.Write(DLC.DLC_Unknown5);
                        break;
                    case DLC_Data.Section4Id:
                        memwriter.Write(DLC.SecondaryPackEnabled);
                        // The DLC backpack items
                        WriteObjects(memwriter, Items2);
                        // The DLC backpack weapons
                        WriteObjects(memwriter, Weapons2);
                        break;
                    default:
                        break;
                }
                Section.BaseData = tempStream.ToArray();
                DLC.DLC_Size += Section.BaseData.Count() + Section.RawData.Count() + 8; // 8 = 4 bytes for id, 4 bytes for length
            }

            // Now its time to actually write all the data sections to the output stream
            Write(Out, DLC.DLC_Size, EndianWSG);
            foreach (DLCSection Section in DLC.DataSections)
            {
                Write(Out, Section.Id, EndianWSG);
                int SectionLength = Section.BaseData.Count() + Section.RawData.Count();
                Write(Out, SectionLength, EndianWSG);
                Out.Write(Section.BaseData);
                Out.Write(Section.RawData);
                Section.BaseData = null; // BaseData isn't needed anymore.  Free it.
            }
            if (RevisionNumber > 38)
            {
                //Past end padding
                Write(Out, Unknown3, EndianWSG);
            }
            // Clear the temporary lists used to split primary and DLC pack data
            Items1 = null;
            Items2 = null;
            Weapons1 = null;
            Weapons2 = null;
            return OutStream.ToArray();
        }
        ///<summary>Split the weapon and item lists into two parts: one for the primary pack and one for DLC backpack</summary>
        public void SplitInventoryIntoPacks()
        {
            Items1 = new List<Item>();
            Items2 = new List<Item>();
            Weapons1 = new List<Weapon>();
            Weapons2 = new List<Weapon>();
            // Split items and weapons into two lists each so they can be put into the 
            // DLC backpack or regular backpack area as needed.  Any item with a level 
            // override and special dlc items go in the DLC backpack.  All others go 
            // in the regular inventory.
            if ((DLC.HasSection4 == false) || (DLC.SecondaryPackEnabled == 0))
            {
                // no secondary pack so put it all in primary pack
                foreach (var item in Items)
                    Items1.Add(item);
                foreach (var weapon in Weapons)
                    Weapons1.Add(weapon);
                return;
            }
            foreach (var item in Items)
            {

                if ((item.Values[3] == 0) && (item.Strings[0].Substring(0, 3) != "dlc"))
                    Items1.Add(item);
                else
                    Items2.Add(item);
            }
            foreach (var weapon in Weapons)
            {

                if ((weapon.Values[3] == 0) && (weapon.Strings[0].Substring(0, 3) != "dlc"))
                    Weapons1.Add(weapon);
                else
                    Weapons2.Add(weapon);
            }
        }

        //public sealed class BankEntry
        //{
        //    public Byte TypeId;
        //    public List<string> Parts = new List<string>();
        //    public Int32 Quantity; //AmmoOrQuantity
        //    public Byte Equipped;
        //    public Int16 Quality;
        //    public Int16 Level;
        //    public byte[] padding;

        //    public void PrintParts()
        //    {
        //        foreach (var part in Parts)
        //        {
        //            Console.WriteLine(part);
        //        }
        //    }
        //}
        private static byte SUBPART = 32;
        public interface IPart
        {
            byte[] GetBytes(ByteOrder endian);
            string Init(BinaryReader reader, ByteOrder endian, BankEntry entry);
        }

        public class Part : IPart {
            public byte[] Unknown1;
            public byte Mask;
            public byte[] Unknown2;// Could be size 12 or 8 for Unique part
            public string Name;
            public byte[] ExtraData = null;

            public virtual byte[] GetBytes(ByteOrder endian)
            {
                var bytes = new List<byte>();

                if (Unknown1 != null)
                    bytes.AddRange(Unknown1);
                bytes.Add(Mask);
                bytes.AddRange(Unknown2);
                string[] splitPart = Name.Split('.');
                foreach (var str in splitPart)
                {
                    var tmpStr = GetBytesFromString(str, endian);
                    foreach (var b in tmpStr)
                    {
                        bytes.Add(b);
                    }
                }
                if (ExtraData != null)
                    bytes.AddRange(ExtraData);
                return bytes.ToArray();
            }

            public virtual string Init(BinaryReader reader, ByteOrder endian, BankEntry entry)
            {
                var bytes1 = new List<byte>();
                var bytes2 = new List<byte>();

                var mask = reader.ReadByte();
                if (mask == SUBPART)
                {
                    Mask = mask;
                }
                else if (mask == 0)
                {
                    reader.BaseStream.Position -= 1;
                    bytes1.AddRange(PaddingShort(reader, endian));
                    Mask = reader.ReadByte();
                }
                else
                {
                    reader.BaseStream.Position -= 1;

                    UInt32 temp = (UInt32)ReadInt32(reader, endian);
                    entry.Quality = (Int16)(temp % (UInt32)65536);
                    entry.Level = (Int16)(temp / (UInt32)65536);
                    Mask = reader.ReadByte();
                }
                Unknown1 = bytes1.ToArray();
                bytes2.AddRange(PaddingShort(reader, endian));
                Unknown2 = bytes2.ToArray();

                Name = ReadBankEntryPart(reader, endian, Unknown2.Length);

                return Name.Split('.')[1].Split('_')[0];
            }
            protected static byte[] PaddingShort(BinaryReader reader, ByteOrder endian)
            {
                var bytes = new List<byte>();
                var b = ReadBytes(reader, 1, endian);
                short val = b[0];
                if (val != 0)
                {
                    reader.BaseStream.Position -= 1;
                    return null;
                }

                bytes.AddRange(b);
                //Looking for next byte != 0
                while (val == 0)
                {
                    b = ReadBytes(reader, 1, endian);
                    val = b[0];
                    if (val == 0)
                        bytes.AddRange(b);
                    else
                        reader.BaseStream.Position -= 1;
                }
                return bytes.ToArray();
            }
        }
        public class WeaponPart : Part
        {
            public override string Init(BinaryReader reader, ByteOrder endian, BankEntry entry)
            {
                var category = base.Init(reader, endian, entry);
                switch (category)
                {
                    case "Manufacturers":
                        ExtraData = ReadBytes(reader, 4, endian); //Manufacturers ID (int)
                        break;
                    case "Title":
                        ExtraData = ReadBytes(reader, 13, endian);//??
                        var padding = PaddingShort(reader, endian);
                        if (padding != null)
                        {
                            if (ExtraData == null)
                                ExtraData = padding;
                            else
                                ExtraData = ExtraData.Concat(padding).ToArray();
                        }
                        break;
                    default:
                        break;
                }

                return category;
            }
    }

        public class ItemPart : Part
        {
            public override string Init(BinaryReader reader, ByteOrder endian, BankEntry entry)
            {
                var category = base.Init(reader, endian, entry);
                switch (category)
                {
                    case "Title":
                        ExtraData = ReadBytes(reader, 10, endian);//Unknown
                        var padding = PaddingShort(reader, endian);
                        if (padding != null)
                        {
                            if (ExtraData == null)
                                ExtraData = padding;
                            else
                                ExtraData = ExtraData.Concat(padding).ToArray();
                        }
                        break;
                    default:
                        break;
                }
                //var padding = PaddingShort(reader, endian);
                //if (padding != null)
                //{
                //    if (ExtraData == null)
                //        ExtraData = padding;
                //    else
                //        ExtraData = ExtraData.Concat(padding).ToArray();
                //}
                return category;
            }
        }

        public sealed class BankEntry
        {
            public Byte TypeId;
            public List<Part> Parts = new List<Part>();
            public Int32 Quantity; //AmmoOrQuantity
            public Byte Equipped;
            public Int16 Quality;
            public Int16 Level;
            public byte[] padding;

            public void PrintParts()
            {
                foreach (var part in Parts)
                {
                    Console.WriteLine(part.Name);
                }
            }
        }


        public enum BankEntryType
        {
            Item,
            Weapon,
            Unknown
        }

        private static string ReadBankEntryPart(BinaryReader reader, ByteOrder endian, int size)
        {
            string partName = "";
            for (int i = 0; i < (size == 8 ? 4 : 3); i++)
            {
                var tmp = ReadString(reader, endian);
                if (i != 0)
                    partName += "." + tmp;
                else
                    partName += tmp;
            }

            return partName;
        }

        private static string[] checkItemIntegrity = new string[9]
        {
            "A",
            "",
            "Manufacturers",
            "Body",
            "LeftSide",
            "RightSide",
            "ManufacturerMaterials",
            "Prefix",
            "Title"
        };

        private void BankEntryParseItem(BinaryReader reader, out BankEntry entry)
        {
            Console.WriteLine("Item");
            entry = new BankEntry();
            //Read PartName
            //
            //Type of Item
            //Gear
            //Manufacturer
            //Body
            //Left Side
            //Right Side
            //Material (Optional)
            //Prefix
            //Title
            var category = "";
            while (category != "Title")
            {
                Part part = new ItemPart();
                category = part.Init(reader, EndianWSG, entry);
                entry.Parts.Add(part);
            }

            for (int i = 0; i < 9; i++)
            {
                category = entry.Parts[i].Name.Split('.')[1].Split('_')[0];
                if (checkItemIntegrity[i] != category && i != 1)
                {
                    Console.WriteLine(checkItemIntegrity[i] + "/" + category);
                    var part = new ItemPart
                    {
                        Name = "None"
                    };
                    entry.Parts.Insert(i, part);
                }
            }

            entry.PrintParts();
        }
        private static string[] checkWeaponIntegrity = new string[14]
        {
            "a",
            "weapons",
            "manufacturers",
            "body",
            "grip",
            "mag",
            "barrel",
            "sight",
            "stock",
            "action",
            "acc",
            "material",
            "prefix",
            "title"
        };
        private void BankEntryParseWeapon(BinaryReader reader, out BankEntry entry)
        {
            Console.WriteLine("Weapon");
            entry = new BankEntry();
            var category = "";
            while (category != "Title")
            {
                Part part = new WeaponPart();
                category = part.Init(reader, EndianWSG, entry);
                entry.Parts.Add(part);
            }

            for (int i = 0; i < 14; i++)
            {
                category = entry.Parts[i].Name.ToLower();
                if (!category.Contains(checkWeaponIntegrity[i]))
                {
                    Console.WriteLine(checkWeaponIntegrity[i] + "/" + category);
                    var part = new WeaponPart
                    {
                        Name = "None"
                    };
                    entry.Parts.Insert(i, part);
                }
            }
            
            entry.PrintParts();
        }

        private BankEntryType CheckEntryType(BinaryReader reader)
        {
            var t = reader.ReadByte();
            switch (t)
            {
                case 1:
                    return BankEntryType.Weapon;
                case 2:
                    return BankEntryType.Item;
                default:
                    Console.WriteLine("Unknown type->" + t);
                    return BankEntryType.Unknown;
            }
        }

        private BankEntry CreateBankEntry(BinaryReader reader)
        {
            //Check entry type
            var entryType = CheckEntryType(reader);

            //Create new entry
            BankEntry entry;

            //Select the right parsing function
            switch (entryType)
            {
                case BankEntryType.Item:
                    BankEntryParseItem(reader, out entry);
                    entry.TypeId = 2;
                    return entry;
                case BankEntryType.Weapon:
                    BankEntryParseWeapon(reader, out entry);
                    entry.TypeId = 1;
                    return entry;
                case BankEntryType.Unknown:
                    break;
                default:
                    break;
            }
            return null;
        }

        private static string ReadBankString(BinaryReader reader, ByteOrder EndianWSG)
        {
            byte subObjectMask = reader.ReadByte();
            //if ((subObjectMask != 32) && (subObjectMask != 0))
            //    throw new FileFormatException("Bank string has an unknown sub-object mask.  Mask = " + subObjectMask);

            string composed = ReadString(reader, EndianWSG);
            bool isPreviousSubObject = (subObjectMask & 1) == 1;
            subObjectMask >>= 1;

            for (int i = 1; i < 6; i++)
            {
                string substring = ReadString(reader, EndianWSG);
                if (!string.IsNullOrEmpty(substring))
                {
                    if (isPreviousSubObject)
                        if (string.IsNullOrEmpty(composed))
                            composed = substring;
                        else
                            composed += ":" + substring;
                    else
                        if (string.IsNullOrEmpty(composed))
                            composed = substring;
                        else
                            composed += "." + substring;
                }

                isPreviousSubObject = (subObjectMask & 1) == 1;
                subObjectMask >>= 1;
            }

            return composed;
        }

        private static void WriteBankString(BinaryWriter bw, string value, ByteOrder Endian)
        {
            if (string.IsNullOrEmpty(value))
            {
                // Endianness does not matter here.
                bw.Write((byte)0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
            }
            else
            {
                byte subObjectMask = 32;
                string[] pathComponentNames = value.Split('.');

                bw.Write(subObjectMask);

                // Write the empty strings first.
                // Endianness does not matter here.
                for (int j = pathComponentNames.Length; j < 6; j++)
                    bw.Write(0);

                // Then write the strings that are not empty.
                for (int j = 0; j < pathComponentNames.Length; j++)
                    Write(bw, pathComponentNames[j], Endian);
            }
        }

        //private BankEntry ReadBankEntry(BinaryReader br, ByteOrder endian)
        //{
        //    Console.WriteLine("Bank");
        //    BankEntry entry = new BankEntry();
        //    int partCount;
        //    entry.TypeId = br.ReadByte();

        //    switch (entry.TypeId)
        //    {
        //        case 1:
        //        case 2:
        //            break;
        //        default:
        //            throw new FormatException("Bank entry to be written has invalid Type ID.  TypeId = " + entry.TypeId);
        //    }

        //    for (int i = 0; i < 3; i++)
        //    {
        //        var s = ReadString(br, endian);
        //        Console.WriteLine(s);
        //        entry.Parts.Add(s);
        //    }
                

        //    UInt32 temp = (UInt32)ReadInt32(br, endian);
        //    entry.Quality = (Int16)(temp % (UInt32)65536);
        //    entry.Level = (Int16)(temp / (UInt32)65536);

        //    switch (entry.TypeId)
        //    {
        //        case 1:
        //            partCount = 14;
        //            break;
        //        case 2:
        //            partCount = 9;
        //            break;
        //        default:
        //            partCount = 0;
        //            break;
        //    }

        //    for (int i = 3; i < partCount; i++)
        //        entry.Parts.Add(ReadBankString(br, endian));

        //    byte[] Footer = br.ReadBytes(10);
        //    // da_fileserver's investigation has led him to believe the footer bytes are:
        //    // (Int)GameStage - default 0
        //    // (Int)AwesomeLevel - default 0
        //    // (Byte)Equipped - default 0
        //    // (Byte)DropOnDeath - default 1 (this is whether an npc would drop it when it dies not you)
        //    // (Byte)ShopsHaveInfiniteQuantity - default 0
        //    // matt911 - It seems apparent that this table is used for more than just the bank inventory 
        //    // in the game.  None of the values are stored in the inventory part of the savegame
        //    // except Equipped and even that will be updated immediately when you take the item
        //    // out of the bank.  I've never seen any of these with anything except the default value
        //    // in the bank except Equipped so I will store that in case it is not what we think it
        //    // is and it is important, but I am doubtful that it is.
        //    for (int i = 0; i < 7; i++)
        //        System.Diagnostics.Debug.Assert(Footer[i] == 0);
        //    System.Diagnostics.Debug.Assert(Footer[9] == 1); // This might be 1 for a health pack
        //    entry.Equipped = Footer[8];

        //    switch (entry.TypeId)
        //    {
        //        case 1: // weapon
        //            entry.Quantity = ReadInt32(br, endian);
        //            break;
        //        case 2: // item
        //            entry.Quantity = (int)br.ReadByte();
        //            break;
        //        default:
        //            entry.Quantity = 0;
        //            break;
        //    }
        //    if (RevisionNumber > 38)
        //    {
        //        RawDataInfo rd = null;
        //        do
        //        {
        //            rd = CheckPadding(br, 1);
        //            if (entry.padding == null)
        //                entry.padding = rd.data;
        //            else
        //                entry.padding = entry.padding.Concat(rd.data).ToArray();
        //        } while (rd.nextValue == 0);
        //    }
        //    return entry;
        //}
        private static void WriteBankEntry(BinaryWriter bw, BankEntry entry, ByteOrder Endian, int version)
        {
            if (entry.Parts.Count < 3)
                throw new FormatException("Bank entry has an invalid part count. Parts.Count = " + entry.Parts.Count);

            bw.Write(entry.TypeId);

            switch (entry.TypeId)
            {
                case 1:
                case 2:
                    break;
                default:
                    throw new FormatException("Bank entry to be written has an invalid Type ID.  TypeId = " + entry.TypeId);
            }

            //for (int i = 0; i < 3; i++)
            //    WriteBankString(bw, entry.Parts[i], Endian);

            //UInt32 grade = (UInt16)entry.Quality + (UInt16)entry.Level * (UInt32)65536;

            //Write(bw, (Int32)grade, Endian);

            //for (int i = 3; i < entry.Parts.Count; i++)
            //    WriteBankString(bw, entry.Parts[i], Endian);

            // see ReadBankEntry for notes about the footer bytes
            Byte[] Footer = new Byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            Footer[8] = entry.Equipped;
            bw.Write(Footer);

            switch (entry.TypeId)
            {
                case 1: // weapon
                    Write(bw, entry.Quantity, Endian);
                    break;
                case 2: // item
                    bw.Write((Byte)entry.Quantity);
                    break;
                default:
                    throw new FormatException("Bank entry to be written has an invalid Type ID.  TypeId = " + entry.TypeId);
            }
            if (version > 38)
                bw.Write(entry.padding);
        }
    }
}
