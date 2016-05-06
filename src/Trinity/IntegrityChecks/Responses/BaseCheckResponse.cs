using System.Collections.Generic;

namespace Trinity.IntegrityChecks
{
    public interface IBaseCheckResponse
    {
        bool HasViolations { get; }

        List<Violation> Violations { get; }
    }

    public abstract class BaseCheckResponse<TCheckRequest> : IBaseCheckResponse
        where TCheckRequest : IBaseCheckRequest
    {

        public TCheckRequest Request { get; set; }

        public List<Violation> Violations { get; }

        public bool HasViolations => Violations.Count > 0;

        protected BaseCheckResponse()
        {
            Violations = new List<Violation>();
        }
    }
}