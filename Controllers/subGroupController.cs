using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.SubGroups;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("subGroups")]
    public class subGroupController : ControllerBase
    {
        private ISubGroupsService _subGroupService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public subGroupController(
            ISubGroupsService subGroupService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _subGroupService = subGroupService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // GET: api/<subGroupController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _subGroupService.GetAll();
            var model = _mapper.Map<IList<SubGroupModel>>(result);
            return Ok(model);
        }

        // POST api/<subGroupController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterSubGroupModel model)
        {
            // map model to entity
            var subGroups = _mapper.Map<SubGroups>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create SubGroups
                var result = _subGroupService.Create(subGroups, user);
                return Ok(_mapper.Map<SubGroupModel>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<subGroupController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]subGroupUpdateModel model)
        {
            var subGroups = _mapper.Map<SubGroups>(model);
            var user = Utilities.getUserId(User);
            subGroups.Id = id;

            try
            {
                // update SubGroups
                var subgroup = _subGroupService.Update(subGroups, user);
                return Ok(_mapper.Map<SubGroupModel>(subgroup));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<subGroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete SubGroups
                _subGroupService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _subGroupService.GetById(id);
                var model = _mapper.Map<SubGroupModel>(result);
                return Ok(model);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
