using System.Collections.Generic;
using Trinity.IntegrityChecks.Requests;

namespace Trinity.IntegrityChecks
{
    public interface IIntegrityCheckRepository
    {
        RelationshipCheckResponse Check(IRelationshipCheckRequest request);

        RawCheckResponse Check(IRawCheckRequest request);

        RelationshipCheckResponse Check(IInboundRelationshipCheckRequest request);
    }
}