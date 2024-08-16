using MongoDB.Driver;
using MongoDB.Bson;
using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back.Services
{
	public class ProductService : IProductService
	{
		private readonly IMongoCollection<Product> _productCollection;

		public ProductService(MongoDbService mongoDbService)
		{
			_productCollection = mongoDbService.GetCollection<Product>("Products");
		}

		public async Task<ProductDto[]> GetProductsAsync()
		{
			var products = await _productCollection.Find(new BsonDocument()).ToListAsync();
			return products.Select(product => new ProductDto
			{
				Id = product.Id.ToString(),
				Name = product.Name,
				Price = product.Price,
				Img = product.Img
			}).ToArray();
		}

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
			var productId = new string(id);
            var product = await _productCollection.Find(p => p.Id == productId).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price,
                Img = product.Img
            };
        }
    }
}