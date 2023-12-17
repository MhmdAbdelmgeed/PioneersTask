using DataModel;
using DtoModel;
using GenericRepository;
using Shared.ServiceRegister;

namespace Business.business_services
{
    public interface ITransactionService : IGenericService<TransactionDTO, TransactionEntity>, IScopedService
    {
        PagedList<TransactionDTO> GetAll(PagedParameters pagedParameters);
        PagedList<TransactionDTO> GetFilteredData(TransactionFillter filter, PagedParameters pagedParameters);

    }
}
