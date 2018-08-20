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
    public class TcpHeader
    {
        // Sixteen bits for the source port number. 
        private ushort _sourcePort;
        // Sixteen bits for the destination port number. 
        private ushort _destinationPort;
        // Thirty two bits for the sequence number. 
        private uint _sequenceNumber = 555;
        // Thirty two bits for the acknowledgement number. 
        private uint _acknowledgementNumber = 555;
        // Sixteen bits for flags and data offset. 
        private ushort _dataOffset = 555;
        // Sixteen bits for the window size. 
        private ushort _window = 555;
        // Sixteen bits for the checksum, (checksum can be negative so taken as short). 
        private short _checksum = 555;
        // Sixteen bits for the urgent pointer.   
        private ushort _urgentPointer;
        // Header length. 
        private byte _headerLength;
        // Length of the data being carried. 
        private ushort _messageLength;

        private int _nFlags;
        private string _sFlagsCache = null;
        private string _UsUrgentPointerCache = null;
        private string _UiAcknowledgementNumberCache = null;
        private BitArrayReader _dataGram;

        public TcpHeader(BitArrayReader dataGram, int nReceived)
        {
            _dataGram = dataGram;
            try
            {
                // The first sixteen bits contain the source port. 
                _sourcePort = _dataGram.ReadShort();
                // The next sixteen contain the destiination port. 
                _destinationPort = _dataGram.ReadShort();
                // Next thirty two have the sequence number. 
                _sequenceNumber = _dataGram.ReadInt();
                // Next thirty two have the acknowledgement number. 
                _acknowledgementNumber = _dataGram.ReadInt();
                // The next sixteen bits hold the flags and the data offset. 
                _headerLength = _dataGram.ReadByte(4);
                _dataGram.SkipBits(3);
                _nFlags = _dataGram.ReadShort(9);
                // The next sixteen contain the window size. 
                _window = _dataGram.ReadShort();
                // In the next sixteen we have the checksum. 
                _checksum = (short)_dataGram.ReadShort();
                // The following sixteen contain the urgent pointer. 
                _urgentPointer = _dataGram.ReadShort();
                //// The data offset indicates where the data begins, so using it we 
                //// calculate the header length. 
                //_headerLength = (byte)(_dataOffset >> 12);
                _headerLength *= 4;
                //// Message length = Total length of the TCP packet - Header length. 
                //_messageLength = (ushort)(nReceived - _headerLength);
                //// Copy the TCP data into the data buffer. 
                //_nFlags = _dataOffset & 0x3F;
                _dataGram.CurrentPositionInBytes = _headerLength;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Network Sniffer" + (nReceived), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        public uint SequenceNumber
        {
            get
            {
                return _sequenceNumber;
            }
        }

        public string AcknowledgementNumber
        {
            get
            {
                if (_UiAcknowledgementNumberCache == null)
                {
                    // If the ACK flag is set then only we have a valid value in the 
                    // acknowlegement field, so check for it beore returning anything. 
                    if ((_nFlags & 0x10) != 0)
                    {
                        _UiAcknowledgementNumberCache = _acknowledgementNumber.ToString();
                    }
                    else
                    {
                        _UiAcknowledgementNumberCache = string.Empty;
                    }
                }
                return _UiAcknowledgementNumberCache;
            }
        }

        public byte HeaderLength
        {
            get
            {
                return _headerLength;
            }
        }

        public ushort WindowSize
        {
            get
            {
                return _window;
            }
        }

        public string UrgentPointer
        {
            get
            {
                if (_UsUrgentPointerCache == null)
                {
                    // If the URG flag is set then only we have a valid value in the urgent 
                    // pointer field, so check for it beore returning anything. 
                    if ((_nFlags & 0x20) != 0)
                    {
                        _UsUrgentPointerCache = _urgentPointer.ToString();
                    }
                    else
                    {
                        _UsUrgentPointerCache = string.Empty;
                    }
                }
                return _UsUrgentPointerCache;
            }
        }

        public string Flags
        {
            get
            {
                if (_sFlagsCache == null)
                {
                    // The last six bits of data offset and flags contain the control bits. 
                    // First we extract the flags. 
                    StringBuilder strFlags = new StringBuilder();
                    strFlags.AppendFormat("0x{0:x2}", _nFlags);
                    // Now we start looking whether individual bits are set or not. 
                    if ((_nFlags & 0x01) != 0)
                    {
                        strFlags.Append(" FIN");
                    }
                    if ((_nFlags & 0x02) != 0)
                    {
                        strFlags.Append(" SYN");
                    }
                    if ((_nFlags & 0x04) != 0)
                    {
                        strFlags.Append(" RST");
                    }
                    if ((_nFlags & 0x08) != 0)
                    {
                        strFlags.Append(" PSH");
                    }
                    if ((_nFlags & 0x10) != 0)
                    {
                        strFlags.Append(" ACK");
                    }
                    if ((_nFlags & 0x20) != 0)
                    {
                        strFlags.Append(" URG");
                    }
                    if ((_nFlags & 0x40) != 0)
                    {
                        strFlags.Append(" ECE");
                    }
                    if ((_nFlags & 0x80) != 0)
                    {
                        strFlags.Append(" CWR");
                    }
                    if ((_nFlags & 0x100) != 0)
                    {
                        strFlags.Append(" NS");
                    }
                    _sFlagsCache = strFlags.ToString();
                }
                return _sFlagsCache;
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

        public ushort MessageLength
        {
            get
            {
                return _messageLength;
            }
        }
    }
}
