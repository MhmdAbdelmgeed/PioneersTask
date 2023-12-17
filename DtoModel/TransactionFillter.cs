using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModel
{
    public class TransactionFillter
    {
        public int? Amount { get; set; }
        public string? Direction { get; set; }
        public string? Comments { get; set; }

        public int? GoodId { get; set; }

        public Status? Status { get; set; }
        public string? TransactionStatusIds { get; set; }

    }
}
