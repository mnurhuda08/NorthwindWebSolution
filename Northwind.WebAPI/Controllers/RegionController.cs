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
            try
            {
                IEnumerable<Region> regions = _repositoryManager.RegionRepository.FindAllRegion();

                //use DTO
                var regionDto = regions.Select(r => new RegionDTO
                {
                    RegionId = r.RegionId,
                    RegionDescription = r.RegionDescription,
                });

                return Ok(regionDto);
            }
            catch (Exception)
            {
                _logger.LogError($"Error : {nameof(Get)}");
                throw;
            }
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}", Name = "GetRegion")]
        public IActionResult GetRegionById(int id)
        {
            try
            {
                var regionById = _repositoryManager.RegionRepository.FindRegionByID(id);
                if (regionById == null)
                {
                    _logger.LogError($"Data Region Not Found");
                    return BadRequest("Region Not Found");
                }
                var regionDTO = new RegionDTO
                {
                    RegionId = regionById.RegionId,
                    RegionDescription = regionById.RegionDescription,
                };

                return Ok(regionDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Error : {nameof(GetRegionById)}");
                throw;
            }
        }

        // POST api/<RegionController>
        [HttpPost]
        public IActionResult Create([FromBody] RegionDTO regionDTO)
        {
            //check is regionDTO null
            if (regionDTO == null)
            {
                _logger.LogError("RegionDto object sent from client is NULL");
                return BadRequest("RegionDto Object is NULL");
            }

            var region = new Region()
            {
                RegionId = regionDTO.RegionId,
                RegionDescription = regionDTO.RegionDescription,
            };

            //post to db
            _repositoryManager.RegionRepository.Insert(region);

            //forward
            return CreatedAtRoute("GetRegion", new { id = regionDTO.RegionId }, regionDTO);
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] RegionDTO regionDTO)
        {
            if (regionDTO == null)
            {
                _logger.LogError("RegionDto object sent from client is NULL");
                return BadRequest("RegionDto Object is NULL");
            }

            var region = new Region()
            {
                RegionId = id,
                RegionDescription = regionDTO.RegionDescription,
            };

            _repositoryManager.RegionRepository.Edit(region);

            //forward
            return CreatedAtRoute("GetRegion", new { id = regionDTO.RegionId }, new RegionDTO { RegionId = id, RegionDescription = region.RegionDescription });
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("RegionDto object sent from client is NULL");
                return BadRequest("RegionDto Object is NULL");
            }

            var region = _repositoryManager.RegionRepository.FindRegionByID(id.Value);

            if (region == null)
            {
                _logger.LogError($"{id} Not Found");
                return NotFound();
            }
            _repositoryManager.RegionRepository.Remove(region);
            return Ok("Delete Data Success");
        }
    }
}