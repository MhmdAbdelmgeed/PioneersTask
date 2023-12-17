using AutoMapper;
using Business.business_services;
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
    public class GoodController : ControllerBase
    {
        private readonly IGoodService _GoodService;
        private readonly ILogger<GoodController> _logger;
        private readonly IMapper _mapper;

        public GoodController(IGoodService goodService, ILogger<GoodController> logger, IMapper mapper)
        {
            _GoodService = goodService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("GetAllGood")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllGood([FromQuery] PagedParameters pagedParameters)
        {
            try
            {
                var GoodList = _GoodService.GetAll(pagedParameters);

                PaginationData metadata = new PaginationData
                {
                    TotalCount = GoodList.TotalCount,
                    PageSize = GoodList.PageSize,
                    CurrentPage = GoodList.CurrentPage,
                    TotalPages = GoodList.TotalPages,
                    HasNext = GoodList.HasNext,
                    HasPrevious = GoodList.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));


                return Ok(GoodList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetAllGood)}");
                return StatusCode(500, "An error occurred. " + ex.InnerException.Message);
            }
        }

   
        [Route("GetGood/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetGood(int id)
        {
            try
            {
                GoodDTO Good = _GoodService.Get(id);
                return Ok(Good);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetGood)}");
                return StatusCode(500, "An error occurred. " + ex.InnerException.Message);
            }

        }

        [Route("AddGood")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddGood([FromBody] GoodDTO Good)
        {
            try
            {
                OperationResult result = _GoodService.Add(Good);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddGood)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("UpdateGood")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateGood(GoodDTO Good)
        {
            try
            {
                OperationResult result = _GoodService.Update(Good);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateGood)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("RemoveGood/{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveGood(int Id)
        {
            try
            {
                OperationResult result = _GoodService.Remove(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(RemoveGood)}");
                return BadRequest(new OperationResult { Result = QueryResult.Failed, ExceptionMessage = String.Format("Message: {0}", ex.Message) });
            }
        }
    }
}
