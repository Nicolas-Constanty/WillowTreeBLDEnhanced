using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using X360.IO;
using X360.STFS;

namespace WillowTree
{

    // Fun fun fun time. Okay, well after I decided to ditch using DJsIO I ran into a number of problems.
    // 90% of them revolved around Microsofts total lack of Big Endian support. So I had to add my own, 
    // and by that I mean I snatched some code off of Google and twisted it to work for WillowTree. Well,
    // it works doesn't it? Aaaaaaaanyway, I really want to fix up the opening/saving process so that you
    // can edit specific parts of the save without requiring a complete rebuild. For example, if you just 
    // (want to) edit a weapon, it rebuilds the weapon list only.
    public enum ByteOrder
    {
        LittleEndian,
        BigEndian
    }

    public class WillowSaveGame
    {
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

        private static long ReadInt64(BinaryReader reader, ByteOrder Endian)
        {
            return BitConverter.ToInt64(ReadBytes(reader, sizeof(long), Endian), 0);
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

        
    ///<summary>Reads a string in the format used by the WSGs</summary>
        private static string ReadString(BinaryReader reader, ByteOrder endian, int tempLengthValue)
    {
        if (tempLengthValue == 0)
            return string.Empty;

        string value;

        // Read string data (either single-byte or Unicode).
        if (tempLengthValue < 0)
        {
            // Convert the length value into a unicode byte count.
            tempLengthValue = -tempLengthValue * 2;

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
            // Read the byte data (and ensure that the number of bytes
            // read matches the number of bytes it was supposed to read--
            // BinaryReader may not return the same number of bytes read).
            byte[] data = reader.ReadBytes(tempLengthValue);
            if (data.Length != tempLengthValue)
                throw new EndOfStreamException();

            // Convert the byte data into a string.
            value = SingleByteEncoding.GetString(data);
        }

        // Look for the null terminator character. If not found, return
        // the entire string.
        int nullTerminatorIndex = value.IndexOf('\0');
        if (nullTerminatorIndex < 0)
            return value;

        // If the null terminator is the first character in the string,
        // then return an empty string (small reference optimization).
        if (nullTerminatorIndex == 0)
            return string.Empty;

        // Return a portion of the string, excluding the null terminator
        // and any characters after the null terminator.
        return value.Substring(0, nullTerminatorIndex);
    }
///<summary>Reads a string in the format used by the WSGs</summary>
private static string ReadString(BinaryReader reader, ByteOrder endian)
        {
            int tempLengthValue = ReadInt32(reader, endian);
            if (tempLengthValue == 0)
                return string.Empty;

            string value;

            // Read string data (either single-byte or Unicode).
            if (tempLengthValue < 0)
            {
                // Convert the length value into a unicode byte count.
                tempLengthValue = -tempLengthValue * 2;

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
                // Read the byte data (and ensure that the number of bytes
                // read matches the number of bytes it was supposed to read--
                // BinaryReader may not return the same number of bytes read).
                byte[] data = reader.ReadBytes(tempLengthValue);
                if (data.Length != tempLengthValue)
                    throw new EndOfStreamException();

                // Convert the byte data into a string.
                value = SingleByteEncoding.GetString(data);
            }

            // Look for the null terminator character. If not found, return
            // the entire string.
            int nullTerminatorIndex = value.IndexOf('\0');
            if (nullTerminatorIndex < 0)
                return value;

            // If the null terminator is the first character in the string,
            // then return an empty string (small reference optimization).
            if (nullTerminatorIndex == 0)
                return string.Empty;

            // Return a portion of the string, excluding the null terminator
            // and any characters after the null terminator.
            return value.Substring(0, nullTerminatorIndex);
        }

        private static void Write(BinaryWriter writer, byte[] value, ByteOrder endian)
        {
            writer.Write(value);
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

            // Look for any non-ASCII characters in the input.
            bool requiresUnicode = false;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] > 256)
                {
                    requiresUnicode = true;
                    break;
                }
            }

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

        public ByteOrder EndianWSG;
        public BinaryReader WSGReader;

        public string Platform;
        public string OpenedWSG;
        public bool ContainsRawData;
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

        public class RawDataInfo
        {
            public byte[] data = new byte[0];
            public int nextValue;
        }
        //Delegate for read String and Value
        public delegate List<string> ReadStringsFunction(BinaryReader reader, ByteOrder bo);
        public delegate List<int> ReadValuesFunction(BinaryReader reader, ByteOrder bo);

        public class Object
        {
            public List<string> Strings = new List<string>();
            public List<int> Values = new List<int>();
            public byte[] Paddings;

            public ReadStringsFunction readStrings = null;
            public ReadValuesFunction readValues = ReadObjectValues;
        }
        public class Item : Object {

            public Item()
            {
                readStrings = ReadItemStrings;
            }
        }
        public class Weapon : Object {

            public Weapon()
            {
                readStrings = ReadWeaponStrings;
            }

            public int Ammo {
                get { return Values[0]; }
                set { Values[0] = value; }
            }
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

        //Item Arrays
        public int NumberOfItems;
        public List<Item> Items = new List<Item>();
        public List<Weapon> Weapons = new List<Weapon>();

        //Backpack Info
        public int BackpackSize;
        public int EquipSlots;
        //Weapon Arrays
        public int NumberOfWeapons;


        //Look, stuff!
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
        //Quest Arrays/Info
        public string CurrentQuest = "None";
        public int TotalPT1Quests;
        public string[] PT1Strings;
        public int[,] PT1Values;
        public string[,] PT1Subfolders;
        public int UnknownPT1QuestValue;
        public int TotalPT2Quests;
        public string SecondaryQuest = "None";
        public string[] PT2Strings;
        public int[,] PT2Values;
        public string[,] PT2Subfolders;
        public int UnknownPT2QuestValue;
        //More unknowns and color info.
        public int TotalPlayTime;
        public string LastPlayedDate;
        public string CharacterName;
        public int Color1;
        public int Color2;
        public int Color3;
        public int Head;
        public List<int> PromoCodesUsed;
        public List<int> PromoCodesRequiringNotification;
        //Echo Info
        public int NumberOfEchoLists;
        public int EchoIndex0;
        public int NumberOfEchos;
        public string[] EchoStrings;
        public int[,] EchoValues;
        public int EchoIndex1;
        public int NumberOfEchosPT2;
        public string[] EchoStringsPT2;
        public int[,] EchoValuesPT2;
        public byte[] Unknown2;
        public byte[] Unknown3;
        //public byte[] MiscData;

        // Temporary lists used for primary pack data when the inventory is split
        public List<Item> Items1 = new List<Item>();
        public List<Weapon> Weapons1 = new List<Weapon>();
        // Temporary lists used for primary pack data when the inventory is split
        public List<Item> Items2 = new List<Item>();
        public List<Weapon> Weapons2 = new List<Weapon>();


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
            public int SkipDLC2Intro;//
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

            public int TotalItems;
            public List<List<string>> ItemParts = new List<List<string>>();
            public List<int> ItemQuantity = new List<int>();
            public List<int> ItemLevel = new List<int>();
            public List<int> ItemQuality = new List<int>();
            public List<int> ItemEquipped = new List<int>();

            public int TotalWeapons;
            public List<List<string>> WeaponParts = new List<List<string>>();
            public List<int> WeaponAmmo = new List<int>();
            public List<int> WeaponLevel = new List<int>();
            public List<int> WeaponQuality = new List<int>();
            public List<int> WeaponEquippedSlot = new List<int>();
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
                        return "X360";
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
        public MemoryStream WSGExtract(Stream InputX360File)
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

        ///<summary>Converts a file to a byte[] for use with ReadWSG</summary>
        public void OpenWSG(string inputFile)
        {
            using (FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Platform = WSGType(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);

                if (string.Equals(Platform, "X360", StringComparison.Ordinal))
                {
                    using (MemoryStream x360FileStream = WSGExtract(fileStream))
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

        private static bool EOF(BinaryReader binaryReader)
        {
            var bs = binaryReader.BaseStream;
            return (bs.Position == bs.Length);
        }

        ///<summary>Reads and decompiles the contents of a WSG</summary>
        public void ReadWSG(Stream fileStream)
        {
            BinaryReader TestReader = new BinaryReader(fileStream, Encoding.ASCII);

            ContainsRawData = false;
            MagicHeader = new string(TestReader.ReadChars(3));
            VersionNumber = TestReader.ReadInt32();

            // BinaryReader.ReadInt32 always uses little-endian byte order.
            bool LittleEndian;
            if (VersionNumber == 0x00000002)
            {
                LittleEndian = true;
            }
            else if (VersionNumber == 0x02000000)
            {
                VersionNumber = 2;
                LittleEndian = false;
            }
            else
            {
                throw new FileFormatException("WSG version number does match any known version (" + VersionNumber + ").");
            }
            EndianWSG = (LittleEndian ? ByteOrder.LittleEndian : ByteOrder.BigEndian);

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
            NumberOfSkills = ReadInt32(TestReader, EndianWSG);
            Skills(TestReader, NumberOfSkills);
            Vehi1Color = ReadInt32(TestReader, EndianWSG);
            Vehi2Color = ReadInt32(TestReader, EndianWSG);
            Vehi1Type = ReadInt32(TestReader, EndianWSG);
            Vehi2Type = ReadInt32(TestReader, EndianWSG);
            NumberOfPools = ReadInt32(TestReader, EndianWSG);
            Ammo(TestReader, NumberOfPools);
            NumberOfItems = ReadInt32(TestReader, EndianWSG);
            ReadObjects(TestReader, ref Items, NumberOfItems, sizeof(int) * 2, false);
            BackpackSize = ReadInt32(TestReader, EndianWSG);
            EquipSlots = ReadInt32(TestReader, EndianWSG);
            NumberOfWeapons = ReadInt32(TestReader, EndianWSG);
            ReadObjects(TestReader, ref Weapons, NumberOfWeapons, sizeof(int) * 2, false);

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

            TotalLocations = ReadInt32(TestReader, EndianWSG);
            Locations(TestReader, TotalLocations);
            CurrentLocation = ReadString(TestReader, EndianWSG);
            SaveInfo1to5[0] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[1] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[2] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[3] = ReadInt32(TestReader, EndianWSG);
            SaveInfo1to5[4] = ReadInt32(TestReader, EndianWSG);
            SaveNumber = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[0] = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[1] = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[2] = ReadInt32(TestReader, EndianWSG);
            SaveInfo7to10[3] = ReadInt32(TestReader, EndianWSG);
            CurrentQuest = ReadString(TestReader, EndianWSG);
            TotalPT1Quests = ReadInt32(TestReader, EndianWSG);
            PT1Quests(TestReader, TotalPT1Quests);
            UnknownPT1QuestValue = ReadInt32(TestReader, EndianWSG);
            SecondaryQuest = ReadString(TestReader, EndianWSG);
            TotalPT2Quests = ReadInt32(TestReader, EndianWSG);
            PT2Quests(TestReader, TotalPT2Quests); //Saves playthrough 2 quests as arrays of strings and ints (15 values, -1 for none if not used)
            UnknownPT2QuestValue = ReadInt32(TestReader, EndianWSG); //Is either 2 or 0
            ReadString(TestReader, EndianWSG); //Z0_Missions.Missions.M_IntroStateSaver
            ReadInt32(TestReader, EndianWSG); //1
            ReadString(TestReader, EndianWSG); //Z0_Missions.Missions.M_IntroStateSaver
            ReadInt32(TestReader, EndianWSG); //2
            ReadInt32(TestReader, EndianWSG); //0
            ReadInt32(TestReader, EndianWSG); //0
            ReadInt32(TestReader, EndianWSG); //0
            TotalPlayTime = ReadInt32(TestReader, EndianWSG);
            LastPlayedDate = ReadString(TestReader, EndianWSG);
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

            NumberOfEchoLists = ReadInt32(TestReader, EndianWSG);

            EchoIndex0 = ReadInt32(TestReader, EndianWSG);
            NumberOfEchos = ReadInt32(TestReader, EndianWSG);
            Echos(TestReader, NumberOfEchos); //One string, two identical int32s

            if (NumberOfEchoLists > 1) // there can be 1 or 2 echo lists
            {
                EchoIndex1 = ReadInt32(TestReader, EndianWSG);
                NumberOfEchosPT2 = ReadInt32(TestReader, EndianWSG);
                EchosPT2(TestReader, NumberOfEchosPT2);
            }

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
                        case DLC_Data.Section1Id:
                            DLC.HasSection1 = true;
                            DLC.DLC_Unknown1 = dlcDataReader.ReadByte();
                            DLC.BankSize = ReadInt32(dlcDataReader, EndianWSG);
                            int bankEntriesCount = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.BankInventory = new List<BankEntry>();
                            for (int i = 0; i < bankEntriesCount; i++)
                            {
                                DLC.BankInventory.Add(ReadBankEntry(dlcDataReader, EndianWSG));
                            }
                            break;
                        case DLC_Data.Section2Id:
                            DLC.HasSection2 = true;
                            DLC.DLC_Unknown2 = ReadInt32(dlcDataReader, EndianWSG);

                            DLC.DLC_Unknown3 = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.DLC_Unknown4 = ReadInt32(dlcDataReader, EndianWSG);
                            DLC.SkipDLC2Intro = ReadInt32(dlcDataReader, EndianWSG);
                            break;
                        case DLC_Data.Section3Id:
                            DLC.HasSection3 = true;
                            DLC.DLC_Unknown5 = dlcDataReader.ReadByte();
                            break;
                        case DLC_Data.Section4Id:
                            DLC.HasSection4 = true;
                            DLC.SecondaryPackEnabled = dlcDataReader.ReadByte();
                            DLC.NumberOfItems = ReadInt32(dlcDataReader, EndianWSG);
                            ReadObjects(dlcDataReader, ref Items, DLC.NumberOfItems, sizeof(int), true);
                            NumberOfItems += DLC.NumberOfItems;
                            DLC.NumberOfWeapons = ReadInt32(dlcDataReader, EndianWSG);
                            ReadObjects(dlcDataReader, ref Weapons, DLC.NumberOfWeapons, sizeof(int), true);
                            NumberOfWeapons += DLC.NumberOfWeapons;
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

        private void Skills(BinaryReader DJsIO, int NumOfSkills)
        {
            string[] TempSkillNames = new string[NumOfSkills];
            int[] TempLevelOfSkills = new int[NumOfSkills];
            int[] TempExpOfSkills = new int[NumOfSkills];
            int[] TempInUse = new int[NumOfSkills];

            for (int Progress = 0; Progress < NumOfSkills; Progress++)
            {
                TempSkillNames[Progress] = ReadString(DJsIO, EndianWSG);
                TempLevelOfSkills[Progress] = ReadInt32(DJsIO, EndianWSG);
                TempExpOfSkills[Progress] = ReadInt32(DJsIO, EndianWSG);
                TempInUse[Progress] = ReadInt32(DJsIO, EndianWSG);
            }
            SkillNames = TempSkillNames;
            LevelOfSkills = TempLevelOfSkills;
            ExpOfSkills = TempExpOfSkills;
            InUse = TempInUse;
        } //Ignore all of the "DJsIO", I was just too lazy to rename them after I removed most of my dependence on the X360 DLL.
        private void Ammo(BinaryReader reader, int NumOfPools)
        {
            string[] TempResourcePools = new string[NumOfPools];
            string[] TempAmmoPools = new string[NumOfPools];
            float[] TempRemainingPools = new float[NumOfPools];
            int[] TempPoolLevels = new int[NumOfPools];

            for (int Progress = 0; Progress < NumOfPools; Progress++)
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
        }

        private RawDataInfo CheckIntPadding(BinaryReader reader, int paddingSize)
        {
            var rd = new RawDataInfo();
            int count = -paddingSize;
            rd.data = ReadBytes(reader, paddingSize, EndianWSG);
            bool findString = false;
            while (!findString && rd.data.Length < sizeof(int) * 2)
            {
                var extraPaddingData = ReadInt32(reader, EndianWSG);

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
                    rd.data = rd.data.Concat(
        ReadBytes(BitConverter.GetBytes(extraPaddingData), sizeof(int), EndianWSG)
        ).ToArray();
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
            if (RevisionNumber > 38)
            {
                if ((item.Values[3] == 0) || (item.Strings[0].Substring(0, 3) != "dlc"))
                {
                    rd = CheckIntPadding(reader, isDLC ? 0 : paddingSize);
                }
                else
                {
                    paddingSize += sizeof(int);
                    rd = CheckIntPadding(reader, paddingSize);
                }
                item.Paddings = rd.data;
            }
            return item;
        }

        private void ReadObjects<T>(BinaryReader reader, ref List<T> Objects, int groupSize, int paddingSize, bool isDLC) where T : Object, new()
        {
            RawDataInfo rd = null;
            for (int Progress = 0; Progress < groupSize; Progress++)
            {
                Console.WriteLine(Progress + "/" + groupSize);
                Objects.Add(ReadObject<T>(reader, ref rd, paddingSize, isDLC));
            }
        }

        //private void ReadItem(BinaryReader reader, ref List<Weapon> Objects, int groupSize, int paddingSize)
        //{
        //    ReadObjects(reader, ref Objects, groupSize, paddingSize);
        //    if (groupSize > 0)
        //        reader.BaseStream.Position -= paddingSize;
        //}

        //private void ReadWeapons(BinaryReader reader, ref List<Weapon> Objects, int groupSize, int paddingSize)
        //{
        //    ReadObjects(reader, ref Objects, groupSize, paddingSize);
        //}

        private void PT1Quests(BinaryReader DJsIO, int NumOfQuests)
        {
            string[] TempQuestStrings = new string[NumOfQuests];
            int[,] TempQuestValues = new int[NumOfQuests, 9];
            string[,] TempQuestSubfolders = new string[NumOfQuests, 5];

            for (int Progress = 0; Progress < NumOfQuests; Progress++)
            {
                TempQuestStrings[Progress] = ReadString(DJsIO, EndianWSG);
                for (int TotalValues = 0; TotalValues < 4; TotalValues++)
                    TempQuestValues[Progress, TotalValues] = ReadInt32(DJsIO, EndianWSG);

                if (TempQuestValues[Progress, 3] > 0)
                {
                    for (int ExtraValues = 0; ExtraValues < TempQuestValues[Progress, 3]; ExtraValues++)
                    {
                        TempQuestSubfolders[Progress, ExtraValues] = ReadString(DJsIO, EndianWSG);
                        TempQuestValues[Progress, ExtraValues + 4] = ReadInt32(DJsIO, EndianWSG);
                    }
                }
                for (int FillGaps = TempQuestValues[Progress, 3] + 4; FillGaps < 9; FillGaps++)
                    TempQuestValues[Progress, FillGaps] = 0;
            }
            if (CurrentQuest == "None") CurrentQuest = TempQuestStrings[0];
            PT1Strings = TempQuestStrings;
            PT1Values = TempQuestValues;
            PT1Subfolders = TempQuestSubfolders;
        }
        private void PT2Quests(BinaryReader DJsIO, int NumOfQuests)
        {
            string[] TempQuestStrings = new string[NumOfQuests];
            int[,] TempQuestValues = new int[NumOfQuests, 9];
            string[,] TempQuestSubfolders = new string[NumOfQuests, 5];

            for (int Progress = 0; Progress < NumOfQuests; Progress++)
            {
                TempQuestStrings[Progress] = ReadString(DJsIO, EndianWSG);

                for (int TotalValues = 0; TotalValues < 4; TotalValues++)
                    TempQuestValues[Progress, TotalValues] = ReadInt32(DJsIO, EndianWSG);

                if (TempQuestValues[Progress, 3] > 0)
                {
                    for (int ExtraValues = 0; ExtraValues < TempQuestValues[Progress, 3]; ExtraValues++)
                    {
                        TempQuestSubfolders[Progress, ExtraValues] = ReadString(DJsIO, EndianWSG);
                        TempQuestValues[Progress, ExtraValues + 4] = ReadInt32(DJsIO, EndianWSG);
                    }
                    for (int FillGaps = TempQuestValues[Progress, 3] + 4; FillGaps < 9; FillGaps++)
                        TempQuestValues[Progress, FillGaps] = 0;
                }
            }

            if (SecondaryQuest == "None") SecondaryQuest = TempQuestStrings[0];
            PT2Strings = TempQuestStrings;
            PT2Values = TempQuestValues;
            PT2Subfolders = TempQuestSubfolders;
        }
        private void Locations(BinaryReader DJsIO, int NumOfLocations)
        {
            string[] TempLocationStrings = new string[NumOfLocations];

            for (int Progress = 0; Progress < NumOfLocations; Progress++)
                TempLocationStrings[Progress] = ReadString(DJsIO, EndianWSG);
            LocationStrings = TempLocationStrings;
        }
        private void Echos(BinaryReader DJsIO, int NumOfEchos)
        {
            string[] TempEchoStrings = new string[NumOfEchos];
            int[,] TempEchoValues = new int[NumOfEchos, 2];

            for (int Progress = 0; Progress < NumOfEchos; Progress++)
            {
                TempEchoStrings[Progress] = ReadString(DJsIO, EndianWSG);
                for (int TotalValues = 0; TotalValues < 2; TotalValues++)
                    TempEchoValues[Progress, TotalValues] = ReadInt32(DJsIO, EndianWSG);
            }
            EchoStrings = TempEchoStrings;
            EchoValues = TempEchoValues;
        }
        private void EchosPT2(BinaryReader DJsIO, int NumOfEchos)
        {
            string[] TempEchoStrings = new string[NumOfEchos];
            int[,] TempEchoValues = new int[NumOfEchos, 2];

            for (int Progress = 0; Progress < NumOfEchos; Progress++)
            {
                TempEchoStrings[Progress] = ReadString(DJsIO, EndianWSG);

                for (int TotalValues = 0; TotalValues < 2; TotalValues++)
                    TempEchoValues[Progress, TotalValues] = ReadInt32(DJsIO, EndianWSG);
            }
            EchoStringsPT2 = TempEchoStrings;
            EchoValuesPT2 = TempEchoValues;
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
        public byte[] SaveWSG()
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

            //Write Backpack's items
            WriteObjects(Out, Items1);

            Write(Out, BackpackSize, EndianWSG);
            Write(Out, EquipSlots, EndianWSG);

            //Write Backpack's weapons
            WriteObjects(Out, Weapons1);

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
            Write(Out, SaveInfo7to10[2], EndianWSG);
            Write(Out, SaveInfo7to10[3], EndianWSG);

            Write(Out, CurrentQuest, EndianWSG);
            Write(Out, TotalPT1Quests, EndianWSG);

            for (int Progress = 0; Progress < TotalPT1Quests; Progress++)  //Write Playthrough 1 Quests
            {
                Write(Out, PT1Strings[Progress], EndianWSG);
                for (int TotalValues = 0; TotalValues < 4; TotalValues++)
                    Write(Out, PT1Values[Progress, TotalValues], EndianWSG);

                if (PT1Values[Progress, 3] > 0) //Checks for subfolders
                {
                    for (int ExtraValues = 0; ExtraValues < PT1Values[Progress, 3]; ExtraValues++)
                    {
                        Write(Out, PT1Subfolders[Progress, ExtraValues], EndianWSG); //Write Subfolder
                        Write(Out, PT1Values[Progress, ExtraValues + 4], EndianWSG); //Write Subfolder value
                    }
                }
            }
            Write(Out, UnknownPT1QuestValue, EndianWSG);

            Write(Out, SecondaryQuest, EndianWSG);
            Write(Out, TotalPT2Quests, EndianWSG);

            for (int Progress = 0; Progress < TotalPT2Quests; Progress++)  //Write Playthrough 2 Quests
            {
                Write(Out, PT2Strings[Progress], EndianWSG);

                for (int TotalValues = 0; TotalValues < 4; TotalValues++)
                    Write(Out, PT2Values[Progress, TotalValues], EndianWSG);

                if (PT2Values[Progress, 3] > 0) //Checks for subfolders
                {
                    for (int ExtraValues = 0; ExtraValues < PT2Values[Progress, 3]; ExtraValues++)
                    {
                        Write(Out, PT2Subfolders[Progress, ExtraValues], EndianWSG); //Write Subfolder
                        Write(Out, PT2Values[Progress, ExtraValues + 4], EndianWSG); //Write Subfolder value
                    }
                }
            }
            Write(Out, UnknownPT2QuestValue, EndianWSG); //Is either 2 or 0

            Write(Out, "Z0_Missions.Missions.M_IntroStateSaver", EndianWSG);
            Write(Out, 1, EndianWSG);
            Write(Out, "Z0_Missions.Missions.M_IntroStateSaver", EndianWSG);
            Write(Out, 2, EndianWSG);
            Write(Out, 0, EndianWSG);
            Write(Out, 0, EndianWSG);
            Write(Out, 0, EndianWSG);
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

            // Override the number of echo lists if the user edited the echo list and added PT2 echoes
            if (NumberOfEchosPT2 > 0)
                NumberOfEchoLists = 2;
            Write(Out, NumberOfEchoLists, EndianWSG);

            Write(Out, EchoIndex0, EndianWSG);
            Write(Out, NumberOfEchos, EndianWSG);
            for (int Progress = 0; Progress < NumberOfEchos; Progress++) //Write Locations
            {
                Write(Out, EchoStrings[Progress], EndianWSG);
                Write(Out, EchoValues[Progress, 0], EndianWSG);
                Write(Out, EchoValues[Progress, 1], EndianWSG);
            }

            if (NumberOfEchoLists > 1)
            {
                Write(Out, EchoIndex1, EndianWSG);
                Write(Out, NumberOfEchosPT2, EndianWSG);
                for (int Progress = 0; Progress < NumberOfEchosPT2; Progress++) //Write Locations
                {
                    Write(Out, EchoStringsPT2[Progress], EndianWSG);
                    Write(Out, EchoValuesPT2[Progress, 0], EndianWSG);
                    Write(Out, EchoValuesPT2[Progress, 1], EndianWSG);
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

        public sealed class BankEntry
        {
            public Byte TypeId;
            public List<string> Parts = new List<string>();
            public Int32 AmmoOrQuantity;
            public Byte Equipped;
            public Int16 Quality;
            public Int16 Level;
            public byte[] padding;
        }

        private static string ReadBankString(BinaryReader br, ByteOrder endian)
        {
            byte subObjectMask = br.ReadByte();
            //if ((subObjectMask != 32) && (subObjectMask != 0))
            //    throw new FileFormatException("Bank string has an unknown sub-object mask.  Mask = " + subObjectMask);

            string composed = ReadString(br, endian);
            bool isPreviousSubObject = (subObjectMask & 1) == 1;
            subObjectMask >>= 1;

            for (int i = 1; i < 6; i++)
            {
                string substring = ReadString(br, endian);
                if (!string.IsNullOrEmpty(substring))
                {
                    if (isPreviousSubObject)
                        composed = composed + ":" + substring;
                    else
                        composed = composed + "." + substring;
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

        private BankEntry ReadBankEntry(BinaryReader br, ByteOrder endian)
        {
            BankEntry entry = new BankEntry();
            int partCount;
            entry.TypeId = br.ReadByte();

            switch (entry.TypeId)
            {
                case 1:
                case 2:
                    break;
                default:
                    throw new FormatException("Bank entry to be written has invalid Type ID.  TypeId = " + entry.TypeId);
            }

            for (int i = 0; i < 3; i++)
                entry.Parts.Add(ReadBankString(br, endian));

            UInt32 temp = (UInt32)ReadInt32(br, endian);
            entry.Quality = (Int16)(temp % (UInt32)65536);
            entry.Level = (Int16)(temp / (UInt32)65536);

            switch (entry.TypeId)
            {
                case 1:
                    partCount = 14;
                    break;
                case 2:
                    partCount = 9;
                    break;
                default:
                    partCount = 0;
                    break;
            }

            for (int i = 3; i < partCount; i++)
                entry.Parts.Add(ReadBankString(br, endian));

            byte[] Footer = br.ReadBytes(10);
            // da_fileserver's investigation has led him to believe the footer bytes are:
            // (Int)GameStage - default 0
            // (Int)AwesomeLevel - default 0
            // (Byte)Equipped - default 0
            // (Byte)DropOnDeath - default 1 (this is whether an npc would drop it when it dies not you)
            // (Byte)ShopsHaveInfiniteQuantity - default 0
            // matt911 - It seems apparent that this table is used for more than just the bank inventory 
            // in the game.  None of the values are stored in the inventory part of the savegame
            // except Equipped and even that will be updated immediately when you take the item
            // out of the bank.  I've never seen any of these with anything except the default value
            // in the bank except Equipped so I will store that in case it is not what we think it
            // is and it is important, but I am doubtful that it is.
            for (int i = 0; i < 7; i++)
                System.Diagnostics.Debug.Assert(Footer[i] == 0);
            System.Diagnostics.Debug.Assert(Footer[9] == 1); // This might be 1 for a health pack
            entry.Equipped = Footer[8];

            switch (entry.TypeId)
            {
                case 1: // weapon
                    entry.AmmoOrQuantity = ReadInt32(br, endian);
                    break;
                case 2: // item
                    entry.AmmoOrQuantity = (int)br.ReadByte();
                    break;
                default:
                    entry.AmmoOrQuantity = 0;
                    break;
            }
            if (RevisionNumber > 38)
            {
                List<byte> bytes = new List<byte>();
                byte value = 0;
                int count = 0;
                while (value == 0)
                {
                    value = ReadBytes(br, 1, EndianWSG)[0];
                    if (value == 0)
                        bytes.Add(value);
                    count++;
                }
                entry.padding = bytes.ToArray();
                br.BaseStream.Position -= 1;
            }
            return entry;
        }
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

            for (int i = 0; i < 3; i++)
                WriteBankString(bw, entry.Parts[i], Endian);

            UInt32 grade = (UInt16)entry.Quality + (UInt16)entry.Level * (UInt32)65536;

            Write(bw, (Int32)grade, Endian);

            for (int i = 3; i < entry.Parts.Count; i++)
                WriteBankString(bw, entry.Parts[i], Endian);

            // see ReadBankItem for notes about the footer bytes
            Byte[] Footer = new Byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            Footer[8] = entry.Equipped;
            bw.Write(Footer);

            switch (entry.TypeId)
            {
                case 1: // weapon
                    Write(bw, entry.AmmoOrQuantity, Endian);
                    break;
                case 2: // item
                    bw.Write((Byte)entry.AmmoOrQuantity);
                    break;
                default:
                    throw new FormatException("Bank entry to be written has an invalid Type ID.  TypeId = " + entry.TypeId);
            }
            if (version > 38)
                bw.Write(entry.padding);
        }
    }
}
