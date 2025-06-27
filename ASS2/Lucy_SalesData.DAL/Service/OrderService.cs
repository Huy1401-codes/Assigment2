using Lucy_SalesData.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepo = new();
        private readonly OrderDetailRepository _detailRepo = new();
        private readonly ProductRepository _productRepo = new();

        public int CreateOrder(int customerId, int employeeId, List<OrderDetail> details)
        {
            var order = new Order
            {
                CustomerID = customerId,
                EmployeeID = employeeId,
                OrderDate = DateTime.Now
            };

            _orderRepo.Add(order);

            foreach (var detail in details)
            {
                detail.OrderID = order.OrderID;
                _detailRepo.Add(detail);

                // Cập nhật tồn kho
                var product = _productRepo.GetById(detail.ProductID);
                if (product != null)
                {
                    product.UnitsInStock -= detail.Quantity;
                    _productRepo.Update(product);
                }
            }

            return order.OrderID;
        }

        public decimal GetTotalAmount(int orderId)
        {
            return _detailRepo.CalculateTotalByOrderId(orderId);
        }

        public IEnumerable<Order> GetOrdersByCustomer(int customerId)
        {
            return _orderRepo.GetByCustomerId(customerId);
        }
    }

}
