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
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using X360.IO;
using X360.STFS;
using System.Reflection;

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

        public static string AppPath = (Assembly.GetEntryAssembly() != null ? Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) : "") +
                                       Path.DirectorySeparatorChar;
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

            var outBytes = new byte[fieldSize];
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

        private static float ReadSingle(BinaryReader reader, ByteOrder endian)
        {
            return BitConverter.ToSingle(ReadBytes(reader, sizeof(float), endian), 0);
        }
        private static int ReadInt32(BinaryReader reader, ByteOrder endian)
        {
            return BitConverter.ToInt32(ReadBytes(reader, sizeof(int), endian), 0);
        }
        private static short ReadInt16(BinaryReader reader, ByteOrder endian)
        {
            return BitConverter.ToInt16(ReadBytes(reader, sizeof(short), endian), 0);
        }
        private static List<int> ReadListInt32(BinaryReader reader, ByteOrder endian)
        {
            var count = ReadInt32(reader, endian);
            var list = new List<int>(count);
            for (var i = 0; i < count; i++)
            {
                var value = ReadInt32(reader, endian);
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
        private static void Write(BinaryWriter writer, short value, ByteOrder endian)
        {
            writer.Write(ReadBytes(BitConverter.GetBytes(value), sizeof(short), endian));
        }

        private static byte[] GetBytesFromInt(int value, ByteOrder endian)
        {
            return ReadBytes(BitConverter.GetBytes(value), sizeof(int), endian);
        }

        private static byte[] GetBytesFromInt(uint value, ByteOrder endian)
        {
            return ReadBytes(BitConverter.GetBytes(value), sizeof(int), endian);
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
                if (tempLengthValue < 5 || tempLengthValue > 4096)
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
                    Console.WriteLine("Value :" + value);
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

            bool requiresUnicode = IsUnicode(value);
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

            bool requiresUnicode = IsUnicode(value);
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
                bytes.Add(0);
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
                bytes.Add(0);
                bytes.Add(0);
            }
            return bytes.ToArray();
        }
        /// <summary> Look for any non-ASCII characters in the input.</summary>
        private static bool IsUnicode(string value)
        {
            foreach (var t in value)
                if (t > 256)
                    return true;

            return false;
        }

#region Members

        private static readonly int EnhancedVersion = 0x27;
        public ByteOrder EndianWsg;

        public string Platform;
        public string OpenedWsg;
        public bool ContainsRawData;
        // Whether WSG should try to automatically repair or discard any invalid data
        // to recover from an invalid state.  This will allow partial data loss but 
        // may allow partial data recovery as well.
        public bool AutoRepair = false;
        public bool RequiredRepair;

        //General Info
        public string MagicHeader;
        public int VersionNumber;
        public string Plyr;
        public int RevisionNumber;
        public static int ExportValuesCount;
        public static int BankValuesCount;
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
        public delegate List<int> ReadValuesFunction(BinaryReader reader, ByteOrder bo, int revisionNumber);

        public class Object
        {
            public List<string> Strings = new List<string>();
            protected int[] _values = new int[6];

            public ReadStringsFunction ReadStrings;
            public ReadValuesFunction ReadValues = ReadObjectValues;

            public void PrintParts()
            {
                foreach (var part in Strings)
                {
                    Console.WriteLine(part);
                }
            }

            public void SetValues(List<int> values)
            {
                _values = values.ToArray();
            }

            public List<int> GetValues()
            {
                return _values.ToList();
            }

            public int Quality
            {
                get { return _values[1]; }
                set { _values[1] = value; }
            }
            public int EquipedSlot
            {
                get { return _values[2]; }
                set { _values[2] = value; }
            }
            public int Level
            {
                get { return _values[3]; }
                set { _values[3] = value; }
            }

            public int Junk
            {
                get { return _values[4]; }
                set { _values[4] = value; }
            }

            public int Locked
            {
                get { return _values[5]; }
                set { _values[5] = value; }
            }
        }
        public class Item : Object
        {

            public Item()
            {
                ReadStrings = ReadItemStrings;
            }

            public int Quantity
            {
                get { return _values[0]; }
                set { _values[0] = value; }
            }
        }
        public class Weapon : Object
        {

            public Weapon()
            {
                ReadStrings = ReadWeaponStrings;
            }

            public int Ammo
            {
                get { return _values[0]; }
                set { _values[0] = value; }
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
        List<ChallengeDataEntry> _challenges;

        public byte[] ChallengeData;
        public int TotalLocations;
        public string[] LocationStrings;
        public string CurrentLocation;
        public int[] SaveInfo1To5 = new int[5];
        public int SaveNumber;
        public int[] SaveInfo7To10 = new int[4];

        public struct QuestObjective
        {
            public int Progress;
            public string Description;
        }

        public class QuestEntry
        {
            public string Name;
            public int Progress;
            public int DlcValue1;
            public int DlcValue2;
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
            public int DlcValue1;
            public int DlcValue2;
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

        public class DlcData
        {
            public const int Section1Id = 0x43211234;
            public const int Section2Id = 0x02151984;
            public const int Section3Id = 0x32235947;
            public const int Section4Id = 0x234BA901;

            public bool HasSection1;
            public bool HasSection2;
            public bool HasSection3;
            public bool HasSection4;

            public List<DlcSection> DataSections;

            public int DlcSize;

            // DLC Section 1 Data (bank data)
            public byte DlcUnknown1;  // Read only flag. Always resets to 1 in ver 1.41.  Probably CanAccessBank.
            public int BankSize;
            public List<BankEntry> BankInventory = new List<BankEntry>();
            // DLC Section 2 Data (don't know)
            public int DlcUnknown2; // All four of these are boolean flags.
            public int DlcUnknown3; // If you set them to any value except 0
            public int DlcUnknown4; // the game will rewrite them as 1.
            public int SkipDlc2Intro; //
            // DLC Section 3 Data (related to the level cap.  removing this section will delevel your character to 50)
            public byte DlcUnknown5;  // Read only flag. Always resets to 1 in ver 1.41.  Probably CanExceedLevel50
            // DLC Section 4 Data (DLC backpack)
            public byte SecondaryPackEnabled;  // Read only flag. Always resets to 1 in ver 1.41.
            public int NumberOfWeapons;
        }

        public DlcData Dlc = new DlcData();

        public class DlcSection
        {
            public int Id;
            public byte[] RawData;
            public byte[] BaseData; // used temporarily in SaveWSG to store the base data for a section as a byte array
        }

        //Xbox 360 only
        public long ProfileId;
        public byte[] DeviceId;
        public byte[] ConImage;
        public string TitleDisplay;
        public string TitlePackage;
        public uint TitleId = 1414793191;
#endregion


        public static uint GetXboxTitleId(Stream inputWsg)
        {
            BinaryReader br = new BinaryReader(inputWsg);
            byte[] fileInMemory = br.ReadBytes((int)inputWsg.Length);
            if (fileInMemory.Count() != inputWsg.Length)
                throw new EndOfStreamException();

            try
            {
                STFSPackage con = new STFSPackage(new DJsIO(fileInMemory, true), new X360.Other.LogRecord());
                return con.Header.TitleID;
            }
            catch { return 0; }
        }

        ///<summary>Reports back the expected platform this WSG was created on.</summary>
        public static string WsgType(Stream inputWsg)
        {
            BinaryReader saveReader = new BinaryReader(inputWsg);

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
                        uint titleId = ((uint)saveReader.ReadByte() << 24) + ((uint)saveReader.ReadByte() << 16) +
                            ((uint)saveReader.ReadByte() << 8) + saveReader.ReadByte();


//                        uint titleID = GetXboxTitleID(inputWSG);
                        switch (titleId) 
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

                return littleEndian ? "PC" : "PS3";
            }

            return "Not WSG";
        }

        ///<summary>Extracts a WSG from a CON (Xbox 360 Container File).</summary>
        public MemoryStream OpenXboxWsgStream(Stream inputX360File)
        {
            BinaryReader br = new BinaryReader(inputX360File);
            byte[] fileInMemory = br.ReadBytes((int)inputX360File.Length);
            if (fileInMemory.Count() != inputX360File.Length)
                throw new EndOfStreamException();

            try
            {
                STFSPackage con = new STFSPackage(new DJsIO(fileInMemory, true), new X360.Other.LogRecord());
                //DJsIO Extract = new DJsIO(true);
                //CON.FileDirectory[0].Extract(Extract);
                ProfileId = con.Header.ProfileID;
                DeviceId = con.Header.DeviceID;
                
                //DJsIO Save = new DJsIO("C:\\temp.sav", DJFileMode.Create, true);
                //Save.Write(Extract.ReadStream());
                //Save.Close();
                //byte[] nom = CON.GetFile("SaveGame.sav").GetEntryData(); 
                return new MemoryStream(con.GetFile("SaveGame.sav").GetTempIO(true).ReadStream(), false);
            }
            catch
            {
                try
                {
                    DJsIO manual = new DJsIO(fileInMemory, true);
                    manual.ReadBytes(881);
                    ProfileId = manual.ReadInt64();
                    manual.ReadBytes(132);
                    DeviceId = manual.ReadBytes(20);
                    manual.ReadBytes(48163);
                    int size = manual.ReadInt32();
                    manual.ReadBytes(4040);
                    return new MemoryStream(manual.ReadBytes(size), false);
                }
                catch { return null; }
            }
        }

        ///<summary>Reads savegame data from a file</summary>
        public void LoadWsg(string inputFile)
        {
            using (FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Platform = WsgType(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);

                if (string.Equals(Platform, "X360", StringComparison.Ordinal) ||
                    string.Equals(Platform, "X360JP", StringComparison.Ordinal))
                {
                    using (MemoryStream x360FileStream = OpenXboxWsgStream(fileStream))
                    {
                        ReadWsg(x360FileStream);
                    }
                }
                else if (string.Equals(Platform, "PS3", StringComparison.Ordinal) ||
                    string.Equals(Platform, "PC", StringComparison.Ordinal))
                {
                    ReadWsg(fileStream);
                }
                else
                {
                    throw new FileFormatException("Input file is not a WSG (platform is " + Platform + ").");
                }

                OpenedWsg = inputFile;
            }
        }
        
        private void BuildXboxPackage(string packageFileName, string saveFileName, int locale)
        {
            CreateSTFS package = new CreateSTFS();

            package.STFSType = STFSType.Type1;
            package.HeaderData.ProfileID = ProfileId;
            package.HeaderData.DeviceID = DeviceId;

            Assembly newAssembly = Assembly.GetExecutingAssembly();
            // WARNING: GetManifestResourceStream is case-sensitive.
            Stream wtIcon = newAssembly.GetManifestResourceStream("WillowTree.Resources.WT_CON.png");
            package.HeaderData.ContentImage = System.Drawing.Image.FromStream(wtIcon ?? throw new NoNullAllowedException("wtIcon don't found."));
            package.HeaderData.PackageImage = package.HeaderData.ContentImage;
            package.HeaderData.Title_Display = CharacterName + " - Level " + Level + " - " + CurrentLocation;
            package.HeaderData.Title_Package = "Borderlands";
            switch (locale)
            {
                case 1: // US or International version
                    package.HeaderData.Title_Package = "Borderlands";
                    package.HeaderData.TitleID = 1414793191;
                    break;                   
                case 2: // JP version
                    package.HeaderData.Title_Package = "Borderlands (JP)";
                    package.HeaderData.TitleID = 1414793318;
                    break;
            }
            package.AddFile(saveFileName, "SaveGame.sav");


            STFSPackage con = new STFSPackage(package, new RSAParams(DataPath + "KV.bin"), packageFileName, new X360.Other.LogRecord());

            con.FlushPackage(new RSAParams(DataPath + "KV.bin"));
            con.CloseIO();
            wtIcon.Close();
        }

        private static List<string> ReadItemStrings(BinaryReader reader, ByteOrder bo)
        {
            List<string> strings = new List<string>();
            for (int totalStrings = 0; totalStrings < 9; totalStrings++)
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
            for (int totalStrings = 0; totalStrings < 14; totalStrings++)
                strings.Add(ReadString(reader, bo));
            foreach (var item in strings)
            {
                Console.WriteLine(item);
            }
            return strings;
        }

        private static List<int> ReadObjectValues(BinaryReader reader, ByteOrder bo, int revisionNumber)
        {
            Int32 ammoQuantityCount = ReadInt32(reader, bo);
            UInt32 tempLevelQuality = (UInt32)ReadInt32(reader, bo);
            Int16 quality = (Int16)(tempLevelQuality % 65536);
            Int16 level = (Int16)(tempLevelQuality / 65536);
            Int32 equippedSlot = ReadInt32(reader, bo);

            var values = new List<int>() {
                ammoQuantityCount,
                quality,
                equippedSlot,
                level
            };

            if (revisionNumber < EnhancedVersion) return values;
            Int32 junk = ReadInt32(reader, bo);
            Int32 locked = ReadInt32(reader, bo);
            Console.WriteLine(locked);
            if (locked != 0 && locked != 1)
            {
                reader.BaseStream.Position -= 4;
                values.Add(0);
            }
            else
                values.Add(junk);
            if (locked != 0 && locked != 1)
            {
                reader.BaseStream.Position -= 4;
                values.Add(0);
            }
            else
                values.Add(locked);

            return values;
        }

        private T ReadObject<T>(BinaryReader reader) where T : Object, new()
        {
            var item = new T();
            item.Strings = item.ReadStrings(reader, EndianWsg);
            item.SetValues(item.ReadValues(reader, EndianWsg, RevisionNumber));
            return item;
        }

        private void ReadObjects<T>(BinaryReader reader, ref List<T> objects, int groupSize) where T : Object, new()
        {
            for (int progress = 0; progress < groupSize; progress++)
            {
                Console.WriteLine(progress + "/" + groupSize);
                var item = ReadObject<T>(reader);
                objects.Add(item);
            }
        }

        public void SaveWsg(string filename)
        {
            if (Platform == "PS3" || Platform == "PC")
            {
                using (BinaryWriter save = new BinaryWriter(new FileStream(filename, FileMode.Create)))
                {
                    save.Write(WriteWsg());
                }
            }

            else if (Platform == "X360")
            {
                string tempSaveName = filename + ".temp";
                using (BinaryWriter save = new BinaryWriter(new FileStream(tempSaveName, FileMode.Create)))
                {
                    save.Write(WriteWsg());
                }

                BuildXboxPackage(filename, tempSaveName, 1);
                File.Delete(tempSaveName);
            }

            else if (Platform == "X360JP")
            {
                string tempSaveName = filename + ".temp";
                using (BinaryWriter save = new BinaryWriter(new FileStream(tempSaveName, FileMode.Create)))
                {
                    save.Write(WriteWsg());
                }

                BuildXboxPackage(filename, tempSaveName, 2);
                File.Delete(tempSaveName);
            }

        }

        private static bool Eof(BinaryReader binaryReader)
        {
            var bs = binaryReader.BaseStream;
            return (bs.Position == bs.Length);
        }

        ///<summary>Read savegame data from an open stream</summary>
        public void ReadWsg(Stream fileStream)
        {
            BinaryReader testReader = new BinaryReader(fileStream, Encoding.ASCII);

            ContainsRawData = false;
            RequiredRepair = false;
            MagicHeader = new string(testReader.ReadChars(3));
            VersionNumber = testReader.ReadInt32();

            if (VersionNumber == 2)
                EndianWsg = ByteOrder.LittleEndian;
            else if (VersionNumber == 0x02000000)
            {
                VersionNumber = 2;
                EndianWsg = ByteOrder.BigEndian;
            }
            else
                throw new FileFormatException("WSG version number does match any known version (" + VersionNumber + ").");
         
            Plyr = new string(testReader.ReadChars(4));
            if (!string.Equals(Plyr, "PLYR", StringComparison.Ordinal))
                throw new FileFormatException("Player header does not match expected value.");

            RevisionNumber = ReadInt32(testReader, EndianWsg);
            ExportValuesCount = RevisionNumber < EnhancedVersion ? 4 : 6;
            Class = ReadString(testReader, EndianWsg);
            Level = ReadInt32(testReader, EndianWsg);
            Experience = ReadInt32(testReader, EndianWsg);
            SkillPoints = ReadInt32(testReader, EndianWsg);
            Unknown1 = ReadInt32(testReader, EndianWsg);
            Cash = ReadInt32(testReader, EndianWsg);
            FinishedPlaythrough1 = ReadInt32(testReader, EndianWsg);
            NumberOfSkills = ReadSkills(testReader, EndianWsg);
            Vehi1Color = ReadInt32(testReader, EndianWsg);
            Vehi2Color = ReadInt32(testReader, EndianWsg);
            Vehi1Type = ReadInt32(testReader, EndianWsg);
            Vehi2Type = ReadInt32(testReader, EndianWsg);
            NumberOfPools = ReadAmmo(testReader, EndianWsg);
            Console.WriteLine(@"====== ENTER ITEM ======");
            ReadObjects(testReader, ref Items, ReadInt32(testReader, EndianWsg));
            Console.WriteLine(@"====== EXIT ITEM ======");
            BackpackSize = ReadInt32(testReader, EndianWsg);
            EquipSlots = ReadInt32(testReader, EndianWsg);
            Console.WriteLine(@"====== ENTER WEAPON ======");
            ReadObjects(testReader, ref Weapons, ReadInt32(testReader, EndianWsg));
            Console.WriteLine(@"====== EXIT WEAPON ======");

            ChallengeDataBlockLength = ReadInt32(testReader, EndianWsg);
            byte[] challengeDataBlock = testReader.ReadBytes(ChallengeDataBlockLength);
            if (challengeDataBlock.Length != ChallengeDataBlockLength)
                throw new EndOfStreamException();

            using (BinaryReader challengeReader = new BinaryReader(new MemoryStream(challengeDataBlock, false), Encoding.ASCII))
            {
                ChallengeDataBlockId = ReadInt32(challengeReader, EndianWsg);
                ChallengeDataLength = ReadInt32(challengeReader, EndianWsg);
                ChallengeDataEntries = ReadInt16(challengeReader, EndianWsg);
                _challenges = new List<ChallengeDataEntry>();
                for (int i = 0; i < ChallengeDataEntries; i++)
                {
                    ChallengeDataEntry challenge;
                    challenge.Id = ReadInt16(challengeReader, EndianWsg);
                    challenge.TypeId = challengeReader.ReadByte();
                    challenge.Value = ReadInt32(challengeReader, EndianWsg);
                    _challenges.Add(challenge);
                }
            }

            TotalLocations = ReadLocations(testReader, EndianWsg);
            CurrentLocation = ReadString(testReader, EndianWsg);
            SaveInfo1To5[0] = ReadInt32(testReader, EndianWsg);
            SaveInfo1To5[1] = ReadInt32(testReader, EndianWsg);
            SaveInfo1To5[2] = ReadInt32(testReader, EndianWsg);
            SaveInfo1To5[3] = ReadInt32(testReader, EndianWsg);
            SaveInfo1To5[4] = ReadInt32(testReader, EndianWsg);
            SaveNumber = ReadInt32(testReader, EndianWsg);
            SaveInfo7To10[0] = ReadInt32(testReader, EndianWsg);
            SaveInfo7To10[1] = ReadInt32(testReader, EndianWsg);
            NumberOfQuestLists = ReadQuests(testReader, EndianWsg);

            TotalPlayTime = ReadInt32(testReader, EndianWsg);
            LastPlayedDate = ReadString(testReader, EndianWsg); //YYYYMMDDHHMMSS
            CharacterName = ReadString(testReader, EndianWsg);
            Color1 = ReadInt32(testReader, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Color2 = ReadInt32(testReader, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Color3 = ReadInt32(testReader, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Head = ReadInt32(testReader, EndianWsg);

            if (RevisionNumber >= EnhancedVersion)
            {
                Unknown2 = ReadBytes(testReader, 93, EndianWsg);
            }
            else
            {
                PromoCodesUsed = ReadListInt32(testReader, EndianWsg);
                PromoCodesRequiringNotification = ReadListInt32(testReader, EndianWsg);
            }
            NumberOfEchoLists = ReadEchoes(testReader, EndianWsg);

            Dlc.DataSections = new List<DlcSection>();
            Dlc.DlcSize = ReadInt32(testReader, EndianWsg);
            byte[] dlcDataBlock = testReader.ReadBytes(Dlc.DlcSize);
            if (dlcDataBlock.Length != Dlc.DlcSize)
                throw new EndOfStreamException();

            using (BinaryReader dlcDataReader = new BinaryReader(new MemoryStream(dlcDataBlock, false), Encoding.ASCII))
            {
                int remainingBytes = Dlc.DlcSize;
                while (remainingBytes > 0)
                {
                    DlcSection section = new DlcSection();
                    section.Id = ReadInt32(dlcDataReader, EndianWsg);
                    int sectionLength = ReadInt32(dlcDataReader, EndianWsg);
                    long sectionStartPos = (int)dlcDataReader.BaseStream.Position;
                    switch (section.Id)
                    {
                        case DlcData.Section1Id: // 0x43211234
                            Dlc.HasSection1 = true;
                            Dlc.DlcUnknown1 = dlcDataReader.ReadByte();
                            Dlc.BankSize = ReadInt32(dlcDataReader, EndianWsg);
                            int bankEntriesCount = ReadInt32(dlcDataReader, EndianWsg);
                            Dlc.BankInventory = new List<BankEntry>();
                            Console.WriteLine(@"====== ENTER BANK ======");
                            BankValuesCount = ExportValuesCount;
                            for (int i = 0; i < bankEntriesCount; i++)
                            {
                                Console.WriteLine(i + "/" + bankEntriesCount);
                                Dlc.BankInventory.Add(CreateBankEntry(dlcDataReader, i == bankEntriesCount - 1));
                            }
                            Console.WriteLine(@"====== EXIT BANK ======");
                            break;
                        case DlcData.Section2Id: // 0x02151984
                            Dlc.HasSection2 = true;
                            Dlc.DlcUnknown2 = ReadInt32(dlcDataReader, EndianWsg);
                            Dlc.DlcUnknown3 = ReadInt32(dlcDataReader, EndianWsg);
                            Dlc.DlcUnknown4 = ReadInt32(dlcDataReader, EndianWsg);
                            Dlc.SkipDlc2Intro = ReadInt32(dlcDataReader, EndianWsg);
                            break;
                        case DlcData.Section3Id: // 0x32235947
                            Dlc.HasSection3 = true;
                            Dlc.DlcUnknown5 = dlcDataReader.ReadByte();
                            break;
                        case DlcData.Section4Id: // 0x234ba901
                            Dlc.HasSection4 = true;
                            Dlc.SecondaryPackEnabled = dlcDataReader.ReadByte();
 
                            try
                            {
                                Console.WriteLine(@"====== ENTER DLC ITEM ======");
                                ReadObjects(dlcDataReader, ref Items, ReadInt32(dlcDataReader, EndianWsg));
                                Console.WriteLine(@"====== EXIT DLC ITEM ======");
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
                                //if (ItemStrings.Count > Ite.Count)
                                //    ItemStrings.RemoveAt(ItemStrings.Count - 1);

                                // Figure out how many weapons were successfully read
                                //DLC.NumberOfWeapons = ItemStrings.Count - NumberOfItems;

                                // If the data is invalid here, the whole DLC weapon list is invalid so
                                // set its length to 0 and be done
                                Dlc.NumberOfWeapons = 0;

                                // Skip to the end of the section to discard any raw data that is left over
                                dlcDataReader.BaseStream.Position = sectionStartPos + sectionLength;
                            }
                            //NumberOfItems += DLC.NumberOfItems;

                            try
                            {
                                Console.WriteLine(@"====== ENTER DLC WEAPON ======");
                                ReadObjects(dlcDataReader, ref Weapons, ReadInt32(dlcDataReader, EndianWsg));
                                Console.WriteLine(@"====== EXIT DLC WEAPON ======");
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
                                dlcDataReader.BaseStream.Position = sectionStartPos + sectionLength;
                            }
                            //NumberOfWeapons += DLC.NumberOfWeapons;
                            break;
                    }

                    // I don't pretend to know if any of the DLC sections will ever expand
                    // and store more data.  RawData stores any extra data at the end of
                    // the known data in any section and stores the entirety of sections 
                    // with unknown ids in a buffer in its raw byte order dependent form.
                    int rawDataCount = sectionLength - (int)(dlcDataReader.BaseStream.Position - sectionStartPos);

                    section.RawData = dlcDataReader.ReadBytes(rawDataCount);
                    if (rawDataCount > 0)
                        ContainsRawData = true;
                    remainingBytes -= sectionLength + 8;
                    Dlc.DataSections.Add(section);
                }

                if (RevisionNumber < EnhancedVersion) return;
                //Padding at the end of file, don't know exactly why
                var temp = new List<byte>();
                while (!Eof(testReader))
                {
                    temp.Add(ReadBytes(testReader, 1, EndianWsg)[0]);
                }
                Unknown3 = temp.ToArray();
            }
        }

        private int ReadEchoes(BinaryReader reader, ByteOrder endianWsg)
        {
            int echoListCount = ReadInt32(reader, endianWsg);

            EchoLists.Clear();
            for (int i = 0; i < echoListCount; i++)
            {
                EchoTable et = new EchoTable();
                et.Index = ReadInt32(reader, endianWsg);
                et.TotalEchoes = ReadInt32(reader, endianWsg);
                et.Echoes = new List<EchoEntry>();

                for (int echoIndex = 0; echoIndex < et.TotalEchoes; echoIndex++)
                {
                    EchoEntry ee = new EchoEntry();
                    ee.Name = ReadString(reader, endianWsg);
                    ee.DlcValue1 = ReadInt32(reader, endianWsg);
                    ee.DlcValue2 = ReadInt32(reader, endianWsg);
                    et.Echoes.Add(ee);
                }
                EchoLists.Add(et);
            }
            return echoListCount;
        }
        
        private int ReadQuests(BinaryReader reader, ByteOrder endianWsg)
        {
            int numberOfQuestList = ReadInt32(reader, endianWsg);

            QuestLists.Clear();
            for (int listIndex = 0; listIndex < numberOfQuestList; listIndex++)
            {
                QuestTable qt = new QuestTable();
                qt.Index = ReadInt32(reader, endianWsg);
                qt.CurrentQuest = ReadString(reader, endianWsg);
                qt.TotalQuests = ReadInt32(reader, endianWsg);
                qt.Quests = new List<QuestEntry>();
                int questCount = qt.TotalQuests;

                for (int questIndex = 0; questIndex < questCount; questIndex++)
                {
                    QuestEntry qe = new QuestEntry();
                    qe.Name = ReadString(reader, endianWsg);
                    qe.Progress = ReadInt32(reader, endianWsg);
                    qe.DlcValue1 = ReadInt32(reader, endianWsg);
                    qe.DlcValue2 = ReadInt32(reader, endianWsg);

                    int objectiveCount = ReadInt32(reader, endianWsg);
                    qe.NumberOfObjectives = objectiveCount;
                    qe.Objectives = new QuestObjective[objectiveCount];

                    for (int objectiveIndex = 0; objectiveIndex < objectiveCount; objectiveIndex++)
                    {
                        qe.Objectives[objectiveIndex].Description = ReadString(reader, endianWsg);
                        qe.Objectives[objectiveIndex].Progress = ReadInt32(reader, endianWsg);
                    }
                    qt.Quests.Add(qe);
                }

                if (qt.CurrentQuest == "None" & qt.Quests.Count > 0) 
                    qt.CurrentQuest = qt.Quests[0].Name;

                QuestLists.Add(qt);
            }
            return numberOfQuestList;
        }

        private int ReadSkills(BinaryReader reader, ByteOrder endianWsg)
        {
            int skillsCount = ReadInt32(reader, endianWsg);

            string[] tempSkillNames = new string[skillsCount];
            int[] tempLevelOfSkills = new int[skillsCount];
            int[] tempExpOfSkills = new int[skillsCount];
            int[] tempInUse = new int[skillsCount];

            for (int progress = 0; progress < skillsCount; progress++)
            {
                tempSkillNames[progress] = ReadString(reader, endianWsg);
                tempLevelOfSkills[progress] = ReadInt32(reader, endianWsg);
                tempExpOfSkills[progress] = ReadInt32(reader, endianWsg);
                tempInUse[progress] = ReadInt32(reader, endianWsg);
            }

            SkillNames = tempSkillNames;
            LevelOfSkills = tempLevelOfSkills;
            ExpOfSkills = tempExpOfSkills;
            InUse = tempInUse;

            return skillsCount;
        }

        private int ReadAmmo(BinaryReader reader, ByteOrder endianWsg)
        {
            int poolsCount = ReadInt32(reader, endianWsg);
 
            string[] tempResourcePools = new string[poolsCount];
            string[] tempAmmoPools = new string[poolsCount];
            float[] tempRemainingPools = new float[poolsCount];
            int[] tempPoolLevels = new int[poolsCount];

            for (int progress = 0; progress < poolsCount; progress++)
            {
                tempResourcePools[progress] = ReadString(reader, endianWsg);
                tempAmmoPools[progress] = ReadString(reader, endianWsg);
                tempRemainingPools[progress] = ReadSingle(reader, endianWsg);
                tempPoolLevels[progress] = ReadInt32(reader, endianWsg);
            }

            ResourcePools = tempResourcePools;
            AmmoPools = tempAmmoPools;
            RemainingPools = tempRemainingPools;
            PoolLevels = tempPoolLevels;

            return poolsCount;
        }

        private int ReadLocations(BinaryReader reader, ByteOrder endianWsg)
        {
            int locationCount = ReadInt32(reader, endianWsg);
            string[] tempLocationStrings = new string[locationCount];

            for (int progress = 0; progress < locationCount; progress++)
                tempLocationStrings[progress] = ReadString(reader, endianWsg);

            LocationStrings = tempLocationStrings;
            return locationCount;
        }

        public void DiscardRawData()
        {
            // Make a list of all the known data sections to compare against.
            List<int> knownSectionIds = new List<int>() {
                DlcData.Section1Id,
                DlcData.Section2Id,
                DlcData.Section3Id,
                DlcData.Section4Id,
            };

            // Traverse the list of data sections from end to beginning because when
            // an item gets deleted it does not affect the index of the ones before it,
            // but it does change the index of the ones after it.
            for (int i = Dlc.DataSections.Count - 1; i >= 0; i--)
            {
                DlcSection section = Dlc.DataSections[i];

                if (knownSectionIds.Contains(section.Id))
                {
                    // clear the raw data in this DLC data section
                    section.RawData = new byte[0];
                }
                else
                {
                    // if the section id is not recognized remove it completely
                    section.RawData = null;
                    Dlc.DataSections.RemoveAt(i);
                }
            }

            // Now that all the raw data has been removed, reset the raw data flag
            ContainsRawData = false;
        }

        private static void Write(BinaryWriter writer, byte[] value)
        {
            writer.Write(value);
        }

        private void WriteValues(BinaryWriter Out, List<int> values)
        {
            Write(Out, values[0], EndianWsg);
            UInt32 tempLevelQuality = (UInt16)values[1] + (UInt16)values[3] * (UInt32)65536;
            Write(Out, (Int32)tempLevelQuality, EndianWsg);
            Write(Out, values[2], EndianWsg);
            if (RevisionNumber < EnhancedVersion) return;
            Write(Out, values[4], EndianWsg);
            Write(Out, values[5], EndianWsg);
        }

        private void WriteStrings(BinaryWriter Out, List<string> strings)
        {
            foreach (var s in strings)
            {
                Write(Out, s, EndianWsg);
            }
        }

        private void WritePaddings(BinaryWriter Out, byte[] data)
        {
            Write(Out, data);
        }

        private void WriteObject<T>(BinaryWriter Out, T obj) where T : Object
        {
            WriteStrings(Out, obj.Strings);
            WriteValues(Out, obj.GetValues());
        }

        private void WriteObjects<T>(BinaryWriter Out, List<T> objs) where T : Object
        {
            Write(Out, objs.Count, EndianWsg);
            foreach (var obj in objs)
            {
                WriteObject(Out, obj);
            }
        }

        ///<summary>Save the current data to a WSG as a byte[]</summary>
        public byte[] WriteWsg()
        {
            MemoryStream outStream = new MemoryStream();
            BinaryWriter Out = new BinaryWriter(outStream);

            SplitInventoryIntoPacks();

            Out.Write(Encoding.ASCII.GetBytes(MagicHeader));
            Write(Out, VersionNumber, EndianWsg);
            Out.Write(Encoding.ASCII.GetBytes(Plyr));
            Write(Out, RevisionNumber, EndianWsg);
            Write(Out, Class, EndianWsg);
            Write(Out, Level, EndianWsg);
            Write(Out, Experience, EndianWsg);
            Write(Out, SkillPoints, EndianWsg);
            Write(Out, Unknown1, EndianWsg);
            Write(Out, Cash, EndianWsg);
            Write(Out, FinishedPlaythrough1, EndianWsg);
            Write(Out, NumberOfSkills, EndianWsg);

            for (int progress = 0; progress < NumberOfSkills; progress++) //Write Skills
            {
                Write(Out, SkillNames[progress], EndianWsg);
                Write(Out, LevelOfSkills[progress], EndianWsg);
                Write(Out, ExpOfSkills[progress], EndianWsg);
                Write(Out, InUse[progress], EndianWsg);
            }

            Write(Out, Vehi1Color, EndianWsg);
            Write(Out, Vehi2Color, EndianWsg);
            Write(Out, Vehi1Type, EndianWsg);
            Write(Out, Vehi2Type, EndianWsg);
            Write(Out, NumberOfPools, EndianWsg);

            for (int progress = 0; progress < NumberOfPools; progress++) //Write Ammo Pools
            {
                Write(Out, ResourcePools[progress], EndianWsg);
                Write(Out, AmmoPools[progress], EndianWsg);
                Write(Out, RemainingPools[progress], EndianWsg);
                Write(Out, PoolLevels[progress], EndianWsg);
            }


            WriteObjects(Out, Items1); //Write Items

            Write(Out, BackpackSize, EndianWsg);
            Write(Out, EquipSlots, EndianWsg);

            WriteObjects(Out, Weapons1); //Write Weapons

            Int16 count = (Int16)_challenges.Count();
            Write(Out, count * 7 + 10, EndianWsg);
            Write(Out, ChallengeDataBlockId, EndianWsg);
            Write(Out, count * 7 + 2, EndianWsg);
            Write(Out, count, EndianWsg);
            foreach (ChallengeDataEntry challenge in _challenges)
            {
                Write(Out, challenge.Id, EndianWsg);
                Out.Write(challenge.TypeId);
                Write(Out, challenge.Value, EndianWsg);
            }

            Write(Out, TotalLocations, EndianWsg);

            for (int progress = 0; progress < TotalLocations; progress++) //Write Locations
                Write(Out, LocationStrings[progress], EndianWsg);

            Write(Out, CurrentLocation, EndianWsg);
            Write(Out, SaveInfo1To5[0], EndianWsg);
            Write(Out, SaveInfo1To5[1], EndianWsg);
            Write(Out, SaveInfo1To5[2], EndianWsg);
            Write(Out, SaveInfo1To5[3], EndianWsg);
            Write(Out, SaveInfo1To5[4], EndianWsg);
            Write(Out, SaveNumber, EndianWsg);
            Write(Out, SaveInfo7To10[0], EndianWsg);
            Write(Out, SaveInfo7To10[1], EndianWsg);
            Write(Out, NumberOfQuestLists, EndianWsg);

            for (int listIndex = 0; listIndex < NumberOfQuestLists; listIndex++)
            {
                QuestTable qt = QuestLists[listIndex];
                Write(Out, qt.Index, EndianWsg);
                Write(Out, qt.CurrentQuest, EndianWsg);
                Write(Out, qt.TotalQuests, EndianWsg);

                int questCount = qt.TotalQuests;
                for (int questIndex = 0; questIndex < questCount; questIndex++)  //Write Playthrough 1 Quests
                {
                    QuestEntry qe = qt.Quests[questIndex];
                    Write(Out, qe.Name, EndianWsg);
                    Write(Out, qe.Progress, EndianWsg);
                    Write(Out, qe.DlcValue1, EndianWsg);
                    Write(Out, qe.DlcValue2, EndianWsg);

                    int objectiveCount = qe.NumberOfObjectives;
                    Write(Out, objectiveCount, EndianWsg);

                    for (int i = 0; i < objectiveCount; i++)
                    {
                        Write(Out, qe.Objectives[i].Description, EndianWsg);
                        Write(Out, qe.Objectives[i].Progress, EndianWsg);
                    }
                }
            }

            Write(Out, TotalPlayTime, EndianWsg);
            Write(Out, LastPlayedDate, EndianWsg);
            Write(Out, CharacterName, EndianWsg);
            Write(Out, Color1, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Color2, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Color3, EndianWsg); //ABGR Big (X360, PS3), RGBA Little (PC)
            Write(Out, Head, EndianWsg);

            if (RevisionNumber >= EnhancedVersion)
            {
                Write(Out, Unknown2);
            }
            else
            {
                int numberOfPromoCodesUsed = PromoCodesUsed.Count;
                Write(Out, numberOfPromoCodesUsed, EndianWsg);
                for (int i = 0; i < numberOfPromoCodesUsed; i++)
                    Write(Out, PromoCodesUsed[i], EndianWsg);
                int numberOfPromoCodesRequiringNotification = PromoCodesRequiringNotification.Count;
                Write(Out, numberOfPromoCodesRequiringNotification, EndianWsg);
                for (int i = 0; i < numberOfPromoCodesRequiringNotification; i++)
                    Write(Out, PromoCodesRequiringNotification[i], EndianWsg);
            }
 
            Write(Out, NumberOfEchoLists, EndianWsg);
            for (int listIndex = 0; listIndex < NumberOfEchoLists; listIndex++)
            {
                EchoTable et = EchoLists[listIndex];
                Write(Out, et.Index, EndianWsg);
                Write(Out, et.TotalEchoes, EndianWsg);

                for (int echoIndex = 0; echoIndex < et.TotalEchoes; echoIndex++) //Write Locations
                {
                    EchoEntry ee = et.Echoes[echoIndex];
                    Write(Out, ee.Name, EndianWsg);
                    Write(Out, ee.DlcValue1, EndianWsg);
                    Write(Out, ee.DlcValue2, EndianWsg);
                }
            }

            Dlc.DlcSize = 0;
            // This loop writes the base data for each section into byte[]
            // BaseData so its size can be obtained and it can easily be
            // written to the output stream as a single block.  Calculate
            // DLC.DLC_Size as it goes since that has to be written before
            // the blocks are written to the output stream.
            foreach (DlcSection section in Dlc.DataSections)
            {
                MemoryStream tempStream = new MemoryStream();
                BinaryWriter memwriter = new BinaryWriter(tempStream);
                switch (section.Id)
                {
                    case DlcData.Section1Id:
                        memwriter.Write(Dlc.DlcUnknown1);
                        Write(memwriter, Dlc.BankSize, EndianWsg);
                        Write(memwriter, Dlc.BankInventory.Count, EndianWsg);
                        for (int i = 0; i < Dlc.BankInventory.Count; i++)
                            Write(memwriter, Dlc.BankInventory[i].Serialize(EndianWsg));
                        break;
                    case DlcData.Section2Id:
                        Write(memwriter, Dlc.DlcUnknown2, EndianWsg);
                        Write(memwriter, Dlc.DlcUnknown3, EndianWsg);
                        Write(memwriter, Dlc.DlcUnknown4, EndianWsg);
                        Write(memwriter, Dlc.SkipDlc2Intro, EndianWsg);
                        break;
                    case DlcData.Section3Id:
                        memwriter.Write(Dlc.DlcUnknown5);
                        break;
                    case DlcData.Section4Id:
                        memwriter.Write(Dlc.SecondaryPackEnabled);
                        // The DLC backpack items
                        WriteObjects(memwriter, Items2);
                        // The DLC backpack weapons
                        WriteObjects(memwriter, Weapons2);
                        break;
                }
                section.BaseData = tempStream.ToArray();
                Dlc.DlcSize += section.BaseData.Count() + section.RawData.Count() + 8; // 8 = 4 bytes for id, 4 bytes for length
            }

            // Now its time to actually write all the data sections to the output stream
            Write(Out, Dlc.DlcSize, EndianWsg);
            foreach (DlcSection section in Dlc.DataSections)
            {
                Write(Out, section.Id, EndianWsg);
                int sectionLength = section.BaseData.Count() + section.RawData.Count();
                Write(Out, sectionLength, EndianWsg);
                Out.Write(section.BaseData);
                Out.Write(section.RawData);
                section.BaseData = null; // BaseData isn't needed anymore.  Free it.
            }
            if (RevisionNumber >= EnhancedVersion)
            {
                //Past end padding
                Write(Out, Unknown3);
            }
            // Clear the temporary lists used to split primary and DLC pack data
            Items1 = null;
            Items2 = null;
            Weapons1 = null;
            Weapons2 = null;
            return outStream.ToArray();
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
            if ((Dlc.HasSection4 == false) || (Dlc.SecondaryPackEnabled == 0))
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

                if ((item.Level == 0) && (item.Strings[0].Substring(0, 3) != "dlc"))
                    Items1.Add(item);
                else
                    Items2.Add(item);
            }
            foreach (var weapon in Weapons)
            {

                if ((weapon.Level == 0) && (weapon.Strings[0].Substring(0, 3) != "dlc"))
                    Weapons1.Add(weapon);
                else
                    Weapons2.Add(weapon);
            }
        }

        private const byte SubPart = 32;

        public sealed class BankEntry : Object
        {
            public Byte TypeId;

            public int Quantity
            {
                get { return _values[0]; }
                set { _values[0] = value; }
            }

            public byte[] Serialize(ByteOrder endian)
            {
                var bytes = new List<byte>();
                if (TypeId != 1 && TypeId != 2)
                    throw new FormatException("Bank entry to be written has an invalid Type ID.  TypeId = " + TypeId);
                bytes.Add(TypeId);
                int count = 0;
                foreach (var component in Strings)
                {
                    if (component == "None")
                    {
                        bytes.AddRange(new byte[25]);
                        continue;
                    }
                    bytes.Add(32);
                    Console.WriteLine("Component " + component);
                    var subComponentArray = component.Split('.');
                    bytes.AddRange(new byte[(6 - subComponentArray.Length) * 4]);
                    foreach (var subComponent in subComponentArray)
                    {
                        bytes.AddRange(GetBytesFromString(subComponent, endian));
                    }

                    if (count == 2)
                    {
                        bytes.AddRange(GetBytesFromInt((UInt16)Quality + (UInt16)Level * (UInt32)65536, endian));
                    }
                    count++;
                }
                bytes.AddRange(new byte[8]);
                bytes.Add((byte)EquipedSlot);
                bytes.Add(1);
                if (ExportValuesCount > 4)
                {
                    bytes.Add((byte)Junk);
                    bytes.Add((byte)Locked);
                }
                if (TypeId == 1)
                {
                    bytes.AddRange(GetBytesFromInt(Quantity, endian));
                }
                else
                {
                    if (ExportValuesCount > 4)
                        bytes.Add((byte)Locked);
                    else
                        bytes.Add((byte)Quantity);
                }
                return bytes.ToArray();
            }

            private void DeserializePart(BinaryReader reader, ByteOrder endian, out string part, int index)
            {
                var mask = reader.ReadByte();
                if (mask == 0)
                {
                    part = "None";
                    ReadBytes(reader, 24, endian);
                }
                else
                {
                    var padding = SearchForString(reader, endian);
                    string partName = "";
                    for (int i = 0; i < (padding.Length == 8 ? 4 : 3); i++)
                    {
                        var tmp = ReadString(reader, endian);
                        if (i != 0)
                            partName += "." + tmp;
                        else
                            partName += tmp;
                    }
                    part = partName;
                    if (index == 2)
                    {
                        UInt32 temp = (UInt32)ReadInt32(reader, endian);
                        Quality = (Int16)(temp % (UInt32)65536);
                        Level = (Int16)(temp / (UInt32)65536);
                    }
                }
            }

            private static void ReadOldFooter(BankEntry entry, BinaryReader reader, ByteOrder endian)
            {
                byte[] footer = reader.ReadBytes(10);
                entry.EquipedSlot = footer[8];
                entry.Quantity = entry.TypeId == 1 ? ReadInt32(reader, endian) : reader.ReadByte();
            }

            private static void ReadNewFooter(BankEntry entry, BinaryReader reader, ByteOrder endian)
            {
                byte[] footer = reader.ReadBytes(12);
                entry.EquipedSlot = footer[8];
                if (entry.TypeId == 1)
                {
                    entry.Quantity = ReadInt32(reader, endian);//Ammo
                    entry.Junk = footer[10];
                    entry.Locked = footer[11];
                }
                else
                {
                    entry.Quantity = footer[10];//Ammo
                    entry.Junk = footer[11];
                    entry.Locked = reader.ReadByte();
                }
            }

            public static void RepaireItem(BinaryReader reader, ByteOrder endian, BankEntry entry, int offset)
            {
                Console.WriteLine("Repair item");
                reader.BaseStream.Position -= offset + (entry.TypeId == 1 ? 16 : 11);
                ReadOldFooter(entry, reader, endian);
            }

            public void Deserialize(BinaryReader reader, ByteOrder endian, BankEntry previous)
            {
                TypeId = reader.ReadByte();
                if (TypeId != 1 && TypeId != 2)
                {
                //Try to repair broken item
                    if (previous != null)
                    {
                        RepaireItem(reader, endian, previous, 1);
                        TypeId = reader.ReadByte();
                        Console.WriteLine(TypeId + " " + reader.ReadByte());
                        reader.BaseStream.Position -= 1;
                        if (TypeId != 1 && TypeId != 2)
                        {
                            reader.BaseStream.Position -= 1 + (previous.TypeId == 1 ? 4 : 1);
                            SearchNextItem(reader, endian);
                            TypeId = reader.ReadByte();
                        }
                        else
                        {
                            BankValuesCount = 4;
                        }
                    }
                }
                
                Strings = new List<string>();
                Strings.AddRange(new string[TypeId == 1 ? 14 : 9]);
                for (var i = 0; i < Strings.Count; i++)
                {
                    DeserializePart(reader, endian, out var part, i);
                    Strings[i] = part;
                    Console.WriteLine(part);
                }

                if (BankValuesCount > 4)
                    ReadNewFooter(this, reader, endian);
                else
                    ReadOldFooter(this, reader, endian);
            }

            private static byte[] SearchNextItem(BinaryReader reader, ByteOrder endian)
            {
                var bytes = new List<byte>();
                var b = ReadBytes(reader, 1, endian);
                short val = b[0];
                if (val == SubPart)
                {
                    reader.BaseStream.Position -= 2;
                    return bytes.ToArray();
                }
                bytes.AddRange(b);
                //Looking for next byte != 0
                while (val != SubPart)
                {
                    b = ReadBytes(reader, 1, endian);
                    val = b[0];
                    if (val != SubPart)
                        bytes.AddRange(b);
                    else
                    {
                        bytes.RemoveAt(bytes.Count - 1);
                        reader.BaseStream.Position -= 2;
                    }
                }
                return bytes.ToArray();
            }
        }

        private BankEntry CreateBankEntry(BinaryReader reader, bool last)
        {
            //Create new entry
            BankEntry entry = new BankEntry();
            entry.Deserialize(reader, EndianWsg, Dlc.BankInventory.Count == 0 ? null : Dlc.BankInventory[Dlc.BankInventory.Count - 1]);
            return entry;
        }
    }
}
