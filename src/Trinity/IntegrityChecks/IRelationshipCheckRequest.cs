namespace Trinity.IntegrityChecks
{
    public interface IRelationshipCheckRequest : IBaseCheckRequest
    {
        string FromLabel { get; set; }
        
        string RelationshipLabel { get; set; }

        int Threshold { get; set; }

        RelationshipThresholdType ThresholdType { get; set; }
    }
}