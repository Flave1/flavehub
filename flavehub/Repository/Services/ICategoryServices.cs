using flavehub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Repository.Services
{
    public interface ICategoryServices
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<bool> CreateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int categoryId);
        public Task<bool> UpdateCategoryAsync(Category category);
        public Task<bool> CheckIfcategoryExistById(int categoryId);
    }
}
