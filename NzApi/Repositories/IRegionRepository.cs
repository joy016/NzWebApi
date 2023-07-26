using Microsoft.AspNetCore.Mvc;
using NzApi.Models.Domain;
using NzApi.Models.DTO;

namespace NzApi.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Regions>> GetAllAsync();

        Task<Regions?> GetSingleRegionAsync(Guid id);

        Task<Regions> AddRegionAsync(Regions region);

        Task<Regions> DeleteRegionAsync(Guid id);

        Task<Regions?> UpdateRegionAsync(Guid id,Regions regions);
    }
}
