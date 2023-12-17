namespace DataModel
{
    public class GoodEnitty:BaseModel
    {
        public int Id { get; set; }
        public string? Descrition { get; set; }
        public TransactionEntity TransactionEntity { get; set; }
        public int? TransactionEntityId { get; set; }
    }
}
