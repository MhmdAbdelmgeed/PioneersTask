using Shared;

namespace DtoModel
{
    public class GoodDTO
    {
        public int Id { get; set; }
        public string? Descrition { get; set; }
        public int TransactionEntityId { get; set; }
        public Status Status { get; set; }

    }
}
