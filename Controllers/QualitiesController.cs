using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Qualities;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("qualities")]
    public class QualitiesController : ControllerBase
    {
        private IQualitiesService _qualitiesService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public QualitiesController(
            IQualitiesService qualitiesService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _qualitiesService = qualitiesService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // GET: api/<QualitiesController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _qualitiesService.GetAll();
            var model = _mapper.Map<IList<QualitiesModel>>(result);
            return Ok(model);
        }

        // POST api/<QualitiesController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterQualitiesModel model)
        {
            // map model to entity
            var qualities = _mapper.Map<Qualities>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create qualities
                var result = _qualitiesService.Create(qualities, user);
                return Ok(_mapper.Map<QualitiesModel>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<QualitiesController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]QualitiesUpdateModel model)
        {
            var qualities = _mapper.Map<Qualities>(model);
            var user = Utilities.getUserId(User);
            qualities.Id = id;

            try
            {
                // update Qualities
                var result = _qualitiesService.Update(qualities, user);
                return Ok(_mapper.Map<QualitiesModel>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<QualitiesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete Qualities
                _qualitiesService.Delete(id);
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
                var result = _qualitiesService.GetById(id);
                var model = _mapper.Map<QualitiesModel>(result);
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
