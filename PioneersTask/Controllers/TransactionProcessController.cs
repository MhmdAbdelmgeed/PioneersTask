using Business;
using Business.business_services;
using DtoModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PioneersTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionProcessController : ControllerBase
    {
        private readonly ITransactionProessService _transactionProcessService;

        public TransactionProcessController(ITransactionProessService transactionProcessService)
        {
            _transactionProcessService = transactionProcessService;
        }

        [HttpGet]
        [Route("GetTransactions/{goodId}/{startDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<TransactionProcessDto>>> GetTransactionsByGoodIdAndDateRange(int goodId,DateTime startDate,DateTime endDate)
        {
            try
            {
                var transactions = await _transactionProcessService.GetTransactionsByGoodIdAndDateRange(goodId, startDate, endDate);

                if (transactions == null||!transactions.Any())
                {
                    return NotFound();
                }

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the transactions.");
            }
        }

        [HttpGet]
        [Route("GetSummary/{goodId}/{startDate}/{endDate}")]
        public async Task<ActionResult<Summary>> GetSummaryByGoodIdAndDateRange(int goodId,DateTime startDate,DateTime endDate)
        {
            try
            {
                var summary = await _transactionProcessService.GetSummaryByGoodIdAndDateRange(goodId, startDate, endDate);

                if (summary == null)
                {
                    return NotFound("No transactions found to summarize.");
                }

                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the summary.");
            }
        }
    }
}
