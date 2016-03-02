using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient.Cypher;

namespace Trinity
{
    public static class ICypherFluentQueryExtensions
    {
        public static ICypherFluentQuery With(this ICypherFluentQuery q, IEnumerable<string> identifiers)
        {
            return q.With(string.Join(",", identifiers));
        }

        public static ICypherFluentQuery WithDistinct(this ICypherFluentQuery q, IEnumerable<string> identifiers)
        {
            return q.With("DISTINCT " + string.Join(",", identifiers));
        }
    }
}
