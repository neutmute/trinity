using Trinity.IntegrityChecks;

namespace Trinity.IntegrityChecks
{
    public interface IRelationshipCheckRequest : IBaseCheckRequest
    {
        string RelationshipLabel { get; set; }

        int Threshold { get; set; }

        RelationshipThresholdType ThresholdType { get; set; }

        RelationshipDirection RelationshipDirection { get; set; }

        string NodeLabel { get; set; }
    }
}