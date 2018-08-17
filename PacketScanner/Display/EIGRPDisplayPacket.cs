using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class EIGRPDisplayPacket : IPDisplayPacket
    {
        public EIGRPDisplayPacket(IpV4Header ipHeader, EIGRPHeader icmpHeader)
        {
        }
    }
}
