namespace Trinity.IntegrityChecks
{

  

    public class RelationshipCheckRequest : BaseCheckRequest, IRelationshipCheckRequest
    {

        public string NodeLabel { get; set; }

        public string RelationshipLabel { get; set; }

        public RelationshipDirection RelationshipDirection { get; set; }

        public RelationshipThresholdType ThresholdType { get; set; }

        public int Threshold { get; set; }

        public override string ToString()
        {
            return $"Name={Name}, Node={NodeLabel}, Relationship={RelationshipLabel}, Dir={RelationshipDirection}";
        }
    }
}