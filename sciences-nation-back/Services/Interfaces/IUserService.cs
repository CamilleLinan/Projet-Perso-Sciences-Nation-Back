using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;

namespace sciences_nation_back.Services.Interfaces
{
	public interface IUserService
	{
        Task<UserDto> CreateUserAsync(User user);
        Task<UserDto> GetUserByIdAsync(string id);
        Task<bool> VerifyPasswordAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<UserDto> UpdateUserAsync(User user);
    }
}