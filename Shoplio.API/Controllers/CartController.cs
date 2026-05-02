using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces.IServices;
using System.Security.Claims;

namespace Shoplio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _cartService.AddToCartAsync(userId, dto);
            return Ok("Added to cart");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
    }
}
