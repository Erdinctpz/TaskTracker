using AutoMapper;
using TaskTracker.Domain.DTOs;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappers
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}