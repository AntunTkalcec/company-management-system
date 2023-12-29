﻿using AutoMapper;
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

        CreateMap<Company, CompanyDTO>();
        CreateMap<CompanyDTO, Company>();

        CreateMap<Request, RequestDTO>()
            .ForMember(_ => _.RequestType, opt => opt.MapFrom(_ => (int)_.RequestType));
        CreateMap<RequestDTO, Request>();
    }
}
