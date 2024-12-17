namespace AutoSelect.API.Models.Expert;

/// <summary>
/// Експерт.
/// </summary>
public class Expert : User
{
    /// <summary>
    /// Інформація про послуги та ціни.
    /// </summary>
    public virtual List<ServiceInfo>? ServiceInfos { get; set; }
}