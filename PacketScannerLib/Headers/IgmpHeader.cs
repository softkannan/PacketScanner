using PacketScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PacketScanner.Headers
{
    public class IgmpHeader
    {
        public IgmpHeader(BitArrayReader dataGram, int nReceived)
        {
            // ICMP type
            _Type = dataGram.ReadByte();
            // ICMP subtype
            _MaxRespCode = dataGram.ReadByte();
            // Error checking data, calculated from the ICMP header and data, with value 0 substituted for this field. The Internet Checksum is used, specified in RFC 1071
            _Checksum = dataGram.ReadShort();

            _GroupAddress = new IPAddress(dataGram.ReadInt().Reverse());

            if(_Type == 0x11)
            {
                _Resv = dataGram.ReadByte(4);
                _SFlag = dataGram.ReadByte(1);
                _QRV = dataGram.ReadByte(3);
                _QQIC = dataGram.ReadByte();
                _NumberofSources = dataGram.ReadShort();
            }

            // Four-bytes field, contents vary based on the ICMP type and code.
            //_Header = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());
        }

        private readonly byte _Type;
        private readonly byte _MaxRespCode;
        private readonly ushort _Checksum;
        private readonly IPAddress _GroupAddress;
        private readonly byte _Resv;
        private readonly byte _SFlag;
        private readonly byte _QRV;
        private readonly short _QQIC;
        private readonly int _NumberofSources;
        private readonly List<IPAddress> _Sources;

        /// <summary>
        /// Indicates the message type as follows: Membership Query (0x11), Membership Report (IGMPv1: 0x12, IGMPv2: 0x16, IGMPv3: 0x22), Leave Group (0x17)
        /// </summary>
        public byte Type => _Type;
        /// <summary>
        /// Specifies the time limit for the corresponding report. The field has a resolution of 100 milliseconds, the value is taken directly. This field is meaningful only in Membership Query (0x11); in other messages it is set to 0 and ignored by the receiver.
        /// </summary>
        public byte MaxRespCode => _MaxRespCode; 
        public ushort Checksum => _Checksum;
        /// <summary>
        /// Message Type	Multicast Address
        /// General Query   All hosts(224.0.0.1)
        /// Group-Specific Query    The group being queried
        /// Membership Report   The group being reported
        /// Leave Group All routers(224.0.0.2)
        /// </summary>
        public IPAddress GroupAddress => _GroupAddress;
        public byte Resv => _Resv;
        public byte SFlag => _SFlag;
        public byte QRV => _QRV;
        public short QQIC => _QQIC;
        public int NumberofSources => _NumberofSources;
        public List<IPAddress> Sources => _Sources;
    }
}
