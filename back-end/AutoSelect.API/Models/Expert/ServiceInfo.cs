using System.ComponentModel.DataAnnotations;

namespace AutoSelect.API.Models.Expert;

/// <summary>
/// Інформація про послуги експерта.
/// </summary>
public class ServiceInfo
{
    /// <summary>
    /// Ідентифікатор.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Ціна.
    /// </summary>
    [Range(0, ushort.MaxValue)]
    public ushort Price { get; set; }

    /// <summary>
    /// Назва.
    /// </summary>
    [StringLength(64, MinimumLength = 4)]
    public required string Name { get; set; }

    /// <summary>
    /// Не обов'язковий опис.
    /// </summary>
    [StringLength(256, MinimumLength = 8)]
    public string? Description { get; set; }

    /// <summary>
    /// Власник послуги.
    /// </summary>
    public virtual required User.User Owner { get; set; }

    /// <summary>
    /// Конструктор без параметрів.
    /// </summary>
    public ServiceInfo() { }

    /// <summary>
    /// Копіюючий конструктор.
    /// </summary>
    /// <param name="serviceInfo">Послуги з яких скопіюються дані.</param>
    public ServiceInfo(ServiceInfo serviceInfo)
    {
        Price = serviceInfo.Price;
        Name = serviceInfo.Name;
        Description ??= serviceInfo.Description;
    }
}