namespace Sport.API.Profiles;

using AutoMapper;
using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.DTOs.Responses;

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
        CreateMap<User, UpdateProfileDto>().ReverseMap();
        CreateMap<User, UserPublicShowDto>();
        CreateMap<User, UserPrivateShowDto>();
    }
}
