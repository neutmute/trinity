using Neo4jClient;
using Neo4jClient.Transactions;

namespace Trinity
{
    public interface IGraphConnection
    {
        INeoConnectionConfig ConnectionConfig { get; }
        ITransactionalGraphClient GraphClient { get; }
    }
}
