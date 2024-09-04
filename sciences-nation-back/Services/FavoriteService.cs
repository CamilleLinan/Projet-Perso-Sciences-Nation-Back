using AutoMapper;
using MongoDB.Driver;
using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back.Services
{
	public class FavoriteService : IFavoriteService
	{
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;

        public FavoriteService(MongoDbService mongoDbService, IMapper mapper)
		{
            _productCollection = mongoDbService.GetCollection<Product>("Products");
            _userCollection = mongoDbService.GetCollection<User>("Users");
            _mapper = mapper;
        }

        public async Task<ProductDto[]> GetUserFavoritesAsync(string userId)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

            var productDtos = new List<ProductDto>();
            foreach (var productId in user.Favorites)
            {
                var product = await _productCollection.Find(p => p.Id == productId).FirstOrDefaultAsync() ?? throw new Exception("Product not found");
                if (product != null)
                {
                    productDtos.Add(new ProductDto
                    {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Price = product.Price,
                        Img = product.Img
                    });
                }
            }

            return productDtos.ToArray();
        }

        public async Task<List<string>> GetUserFavoritesIdAsync(string userId)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

            return user.Favorites;
        }

        public async Task<List<string>> AddOrRemoveProductToFavorites(string userId, string productId)
        {
            var userDto = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

            var user = _mapper.Map<User>(userDto);

            if (user.Favorites.Contains(productId))
            {
                user.Favorites.Remove(productId);
            }
            else
            {
                user.Favorites.Add(productId);
            }

            await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);

            return user.Favorites;
        }

        public async Task<List<string>> RemoveProductToFavorites(string userId, string productId)
        {
            var userDto = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

            var user = _mapper.Map<User>(userDto);

            if (user.Favorites.Contains(productId))
            {
                user.Favorites.Remove(productId);
            }

            await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);

            return user.Favorites;
        }
    }
}