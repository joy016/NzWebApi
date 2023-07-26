using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzApi.Data;
using NzApi.Models.Domain;
using NzApi.Models.DTO;
using NzApi.Repositories;
using System.Runtime.InteropServices;

namespace NzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // this will get all the regions, hardcoded temporarily
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10,  string sortDirection = "asc")
        {
            var regions = await regionRepository.GetAllAsync();

            if (sortDirection.ToLower() == "desc")
            {
                regions = regions.OrderByDescending(r => r.Name).ToList();
            }
            else
            {
                regions = regions.OrderBy(r => r.Name).ToList();
            }

            // Pagination

            var totalCount = regions.Count();
            var totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            var pagedRegions = regions.Skip((page - 1) * pageSize).Take(pageSize);


          var regionDto = mapper.Map<List<RegionDTO>>(pagedRegions);
          return Ok(new
          {
              TotalCount = totalCount,
              TotalPage = totalPage,
              Page = page,
              PageSize = pageSize,
              SortDirection = sortDirection,
              Regions = regionDto
          });

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetSingleRegion([FromRoute]Guid id)
        {
            var region = await regionRepository.GetSingleRegionAsync(id);

            if ( region == null)
            {
                return NotFound();
            }


            var regionDto = mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        [Route("AddRegion")]

        public async Task<IActionResult> AddRegion([FromBody] AddRegionDTO addRegionDto)
        {
            if (addRegionDto == null)
            {
                return BadRequest("Invalid data");
            }

           var regionDomainModel = mapper.Map<Regions>(addRegionDto);

           await dbContext.Regions.AddAsync(regionDomainModel);
           await dbContext.SaveChangesAsync();

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetSingleRegion), new {id = regionDto.Id }, regionDto);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
        {
           var regionId = await regionRepository.DeleteRegionAsync(id);

            if(regionId == null)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id, UpdateRegionDTO updateRegion)
        {
            var regionDomainModel = mapper.Map<Regions>(updateRegion);

            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }     

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);

        }
    }



}
