using Business.Handlers.Logs.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Pagedlist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// List Logs
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LogDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetLogDtoQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        /// <summary>
        /// PagedList Logs
        /// </summary>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPagedList<LogDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("getallpagedlist")]
        public async Task<IActionResult> GetPagedList([FromBody] PagedListFilterModel pagedListFilterModel)
        {
            var result = await Mediator.Send(new GetLogDtoPagedListQuery{ pagedListFilterModel = pagedListFilterModel });

            if (result.Success)
            {
                return Ok(result.Data);
            }
  
            return BadRequest(result.Message);
        }
    }
}