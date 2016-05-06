namespace Trinity.IntegrityChecks
{
    public class Violation
    {
        /// <summary>
        /// The internal neo4j id since we don't know anything about the entity
        /// </summary>
        public long NodeId { get; set; }

        public Violation()
        {
            
        }

        public Violation(long nodeId)
        {
            NodeId = nodeId;
        }

        public override string ToString()
        {
            return $"NodeId={NodeId}";
        }
    }
}