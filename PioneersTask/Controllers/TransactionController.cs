using AutoMapper;
using Business.business_services;
using Business.business_services_implementations;
using DtoModel;
using GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;

namespace PioneersTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _TransactionService;
        private readonly ILogger<TransactionController> _logger;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger, IMapper mapper)
        {
            _TransactionService = transactionService;
            _logger = logger;
            _mapper = mapper;
        }


        [Route("GetAllTransactions")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllTransactions([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var TransactionsList = _TransactionService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = TransactionsList.TotalCount,
                    PageSize = TransactionsList.PageSize,
                    CurrentPage = TransactionsList.CurrentPage,
                    TotalPages = TransactionsList.TotalPages,
                    HasNext = TransactionsList.HasNext,
                    HasPrevious = TransactionsList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(TransactionsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllTransactions)}");
                return StatusCode(500, "An error occurred. " + ex.InnerException.Message);
            }
        }


        [Route("GetTransactions/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTransactions(int id)
        {
            try
            {
                TransactionDTO Transactions = _TransactionService.Get(id);
                return Ok(Transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetTransactions)}");
                return StatusCode(500, "An error occurred. " + ex.InnerException.Message);
            }

        }

        [Route("AddTransactions")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddTransactions([FromBody] TransactionDTO Transactions)
        {
            try
            {
                OperationResult result = _TransactionService.Add(Transactions);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddTransactions)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }

        [HttpGet]
        [Route("GetFilteredData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<TransactionDTO>> GetFilteredData([FromQuery] TransactionFillter filter, [FromQuery] PagedParameters pagedParameters)
        {
            var result = _TransactionService.GetFilteredData(filter, pagedParameters);
            PaginationData metadata = new PaginationData
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages,
                HasNext = result.HasNext,
                HasPrevious = result.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateTransactions")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTransactions(TransactionDTO Transactions)
        {
            try
            {
                OperationResult result = _TransactionService.Update(Transactions);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateTransactions)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveTransactions/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveTransactions(int Id)
        {
            try
            {
                OperationResult result =_TransactionService.Remove(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveTransactions)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }
    }
}
