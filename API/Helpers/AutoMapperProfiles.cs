using System.Linq;
using AutoMapper;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;
using DatingApp.API.Extensions;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
                           src.Photos.FirstOrDefault(x => x.IsMain).Url))
                           .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())); // Map individule property
            CreateMap<Photo, PhotoDTO>(); 
            CreateMap<MemberUpdateDTO, AppUser>();
            CreateMap<RegisterDTO, AppUser>(); 
        }
    }
}