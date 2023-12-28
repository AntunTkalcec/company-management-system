using AutoMapper;
using CompanyManagementSystem.Core.DTOs;
using CompanyManagementSystem.Core.Entities;

namespace CompanyManagementSystem.Web.Server.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(_ => _.Password, opt => opt.Ignore());
        CreateMap<UserDTO, User>();
    }
}
