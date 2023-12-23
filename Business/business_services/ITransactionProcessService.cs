using DtoModel;
using Shared.ServiceRegister;

namespace Business.business_services
{
    public interface ITransactionProessService : IScopedService
    {
        Task<IEnumerable<TransactionProcessDto>> GetTransactionsByGoodIdAndDateRange(int goodId, DateTime startDate, DateTime endDate);
        Task<Summary> GetSummaryByGoodIdAndDateRange(int goodId, DateTime startDate, DateTime endDate); // This method is missing in your interface.

    }
}
