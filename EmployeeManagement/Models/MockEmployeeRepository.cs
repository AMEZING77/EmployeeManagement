namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="Mary",Department="HR",Email="mary@emali" },
                new Employee(){Id=1,Name="John",Department="IT",Email="john@emali" },
                new Employee(){Id=1,Name="Sam",Department="IT",Email="sam@emali" },
            };
        }

        public Employee? GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
