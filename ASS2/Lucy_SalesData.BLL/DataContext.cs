using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL
{
    public class DataContext
    {
        private static DataContext _instance;
        public static DataContext Instance => _instance ??= new DataContext();

        public List<Customer> Customers { get; set; } = new();
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<OrderDetail> OrderDetails { get; set; } = new();

        private DataContext()
        {
            // (Optionally) Seed data here
        }
    }

}
