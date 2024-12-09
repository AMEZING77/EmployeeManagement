
namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="Mary",Department=Dept.HR,Email="mary@email" },
                new Employee(){Id=2,Name="John",Department=Dept.IT,Email="john@email" },
                new Employee(){Id=3,Name="Sam",Department=Dept.PayRoll,Email="sam@email" },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeList;
        }

        public Employee? GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
