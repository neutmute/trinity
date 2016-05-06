namespace Trinity.IntegrityChecks
{

  

    public class RelationshipCheckRequest : BaseCheckRequest, IRelationshipCheckRequest
    {

        public string FromLabel { get; set; }

        public string RelationshipLabel { get; set; }


        public RelationshipThresholdType ThresholdType { get; set; }

        public int Threshold { get; set; }

    }
}