using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class GREDisplayPacket : IPDisplayPacket
    {
        public GREDisplayPacket(IpV4Header ipHeader, GREHeader icmpHeader)
        {
        }
    }
}
