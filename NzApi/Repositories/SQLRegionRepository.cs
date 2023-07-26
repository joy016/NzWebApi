using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzApi.Data;
using NzApi.Models.Domain;
using NzApi.Models.DTO;

namespace NzApi.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Regions> AddRegionAsync(Regions regions)
        {
            await dbContext.Regions.AddAsync(regions);
            await dbContext.SaveChangesAsync();
            return regions;
        }

        public async Task<Regions> DeleteRegionAsync(Guid id)
        {
          var existingRegion =  await dbContext.Regions.FindAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Regions>> GetAllAsync()
        {
          return await dbContext.Regions.ToListAsync();
        }

        public async Task<Regions?> GetSingleRegionAsync(Guid id)
        {
          return await dbContext.Regions.FindAsync(id);
        }

        public async Task<Regions?> UpdateRegionAsync(Guid id, Regions regions)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = regions.Code;
            existingRegion.Name = regions.Name;
            existingRegion.RegionImageUrl = regions.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
