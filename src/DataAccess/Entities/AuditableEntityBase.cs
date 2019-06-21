using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
