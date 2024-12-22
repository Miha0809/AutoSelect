using AutoMapper;
using AutoSelect.API.Models.User;
using AutoSelect.API.DTOs.User.Requests;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.DTOs.Expert.Responses;
using AutoSelect.API.Models.Client;

namespace AutoSelect.API.Profiles;

/// <summary>
/// Профіль маппера об'єктів.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public AutoMapperProfile()
    {
        // All users
        CreateMap<User, UpdateProfileAfterFirstLoginDto>().ReverseMap();

        // Expert
        CreateMap<Expert, DTOs.Expert.Responses.ProfileDto>();
        CreateMap<ServiceInfo, ServiceInfoDto>().ReverseMap();

        // Client
        CreateMap<Client, DTOs.Client.Responses.ProfileDto>();
        CreateMap<Client, DTOs.Client.Responses.ProfileDto>();
    }
}
