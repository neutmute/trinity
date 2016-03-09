using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient.Cypher;

namespace Trinity
{
    public static class ICypherFluentQueryExtensions
    {
        public static ICypherFluentQuery With(this ICypherFluentQuery q, params string[] identifiers)
        {
            return q.With(ToCsv(identifiers));
        }

        public static ICypherFluentQuery With(this ICypherFluentQuery q, IEnumerable<string> identifiers)
        {
            return q.With(ToCsv(identifiers));
        }

        public static ICypherFluentQuery WithDistinct(this ICypherFluentQuery q, IEnumerable<string> identifiers)
        {
            return q.With("DISTINCT " + ToCsv(identifiers));
        }

        public static ICypherFluentQuery Delete(this ICypherFluentQuery q, params string[] identifiers)
        {
            return q.Delete(ToCsv(identifiers));
        }

        private static string ToCsv(IEnumerable<string> identifiers)
        {
            return string.Join(",", identifiers);
        }
    }
}
