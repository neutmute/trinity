using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient.Cypher;

namespace Trinity
{
    public static class Extensions
    {
        public static ICypherFluentQuery Match(this ICypherFluentQuery q, RelationshipLink relationship)
        {
            q = q.Match(relationship.ToString());
            return q;
        }
        public static ICypherFluentQuery OptionalMatch(this ICypherFluentQuery q, RelationshipLink relationship)
        {
            q = q.OptionalMatch(relationship.ToString());
            return q;
        }
    }
}
