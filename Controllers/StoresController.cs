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

        // GET: api/<StoresController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _storeService.GetAll();
            var model = _mapper.Map<IList<StoresModel>>(result);
            return Ok(model);
        }

        // POST api/<StoresController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterStoreModel model)
        {
            // map model to entity
            var stores = _mapper.Map<Stores>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create store
                var result = _storeService.Create(stores, user);
                return Ok(_mapper.Map<IList<StoresModel>>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]storeUpdateModel model)
        {
            var stores = _mapper.Map<Stores>(model);
            stores.Id = id;

            try
            {
                // update store
                var store = _storeService.Update(stores);
                return Ok(_mapper.Map<StoresModel>(store));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete store
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
                var result = _storeService.GetById(id);
                var model = _mapper.Map<StoresModel>(result);
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
