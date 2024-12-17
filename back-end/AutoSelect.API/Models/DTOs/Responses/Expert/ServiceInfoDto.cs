namespace AutoSelect.API.Models.DTOs.Responses.Expert;

/// <summary>
/// TODO: name
/// </summary>
public class ServiceInfoDto
{
    /// <summary>
    /// TODO: name
    /// </summary>
    public ushort Price { get; set; }
    
    /// <summary>
    ///  TODO: name
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    ///  TODO: name
    /// </summary>
    public string? Description { get; set; }
}