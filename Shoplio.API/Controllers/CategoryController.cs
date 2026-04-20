using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces.IServices;
using Shoplio.Domain.Statics;

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
        [Authorize(Roles =Roles.Seller)]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost("create")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> CategoryCreate([FromBody]CategoryCreateDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoryService.CreateAsync(dto);
            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> CategoryDeleteById(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = Roles.Seller)]
        public async Task<IActionResult> CategoryUpdate(int id, [FromBody] CategoryUpdateDto dto)
        {
            await _categoryService.UpdateAsync(id, dto);
            return Ok();
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }


    }
}
