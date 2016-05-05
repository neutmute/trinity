using System.Collections.Generic;

namespace Trinity.IntegrityChecks
{
    public interface IIntegrityCheckRepository
    {
        RelationshipCheckResponse CheckRelationshipCount(IRelationshipCheckRequest request);
    }
}