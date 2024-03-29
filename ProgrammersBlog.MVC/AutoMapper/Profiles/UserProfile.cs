﻿using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.MVC.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        /* Mapping using AutoMapper Framework */
        /* CreateMap<TSource, TDestination>() */

        public UserProfile()
        {
            CreateMap<UserAddDto, User>();      // Convert UserAddDto to User.
            CreateMap<User, UserUpdateDto>();   // Convert User to UserUpdateDto
            CreateMap<UserUpdateDto, User>();   // Convert UserUpdateDto to User
        }
    }
}
