using Microsoft.AspNetCore.Mvc;
using NLog;
using Northwind.Contract.Models;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RegionController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            this._repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<RegionController>
        [HttpGet]
        public IActionResult Get()
        {
            try {

                IEnumerable<Region> regions = _repositoryManager.RegionRepository.FindAllRegion();

                //use DTO
                var regionDto = regions.Select(r => new RegionDTO
                {
                    RegionId = r.RegionId,
                    RegionDescription = r.RegionDescription,
                });

                return Ok(regionDto);
            } catch (Exception) {
                _logger.LogError($"Error : {nameof(Get)}");
                throw;
            }
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
