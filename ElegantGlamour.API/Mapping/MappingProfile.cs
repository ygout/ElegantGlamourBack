using AutoMapper;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Api.Dtos;

namespace ElegantGlamour.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // Entity to Dto
            CreateMap<PrestationCategory, GetPrestationDto>();
            CreateMap<Prestation, GetPrestationDto>()
            .ForMember(d => d.PrestationCategory, o => o.MapFrom(s => s.PrestationCategory.Id));

            // Dto to Entity
            CreateMap<AddPrestationCategoryDto, PrestationCategory>();
            CreateMap<UpdatePrestationCategoryDto, PrestationCategory>();
            
            CreateMap<AddPrestationDto, Prestation>();
            CreateMap<UpdatePrestationDto, Prestation>();

        }
    }
}