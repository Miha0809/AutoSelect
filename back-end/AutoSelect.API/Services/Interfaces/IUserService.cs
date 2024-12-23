using AutoSelect.API.Models.User;
using AutoSelect.API.DTOs.User.Requests;

namespace AutoSelect.API.Services.Interfaces;

/// <summary>
/// Інтерфейс сервіса профілю користувача.
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Всі користувачі.
	/// </summary>
	Task<IEnumerable<TUser>> GetAllUsersAsync<TUser>() where TUser : User;

	/// <summary>
	/// Профіль певного користувача.
	/// </summary>
	/// <param name="email">Електронна пошта користувача.</param>
	Task<TUser> GetUserAsync<TUser>(string email) where TUser : User;

	/// <summary>
	/// Редагування профілю користувача.
	/// </summary>
	/// <param name="updateProfileDto">Оновленні дані.</param>
	/// <param name="email">Електронна пошта користувача.</param>
	Task UpdateAsync<TUser, TUpdate>(
		TUpdate updateProfileDto,
		string email
	) where TUser : User
	  where TUpdate : UpdateProfileDto;

	/// <summary>
	/// Видалити профіль.
	/// </summary>
	/// <param name="email">Електронна пошта користувача.</param>
	Task<bool> DeleteAsync<TUser>(string email) where TUser : User;
}
