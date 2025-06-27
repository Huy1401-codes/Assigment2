using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy_SalesData.BLL.Repositories
{
    public class EmployeeRepository
    {
        private readonly List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = DataContext.Instance.Employees;
        }

        public IEnumerable<Employee> GetAll() => _employees;

        public Employee? GetById(int id) =>
            _employees.FirstOrDefault(e => e.EmployeeID == id);

        public void Add(Employee employee)
        {
            employee.EmployeeID = _employees.Any() ? _employees.Max(e => e.EmployeeID) + 1 : 1;
            _employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var index = _employees.FindIndex(e => e.EmployeeID == employee.EmployeeID);
            if (index != -1)
                _employees[index] = employee;
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
                _employees.Remove(employee);
        }

        public Employee? Login(string username, string password) =>
            _employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
    }

}
