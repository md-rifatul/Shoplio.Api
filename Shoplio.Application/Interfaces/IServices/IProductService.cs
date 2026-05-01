using Shoplio.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto, int sellerId);
        Task UpdateAsync(int  id, ProductUpdateDto dto);
        Task UpdateSellerProductAsync(int  id,int userId, ProductUpdateDto dto);
        Task DeleteAsync(int id);
        Task DeleteMineProduct(int id,int userId);
        Task<IEnumerable<ProductResponseDto>> GetProductsBySellerAsync(int sellerId);
        Task<ProductResponseDto> GetProductBySellerIdAsync(int id, int sellerId);
        Task<IEnumerable<ProductResponseDto>> GetProductsBySearch(string  search);


    }
}
