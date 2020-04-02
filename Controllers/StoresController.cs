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
using WebApi.Models.Stores;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("stores")]
    public class StoresController : ControllerBase
    {
        private IStoreService _storeService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public StoresController(
            IStoreService storeService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _storeService = storeService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // GET: api/<CityController>
        [HttpGet]
        public IActionResult Get()
        {
            var city = _storeService.GetAll();
            var model = _mapper.Map<IList<StoresModel>>(city);
            return Ok(model);
        }

        // POST api/<CityController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterStoreModel model)
        {
            // map model to entity
            var stores = _mapper.Map<Stores>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create city
                var result = _storeService.Create(stores, user);
                return Ok(result);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]RegisterStoreModel model)
        {
            var stores = _mapper.Map<Stores>(model);
            stores.Id = id;

            try
            {
                // update city
                _storeService.Update(stores);
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
                _storeService.Delete(id);
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
                var user = _storeService.GetById(id);
                var model = _mapper.Map<StoresModel>(user);
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
