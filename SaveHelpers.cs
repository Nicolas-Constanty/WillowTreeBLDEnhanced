using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WillowTree
{
    public static class WillowHelpers
    {
        // Byte

#if false // This may be confusing to some people, so it's been omitted.
        // Note that this exhibits the same return functionality as
        // BinaryReader's ReadBytes method. It may not always return the number
        // of bytes requested (so it must be checked/enforced by the caller).
        public static byte[] ReadBytes(this BinaryReader reader, int count, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            byte[] data = reader.ReadBytes(count);
            if (BitConverter.IsLittleEndian != isLittleEndian)
                Array.Reverse(data);

            return data;
        }
#endif

        public static void ReadCollection(this BinaryReader reader, ICollection<byte> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadByte());
        }

        public static void Write(this BinaryWriter writer, ICollection<byte> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (byte value in collection)
                writer.Write(value);
        }

        // Int16

        public static short ByteSwap(this short value)
        {
            return unchecked((short)((ushort)value).ByteSwap());
        }

        public static short ReadInt16(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            short value = reader.ReadInt16();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        public static void Write(this BinaryWriter writer, short value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<short> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadInt16(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<short> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (short value in collection)
                writer.Write(value, isLittleEndian);
        }

        // UInt16

        [CLSCompliant(false)]
        public static ushort ByteSwap(this ushort value)
        {
            return unchecked((ushort)(((value & 0xFF00) >> 8) | ((value & 0x00FF) << 8)));
        }

        [CLSCompliant(false)]
        public static ushort ReadUInt16(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            ushort value = reader.ReadUInt16();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, ushort value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        [CLSCompliant(false)]
        public static void ReadCollection(this BinaryReader reader, ICollection<ushort> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadUInt16(isLittleEndian));
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, ICollection<ushort> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (ushort value in collection)
                writer.Write(value, isLittleEndian);
        }

        // Int32

        public static int ByteSwap(this int value)
        {
            return unchecked((int)((uint)value).ByteSwap());
        }

        public static int ReadInt32(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            int value = reader.ReadInt32();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        public static void Write(this BinaryWriter writer, int value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<int> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadInt32(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<int> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (int value in collection)
                writer.Write(value, isLittleEndian);
        }

        // UInt32

        [CLSCompliant(false)]
        public static uint ByteSwap(this uint value)
        {
            return ((value & 0x000000FFU) << 24) | ((value & 0x0000FF00U) << 8) | ((value & 0x00FF0000U) >> 8) | ((value & 0xFF000000U) >> 24);

            //// Another way to do a byte swap.
            //value = ((value & 0x00FF00FFU) << 8) | ((value & 0xFF00FF00U) >> 8);
            //value = ((value & 0x0000FFFFU) << 16) | ((value & 0xFFFF0000U) >> 16);
            //return value;
        }

        [CLSCompliant(false)]
        public static uint ReadUInt32(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            uint value = reader.ReadUInt32();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, uint value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        [CLSCompliant(false)]
        public static void ReadCollection(this BinaryReader reader, ICollection<uint> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadUInt32(isLittleEndian));
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, ICollection<uint> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (uint value in collection)
                writer.Write(value, isLittleEndian);
        }

        // Int64

        public static long ByteSwap(this long value)
        {
            return unchecked((long)((ulong)value).ByteSwap());
        }

        public static long ReadInt64(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            long value = reader.ReadInt64();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        public static void Write(this BinaryWriter writer, long value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<long> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadInt64(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<long> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (long value in collection)
                writer.Write(value, isLittleEndian);
        }

        // UInt64

        [CLSCompliant(false)]
        public static ulong ByteSwap(this ulong value)
        {
            //return ((value & 0xFF00000000000000UL) >> 56) | ((value & 0x00FF000000000000UL) >> 40) | ((value & 0x0000FF0000000000UL) >> 24) | ((value & 0x000000FF00000000UL) >> 8) |
            //    ((value & 0x00000000FF000000UL) << 8) | ((value & 0x0000000000FF0000UL) << 24) | ((value & 0x000000000000FF00UL) << 40) | ((value & 0x00000000000000FFUL) << 56);

            // Another way to do a byte swap. (This implementation uses 6
            // "operations", while the implementation above uses 8
            // "operations".)
            value = ((value & 0x00FF00FF00FF00FFUL) << 8) | ((value & 0xFF00FF00FF00FF00UL) >> 8);
            value = ((value & 0x0000FFFF0000FFFFUL) << 16) | ((value & 0xFFFF0000FFFF0000UL) >> 16);
            value = ((value & 0x00000000FFFFFFFFUL) << 32) | ((value & 0xFFFFFFFF00000000UL) >> 32);
            return value;
        }

        [CLSCompliant(false)]
        public static ulong ReadUInt64(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            ulong value = reader.ReadUInt64();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, ulong value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        [CLSCompliant(false)]
        public static void ReadCollection(this BinaryReader reader, ICollection<ulong> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadUInt64(isLittleEndian));
        }

        [CLSCompliant(false)]
        public static void Write(this BinaryWriter writer, ICollection<ulong> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (ulong value in collection)
                writer.Write(value, isLittleEndian);
        }

        // Single

        public static float ByteSwap(this float value)
        {
#if USE_UNSAFE_CODE
            unsafe
            {
                int valueInt = (*(int*)(&value)).ByteSwap();
                return *(float*)(&valueInt);
            }
#else
            // There might be a way to do it involving messing around with the
            // internal representation of a float, but that's messy work.
            byte[] data = BitConverter.GetBytes(value);
            Array.Reverse(data);
            return BitConverter.ToSingle(data, 0);
#endif
        }

        public static float ReadSingle(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            float value = reader.ReadSingle();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        public static void Write(this BinaryWriter writer, float value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<float> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadSingle(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<float> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (float value in collection)
                writer.Write(value, isLittleEndian);
        }

        // Double

        public static double ByteSwap(this double value)
        {
            return BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(value).ByteSwap());
        }

        public static double ReadDouble(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            double value = reader.ReadSingle();
            return !isLittleEndian ? value.ByteSwap() : value;
        }

        public static void Write(this BinaryWriter writer, double value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(!isLittleEndian ? value.ByteSwap() : value);
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<double> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadDouble(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<double> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (float value in collection)
                writer.Write(value, isLittleEndian);
        }

        // Strings

        public static string ReadString(this BinaryReader reader, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            int length = reader.ReadInt32(isLittleEndian);
            if (length == 0)
                return string.Empty;

            string value;
            if (length > 0)
            {
                // Read the byte data (and ensure that the number of bytes read
                // matches the number of bytes it was supposed to read--
                // BinaryReader may not return the number of bytes read).
                byte[] data = reader.ReadBytes(length);
                if (data.Length != length)
                    throw new EndOfStreamException();

                // Decode value using the ASCII encoding.
                value = Encoding.ASCII.GetString(data);
            }
            else
            {
                // Convert the length value into a Unicode byte count.
                length = -length * 2;

                // Read the byte data (and ensure that the number of bytes read
                // matches the number of bytes it was supposed to read--
                // BinaryReader may not return the number of bytes read).
                byte[] data = reader.ReadBytes(length);
                if (data.Length != length)
                    throw new EndOfStreamException();

                // Decode value using the UTF-16 encoding.
                value = Encoding.Unicode.GetString(data);
            }

            // Look for the null terminator character. If not found, return the
            // entire string.
            int nullTerminatorIndex = value.IndexOf('\0');
            if (nullTerminatorIndex < 0)
                return value;

            // If the null terminator is the first character in the string, then
            // return an empty string (small reference optimization).
            if (nullTerminatorIndex == 0)
                return string.Empty;

            // Return a portion of the string, excluding the null terminator and
            // any characters after the null terminator.
            return value.Substring(0, nullTerminatorIndex);
        }

        public static void Write(this BinaryWriter writer, string value, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("reader");

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

            // Generate the bytes (either ASCII or Unicode, depending on input).
            if (!requiresUnicode)
            {
                // Write character length (including null terminator).
                writer.Write(value.Length + 1, isLittleEndian);

                // Write ASCII encoded string.
                writer.Write(Encoding.ASCII.GetBytes(value));

                // Write null terminator.
                writer.Write((byte)0);
            }
            else
            {
                // Write character length (including null terminator).
                writer.Write(-(value.Length + 1), isLittleEndian);

                // Write UTF-16 encoded string.
                writer.Write(Encoding.Unicode.GetBytes(value));

                // Write null terminator.
                writer.Write((char)0);
            }
        }

        public static void ReadCollection(this BinaryReader reader, ICollection<string> collection, bool isLittleEndian)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (collection == null)
                throw new ArgumentNullException("collection");

            collection.Clear();

            for (int count = reader.ReadInt32(isLittleEndian); count > 0; count--)
                collection.Add(reader.ReadString(isLittleEndian));
        }

        public static void Write(this BinaryWriter writer, ICollection<string> collection, bool isLittleEndian)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (collection == null)
                throw new ArgumentNullException("collection");

            writer.Write(collection.Count, isLittleEndian);
            foreach (string value in collection)
                writer.Write(value, isLittleEndian);
        }
    }
}
