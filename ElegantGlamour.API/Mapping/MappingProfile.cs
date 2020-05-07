using AutoMapper;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // Entity to Dto
            CreateMap<Category, GetCategoryDto>();

            // Dto to Entity
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

        }
    }
}