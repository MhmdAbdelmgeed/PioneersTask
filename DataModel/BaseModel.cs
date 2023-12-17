using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class BaseModel
    {
        public Status Status { get; set; }
        public DateTime? CreationTime { get; set; }
        public string? CreatorUserId { get; set; }

        public string? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }

        public string? LastModifierUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
