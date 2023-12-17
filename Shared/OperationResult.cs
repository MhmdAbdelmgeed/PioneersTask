using System.Transactions;

namespace Shared
{
    public class OperationResult
    {
        public OperationResult()
        {
            ExtendedProperties = new Dictionary<string, object>();
        }

        public QueryResult Result { get; set; }

        public string ExceptionMessage { get; set; }

        public Dictionary<string, object> ExtendedProperties { get; set; }

        public TransactionStatus TransactionStatus { get; set; }


    }

    public enum QueryResult
    {
        Succeeded = 1,
        Failed = 2,
        AlreadyExist = 3,
        DataChangedByAnotherUser = 4,
        DataHasRelatedData = 5,
        RelatedDataAlreadyExist = 6,
        Unauthorized = 7

    }
}
