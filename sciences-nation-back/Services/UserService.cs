using MongoDB.Driver;
using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back.Services
{
	public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(MongoDbService mongoDbService)
        {
            _userCollection = mongoDbService.GetCollection<User>("Users");
        }

        public async Task<UserDto> CreateUserAsync(User user)
        {
            var existingUser = await _userCollection.Find(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userCollection.InsertOneAsync(user);

            return new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Favorites = user.Favorites
            };
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var userId = new string(id);
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Favorites = user.Favorites
            };
        }

        public async Task<bool> VerifyPasswordAsync(string email, string plainTextPassword)
        {
            var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(plainTextPassword, user.Password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}