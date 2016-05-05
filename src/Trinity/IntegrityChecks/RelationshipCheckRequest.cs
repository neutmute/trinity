namespace Trinity.IntegrityChecks
{

    public class RelationshipCheckRequest : IRelationshipCheckRequest
    {
        public virtual string Name => GetType().Name;

        public string FromLabel { get; set; }

        public string RelationshipLabel { get; set; }

        public string Description { get; set; }

        public RelationshipThresholdType ThresholdType { get; set; }

        public int Threshold { get; set; }
    }
}