using System;
using System.Collections.Generic;
using Neo4jClient.Cypher;
using Neo4jClient.Extension.Cypher;
using Trinity;
using System.Linq;
using Neo4jClient;
using Trinity.IntegrityChecks;

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
        
        public RawCheckResponse Check(IRawCheckRequest request)
        {
            /*
            Do i know what I'm doing? lets find out!
            https://github.com/Readify/Neo4jClient/wiki/cypher#manual-queries-highly-discouraged
            */
            var rawClient = (IRawGraphClient) GraphClient;
            var parameters = new Dictionary<string, object>();
            var cypherQuery = new CypherQuery(request.Cypher, parameters, CypherResultMode.Set);

            var violationIds = rawClient
                                .ExecuteGetCypherResults<long>(cypherQuery)
                                .ToList();

            var response = new RawCheckResponse();
            response.Request = request;
            response.Violations.AddRange(violationIds.ConvertAll(l => new Violation(l)));

            return response;
        }

        /// <summary>
        /// For a given label and relationship, returns anything that doesn't conform to expected count
        /// </summary>
        public RelationshipCheckResponse Check(IRelationshipCheckRequest request)
        {
            var inboundRequirement = request.RelationshipDirection == RelationshipDirection.Inbound ? "<" : "";
            var outboundRequirement = request.RelationshipDirection == RelationshipDirection.Outbound ? ">" : "";
            
            var q = CypherQuery
                .Match($"(e:{request.NodeLabel})")
                .OptionalMatch($"(e){inboundRequirement}-[rel:{request.RelationshipLabel}]-{outboundRequirement}()");

            return Check(q, request);
        }

        private RelationshipCheckResponse Check(ICypherFluentQuery q, IRelationshipCheckRequest request)
        {
            q = q.With("ID(e) as nodeId, count(rel) as relationshipCount");

            switch (request.ThresholdType)
            {
                case RelationshipThresholdType.ReturnIfGreaterThan:
                    q = q.Where($"relationshipCount > {request.Threshold}");
                    break;

                case RelationshipThresholdType.ReturnIfNotExact:
                    q = q.Where($"relationshipCount <> {request.Threshold}");
                    break;
                case RelationshipThresholdType.ReturnIfEqual:
                    q = q.Where($"relationshipCount = {request.Threshold}");
                    break;

                default:
                    throw new Exception("<INSERT PR HERE>");
            }

            var responses = q.Return((nodeId, relationshipCount) =>
            new Violation
            {
                NodeId = nodeId.As<long>()
            });

            var response = new RelationshipCheckResponse();
            response.Request = request;
            response.Violations.AddRange(responses.Results.ToList());
            return response;
        }
    }
}
