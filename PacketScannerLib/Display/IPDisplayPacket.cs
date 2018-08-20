using PacketScanner.Common;
using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PacketScanner.Display
{
    public class IPDisplayPacket
    {
        public IPDisplayPacket()
        {

        }
        public IPDisplayPacket(IpV4Header ipHeader)
        {
            Source = ipHeader.SourceAddress;
            Destination = ipHeader.DestinationAddress;
            Type = ipHeader.ProtocolType;
        }

        protected void AppendData(BitArrayReader dataGram, StringBuilder retVal)
        {
            //retVal.Append(", Data:");
            //for (var index = 0; index < data.Length; index++)
            //{
            //    var charVal = Convert.ToChar(data[index]);
            //    if (char.IsLetterOrDigit(charVal) == true)
            //    {
            //        retVal.Append(charVal);
            //    }
            //    else
            //    {
            //        retVal.Append('.');
            //    }
            //}
        }

        protected void AppendIPInfo(IpV4Header ipHeader, StringBuilder retVal)
        {
            retVal.Append("[*IP* ");
            retVal.AppendFormat("Ver:{0}, ", ipHeader.Version);
            retVal.AppendFormat("HL:{0}, ", ipHeader.HeaderLength);
            retVal.AppendFormat("DSCP:0x{0:x2}, ", ipHeader.Dscp);
            retVal.AppendFormat("ECN:0x{0:x2}, ", ipHeader.Ecn);
            retVal.AppendFormat("ID:{0}, ", ipHeader.Identification);
            retVal.AppendFormat("FL:0x{0:x2}, ", ipHeader.Flags);
            retVal.AppendFormat("FO:{0}, ", ipHeader.FragmentationOffset);
            retVal.AppendFormat("TTL:{0}, ", ipHeader.TTL);
            retVal.Append("]");
        }
        protected void AppendDNSInfo(BitArrayReader dataGram, StringBuilder retVal)
        {
            DnsHeader dnsHeader = new DnsHeader(dataGram);
            retVal.AppendFormat("[*DNS* ID:0x{0:x2}, Flags:0x{0:x2}, Questions:{0}, Answer RRs:{0}, Authority RRs:{0}, Additional RRs:{0}]",
                dnsHeader.Identification, dnsHeader.Flags, dnsHeader.TotalQuestions, dnsHeader.TotalAnswerRRs, dnsHeader.TotalAuthorityRRs, dnsHeader.TotalAdditionalRRs);
        }

        public IPAddress Source { get; set; }
        public IPAddress Destination { get; set; }
        public string SourcePort { get; set; }
        public string DestinationPort { get; set; }
        public Protocol Type { get; set; }
        public string Data { get; set; }

    }
}
