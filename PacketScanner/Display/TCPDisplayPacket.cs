using PacketScanner.Common;
using PacketScanner.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacketScanner.Display
{
    public class TCPDisplayPacket : IPDisplayPacket
    {
        public TCPDisplayPacket(IpV4Header ipHeader, TcpHeader tcpHeader)
        {
            Destination = ipHeader.DestinationAddress;
            DestinationPort = string.Format("{0} {1}", tcpHeader.DestinationPort, TCPPortLookup.GetPortName(tcpHeader.DestinationPort));
            Source = ipHeader.SourceAddress;
            SourcePort = string.Format("{0} {1}", tcpHeader.SourcePort, TCPPortLookup.GetPortName(tcpHeader.SourcePort));
            Type = ipHeader.ProtocolType;
            Flags = tcpHeader.Flags;
            SeqNumber = tcpHeader.SequenceNumber.ToString();
            AckNumber = tcpHeader.AcknowledgementNumber;
            StringBuilder retVal = new StringBuilder();
            if (tcpHeader.DestinationPort == 53 || tcpHeader.SourcePort == 53)
            {
                AppendDNSInfo(tcpHeader.Data, retVal);
            }
            AppendTCPInfo(tcpHeader, retVal);
            AppendIPInfo(ipHeader, retVal);
            AppendData(tcpHeader.Data, retVal);
            Data = retVal.ToString();
        }
        
        public string Flags { get; set; }
        public string SeqNumber { get; set; }
        public string AckNumber { get; set; }

        private void AppendTCPInfo(TcpHeader tcpHeader, StringBuilder retVal)
        {
            //retVal.Append("[*TCP* ");
            //retVal.AppendFormat("SNo:{0}, " , tcpHeader.SequenceNumber);
            //if (tcpHeader.AcknowledgementNumber != "")
            //{
            //    retVal.AppendFormat("AckNo:{0} ,", tcpHeader.AcknowledgementNumber);
            //}
            //retVal.AppendFormat("HLen:{0}, " , tcpHeader.HeaderLength);
            //retVal.AppendFormat("Flags:{0}, " , tcpHeader.Flags);
            //retVal.AppendFormat("WSize:{0}, " , tcpHeader.WindowSize);
            //if (tcpHeader.UrgentPointer != "")
            //{
                //retVal.AppendFormat("UPtr:{0}, ", tcpHeader.UrgentPointer);
            //}
            //retVal.AppendFormat("Checksum:{0}" , tcpHeader.Checksum);
            //retVal.Append("]");
        }
    }
}
