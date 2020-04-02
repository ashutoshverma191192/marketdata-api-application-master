using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Group;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("groups")]
    public class GroupController : ControllerBase
    {
        private IGroupService _groupService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public GroupController(
            IGroupService groupService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _groupService = groupService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // GET: api/<GroupController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _groupService.GetAll();
            var model = _mapper.Map<IList<GroupModel>>(result);
            return Ok(model);
        }

        // POST api/<GroupController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterGroupModel model)
        {
            // map model to entity
            var groups = _mapper.Map<Groups>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create Group
                var result = _groupService.Create(groups, user);
                return Ok(_mapper.Map<GroupModel>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]GroupUpdateModel model)
        {
            var groups = _mapper.Map<Groups>(model);
            var user = Utilities.getUserId(User);
            groups.Id = id;

            try
            {
                // update group
                var group = _groupService.Update(groups, user);
                return Ok(_mapper.Map<GroupModel>(group));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete group
                _groupService.Delete(id);
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
                var result = _groupService.GetById(id);
                var model = _mapper.Map<GroupModel>(result);
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
