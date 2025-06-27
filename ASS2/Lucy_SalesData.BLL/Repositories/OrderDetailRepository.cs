using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repositories
{
    public class OrderDetailRepository
    {
        private readonly List<OrderDetail> _orderDetails;

        public OrderDetailRepository()
        {
            _orderDetails = DataContext.Instance.OrderDetails;
        }

        public IEnumerable<OrderDetail> GetAll() => _orderDetails;

        public IEnumerable<OrderDetail> GetByOrderId(int orderId) =>
            _orderDetails.Where(od => od.OrderID == orderId);

        public void Add(OrderDetail detail)
        {
            // Tránh thêm trùng cùng OrderID + ProductID
            var exists = _orderDetails.Any(od => od.OrderID == detail.OrderID && od.ProductID == detail.ProductID);
            if (!exists)
            {
                _orderDetails.Add(detail);
            }
        }

        public void Update(OrderDetail detail)
        {
            var index = _orderDetails.FindIndex(od =>
                od.OrderID == detail.OrderID && od.ProductID == detail.ProductID);
            if (index != -1)
            {
                _orderDetails[index] = detail;
            }
        }

        public void Delete(int orderId, int productId)
        {
            var detail = _orderDetails.FirstOrDefault(od =>
                od.OrderID == orderId && od.ProductID == productId);
            if (detail != null)
            {
                _orderDetails.Remove(detail);
            }
        }

        public decimal CalculateTotalByOrderId(int orderId)
        {
            return _orderDetails
                .Where(od => od.OrderID == orderId)
                .Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
        }
    }

}
