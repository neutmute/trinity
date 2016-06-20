using Trinity.IntegrityChecks;

namespace Trinity.IntegrityChecks
{
    public interface IRelationshipCheckRequest : IBaseRelationshipCheckRequest
    {
        string FromLabel { get; set; }
    }
}