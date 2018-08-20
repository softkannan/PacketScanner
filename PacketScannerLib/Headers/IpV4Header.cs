using PacketScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PacketScanner.Headers
{
    public class IpV4Header
    {
        // Sixteen bits for total length of the datagram (header + message). 
        private ushort _totalLength;
        // Sixteen bits for identification. 
        private ushort _identification;
        // Eight bits for TTL (Time To Live). 
        private byte _timeToLive;
        // Sixteen bits containing the checksum of the header 
        // (checksum can be negative so taken as short). 
        private short _checksum;
        // Thirty two bit source IP Address. 
        private uint _srcIP;
        private IPAddress _UiSourceIPAddressCache = null;
        // Thirty two bit destination IP Address. 
        private uint _destIP;
        private IPAddress _UiDestinationIPAddressCache = null;
        private Protocol _protocolType;
        // Header length. 
        private byte _headerLength;
        // Data carried by the datagram. 
        private BitArrayReader _dataGram;
        //6 bits Differentiated Services Code Point (DSCP)
        private byte _dscp;
        //2 bits of ECN
        private byte _ecn;
        //3 Bits of flag
        private byte _flags;
        // 13 bit of fragment offset
        private ushort _fragmentOffset;
        public IpV4Header(byte[] byBuffer, int nReceived)
        {
            _dataGram = new BitArrayReader(byBuffer);
            try
            {
                // The first eight bits of the IP header contain the version and 
                // header length so we read them. 
                _version = (int)_dataGram.ReadInt(4);
                _headerLength = (byte)(_dataGram.ReadByte(4) * 4);

                _dscp = _dataGram.ReadByte(6);
                _ecn = _dataGram.ReadByte(2);

                _totalLength = _dataGram.ReadShort();

                _identification = _dataGram.ReadShort();
                _flags = _dataGram.ReadByte(3);
                _fragmentOffset = _dataGram.ReadShort(13);
                _timeToLive = _dataGram.ReadByte();
                _protocolType = (Protocol)_dataGram.ReadByte();
                _checksum = (short) _dataGram.ReadShort();
                _srcIP = _dataGram.ReadInt().Reverse();
                _destIP = _dataGram.ReadInt().Reverse();

                _dataGram.CurrentPositionInBytes = _headerLength;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Network Sniffer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int _version;

        public int Version
        {
            get
            {
                return _version;
            }
        }

        public byte HeaderLength
        {
            get
            {
                return _headerLength;
            }
        }

        public ushort MessageLength
        {
            get
            {
                // MessageLength = Total length of the datagram - Header length. 
                return (ushort)(_totalLength - _headerLength);
            }
        }

        public int Flags
        {
            get
            {
                return _flags; 
            }
        }

        public int FragmentationOffset
        {
            get
            {
                return _fragmentOffset;
            }
        }

        public byte TTL
        {
            get
            {
                return _timeToLive;
            }
        }

        public Protocol ProtocolType
        {
            get
            {
                return _protocolType;
            }
        }

        public string Checksum
        {
            get
            {
                // Returns the checksum in hexadecimal format. 
                return string.Format("0x{0:x2}", _checksum);
            }
        }

        public IPAddress SourceAddress
        {
            get
            {
                if(_UiSourceIPAddressCache == null)
                {
                    _UiSourceIPAddressCache = new IPAddress(_srcIP);
                }
                return _UiSourceIPAddressCache;
            }
        }

        public IPAddress DestinationAddress
        {
            get
            {
                if(_UiDestinationIPAddressCache == null)
                {
                    _UiDestinationIPAddressCache = new IPAddress(_destIP);
                }
                return _UiDestinationIPAddressCache;
            }
        }

        public string TotalLength
        {
            get
            {
                return _totalLength.ToString();
            }
        }

        public ushort Identification
        {
            get
            {
                return _identification;
            }
        }

        public BitArrayReader Data
        {
            get
            {
                return _dataGram;
            }
        }

        public byte Ecn { get => _ecn; }
        public byte Dscp { get => _dscp; }
    }
}
