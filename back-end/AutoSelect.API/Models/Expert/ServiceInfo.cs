using System.ComponentModel.DataAnnotations;

namespace AutoSelect.API.Models.Expert;

/// <summary>
/// TODO: name
/// </summary>
public class ServiceInfo
{
    /// <summary>
    /// Ідентифікатор.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
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