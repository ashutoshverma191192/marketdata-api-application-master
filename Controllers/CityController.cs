using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using WebApi.Models.City;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("city")]
    public class CityController : ControllerBase
    {
        private ICityService _cityService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CityController(
            ICityService cityService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _cityService = cityService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        // GET: api/<CityController>
        [HttpGet]
        public IActionResult Get()
        {
            var city = _cityService.GetAll();
            var model = _mapper.Map<IList<CityModel>>(city);
            return Ok(model);
        }

        // POST api/<CityController>
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterCityModel model)
        {
            // map model to entity
            var cityMasters = _mapper.Map<CityMasters>(model);

            try
            {
                // create city
                _cityService.Create(cityMasters);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]RegisterCityModel model)
        {
            var cityMaster = _mapper.Map<CityMasters>(model);
            cityMaster.Id = id;

            try
            {
                // update city
                _cityService.Update(cityMaster);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete city
                _cityService.Delete(id);
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
                var user = _cityService.GetById(id);
                var model = _mapper.Map<CityModel>(user);
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
