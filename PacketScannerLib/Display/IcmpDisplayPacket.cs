using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class IcmpDisplayPacket : IPDisplayPacket
    {
        public IcmpDisplayPacket(IpV4Header ipHeader, IcmpHeader icmpHeader)
        {
            Destination = ipHeader.DestinationAddress;
            //DestinationPort = udpHeader.DestinationPort;
            Source = ipHeader.SourceAddress;
            //SourcePort = udpHeader.SourcePort;
            Type = ipHeader.ProtocolType;
            Flags = icmpHeader.Flags;
            StringBuilder retVal = new StringBuilder();
            AppendIcmp(icmpHeader, retVal);
            AppendIPInfo(ipHeader, retVal);
            Data = retVal.ToString();
        }

        private void AppendIcmp(IcmpHeader icmpHeader, StringBuilder retVal)
        {
            retVal.AppendFormat("[*ICMP* Header:{0}, ", icmpHeader.Header);
            retVal.AppendFormat("Code:{0},", icmpHeader.Code);
            retVal.AppendFormat("Type:{0}] ", icmpHeader.Type);
        }

        public string Flags { get; set; }
    }
}
