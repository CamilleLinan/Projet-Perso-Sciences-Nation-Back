using sciences_nation_back.Models.Dto;

namespace sciences_nation_back.Services.Interfaces
{
	public interface IFavoriteService
	{
        Task<ProductDto[]> GetUserFavoritesAsync(string userId);
        Task<List<string>> GetUserFavoritesIdAsync(string userId);
        Task<List<string>> AddOrRemoveProductToFavorites(string userId, string productId);
        Task<List<string>> RemoveProductToFavorites(string userId, string productId);
    }
}