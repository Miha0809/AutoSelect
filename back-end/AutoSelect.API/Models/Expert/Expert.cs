namespace AutoSelect.API.Models.Expert;

/// <summary>
/// Експерт.
/// </summary>
public class Expert : User.User
{
    /// <summary>
    /// Інформація про послуги та ціни.
    /// </summary>
    public virtual List<ServiceInfo>? ServiceInfos { get; set; }

    /// <summary>
    /// Конструктор без параметрів.
    /// </summary>
    public Expert() { }

    /// <summary>
    /// Копіюючий конструктор.
    /// </summary>
    /// <param name="user">Користувач з якого скопіюються дані.</param>
    public Expert(User.User user) : base(user) { }
}