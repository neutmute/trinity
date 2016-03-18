using Common.Logging;
using Neo4jClient;
using Neo4jClient.Transactions;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
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
            Guard.NullOrEmpty(ConnectionConfig.GraphUri, "Connection string cannot be null or empty");

            var graphUri = new Uri(ConnectionConfig.GraphUri);

            var httpClientHandler = new HttpClientHandler();

            // We want the staging config for proxyUri  to be overridden when running in TeamCity
            // Or you can do something like this C:\>set Data:Neo4j:ProxyUri=http://teamcity.internal.dev:3128
            var disableProxyHint = "\"<";

            if (ConnectionConfig.HasProxyUri() && !ConnectionConfig.ProxyUri.StartsWith(disableProxyHint))
            {
                httpClientHandler.Proxy = new WebProxy(ConnectionConfig.ProxyUri, true);
                httpClientHandler.UseProxy = true;
            };

            var httpClient = new HttpClient(httpClientHandler);
            
            var httpClientWrapper = new HttpClientWrapper(ConnectionConfig.Username, ConnectionConfig.Password, httpClient);

            _graphClient = new GraphClient(graphUri, httpClientWrapper);
            
            _graphClient.JsonContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void Connect()
        {
            _log.InfoFormat($"Connecting to Neo4j: {ConnectionConfig.ToStringX()}", ConnectionConfig.GraphUri);

            EnsureGraphClientConstructed();
            
            try
            {
                _graphClient.Connect();
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Could not connect to graph server - {0}", ex, ex.Message);
                throw;
            }

            _log.TraceFormat("GraphClient connected to '{0}' ok", ConnectionConfig.GraphUri);
        }
    }
}
