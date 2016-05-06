namespace Trinity.IntegrityChecks
{
    public class RawCheckRequest : BaseCheckRequest, IRawCheckRequest
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
        public string Cypher { get; set; }
    }
}