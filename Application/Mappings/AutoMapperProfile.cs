using Application.Models;
using Application.Models.Auth;
using AutoMapper;
using Domain.Entities.Core;

namespace Application.Mappings
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            // ==================== MODEL TO ENTITY MAPPINGS ====================

            // Register to User and Person
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new Person
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    DNI = src.DNI,
                    Email = src.Email,
                    Phone = src.Phone,
                    Address = src.Address,
                    BirthDate = src.BirthDate,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                }))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // UpdateProfile to Person
            CreateMap<UpdateProfileModel, Person>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // NotificationRequest to Notification Entity
            CreateMap<NotificationRequest, Notification>()
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Read, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ReadAt, opt => opt.Ignore());

            // ==================== ENTITY TO MODEL MAPPINGS ====================

            CreateMap<Notification, NotificationDetailModel>();
            CreateMap<User, UserProfileModel>();
        }
    }
}