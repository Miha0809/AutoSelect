namespace Sport.API.Profiles;

using AutoMapper;
using AutoSelect.API.Models;
using AutoSelect.API.Models.DTOs.Requests;
using AutoSelect.API.Models.DTOs.Responses.Client;
using AutoSelect.API.Models.DTOs.Responses.Expert;

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
        CreateMap<User, UpdateProfileAfterFirstLoginDto>().ReverseMap();
        CreateMap<User, ExpertPublicShowDto>();
        CreateMap<User, ExpertPrivateShowDto>();

        CreateMap<User, ClientPublicShowDto>();
        CreateMap<User, ClientPrivateShowDto>();
    }
}
