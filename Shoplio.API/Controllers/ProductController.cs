using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces.IServices;
using Shoplio.Domain.Enums;
using Shoplio.Domain.Statics;
using System.Security.Claims;

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
        [Authorize(Roles =Roles.Seller)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto)
        {
            var sellerId = int .Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var createdProduct = await _productService.CreateProductAsync(dto, sellerId);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpGet("get/{id}")]
        [Authorize(Roles = Roles.Seller)]
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
        [Authorize(Roles = Roles.Seller)]
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
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> ProductDeleteById(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [Authorize(Roles = Roles.Seller)]
        [HttpGet("mine")]
        public async Task<IActionResult> GetMyProducts()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var products = await _productService.GetProductsBySellerAsync(userId);
            return Ok(products);
        }

        [Authorize(Roles =Roles.Seller)]
        [HttpGet("mine/{id}")]
        public async Task<IActionResult> GetMyProductById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var product = await _productService.GetProductBySellerIdAsync(id,userId);
            return Ok(product);
        }

        [Authorize(Roles = Roles.Seller)]
        [HttpPut("mine/{id}")]
        public async Task<IActionResult> UpdateMineProduct(int id, [FromBody] ProductUpdateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _productService.UpdateSellerProductAsync(id, userId, dto);
            return Ok(dto);
        }
    }
}
