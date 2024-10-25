using AutoMapper;
using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.DTOs.Responses;
using AutoSelect.API.Models.DTOs.Responses.Client;
using AutoSelect.API.Models.DTOs.Responses.Expert;

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
        CreateMap<User, UserInfoAfterFirstLoginDto>();

        // Expert
        CreateMap<User, ExpertPublicShowDto>();
        CreateMap<User, ExpertPrivateShowDto>();

        // Client
        CreateMap<User, ClientPublicShowDto>();
        CreateMap<User, ClientPrivateShowDto>();
    }
}
