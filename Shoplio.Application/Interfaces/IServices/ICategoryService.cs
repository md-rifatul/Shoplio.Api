using Shoplio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> GetByIdAsync(int id);
        Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(int id, CategoryUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoryResponseDto>> SearchAsync(string term);
    }
}
