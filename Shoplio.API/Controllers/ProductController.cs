using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces.IServices;

namespace Shoplio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            var createdProduct = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> ProductUpdate(int id,  [FromBody] ProductUpdateDto dto)
        {
            try
            {
                await _productService.UpdateAsync(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> ProductDeleteById(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
