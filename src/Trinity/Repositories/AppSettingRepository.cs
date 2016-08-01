using Neo4jClient.Cypher;
using Neo4jClient.Extension.Cypher;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Trinity
{
    #region AppSetting* classes - internal consumption only
    /// <summary>
    /// Generic types didn't work with the cypher
    /// </summary>
    class AppSettingCore
    {
        public string Key { get; set; }
        public override string ToString()
        {
            return Key;
        }
    }
    class AppSettingString : AppSettingCore
    {
        public string Value { get; set; }
    }

    class AppSettingLong : AppSettingCore
    {
        public long Value { get; set; }
    } 
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Mixing types for value property seems to work
    /// CREATE(n:AppSetting { key: 'intkey', value: 99 })
    /// CREATE(n:AppSetting { key: 'intkey2', value: '1000' })
    /// CREATE(n:AppSetting { key: 'strkey', value: 'textValue' })
    /// MATCH(p:AppSetting) WHERE(p.key = 'intkey') SET p.value = p.value + 1
    /// MATCH (p:AppSetting) WHERE(p.key = 'intkey2') SET p.value = p.value + 1
    /// </remarks>
    public class AppSettingRepository : NeoRepository, IAppSettingRepository
    {
        public AppSettingRepository(IGraphConnection graphConnection)
            : base(graphConnection)
        {

        }
        public void Set(string key, string value)
        {
            var appSetting = new AppSettingString { Key = key, Value = value };
            var q = CypherQuery
                .MergeEntity(appSetting);

            q.ExecuteWithoutResults();
        }

        public void Set(string key, int value)
        {
            var appSetting = new AppSettingLong { Key = key, Value = value };
            var q = CypherQuery
                .MergeEntity(appSetting);

            q.ExecuteWithoutResults();
        }

        /// <summary>
        /// Assumes a node is already there otherwise i don't know what will happen
        /// </summary>
        /// <returns>New value after increment</returns>
        public long Increment(string key)
        {
            var qResult = CypherQuery
               .Match("(s:AppSetting)")
               .Where((AppSettingLong s) => s.Key == key)
               .Set("s.value = s.value + 1")
               .Return(s => s.As <AppSettingLong>());

            var resultList = qResult.Results.ToList();
            if (resultList.Count != 1)
            {
                throw new Exception($"Expected to find exactly one appSetting entry for '{key}' but found {resultList.Count} entries");
            }
            var appSettingResult = resultList[0];
            return appSettingResult.Value;
        }

        public TResult Get<TResult>(string key)
        {
            var matchModel = new AppSettingString { Key = key };

            var q = CypherQuery
                .MatchEntity(matchModel);
            
            var results = q.Return(appsettingstring => appsettingstring.As<AppSettingString>());

            var resultList = results.Results.ToList();
            if (resultList.Count == 0)
            {
                throw TrinityException.Create($"AppSetting '{key}' does not exist. Maybe you should use Exists<T> or seed the database?");
            }

            var appSetting = resultList[0];

            TResult result = ConvertToType<TResult, string>(appSetting.Value);
            return result;
        }

        public bool Exists(string key)
        {
            var matchModel = new AppSettingString { Key = key };

            var q = CypherQuery
                .MatchEntity(matchModel);

            var results = q.Return(appsettingstring => appsettingstring.As<AppSettingString>());

            var exists = results.Results.ToList().Count > 0;
            return exists;
        }

        private TResult ConvertToType<TResult, TInput>(TInput inputValue)
        {
            TResult result = (TResult)Convert.ChangeType(inputValue, typeof(TResult), CultureInfo.InvariantCulture);
            return result;
        }
    }
}
