using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.IntegrityChecks.Requests
{
    public class InboundRelationshipCheckRequest : BaseCheckRequest, IInboundRelationshipCheckRequest
    {
        public string RelationshipLabel { get; set; }
        public int Threshold { get; set; }
        public RelationshipThresholdType ThresholdType { get; set; }
        public string ToLabel { get; set; }
    }
}
