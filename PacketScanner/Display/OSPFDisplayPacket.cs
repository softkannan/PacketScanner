using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class OSPFDisplayPacket : IPDisplayPacket
    {
        public OSPFDisplayPacket(IpV4Header ipHeader, OSPFHeader icmpHeader)
        {
        }
    }
}
