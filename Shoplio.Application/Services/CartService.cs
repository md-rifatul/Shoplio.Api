using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoplio.Application.DTOs;
using Shoplio.Application.Interfaces;
using Shoplio.Application.Interfaces.IRepository;
using Shoplio.Application.Interfaces.IServices;
using Shoplio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoplio.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            var cart = await GetOrCreateCart(userId);

            var existingItems = cart.CartItems.FirstOrDefault(c=>c.ProductId == dto.ProductId);

            if (existingItems != null)
            {
                existingItems.Quantity += dto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                });
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetByIdAsync(
                filter: u => u.UserId == userId,
                include: query => query.Include(p => p.CartItems)
                    .ThenInclude(c => c.Product)
                    .ThenInclude(i => i.Images)
            );

            var result = _mapper.Map<CartDto>(cart);


            return result;

        }

        public async Task<Cart> GetOrCreateCart(int userId)
        {
            var cart = await _cartRepository.GetByIdAsync(
                filter: u=>u.UserId == userId,
                include:query=>query.Include(p=>p.CartItems)
                );

            if(cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                await _cartRepository.AddAsync(cart);
                await _unitOfWork.CommitAsync();
            }
            return cart;
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _cartRepository.GetByIdAsync(
                filter: u=>u.UserId==userId,
                include:query=>query.Include(p=>p.CartItems)
                );

            if( cart == null)
            {
                return;
            }

            var item = cart.CartItems.FirstOrDefault(x=>x.ProductId == productId);

            if(item!=null)
            {
                cart.CartItems.Remove(item);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
