using PacketScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PacketScanner.Headers
{
    public class IcmpHeader
    {
        private readonly byte _Type;
        private readonly byte _Code;
        private readonly ushort _Checksum;
        private readonly uint _Header;

        private static Dictionary<int, Dictionary<int, string>> _lookUpTable;

        static IcmpHeader()
        {
            _lookUpTable = new Dictionary<int, Dictionary<int, string>>();

            for (int index = 0; index < 44; index++)
            {
                _lookUpTable.Add(index, new Dictionary<int, string>());
            }

            _lookUpTable[0][0] = "Echo reply (used to ping)";
            _lookUpTable[1][0] = "Reserved";
            _lookUpTable[2][0] = "Reserved";
            _lookUpTable[3][0] = "Destination network unreachable";
            _lookUpTable[3][1] = "Destination host unreachable";
            _lookUpTable[3][2] = "Destination protocol unreachable";
            _lookUpTable[3][3] = "Destination port unreachable";
            _lookUpTable[3][4] = "Fragmentation required]=and DF flag set";
            _lookUpTable[3][5] = "Source route failed";
            _lookUpTable[3][6] = "Destination network unknown";
            _lookUpTable[3][7] = "Destination host unknown";
            _lookUpTable[3][8] = "Source host isolated";
            _lookUpTable[3][9] = "Network administratively prohibited";
            _lookUpTable[3][10] = "Host administratively prohibited";
            _lookUpTable[3][11] = "Network unreachable for ToS";
            _lookUpTable[3][12] = "Host unreachable for ToS";
            _lookUpTable[3][13] = "Communication administratively prohibited";
            _lookUpTable[3][14] = "Host Precedence Violation";
            _lookUpTable[3][15] = "Precedence cutoff in effect";
            _lookUpTable[4][0] = "Source quench (congestion control)";
            _lookUpTable[5][0] = "Redirect Datagram for the Network";
            _lookUpTable[5][1] = "Redirect Datagram for the Host";
            _lookUpTable[5][2] = "Redirect Datagram for the ToS & network";
            _lookUpTable[5][3] = "Redirect Datagram for the ToS & host";
            _lookUpTable[6][0] = "Alternate Host Address";
            _lookUpTable[7][0] = "Reserved";
            _lookUpTable[8][0] = "Echo request (used to ping)";
            _lookUpTable[9][0] = "Router Advertisement";
            _lookUpTable[10][0] = "Router discovery/selection/solicitation";
            _lookUpTable[11][0] = " Time Exceeded - TTL expired in transit";
            _lookUpTable[11][1] = " Time Exceeded - Fragment reassembly time exceeded";
            _lookUpTable[12][0] = "Pointer indicates the error";
            _lookUpTable[12][1] = "Bad IP header - Missing a required option";
            _lookUpTable[12][2] = "Bad IP header - Bad length";
            _lookUpTable[13][0] = "Timestamp";
            _lookUpTable[14][0] = "Timestamp reply";
            _lookUpTable[15][0] = "Information Request";
            _lookUpTable[16][0] = "Information Reply";
            _lookUpTable[17][0] = "Address Mask Request";
            _lookUpTable[18][0] = "Address Mask Reply";
            _lookUpTable[19][0] = "Reserved for security";
            _lookUpTable[20][0] = "Reserved for robustness experiment";
            _lookUpTable[21][0] = "Reserved for robustness experiment";
            _lookUpTable[22][0] = "Reserved for robustness experiment";
            _lookUpTable[23][0] = "Reserved for robustness experiment";
            _lookUpTable[24][0] = "Reserved for robustness experiment";
            _lookUpTable[25][0] = "Reserved for robustness experiment";
            _lookUpTable[26][0] = "Reserved for robustness experiment";
            _lookUpTable[27][0] = "Reserved for robustness experiment";
            _lookUpTable[28][0] = "Reserved for robustness experiment";
            _lookUpTable[29][0] = "Reserved for robustness experiment";
            _lookUpTable[30][0] = "Traceroute - Information Request";
            _lookUpTable[31][0] = "Datagram Conversion Error";
            _lookUpTable[32][0] = "Mobile Host Redirect";
            _lookUpTable[33][0] = "Where-Are-You (originally meant for IPv6)";
            _lookUpTable[34][0] = "Here-I-Am (originally meant for IPv6)";
            _lookUpTable[35][0] = "Mobile Registration Request";
            _lookUpTable[36][0] = "Mobile Registration Reply";
            _lookUpTable[37][0] = "Domain Name Request";
            _lookUpTable[38][0] = "Domain Name Reply";
            _lookUpTable[39][0] = "SKIP Algorithm Discovery Protocol, Simple Key-Management for Internet Protocol";
            _lookUpTable[40][0] = "Photuris]=Security failures";
            _lookUpTable[41][0] = "ICMP for experimental mobility protocols such as Seamoby [RFC4065]";
            _lookUpTable[42][0] = "Extended Echo Request - No Error";
            _lookUpTable[43][0] = "Extended Echo Reply - No Error";
            _lookUpTable[43][1] = "Extended Echo Reply - Malformed Query";
            _lookUpTable[43][2] = "Extended Echo Reply - No Such Interface";
            _lookUpTable[43][3] = "Extended Echo Reply - No Such Table Entry";
            _lookUpTable[43][4] = "Extended Echo Reply - Multiple Interfaces Satisfy Query";
        }

        public IcmpHeader(BitArrayReader dataGram, int nReceived)
        {
            // ICMP type
            _Type = dataGram.ReadByte();
            // ICMP subtype
            _Code = dataGram.ReadByte();
            // Error checking data, calculated from the ICMP header and data, with value 0 substituted for this field. The Internet Checksum is used, specified in RFC 1071
            _Checksum = dataGram.ReadShort();
            // Four-bytes field, contents vary based on the ICMP type and code.
            _Header = dataGram.ReadInt();
        }

        private string _sFlagsCache;
        public string Flags
        {
            get
            {
                if (_sFlagsCache == null)
                {
                    _sFlagsCache = "";
                    Dictionary<int, string> codeMsg;
                    if(_lookUpTable.TryGetValue(_Type,out codeMsg))
                    {
                        string flagMsg;
                        if(codeMsg.TryGetValue(_Code,out flagMsg))
                        {
                            _sFlagsCache = flagMsg;
                        }
                    }
                }
                return _sFlagsCache;
            }
        }

        public byte Type { get { return _Type; } }
        public byte Code { get { return _Code; } }
        public ushort Checksum { get { return _Checksum; } }
        public uint Header { get { return _Header; } }
    }
}
