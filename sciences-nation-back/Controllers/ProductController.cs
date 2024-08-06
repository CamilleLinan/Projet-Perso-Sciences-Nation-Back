using Microsoft.AspNetCore.Mvc;
using sciences_nation_back.Services;
using sciences_nation_back.Models;
using sciences_nation_back.Models.Dto;

namespace sciences_nation_back.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly ProductService _productService;
		private readonly JwtService _jwtService;

		public ProductController(ProductService productService, JwtService jwtService)
		{
			_productService = productService;
			_jwtService = jwtService;
		}

		[HttpGet("all")]
		public async Task<IActionResult> GetAll()
		{
			var productsDto = await _productService.GetProductsAsync();

			return Ok(productsDto);
		}
	}
}

