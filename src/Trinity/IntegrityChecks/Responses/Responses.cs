using Trinity.IntegrityChecks;

namespace Trinity.IntegrityChecks
{
    public class RawCheckResponse : BaseCheckResponse<IRawCheckRequest>
    {
    }

    public class RelationshipCheckResponse : BaseCheckResponse<IBaseRelationshipCheckRequest>
    {
    }
}