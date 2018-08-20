using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PacketScanner.Common
{
    public static class BitArrayReaderExtensions
    {
        public enum Endianness
        {
            LittleEndian,
            BigEndian
        }

        private static readonly Endianness PlatformEndianness = BitConverter.IsLittleEndian
            ? Endianness.LittleEndian
            : Endianness.BigEndian;

        public static string ReadStringAscii(this BinaryReader reader, int length)
        {
            var bytes = reader.ReadBytes(length);
            var s = Encoding.ASCII.GetString(bytes);
            return s;
        }

        public static string ReadStringAsciiNullTerminated(this BinaryReader reader)
        {
            var list = new List<byte>();
            byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                list.Add(b);
            }
            var s = Encoding.ASCII.GetString(list.ToArray());
            return s;
        }

        #region Numbers (with offset)

        private static uint ReadUInt32(BinaryReader reader, Endianness endianness, long offset)
        {
            return ReadAt(reader, offset, () => reader.ReadUInt32(endianness));
        }

        public static uint ReadUInt32BE(this BinaryReader reader, long offset)
        {
            return ReadUInt32(reader, Endianness.BigEndian, offset);
        }

        public static uint ReadUInt32LE(this BinaryReader reader, long offset)
        {
            return ReadUInt32(reader, Endianness.LittleEndian, offset);
        }

        public static void SetPosition(this BinaryReader reader, long offset)
        {
            reader.BaseStream.Position = offset;
        }

        private static T ReadAt<T>(BinaryReader reader, long offset, Func<T> func)
        {
            reader.SetPosition(offset);
            var t = func();
            return t;
        }

        #endregion

        #region Numbers (with endianness)

        public static short ReadInt16(this BinaryReader reader, Endianness endianness)
        {
            var i = reader.ReadInt16();
            return Convert(i, endianness);
        }

        public static int ReadInt32(this BinaryReader reader, Endianness endianness)
        {
            var i = reader.ReadInt32();
            return Convert(i, endianness);
        }

        public static ushort ReadUInt16(this BinaryReader reader, Endianness endianness)
        {
            var i = reader.ReadUInt16();
            return Convert(i, endianness);
        }

        public static uint ReadUInt32(this BinaryReader reader, Endianness endianness)
        {
            var i = reader.ReadUInt32();
            return Convert(i, endianness);
        }

        private static short Convert(short i, Endianness endianness)
        {
            return endianness == PlatformEndianness ? i : Reverse(i);
        }

        private static int Convert(int i, Endianness endianness)
        {
            return endianness == PlatformEndianness ? i : Reverse(i);
        }

        private static ushort Convert(ushort i, Endianness endianness)
        {
            return endianness == PlatformEndianness ? i : Reverse(i);
        }

        private static uint Convert(uint i, Endianness endianness)
        {
            return endianness == PlatformEndianness ? i : Reverse(i);
        }

        private static short Reverse(short i)
        {
            return (short)(((i & 0xFF00) >> 8) |
                            ((i & 0x00FF) << 8));
        }

        public static int SwapEndianness(int value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }

        public static int Reverse(this int i)
        {
            return SwapEndianness(i);
        }

        private static ushort Reverse(ushort i)
        {
            return (ushort)(((i & 0xFF00) >> 8) |
                             ((i & 0x00FF) << 8));
        }

        public static uint Reverse(this uint pThis)
        {
            return ((pThis & 0xFF000000) >> 24) |
                   ((pThis & 0x00FF0000) >> 8) |
                   ((pThis & 0x0000FF00) << 8) |
                   ((pThis & 0x000000FF) << 24);
        }

        #endregion

        #region Numbers (single)

        public static short ReadInt16BE(this BinaryReader reader)
        {
            return ReadInt16(reader, Endianness.BigEndian);
        }

        public static int ReadInt32BE(this BinaryReader reader)
        {
            return ReadInt32(reader, Endianness.BigEndian);
        }

        public static int ReadInt32LE(this BinaryReader reader)
        {
            return ReadInt32(reader, Endianness.LittleEndian);
        }

        public static ushort ReadUInt16BE(this BinaryReader reader)
        {
            return ReadUInt16(reader, Endianness.BigEndian);
        }

        public static ushort ReadUInt16LE(this BinaryReader reader)
        {
            return ReadUInt16(reader, Endianness.LittleEndian);
        }

        public static uint ReadUInt32BE(this BinaryReader reader)
        {
            return ReadUInt32(reader, Endianness.BigEndian);
        }

        public static uint ReadUInt32LE(this BinaryReader reader)
        {
            return ReadUInt32(reader, Endianness.LittleEndian);
        }

        #endregion

        #region Numbers (array)

        public static short[] ReadInt16BEs(this BinaryReader reader, int count)
        {
            return Read<short>(count, reader.ReadInt16BE);
        }

        public static int[] ReadInt32BEs(this BinaryReader reader, int count)
        {
            return Read<int>(count, reader.ReadInt32BE);
        }

        public static int[] ReadInt32LEs(this BinaryReader reader, int count)
        {
            return Read<int>(count, reader.ReadInt32LE);
        }

        public static ushort[] ReadUInt16BEs(this BinaryReader reader, int count)
        {
            return Read<ushort>(count, reader.ReadUInt16BE);
        }

        public static ushort[] ReadUInt16LEs(this BinaryReader reader, int count)
        {
            return Read<ushort>(count, reader.ReadUInt16LE);
        }

        public static uint[] ReadUInt32BEs(this BinaryReader reader, int count)
        {
            return Read<uint>(count, reader.ReadUInt32BE);
        }

        public static uint[] ReadUInt32LEs(this BinaryReader reader, int count)
        {
            return Read<uint>(count, reader.ReadUInt32LE);
        }

        private static T[] Read<T>(int count, Func<T> func)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException("count");
            if (func == null) throw new ArgumentNullException("func");
            var t = new T[count];
            for (var i = 0; i < t.Length; i++)
            {
                t[i] = func();
            }
            return t;
        }

        #endregion
    }
}
