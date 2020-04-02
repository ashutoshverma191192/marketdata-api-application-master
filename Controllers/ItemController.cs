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
using WebApi.Models.Item;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private IItemService _ItemService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ItemController(
            IItemService ItemService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _ItemService = ItemService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _ItemService.GetAll();
            var model = _mapper.Map<IList<ItemModel>>(result);
            return Ok(model);
        }

        // POST api/<ItemController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody]RegisterItemModel model)
        {
            // map model to entity
            var Items = _mapper.Map<Items>(model);
            var user = Utilities.getUserId(User);
            try
            {
                // create Items
                var result = _ItemService.Create(Items, user);
                return Ok(_mapper.Map<ItemModel>(result));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ItemUpdateModel model)
        {
            var Items = _mapper.Map<Items>(model);
            var user = Utilities.getUserId(User);
            Items.Id = id;

            try
            {
                // update Items
                var Item = _ItemService.Update(Items, user);
                return Ok(_mapper.Map<ItemModel>(Item));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // delete Items
                _ItemService.Delete(id);
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
                var result = _ItemService.GetById(id);
                var model = _mapper.Map<ItemModel>(result);
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
