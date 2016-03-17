using System;
using Microsoft.Extensions.Configuration;
using Neo4jClient.Extension.Cypher;
using Trinity;

namespace Trinity
{
    public class NeoConfig
    {
        internal const string AppSettingLabel = "AppSetting";

        #region Singleton
        // http://csharpindepth.com/articles/general/singleton.aspx   
        private static readonly NeoConfig instance = new NeoConfig();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static NeoConfig()
        {
        }

        private NeoConfig()
        {
        }

        public static NeoConfig Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion
        
        public bool IsCypherLoggingEnabled { get; set; }
        
        #region Public Methods
        
        public static INeoConnectionConfig GetConnectionConfig(IConfiguration configuration)
        {
            var config = configuration.GetSection("Data:Neo4j");
            var graphConfig = new NeoConnectionConfig();

            var logEnabledString = config["IsCypherLoggingEnabled"];

            graphConfig.GraphUri = config["ConnectionString"];
            graphConfig.Username = config["Username"];
            graphConfig.Password = config["Password"];

            if (!string.IsNullOrEmpty(logEnabledString))
            {
                graphConfig.IsCypherLoggingEnabled = Boolean.Parse(logEnabledString);
            }
            return graphConfig;
        }


        public static void SetupFluentConfig()
        {

            // Tried with generics but it breaks
            FluentConfig.Config()
                .With<AppSettingString>(AppSettingLabel)
                .Match(x => x.Key)
                .Merge(x => x.Key)
                .MergeOnMatchOrCreate(p => p.Value)
                .MergeOnCreate(p => p.Key)
                .Set();

            FluentConfig.Config()
                .With<AppSettingLong>(AppSettingLabel)
                .Match(x => x.Key)
                .Merge(x => x.Key)
                .MergeOnMatchOrCreate(p => p.Value)
                .MergeOnCreate(p => p.Key)
                .Set();
        }

        #endregion

    }
}
