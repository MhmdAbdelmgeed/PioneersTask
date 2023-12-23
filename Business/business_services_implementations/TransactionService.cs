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
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork<TransactionEntity> _transactionRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly ApplicationDataContext _context;
        private readonly ILogger<TransactionService> _logger;
        private readonly string _cacheKey = CacheKey.transactionsCacheKey;

        public TransactionService(IUnitOfWork<TransactionEntity> transactionRepository, IMapper mapper, ApplicationDataContext context, ILogger<TransactionService> logger, ICacheService cache)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public OperationResult Add(TransactionDTO refDataDTO, bool updateCache = true)
        {
            TransactionEntity Transaction = _mapper.Map<TransactionEntity>(refDataDTO);
            _transactionRepository.Repository.Attach(Transaction);
            _transactionRepository.Save();
            if (updateCache)
            {
                _cache.RemoveData(_cacheKey);
            }

            return new OperationResult
            {
                Result = QueryResult.Succeeded
            };
        }

        public TransactionDTO Get(int id)
        {
            var TransactionDTO = GetAll().FirstOrDefault(x => x.Id == id);
            return TransactionDTO;
        }

        public TransactionDTO Get(Expression<Func<TransactionEntity, bool>> expression)
        {
            var TransactionList = _transactionRepository.Repository.Get(expression);
            var TransactionDtoList = _mapper.Map<TransactionDTO>(TransactionList);

            if (TransactionDtoList != null)
            {
                return TransactionDtoList;
            }
            return null;
        }

        public PagedList<TransactionDTO> GetAll(PagedParameters pagedParameters)
        {
            var TransactionDtoList = GetAll();

            return PagedList<TransactionDTO>.ToGenericPagedList(TransactionDtoList, pagedParameters);
        }

        public List<TransactionDTO> GetAll()
        {
            var cachedData = _cache.GetData<List<TransactionDTO>>(_cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            else
            {
                //with using repository
                //var TransactionList = _transactionRepository.Repository.GetAll();
                //var TransactionDtoList = _mapper.Map<IList<TransactionDTO>>(TransactionList);
                //_cache.SetData(_cacheKey, TransactionDtoList, DateTimeOffset.UtcNow.AddDays(1));
                //return (List<TransactionDTO>)TransactionDtoList;

                //with linq
                List<TransactionDTO> TransactionDtoList = (from transactions in _context.Transactions
                                                           orderby transactions.LastModificationTime descending
                                                           select new TransactionDTO
                                                           {
                                                               Id = transactions.Id,
                                                               Status = transactions.Status,
                                                               Amount = transactions.Amount,
                                                               Comments = transactions.Comments,
                                                               Direction = transactions.Direction,
                                                               TransactionDate = transactions.TransactionDate,
                                                               Goods = (from good in _context.Goods
                                                                        where good.TransactionEntityId == transactions.Id
                                                                        select new GoodDTO
                                                                        {
                                                                            Id = good.Id,
                                                                            Descrition = good.Descrition,
                                                                            Status = good.Status,
                                                                            TransactionEntityId = transactions.Id,
                                                                        }).ToList()
                                                           }).ToList();
                _cache.SetData(_cacheKey, TransactionDtoList, DateTimeOffset.UtcNow.AddDays(1));
                return (List<TransactionDTO>)TransactionDtoList;
            }
        }

        public PagedList<TransactionDTO> GetFilteredData(TransactionFillter filter, PagedParameters pagedParameters)
        {
            _logger.LogInformation($"-----------GetFilteredTransaction----------------");

            var TransactionStatuslist = filter.TransactionStatusIds != null ? filter.TransactionStatusIds.Split(",").ToList().ConvertAll(delegate (string x)
            {
                return (Status)Enum.Parse(typeof(Status), x);
            }) : new List<Status>();

            var refDataDtoList = GetAll().ToList();

            refDataDtoList = refDataDtoList.Where(x => (!filter.Status.HasValue || x.Status == filter.Status.Value)
                                                    && (!filter.Amount.HasValue || x.Amount == filter.Amount.Value)
                                                    && (TransactionStatuslist == null || TransactionStatuslist.Count == 0 || TransactionStatuslist.Contains(x.Status))
                                                    && (string.IsNullOrEmpty(filter.Direction) || x.Direction.ToLower().Trim().Contains(filter.Direction.ToLower().Trim()))
                                                    && (string.IsNullOrEmpty(filter.Comments) || x.Comments.ToLower().Trim().Contains(filter.Comments.ToLower().Trim()))).ToList();

            return PagedList<TransactionDTO>.ToGenericPagedList(refDataDtoList, pagedParameters);
        }

        public OperationResult Remove(int id, bool updateCache = true)
        {
            //way 1
            //delete from db
            //_transactionRepository.Repository.Delete(id);
            //_transactionRepository.Save();
            //if (updateCache)
            //    _cache.RemoveData(_cacheKey);


            //way2
            //update his status into db to isdeleted 

            var Transaction = Get(id);

            if (Transaction != null)
            {
                Transaction.Status = Status.isDeleted;
                OperationResult result = Update(Transaction);

                return result;
            }

            return new OperationResult
            {
                Result = QueryResult.Failed,
                ExceptionMessage = "transaction not found !"
            };
        }

        public OperationResult Update(TransactionDTO refDataDTO, bool updateCache = true)
        {
            TransactionEntity Evalution = _mapper.Map<TransactionEntity>(refDataDTO);
            _transactionRepository.Repository.Update(Evalution);
            _transactionRepository.Save();
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
