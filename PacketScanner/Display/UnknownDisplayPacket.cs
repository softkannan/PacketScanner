using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class UnknownDisplayPacket : IPDisplayPacket
    {
        public UnknownDisplayPacket(IpV4Header ipHeader)
        {

        }
    }
}
