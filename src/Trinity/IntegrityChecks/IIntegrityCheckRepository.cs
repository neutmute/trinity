using System.Collections.Generic;

namespace Trinity.IntegrityChecks
{
    public interface IIntegrityCheckRepository
    {
        RelationshipCheckResponse Check(IRelationshipCheckRequest request);

        RawCheckResponse Check(IRawCheckRequest request);
    }
}