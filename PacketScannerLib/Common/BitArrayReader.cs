using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Common
{
    public class BitArrayReader
    {
        protected const int BitsPerByte = 8;
        protected const int BitsPerLong = 64;
        protected const int BitsPerShort = 16;
        protected const int BitsPerInt = 32;

        private byte[] Data;
        private int BitOffset;
        private int ByteOffset;

        public BitArrayReader(byte[] Data)
        {
            this.Data = Data;
            Reset();
        }

        public void Reset()
        {
            ByteOffset = 0;
            BitOffset = 0;
        }

        /// <summary>
        /// get / set current bit possition
        /// </summary>
        public int CurrentPositionInBits
        {
            get
            {
                return this.ByteOffset * BitsPerByte + this.BitOffset;
            }
            set
            {
                this.BitOffset = value % BitsPerByte;
                this.ByteOffset = value / BitsPerByte;
            }
            //Console.WriteLine("bit: {0}, byte: {1}", this.BitOffset, this.ByteOffset);
        }

        public int CurrentPositionInBytes
        {
            get
            {
                return CurrentPositionInBits / BitsPerByte;
            }
            set
            {
                CurrentPositionInBits = value * BitsPerByte;
            }
            //Console.WriteLine("bit: {0}, byte: {1}", this.BitOffset, this.ByteOffset);
        }

        public int BitsLeft
        {
            get
            {
                return ((Data.Length - ByteOffset) - 1) * BitsPerByte + (BitsPerByte - BitOffset);
            }
        }

        private ulong ReadBits(int Count)
        {
            ulong Value = 0;
            int ReadOffset = 0;
            int LocalCount = Count;
            int LastReadCount = 0;

            while (LocalCount > 0)
            {
                //Left over bits from last read
                int LeftInByte = BitsPerByte - BitOffset;
                //How many bits will read from this read
                int ReadCount = Math.Min(LocalCount, LeftInByte);
                //discard already read bits
                int tempVal = Data[ByteOffset] << BitOffset;
                int LeftInByteValue = tempVal >> BitOffset;
                //how many bits from right most
                int BitsFromRightMost = LeftInByte - ReadCount;
                //extract bit pattern
                int ExtractBitPattern = ((1 << ReadCount) - 1) << BitsFromRightMost;
                //extracted value
                int ExtractedBits = LeftInByteValue & ExtractBitPattern;
                //make proper number
                int CleanValue = ExtractedBits >> BitsFromRightMost;
                //shift existing bits to left then merge new value
                Value = (Value << LastReadCount) | (uint)CleanValue;

                ReadOffset += ReadCount;
                LastReadCount = ReadCount;
                BitOffset += ReadCount;
                if (BitOffset == BitsPerByte)
                {
                    BitOffset = 0;
                    ByteOffset++;
                }
                LocalCount -= ReadCount;
            }

            return Value;
        }

        public void SkipBits(int Count)
        {
            BitOffset += Count % BitsPerByte;
            ByteOffset += Count / BitsPerByte;
        }

        public void SkipBytes(int Count)
        {
            SkipBits(Count * BitsPerByte);
        }


        static public ulong ReadBitsAt(byte[] Data, int Offset, int Count)
        {
            var BitReader = new BitArrayReader(Data);
            BitReader.CurrentPositionInBits = Offset;
            return BitReader.ReadBits(Count);
        }

        static public IEnumerable<KeyValuePair<uint, ulong>> FixedBitReader(byte[] Data, int BitCount = 0, int Offset = 0)
        {
            var BitReader = new BitArrayReader(Data);
            {
                BitReader.CurrentPositionInBits = Offset;

                uint Index = 0;
                while (BitReader.BitsLeft >= BitCount)
                {
                    yield return new KeyValuePair<uint, ulong>(Index++, BitReader.ReadBits(BitCount));
                }
            }
        }
        public ulong ReadLong(int countOfBits = 64)
        {
            if (countOfBits > BitsPerLong || countOfBits <= 0)
            {
                throw new ArgumentOutOfRangeException("countOfBits", countOfBits, "");
            }
            return ReadBits(countOfBits);
        }

        public uint ReadInt(int countOfBits = 32)
        {
            if (countOfBits > BitsPerInt || countOfBits <= 0)
            {
                throw new ArgumentOutOfRangeException("countOfBits", countOfBits, "");
            }
            return (uint) ReadBits(countOfBits);
        }

        public ushort ReadShort(int countOfBits = 16)
        {
            if (countOfBits > BitsPerShort || countOfBits <= 0)
            {
                throw new ArgumentOutOfRangeException("countOfBits", countOfBits, "");
            }
            return (ushort) ReadBits(countOfBits);
        }

        public byte ReadByte(int countOfBits = 8)
        {
            if (countOfBits > BitsPerByte || countOfBits <= 0)
            {
                throw new ArgumentOutOfRangeException("countOfBits", countOfBits, "");
            }
            return (byte)ReadBits(countOfBits);
        }
    }
}
