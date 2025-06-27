using Lucy_SalesData.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Service
{
    public class ReportService
    {
        private readonly OrderRepository _orderRepo = new();
        private readonly OrderDetailRepository _detailRepo = new();

        public IEnumerable<(DateTime Date, decimal Total)> GetOrderSummaryByDate(DateTime from, DateTime to)
        {
            return _orderRepo.GetAll()
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => (
                    Date: g.Key,
                    Total: g.Sum(order => _detailRepo.CalculateTotalByOrderId(order.OrderID))
                ))
                .OrderByDescending(r => r.Date);
        }

        public decimal GetTotalRevenue()
        {
            return _orderRepo.GetAll()
                .Sum(o => _detailRepo.CalculateTotalByOrderId(o.OrderID));
        }
    }

}
