using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repositories
{
    public class OrderRepository
    {
        private readonly List<Order> _orders;

        public OrderRepository()
        {
            _orders = DataContext.Instance.Orders;
        }

        public IEnumerable<Order> GetAll() => _orders;

        public Order? GetById(int id) =>
            _orders.FirstOrDefault(o => o.OrderID == id);

        public void Add(Order order)
        {
            order.OrderID = _orders.Any() ? _orders.Max(o => o.OrderID) + 1 : 1;
            _orders.Add(order);
        }

        public void Update(Order order)
        {
            var index = _orders.FindIndex(o => o.OrderID == order.OrderID);
            if (index != -1)
                _orders[index] = order;
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
                _orders.Remove(order);
        }

        public IEnumerable<Order> GetByCustomerId(int customerId) =>
            _orders.Where(o => o.CustomerID == customerId);
    }

}
