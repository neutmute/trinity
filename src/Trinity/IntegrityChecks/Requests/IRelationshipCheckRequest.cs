using Trinity.IntegrityChecks.Requests;

namespace Trinity.IntegrityChecks
{
    public interface IRelationshipCheckRequest : IBaseRelationshipCheckRequest
    {
        string FromLabel { get; set; }
    }
}