using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flavehub.Data;
using flavehub.Domain;
using Microsoft.EntityFrameworkCore;

namespace flavehub.Repository.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly DataContext _dataContext;
        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<bool> CheckIfcategoryExistById(int categoryId)
        {
            if (await _dataContext.Categories.AnyAsync(m => m.CategoryId == categoryId)) return true;
            return false;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _dataContext.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dataContext.Categories.ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            await _dataContext.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return true; 
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
           return await _dataContext.Categories.SingleOrDefaultAsync(x=>x.CategoryId == categoryId);
        }
    }
}
