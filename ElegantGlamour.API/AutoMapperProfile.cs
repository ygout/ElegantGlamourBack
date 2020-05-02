using AutoMapper;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Models;

namespace ElegantGlamour.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Prestation, GetPrestationDto>();
            CreateMap<AddPrestationDto, Prestation>();
            CreateMap<UpdatePrestationDto, Prestation>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}