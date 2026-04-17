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

namespace Shoplio.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category Not Found");
            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public Task<IEnumerable<CategoryResponseDto>> SearchAsync(string term)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category Not Found");

            _mapper.Map(dto, category);
            await _unitOfWork.CommitAsync();
        }
    }
}
