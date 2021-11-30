using Business.Handlers.TemplateSettings.Commands;
using Core.Aspects.Autofac.Caching;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateSettingController : BaseApiController
    {

        ///<summary>
        ///templateSetting bilgisi
        ///</summary>
        ///<remarks>Get Json</remarks>
        ///<return>templateSetting</return>
        ///<response code="200"></response>  
        [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemplateSetting))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getjson")]
        public async Task<IActionResult> GetJson()
        {
            var result = await Mediator.Send(new GetTemplateSettingQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(bool))]
        [HttpPut]
        public async Task<IActionResult> ChangeFile(TemplateSetting templateSetting)
        {
            var result = await Mediator.Send(new UpdateTemplateSettingCommand{ templateSetting=templateSetting});
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        }

    }
}