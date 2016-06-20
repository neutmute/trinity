using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.IntegrityChecks
{
    public interface IInboundRelationshipCheckRequest : IBaseRelationshipCheckRequest
    {
        string ToLabel { get; set; }
    }
}
