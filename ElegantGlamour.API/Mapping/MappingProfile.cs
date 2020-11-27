using AutoMapper;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Api.Dtos;
using ElegantGlamour.Core.Models.Entity.Auth;
using ElegantGlamour.Api.Dtos.User;

namespace ElegantGlamour.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // Entity to Dto
            CreateMap<PrestationCategory, GetPrestationCategoryDto>();
            CreateMap<Prestation, GetPrestationDto>()
            .ForMember(d => d.PrestationCategory, o => o.MapFrom(s => s.PrestationCategory.Id));

            // Dto to Entity
            CreateMap<AddPrestationCategoryDto, PrestationCategory>();
            CreateMap<UpdatePrestationCategoryDto, PrestationCategory>();
            
            CreateMap<AddPrestationDto, Prestation>();
            CreateMap<UpdatePrestationDto, Prestation>();

            CreateMap<UserSignUpDto, User>().ForMember(u => u.UserName, o =>  o.MapFrom(ur => ur.Email));

        }
    }
}