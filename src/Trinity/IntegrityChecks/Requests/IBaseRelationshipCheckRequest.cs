using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.IntegrityChecks
{
    public interface IBaseRelationshipCheckRequest : IBaseCheckRequest
    {
        string RelationshipLabel { get; set; }

        int Threshold { get; set; }

        RelationshipThresholdType ThresholdType { get; set; }
    }
}
