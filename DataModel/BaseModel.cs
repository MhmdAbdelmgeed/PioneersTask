using Shared;

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
