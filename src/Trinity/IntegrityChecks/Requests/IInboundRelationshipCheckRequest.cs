using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.IntegrityChecks.Requests
{
    public interface IInboundRelationshipCheckRequest : IBaseRelationshipCheckRequest
    {
        string ToLabel { get; set; }
    }
}
