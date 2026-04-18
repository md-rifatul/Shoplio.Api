using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces.IServices;

namespace Shoplio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CategoryCreate([FromBody]CategoryCreateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoryService.CreateAsync(dto);
            return Ok(category);
        }

    }
}
