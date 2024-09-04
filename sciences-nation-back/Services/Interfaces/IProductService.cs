using sciences_nation_back.Models.Dto;

namespace sciences_nation_back.Services.Interfaces
{
	public interface IProductService
	{
		Task<ProductDto[]> GetProductsAsync();
		Task<ProductDto> GetProductByIdAsync(string id);
    }
}