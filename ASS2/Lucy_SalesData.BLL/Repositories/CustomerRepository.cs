using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repository
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = DataContext.Instance.Customers;
        }

        public IEnumerable<Customer> GetAll() => _customers;

        public Customer? GetById(int id) =>
            _customers.FirstOrDefault(c => c.CustomerID == id);

        public void Add(Customer customer)
        {
            customer.CustomerID = _customers.Any() ? _customers.Max(c => c.CustomerID) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var index = _customers.FindIndex(c => c.CustomerID == customer.CustomerID);
            if (index != -1)
                _customers[index] = customer;
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
                _customers.Remove(customer);
        }

        public IEnumerable<Customer> Search(string keyword) =>
            _customers.Where(c => c.CompanyName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

}
