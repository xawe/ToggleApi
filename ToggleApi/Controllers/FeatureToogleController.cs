using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToogleApi.DataAccess;
using ToogleApi.Models;

namespace ToogleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureToogleController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public FeatureToogleController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<FeatureFlag> Get()
        {
            return _dataAccessProvider.GetFlagsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] FeatureFlag featureFlag)
        {
            if(ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                featureFlag.Id = obj.ToString();
                _dataAccessProvider.AddFlagRecord(featureFlag);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public FeatureFlag Details(string id)
        {
            return _dataAccessProvider.GetFlagByIdSingleRecord(id);
        }

        [HttpGet("key/{key}")]
        public FeatureFlag GetByKey(string key)
        {
            return _dataAccessProvider.GetFlagByKeySingleRecord(key);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] FeatureFlag featureFlag)
        {
            if(ModelState.IsValid)
            {                
                _dataAccessProvider.UpdateFlagRecord(featureFlag);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetFlagByIdSingleRecord(id);
            if(data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFlagRecord(id);
            return Ok();
        }
    }
}
