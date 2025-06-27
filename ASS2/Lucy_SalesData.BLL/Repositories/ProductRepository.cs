using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repositories
{
    public class ProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = DataContext.Instance.Products;
        }

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) =>
            _products.FirstOrDefault(p => p.ProductID == id);

        public void Add(Product product)
        {
            product.ProductID = _products.Any() ? _products.Max(p => p.ProductID) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.ProductID == product.ProductID);
            if (index != -1)
                _products[index] = product;
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
                _products.Remove(product);
        }

        public IEnumerable<Product> Search(string keyword) =>
            _products.Where(p => p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

}
