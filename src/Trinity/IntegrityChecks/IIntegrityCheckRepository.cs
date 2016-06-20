using System.Collections.Generic;
using Trinity.IntegrityChecks;

namespace Trinity.IntegrityChecks
{
    public interface IIntegrityCheckRepository
    {
        RelationshipCheckResponse Check(IRelationshipCheckRequest request);

        RawCheckResponse Check(IRawCheckRequest request);
    }
}