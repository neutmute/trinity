using System.Collections.Generic;

namespace Trinity.IntegrityChecks
{
    public class RelationshipCheckResponse
    {
        public IRelationshipCheckRequest Request { get; set; }

        public List<ThresholdViolation> Violations { get; }

        public bool HasViolations => Violations.Count > 0;

        public RelationshipCheckResponse()
        {
            Violations = new List<ThresholdViolation>();
        }
    }

    public class ThresholdViolation
    {
        /// <summary>
        /// The internal neo4j id since we don't know anything about the entity
        /// </summary>
        public long NodeId { get; set; }

        public long Value { get; set; }

        public override string ToString()
        {
            return $"NodeId={NodeId}, Value={Value}";
        }
    }
}