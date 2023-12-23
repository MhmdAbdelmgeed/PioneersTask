namespace DtoModel
{
    public class TransactionProcessDto
    {
        public int? GoodID { get; set; }
        public string TransactionID { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Direction { get; set; }
        public string Comments { get; set; }
    }
}
