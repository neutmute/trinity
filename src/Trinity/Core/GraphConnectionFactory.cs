using System;
using System.Collections.Generic;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Trinity
{
    public class GraphConnectionFactory
    {
        private static List<JsonConverter> _registeredJsonConverters = new List<JsonConverter>();

        public static void RegisterJsonConverter(JsonConverter converter)
        {
            if (_registeredJsonConverters == null)
            {
                _registeredJsonConverters = new List<JsonConverter>();
            }
            _registeredJsonConverters.Add(converter);
        }

        public static GraphConnection Create(IConfiguration rawConfig)
        {
            var neoConnConfig = NeoConfig.GetConnectionConfig(rawConfig);

            NeoConfig.Instance.IsCypherLoggingEnabled = neoConnConfig.IsCypherLoggingEnabled;

            var graphConnection = new GraphConnection(neoConnConfig);

            SubscribeEvents(graphConnection);

            graphConnection.Connect();

            AddJsonConverters(graphConnection);
            
            return graphConnection;
        }

        private static void SubscribeEvents(GraphConnection graphConnection)
        {
            Action<LogLevel, string> log = (l, m) =>
            {
                var cypherLog = LogManager.GetLogger("Cypher");
                switch (l)
                {
                    case LogLevel.Error:
                        cypherLog.Error(m);
                        break;
                    case LogLevel.Info:
                        cypherLog.Info(m);
                        break;
                    default:
                        throw new Exception("Why doens't ILog have a generic Log method accepting log level?");
                }
            };

            graphConnection.GraphClient.OperationCompleted += (sender, e) =>
            {
                if (e.HasException)
                {
                    log(LogLevel.Error, $"Neo4j is about to throw exception '{e.Exception.Message}'. QueryText:\r\n{e.QueryText}");
                }
                
                ExecutionLog.Instance.AddIfEnabled(Execution.FromOperationCompleted(e));

                if (NeoConfig.Instance.IsCypherLoggingEnabled)
                {
                    log(LogLevel.Info, e.QueryText);
                }
            };
        }

        private static void AddJsonConverters(GraphConnection graphConnection)
        {
            graphConnection.GraphClient.JsonConverters.AddRange(_registeredJsonConverters);
        }
    }
}
