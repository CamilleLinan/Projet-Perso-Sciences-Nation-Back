using Microsoft.AspNetCore.Mvc;
using sciences_nation_back.Services.Interfaces;

namespace sciences_nation_back.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var productsDto = await _productService.GetProductsAsync();

			return Ok(productsDto);
		}
	}
}