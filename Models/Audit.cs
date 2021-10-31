using System;
using System.Collections.Generic;
using System.Text;

namespace AuditFunction.Models
{
    public class Audit
    {
        public int ClaimId { get; set; }
        public string Timestamp { get; set; }
        public string Operation { get; set; }
    }
}
