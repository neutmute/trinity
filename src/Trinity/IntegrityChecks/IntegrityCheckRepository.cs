using System;
using System.Collections.Generic;
using Neo4jClient.Cypher;
using Neo4jClient.Extension.Cypher;
using Trinity;
using System.Linq;

namespace Trinity.IntegrityChecks
{
    /// <summary>
    /// Sanity check your nodes
    /// </summary>
    public class IntegrityCheckRepository : NeoRepository, IIntegrityCheckRepository
    {

        public IntegrityCheckRepository(IGraphConnection graphConnection) : base(graphConnection)
        {

        }

        /// <summary>
        /// For a given label and relationship, returns anything that doesn't conform to expected count
        /// </summary>
        public RelationshipCheckResponse CheckRelationshipCount(IRelationshipCheckRequest request)
        {
            var q = CypherQuery
                .Match($"(e:{request.FromLabel})-[rel:{request.RelationshipLabel}]->()")
                .With("ID(e) as nodeId, count(rel) as relationshipCount");

            switch (request.ThresholdType)
            {
                case RelationshipThresholdType.ReturnIfGreaterThan:
                    q = q.Where($"relationshipCount > {request.Threshold}");
                    break;
                default:
                    throw new Exception("<INSERT PR HERE>");
            }

            var responses = q.Return((nodeId, relationshipCount) =>
            new ThresholdViolation{
                NodeId = nodeId.As<long>()
                ,Value = relationshipCount.As<long>()
            });

            var response = new RelationshipCheckResponse();
            response.Request = request;
            response.Violations.AddRange(responses.Results.ToList());
            return response;
        }
    }
}
