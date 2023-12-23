using DataModel;
using DtoModel;
using GenericRepository;
using Shared.ServiceRegister;

namespace Business.business_services
{
    public interface IGoodService : IGenericService<GoodDTO, GoodEnitty>, IScopedService
    {
        PagedList<GoodDTO> GetAll(PagedParameters pagedParameters);
    }
}
