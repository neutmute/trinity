namespace Trinity.IntegrityChecks
{
    public interface IRelationshipCheckRequest
    {
        string Description { get; set; }
        string FromLabel { get; set; }
        string Name { get; }
        string RelationshipLabel { get; set; }
        int Threshold { get; set; }
        RelationshipThresholdType ThresholdType { get; set; }
    }
}