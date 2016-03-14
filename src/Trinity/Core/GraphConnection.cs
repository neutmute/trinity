using Common.Logging;
using Neo4jClient;
using Neo4jClient.Transactions;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;

namespace Trinity
{
    public class GraphConnection : IGraphConnection
    {
        #region Fields
        private readonly ILog _log = LogManager.GetLogger<GraphConnection>();
        private static GraphClient _graphClient;
        #endregion

        #region Properties
        public INeoConnectionConfig ConnectionConfig { get; }

        public ITransactionalGraphClient GraphClient
        {
            get
            {
                EnsureGraphClientConstructed();
                return _graphClient;
            }
        }
        #endregion
        
        #region ctor
        public GraphConnection(INeoConnectionConfig config)
        {
            Guard.Null(config);
            ConnectionConfig = config;
        } 
        #endregion

        private void EnsureGraphClientConstructed()
        {
            if (_graphClient != null)
            {
                return;
            }
            Guard.Null(ConnectionConfig);
            Guard.NullOrEmpty(ConnectionConfig.GraphDbConnectionString, "Connection string cannot be null or empty");

            var graphUri = new Uri(ConnectionConfig.GraphDbConnectionString);

            if (ConnectionConfig.HttpClientTimeoutMilliseconds != 0)
            {
                throw new NotSupportedException("Why do you want to use this anyway? The constructor doesn't support this AND username and password. Maybe it could be set by property. I didn't bother checking.");
            }

            if (string.IsNullOrWhiteSpace(ConnectionConfig.Username))
            {
                _graphClient = new GraphClient(graphUri);
            }
            else
            {
                _graphClient = new GraphClient(graphUri, ConnectionConfig.Username, ConnectionConfig.Password);
            }

            _graphClient.JsonContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void Connect()
        {
            EnsureGraphClientConstructed();
            _log.InfoFormat("Connecting to '{0}'", ConnectionConfig.GraphDbConnectionString);

            try
            {
                _graphClient.Connect();
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Could not connect to graph server - {0}", ex, ex.Message);
                throw;
            }

            _log.TraceFormat("GraphClient connected to '{0}' ok", ConnectionConfig.GraphDbConnectionString);
        }
    }
}
