using Shared;

namespace DtoModel
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string Direction { get; set; }
        public string? Comments { get; set; }

        public ICollection<GoodDTO> Goods { get; set; }
        public Status Status { get; set; }

    }
}
