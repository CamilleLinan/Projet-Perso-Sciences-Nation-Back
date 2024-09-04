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
		private readonly string _baseUrl;

		public ProductService(MongoDbService mongoDbService, IConfiguration configuration)
		{
			_productCollection = mongoDbService.GetCollection<Product>("Products");
			_baseUrl = configuration["BaseUrl"] ?? "http://localhost:5011";
		}

		public async Task<ProductDto[]> GetProductsAsync()
		{
            var products = await _productCollection.Find(new BsonDocument()).ToListAsync() ?? throw new Exception("Products not found");

            return products.Select(product => new ProductDto
			{
				Id = product.Id.ToString(),
				Name = product.Name,
				Price = product.Price,
				Img = $"{_baseUrl}{product.Img}"
            }).ToArray();
		}

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync() ?? throw new Exception("Product not found");

            return new ProductDto
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price,
                Img = $"{_baseUrl}{product.Img}"
            };
        }
    }
}