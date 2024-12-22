namespace AutoSelect.API.Models.Client;

/// <summary>
/// Клієнт.
/// </summary>
public class Client : User.User
{
    /// <summary>
    /// Конструктор без параметрів.
    /// </summary>
    public Client() { }

    /// <summary>
    /// Копіюючий конструктор.
    /// </summary>
    /// <param name="user">Користувач з якого скопіюються дані.</param>
    public Client(User.User user) : base(user) { }
}
