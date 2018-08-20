using PacketScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PacketScanner.Headers
{
    public class UdpHeader
    {
        // Sixteen bits for the source port number. 
        private ushort _sourcePort;
        // Sixteen bits for the destination port number. 
        private ushort _destinationPort;
        // Length of the UDP header. 
        private ushort _length;
        // Sixteen bits for the checksum (checksum can be negative so taken as short). 
        private short _checksum;
        private BitArrayReader _dataGram;

        public UdpHeader(BitArrayReader dataGram, int nReceived)
        {
            _dataGram = dataGram;
            // The first sixteen bits contain the source port. 
            _sourcePort = _dataGram.ReadShort();
            // The next sixteen bits contain the destination port. 
            _destinationPort = _dataGram.ReadShort();
            // The next sixteen bits contain the length of the UDP packet. 
            _length = _dataGram.ReadShort();
            // The next sixteen bits contain the checksum. 
            _checksum = (short) _dataGram.ReadShort();
            //_dataGram already in correct position
        }

        public ushort SourcePort
        {
            get
            {
                return _sourcePort;
            }
        }

        public ushort DestinationPort
        {
            get
            {
                return _destinationPort;
            }
        }

        public ushort Length
        {
            get
            {
                return _length;
            }
        }

        public short Checksum
        {
            get
            {
                // Return the checksum in hexadecimal format. 
                //return string.Format("0x{0:x2}", SChecksum);
                return _checksum;
            }
        }

        public BitArrayReader Data
        {
            get
            {
                return _dataGram;
            }
        }
    }
}
