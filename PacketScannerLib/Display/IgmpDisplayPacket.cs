using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class IgmpDisplayPacket : IPDisplayPacket
    {
        public IgmpDisplayPacket(IpV4Header ipHeader, IgmpHeader igmpHeader)
        {
            Destination = ipHeader.DestinationAddress;
            //DestinationPort = udpHeader.DestinationPort;
            Source = ipHeader.SourceAddress;
            //SourcePort = udpHeader.SourcePort;
            Type = ipHeader.ProtocolType;
            StringBuilder retVal = new StringBuilder();
            AppendIcmp(igmpHeader, retVal);
            AppendIPInfo(ipHeader, retVal);
            Data = retVal.ToString();
        }

        private void AppendIcmp(IgmpHeader igmpHeader, StringBuilder retVal)
        {
            retVal.AppendFormat("[*IGMP* MaxRespCode:{0}, ", igmpHeader.MaxRespCode);
            retVal.AppendFormat("Type:{0}] ", igmpHeader.Type);
        }

        public string Flags { get; set; }
    }
}
