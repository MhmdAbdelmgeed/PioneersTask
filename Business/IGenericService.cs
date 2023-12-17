using Shared;
using System.Linq.Expressions;

namespace Business
{
    public interface IGenericService<DTO, T>
    {
        List<DTO> GetAll();

        DTO Get(int id);

        DTO Get(Expression<Func<T, bool>> expression);

        OperationResult Add(DTO refDataDTO, bool updateCache = true);

        OperationResult Remove(int id, bool updateCache = true);

        OperationResult Update(DTO refDataDTO, bool updateCache = true);

    }
}
