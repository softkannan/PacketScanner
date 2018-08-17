using PacketScanner.Common;
using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class UDPDisplayPacket : IPDisplayPacket
    {
        public UDPDisplayPacket(IpV4Header ipHeader, UdpHeader udpHeader)
        {
            Destination = ipHeader.DestinationAddress;
            DestinationPort = string.Format("{0} {1}", udpHeader.DestinationPort, UDPPortLookup.GetPortName(udpHeader.DestinationPort));
            Source = ipHeader.SourceAddress;
            SourcePort = string.Format("{0} {1}", udpHeader.SourcePort, UDPPortLookup.GetPortName(udpHeader.SourcePort));
            Type = ipHeader.ProtocolType;
            StringBuilder retVal = new StringBuilder();

            if(udpHeader.SourcePort == 520 || udpHeader.DestinationPort == 520)
            {
                //RIP Protocol
            }
            else if (udpHeader.SourcePort == 161 || udpHeader.DestinationPort == 161 ||
                udpHeader.SourcePort == 162 || udpHeader.DestinationPort == 162 ||
                udpHeader.SourcePort == 10161 || udpHeader.DestinationPort == 10161 ||
                udpHeader.SourcePort == 10162 || udpHeader.DestinationPort == 10162)
            {
                //SNMP Protocol
            }
            else if (udpHeader.SourcePort == 67 || udpHeader.DestinationPort == 67 ||
                udpHeader.SourcePort == 68 || udpHeader.DestinationPort == 68)
            {
                //DHCP Protocol
            }
            else if (udpHeader.DestinationPort == 53 || udpHeader.SourcePort == 53)
            {
                AppendDNSInfo(udpHeader.Data, retVal);
            }
            AppendUDPInfo(udpHeader, retVal);
            AppendIPInfo(ipHeader, retVal);
            AppendData(udpHeader.Data, retVal);
            Data = retVal.ToString();
        }

        private void AppendUDPInfo(UdpHeader udpHeader, StringBuilder retVal)
        {
            //retVal.AppendFormat("[*UDP* Length:{0}, " , udpHeader.Length);
            //retVal.AppendFormat("Checksum:{0}]" , udpHeader.Checksum);
        }
    }
}
