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
            Guard.NullOrEmpty(ConnectionConfig.GraphDbConnectionString);

            var graphUri = new Uri(ConnectionConfig.GraphDbConnectionString);

            if (ConnectionConfig.HttpClientTimeoutMilliseconds != 0)
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = graphUri,
                    Timeout = TimeSpan.FromMilliseconds(ConnectionConfig.HttpClientTimeoutMilliseconds)
                };
                _graphClient = new GraphClient(graphUri, new HttpClientWrapper(httpClient));
            }
            else
            {
                _graphClient = new GraphClient(graphUri);
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
