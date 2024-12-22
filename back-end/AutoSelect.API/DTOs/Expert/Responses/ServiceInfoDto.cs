namespace AutoSelect.API.DTOs.Expert.Responses;

/// <summary>
/// DTO для інформації послуг експерта.
/// </summary>
public class ServiceInfoDto
{
    /// <summary>
    /// Ідентифікатор послуги.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ціна на послугу.
    /// </summary>
    public required ushort Price { get; set; }

    /// <summary>
    /// Назва послуги.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Не обов'язковий опис послуги.
    /// </summary>
    public string? Description { get; set; }
}