namespace DataModel
{
    public class TransactionEntity:BaseModel
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string Direction { get; set; }
        public string? Comments { get; set; }

        public ICollection<GoodEnitty> Goods { get; set; } = new List<GoodEnitty>();

    }
}
