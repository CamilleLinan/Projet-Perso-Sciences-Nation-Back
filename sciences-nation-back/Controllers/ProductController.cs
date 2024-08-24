using Microsoft.AspNetCore.Mvc;
using sciences_nation_back.Models;
using sciences_nation_back.Services;
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
            try
            {
                var productsDto = await _productService.GetProductsAsync();
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
		}

		[HttpGet("{productId}")]
        public async Task<IActionResult> GetById(string productId)
        {
            try
            {
                var productDto = await _productService.GetProductByIdAsync(productId);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}