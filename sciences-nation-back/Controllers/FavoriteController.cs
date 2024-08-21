using Microsoft.AspNetCore.Mvc;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FavoriteController : ControllerBase
	{
		private readonly IFavoriteService _favoriteService;

		public FavoriteController(IFavoriteService favoriteService)
		{
            _favoriteService = favoriteService;
		}

        [HttpGet("{userId}/all")]
        public async Task<IActionResult> GetUserFavorites(string userId)
        {
            try
            {
                var allFavorites = await _favoriteService.GetUserFavoritesAsync(userId);
                return Ok(allFavorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}/all-id")]
        public async Task<IActionResult> GetUserFavoritesId(string userId)
        {
            try
            {
                var allFavorites = await _favoriteService.GetUserFavoritesIdAsync(userId);
                return Ok(allFavorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{userId}/products/{productId}")]
        public async Task<IActionResult> AddOrRemoveProductToFavorites(string userId, string productId)
        {
            try
            {
                var updatedFavorites = await _favoriteService.AddOrRemoveProductToFavorites(userId, productId);
                return Ok(updatedFavorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductToFavorites(string userId, string productId)
        {
            try
            {
                var updatedFavorites = await _favoriteService.RemoveProductToFavorites(userId, productId);
                return Ok(updatedFavorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}