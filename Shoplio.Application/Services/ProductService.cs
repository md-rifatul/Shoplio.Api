using AutoMapper;
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
using Microsoft.EntityFrameworkCore;

namespace Shoplio.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(
            include: query => query.Include(p => p.Category)
                                   .Include(p => p.Images)
                                   .Include(p => p.Seller)
        );
            var result = products.Select(product =>
            {
                var dto = _mapper.Map<ProductResponseDto>(product);

                dto.ImageUrls = product.Images?
                    .Select(img => img.ImageUrl!)
                    .ToList();

                return dto;
            });
            return result;
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var products = await _productRepository.GetAsync(
                p => p.Id == id,
                include: query => query.Include(p => p.Category)
                                       .Include(p => p.Images)
                                       .Include(p=>p.Seller)
            );
            var product = products.FirstOrDefault();

            var dto = _mapper.Map<ProductResponseDto>(product);

            dto.ImageUrls = product.Images?
                .Select(img=>img.ImageUrl!)
                .ToList();
            return dto;
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto, int sellerId)
        {
            var product = _mapper.Map<Product>(dto);
            product.SellerId = sellerId;
            product.Images = dto.ImageUrls
                .Select(url => new ProductImage
                {
                    ImageUrl = url
                })
                .ToList();

            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            var result = _mapper.Map<ProductResponseDto>(product);

            // 5. Include images in response
            result.ImageUrls = product.Images?
                .Select(i => i.ImageUrl!)
                .ToList();

            return result;
        }

        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            var products = await _productRepository.GetAsync(
                p => p.Id == id,
                include: query => query.Include(p => p.Category)
                                       .Include(p => p.Images));
            var product = products.FirstOrDefault();
            if (product == null)
                throw new KeyNotFoundException("Product Not Found");



            _mapper.Map(dto, product);

            // 2. Handle images (replace all)
            if (dto.ImageUrls != null)
            {
                product.Images = dto.ImageUrls
                    .Where(url => !string.IsNullOrWhiteSpace(url))
                    .Select(url => new ProductImage
                    {
                        ImageUrl = url
                    })
                    .ToList();
            }

            // 3. Save changes
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
             _productRepository.Delete(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsBySellerAsync(int sellerId)
        {
            var products = await _productRepository.GetAllAsync(
                filter: p=>p.SellerId == sellerId,
                include: query=>query.Include(p=>p.Category)
                                     .Include(p=>p.Images)
                                     .Include(p=>p.Seller)
                );
            var result = products.Select(product =>
            {
                var dto = _mapper.Map<ProductResponseDto>(product);

                dto.ImageUrls = product.Images?
                    .Select(img => img.ImageUrl!)
                    .ToList();

                dto.SellerId = product.SellerId ?? 0;
                dto.SellerName = product.Seller?.Name;

                return dto;
            });
            return result;
        }

        public async Task<ProductResponseDto> GetProductBySellerIdAsync(int id, int sellerId)
        {
            var products = await _productRepository.GetAsync(
                p => p.Id == id && p.SellerId == sellerId,
                include: query => query.Include(p => p.Category)
                                       .Include(p => p.Images)
                                       .Include(p=>p.Seller)
            );
            var product = products.FirstOrDefault();

            var dto = _mapper.Map<ProductResponseDto>(product);
            dto.ImageUrls = product.Images?
                .Select(img => img.ImageUrl!)
                .ToList();
            return dto;
        }

        public async Task UpdateSellerProductAsync(int id, int userId, ProductUpdateDto dto)
        {
            var products = await _productRepository.GetAsync(
                filter: p=>p.Id==id && p.SellerId==userId,
                include: query => query.Include(p => p.Category)
                                       .Include(p => p.Images));
            var product = products.FirstOrDefault();
            if (product == null)
                throw new KeyNotFoundException("Product Not Found");



            _mapper.Map(dto, product);

            // 2. Handle images (replace all)
            if (dto.ImageUrls != null)
            {
                product.Images = dto.ImageUrls
                    .Where(url => !string.IsNullOrWhiteSpace(url))
                    .Select(url => new ProductImage
                    {
                        ImageUrl = url
                    })
                    .ToList();
            }

            // 3. Save changes
            await _unitOfWork.CommitAsync();
        }
    }
}
