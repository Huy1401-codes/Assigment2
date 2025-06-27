using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repositories
{
    public class CategoryRepository
    {
        private readonly List<Category> _categories;

        public CategoryRepository()
        {
            _categories = DataContext.Instance.Categories;
        }

        public IEnumerable<Category> GetAll() => _categories;

        public Category? GetById(int id) =>
            _categories.FirstOrDefault(c => c.CategoryID == id);

        public void Add(Category category)
        {
            category.CategoryID = _categories.Any() ? _categories.Max(c => c.CategoryID) + 1 : 1;
            _categories.Add(category);
        }

        public void Update(Category category)
        {
            var index = _categories.FindIndex(c => c.CategoryID == category.CategoryID);
            if (index != -1)
                _categories[index] = category;
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
                _categories.Remove(category);
        }

        public IEnumerable<Category> Search(string keyword) =>
            _categories.Where(c => c.CategoryName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

}
