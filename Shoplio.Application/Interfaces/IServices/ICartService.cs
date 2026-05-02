using Shoplio.Application.DTOs;
using Shoplio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Interfaces.IServices
{
    public interface ICartService
    {
        Task<Cart> GetOrCreateCart(int userId);
        Task AddToCartAsync(int userId, AddToCartDto dto);
        Task RemoveFromCartAsync(int userId, int productId);
        Task<CartDto> GetCartAsync(int userId);
    }
}
