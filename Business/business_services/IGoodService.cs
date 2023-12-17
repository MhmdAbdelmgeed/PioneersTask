using Shared.ServiceRegister;
using Shared;
using DtoModel;
using DataModel;
using GenericRepository;

namespace Business.business_services
{
    public interface IGoodService : IGenericService<GoodDTO, GoodEnitty>, IScopedService
    {
        PagedList<GoodDTO> GetAll(PagedParameters pagedParameters);
    }
}
