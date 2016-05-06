namespace Trinity.IntegrityChecks
{

    public interface IBaseCheckRequest
    {
        string Name { get; }

        string Description { get; set; }
    }

    public abstract class BaseCheckRequest
    {
        public virtual string Name => GetType().Name;

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class RelationshipCheckRequest : BaseCheckRequest, IRelationshipCheckRequest
    {

        public string FromLabel { get; set; }

        public string RelationshipLabel { get; set; }


        public RelationshipThresholdType ThresholdType { get; set; }

        public int Threshold { get; set; }

    }

    public interface IRawCheckRequest : IBaseCheckRequest
    {
        /// <summary>
        /// This query should return a list of node id's that violate the check 
        /// eg:
        /// 
        /// MATCH(i:SchrodingersCat)
        //  WHERE i.isAlive = true 			-- Alive
        //  AND     i.DateOfDeath IS NOT NULL	-- and dead at the same time
        //  RETURN ID(i)
        /// </summary>
        string Cypher { get; set; }
    }

    public class RawCheckRequest : BaseCheckRequest, IRawCheckRequest
    {
        /// <summary>
        /// If this returns more than one result, deem it a failure
        /// </summary>
        public string Cypher { get; set; }
    }
}