using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class DCCPDisplayPacket : IPDisplayPacket
    {
        public DCCPDisplayPacket(IpV4Header ipHeader, DCCPHeader icmpHeader)
        {
        }
    }
}
