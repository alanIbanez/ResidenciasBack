using API.ViewModel.Response;
using API.ViewModel.Requests;
using API.ViewModel.Responses;
using Application.Models;
using Application.Models.Auth;
using AutoMapper;
using Domain.Entities.Core;

namespace API.Mappings
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            // ==================== VIEWMODEL TO MODEL MAPPINGS ====================

            // Auth mappings
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<RegisterRequest, RegisterModel>();
            CreateMap<RefreshTokenRequest, RefreshTokenModel>();

            // User mappings
            CreateMap<UpdateProfileRequest, UpdateProfileModel>();
            CreateMap<ChangePasswordRequest, ChangePasswordModel>();
            CreateMap<UpdateUserStatusRequest, UpdateUserStatusModel>();

            // Notification mappings
            CreateMap<API.ViewModel.Requests.NotificationRequest, Application.Models.NotificationRequest>();
            CreateMap<NotificationTokenRequest, NotificationTokenModel>();

            // ==================== MODEL TO VIEWMODEL MAPPINGS ====================

            // Auth responses
            CreateMap<AuthResult, AuthResponse>();
            CreateMap<OperationResult, OperationResponse>();

            // User responses
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Person.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Person.Address))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            // Notification responses
            CreateMap<NotificationResult, NotificationResponse>();
            CreateMap<Notification, NotificationDetailResponse>()
                .ForMember(dest => dest.SourceUserName, opt => opt.MapFrom(src =>
                    src.SourceUser != null ? $"{src.SourceUser.Person.FirstName} {src.SourceUser.Person.LastName}" : null));
        }
    }
}