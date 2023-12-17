using AutoMapper;
using Business.business_services;
using DataModel;
using DtoModel;
using EntityFramework;
using GenericRepository;
using Microsoft.Extensions.Logging;
using Shared;
using Shared.Cache;
using System.Linq.Expressions;

namespace Business.business_services_implementations
{
    public class GoodService : IGoodService
    {
        private readonly IUnitOfWork<GoodEnitty> _goodRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ApplicationDataContext _context;
        private readonly ILogger<GoodService> _logger;
        private readonly string _cacheKey = CacheKey.goodsCacheKey;

        public GoodService(IUnitOfWork<GoodEnitty> goodRepository, IMapper mapper, ApplicationDataContext context, ILogger<GoodService> logger, ICacheService cache)
        {
            _goodRepository = goodRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public OperationResult Add(GoodDTO refDataDTO, bool updateCache = true)
        {
            GoodEnitty Good = _mapper.Map<GoodEnitty>(refDataDTO);
            _goodRepository.Repository.Attach(Good);
            _goodRepository.Save();
            return new OperationResult
            {
                Result = QueryResult.Succeeded
            };
        }

        public GoodDTO Get(int id)
        {
            var GoodDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return GoodDTO;
        }

        public GoodDTO Get(Expression<Func<GoodEnitty, bool>> expression)
        {
            var GoodList = _goodRepository.Repository.Get(expression);
            var GoodDtoList = _mapper.Map<GoodDTO>(GoodList);

            if (GoodDtoList != null)
            {
                return GoodDtoList;
            }
            return null;
        }

        public PagedList<GoodDTO> GetAll(PagedParameters pagedParameters)
        {
            var GoodDtoList = GetAll();

            return PagedList<GoodDTO>.ToGenericPagedList(GoodDtoList, pagedParameters);
        }

        public List<GoodDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<GoodDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                //with using repository
                //var GoodList = _goodRepository.Repository.GetAll();
                //var GoodDtoList = _mapper.Map<IList<GoodDTO>>(GoodList);
                //_cache.SetData(_cacheKey, GoodDtoList, DateTimeOffset.UtcNow.AddDays(1));
                //return (List<GoodDTO>)GoodDtoList;


                //with linq
                List<GoodDTO> goodDtoList = (from good in _context.Goods
                                             orderby good.LastModificationTime descending
                                             select new GoodDTO
                                             {
                                                 Id = good.Id,
                                                 Descrition = good.Descrition,
                                                 Status = good.Status,
                                                 TransactionEntityId = (int)good.TransactionEntityId
                                             }).ToList();
                _cache.SetData(_cacheKey, goodDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<GoodDTO>)goodDtoList;
            }
        }

        public OperationResult Remove(int id, bool updateCache = true)
        {
            var Good = Get(id);

            if (Good != null)
            {
                Good.Status = Status.isDeleted;
                OperationResult result = Update(Good);

                return result;
            }

            return new OperationResult
            {
                Result = QueryResult.Failed,
                ExceptionMessage = "Good not found !"
            };
        }

        public OperationResult Update(GoodDTO refDataDTO, bool updateCache = true)
        {
            GoodEnitty Evalution = _mapper.Map<GoodEnitty>(refDataDTO);
            _goodRepository.Repository.Update(Evalution);
            _goodRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }

            return new OperationResult
            {
                Result = QueryResult.Succeeded
            };
        }
    }
}
