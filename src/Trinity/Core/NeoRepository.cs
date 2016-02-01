
using Neo4jClient;
using Neo4jClient.Cypher;
using Common.Logging;

namespace Trinity
{
    public abstract class NeoRepository
    {
        #region Fields
        protected static ILog Log = LogManager.GetLogger<NeoRepository>();
        #endregion

        #region Properties
        protected IGraphClient GraphClient { get; private set; }

        protected ICypherFluentQuery CypherQuery => GraphClient.Cypher;
        
        #endregion

        protected NeoRepository(IGraphConnection graphConnection)
        {
            Guard.Null(graphConnection);
            GraphClient = graphConnection.GraphClient;
            NeoConfig.Instance.EnsureConfigured();
        }
    }
}
