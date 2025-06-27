using Lucy_SalesData.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Service
{
    public class ProductService
    {
        private readonly ProductRepository _productRepo = new();

        public IEnumerable<Product> GetAvailableProducts()
        {
            return _productRepo.GetAll().Where(p => p.UnitsInStock > 0 && !p.Discontinued);
        }

        public Product? GetProductById(int id) => _productRepo.GetById(id);

        public void UpdateStock(int productId, int deltaQuantity)
        {
            var product = _productRepo.GetById(productId);
            if (product != null)
            {
                product.UnitsInStock += deltaQuantity;
                _productRepo.Update(product);
            }
        }

        public IEnumerable<Product> Search(string keyword)
        {
            return _productRepo.Search(keyword);
        }
    }

}
