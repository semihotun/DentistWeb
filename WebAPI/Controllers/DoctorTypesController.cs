
using Business.Handlers.DoctorTypes.Commands;
using Business.Handlers.DoctorTypes.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// DoctorTypes If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTypesController : BaseApiController
    {
        ///<summary>
        ///List DoctorTypes
        ///</summary>
        ///<remarks>DoctorTypes</remarks>
        ///<return>List DoctorTypes</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DoctorType>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetDoctorTypesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>DoctorTypes</remarks>
        ///<return>DoctorTypes List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorType))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetDoctorTypeQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add DoctorType.
        /// </summary>
        /// <param name="createDoctorType"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateDoctorTypeCommand createDoctorType)
        {
            var result = await Mediator.Send(createDoctorType);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update DoctorType.
        /// </summary>
        /// <param name="updateDoctorType"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDoctorTypeCommand updateDoctorType)
        {
            var result = await Mediator.Send(updateDoctorType);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete DoctorType.
        /// </summary>
        /// <param name="deleteDoctorType"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteDoctorTypeCommand deleteDoctorType)
        {
            var result = await Mediator.Send(deleteDoctorType);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
