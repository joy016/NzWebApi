using AutoMapper;
using NzApi.Models.Domain;
using NzApi.Models.DTO;

namespace NzApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Regions, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Regions>().ReverseMap();
                
        }
    }
}
