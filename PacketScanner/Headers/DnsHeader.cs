using PacketScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PacketScanner.Headers
{
    public class DnsHeader
    {
        // Sixteen bits for identification. 
        private ushort _identification;
        // Sixteen bits for DNS flags. 
        private ushort _flags;
        // Sixteen bits indicating the number of entries in the questions list. 
        private ushort _totalQuestions;
        // Sixteen bits indicating the number of entries in the answer  
        // resource record list. 
        private ushort _totalAnswerRRs;
        // Sixteen bits indicating the number of entries in the authority 
        // resource record list. 
        private ushort _totalAuthorityRRs;
        // Sixteen bits indicating the number of entries in the additional 
        // resource record list. 
        private ushort _totalAdditionalRRs;

        public DnsHeader(BitArrayReader dataGram)
        {
            // First sixteen bits are for identification. 
            _identification = dataGram.ReadShort();
            // Next sixteen contain the flags. 
            _flags = dataGram.ReadShort();
            // Read the total numbers of questions in the quesion list. 
            _totalQuestions = dataGram.ReadShort();
            // Read the total number of answers in the answer list. 
            _totalAnswerRRs = dataGram.ReadShort();
            // Read the total number of entries in the authority list. 
            _totalAuthorityRRs = dataGram.ReadShort();
            // Total number of entries in the additional resource record list. 
            _totalAdditionalRRs = dataGram.ReadShort();
        }

        public ushort Identification { get => _identification; }

        public ushort Flags { get => _flags; }

        public ushort TotalQuestions { get => _totalQuestions; }

        public ushort TotalAnswerRRs { get => _totalAnswerRRs; }

        public ushort TotalAuthorityRRs { get => _totalAuthorityRRs; }

        public ushort TotalAdditionalRRs { get => _totalAdditionalRRs; }
    }
}
