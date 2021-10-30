using System;
using System.Collections.Generic;
using System.Text;

namespace AuditFunction.Models
{
    public class Audit
    {
        public Guid ClaimId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Operation { get; set; }
    }
}
