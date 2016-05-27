using Trinity.IntegrityChecks.Requests;

namespace Trinity.IntegrityChecks
{
    public class RawCheckResponse : BaseCheckResponse<IRawCheckRequest>
    {
    }

    public class RelationshipCheckResponse : BaseCheckResponse<IBaseRelationshipCheckRequest>
    {
    }
}