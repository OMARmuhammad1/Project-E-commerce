using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.MappingProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>()
                .ForMember(destination=>destination.RoleName,objectt=>objectt.MapFrom(Source=>Source.Name)).ReverseMap();
        }
    }
}
